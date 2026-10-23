using System.Collections.Generic;
using editor.cfg.skill;

namespace FrankGame.Battle
{
    /// <summary>拥有当前实体全部 BuffEffect，并作为 Buff 创建、销毁和回收的唯一入口。</summary>
    public sealed class BuffEffectComponent : ComponentBase
    {
        public override ECompType ComponentType => ECompType.BuffEffect;
        internal Entity OwnerEntity => entity;

        private struct PendingBuffAdd
        {
            public int buffId;
            public int sourceEntityEid;
            public int sourceAffixId;
        }

        private readonly IntDictionary<BuffEffect> activeBuffs = new();
        // BuffTriggerComponent仍只保存Buff ID；这里维护运行时ID索引，事件触发不再扫描全部Buff。
        private readonly Dictionary<int, List<int>> runtimeBuffIdsByBuffId = new();
        private readonly List<int> pendingRemoveIds = new();
        private readonly List<PendingBuffAdd> pendingAdds = new();
        private readonly List<int> recoverRuntimeIds = new();
        private int nextRuntimeBuffId;
        private bool iterating;
        private bool flushing;
        private bool processingPendingAdds;
        private bool recovering; // 实体整体回收期间关闭Buff移除日志，避免每局退出产生批量噪音。

        public BuffEffectComponent(Entity entity) : base(entity) { }

        public void AddBuff(int buffId, int sourceEntityEid, int sourceAffixId)
        {
            if (iterating || (flushing && !processingPendingAdds))
            {
                pendingAdds.Add(new PendingBuffAdd
                {
                    buffId = buffId,
                    sourceEntityEid = sourceEntityEid,
                    sourceAffixId = sourceAffixId,
                });
                return;
            }

            BuffEffect existing = null;
            activeBuffs.Foreach(buff =>
            {
                if (buff.BuffId != buffId || buff.SourceAffixId != sourceAffixId) return false;
                existing = buff;
                return true;
            });
            if (existing != null)
            {
                int oldStackCount = existing.StackCount;
                existing.Stack();
                BattleLog.LocalBuff(entity, existing.SourceEntityEid, existing.SourceOwnerPlayerEid,
                    existing.RuntimeBuffId, existing.BuffId, existing.SourceAffixId, 0,
                    "action=Stack rule=" + existing.BuffConfig.buffStacking.GetType().Name
                    + " stack=" + oldStackCount + "->" + existing.StackCount);
                return;
            }

            BuffConfig config = ConfigSystem.CurrentData.GetBuffConfig(buffId);
            if (config == null)
            {
                BattleLog.Error($"Buff 配置不存在，buffId:{buffId}");
                return;
            }

            int runtimeBuffId = AllocateRuntimeBuffId();
            BuffEffect effect = RefObjectPool<BuffEffect>.Instance.Get();
            effect.SetData(this, runtimeBuffId, buffId, sourceEntityEid, sourceAffixId, config);
            activeBuffs.Add(runtimeBuffId, effect);
            if (!runtimeBuffIdsByBuffId.TryGetValue(buffId, out List<int> runtimeIds))
            {
                runtimeIds = new List<int>(2);
                runtimeBuffIdsByBuffId.Add(buffId, runtimeIds);
            }
            runtimeIds.Add(runtimeBuffId);
            BattleLog.LocalBuff(entity, sourceEntityEid, effect.SourceOwnerPlayerEid,
                runtimeBuffId, buffId, sourceAffixId, 0,
                "action=Add trigger=" + config.triggerType.GetType().Name
                + " effect=" + config.effectContent.GetType().Name
                + " destroy=" + config.destroyType.GetType().Name
                + " stacking=" + config.buffStacking.GetType().Name);
            entity.GetComponent<BuffTriggerComponent>(ECompType.BuffTrigger)
                .RegisterBuff(runtimeBuffId, buffId);
            FlushPending();
        }

        public void RemoveBuff(int buffId, int sourceAffixId)
        {
            int runtimeBuffId = 0;
            activeBuffs.Foreach(buff =>
            {
                if (buff.BuffId != buffId || buff.SourceAffixId != sourceAffixId) return false;
                runtimeBuffId = buff.RuntimeBuffId;
                return true;
            });
            if (runtimeBuffId > 0) RemoveBuff(runtimeBuffId);
        }

        public void RemoveBuff(int runtimeBuffId)
        {
            if (!activeBuffs.TryGetValue(runtimeBuffId, out BuffEffect buff) || buff.IsDirty()) return;
            buff.SetDirty(true);
            if (!pendingRemoveIds.Contains(runtimeBuffId)) pendingRemoveIds.Add(runtimeBuffId);
            if (!iterating && !flushing) FlushPending();
        }

        public void RemoveBySourceAffix(int sourceAffixId)
        {
            iterating = true;
            activeBuffs.Foreach(buff =>
            {
                if (buff.SourceAffixId == sourceAffixId) RemoveBuff(buff.RuntimeBuffId);
            });
            iterating = false;
            FlushPending();
        }

        public bool TryGetBuff(int runtimeBuffId, out BuffEffect buff)
        {
            if (activeBuffs.TryGetValue(runtimeBuffId, out buff) && !buff.IsDirty()) return true;
            buff = null;
            return false;
        }

        public void TriggerBuff(int runtimeBuffId, EBuffTriggerEvent eventType, BuffTriggerContext context)
        {
            if (!TryGetBuff(runtimeBuffId, out BuffEffect buff)) return;
            bool rented = context == null;
            if (rented) context = BuffTriggerContext.Rent();
            iterating = true;
            try
            {
                buff.Trigger(eventType, context);
            }
            finally
            {
                iterating = false;
                if (rented) BuffTriggerContext.Return(context);
                FlushPending();
            }
        }

        public void TriggerBuffs(int buffId, EBuffTriggerEvent eventType, BuffTriggerContext context)
        {
            if (!runtimeBuffIdsByBuffId.TryGetValue(buffId, out List<int> runtimeIds)) return;
            bool rented = context == null;
            if (rented) context = BuffTriggerContext.Rent();
            iterating = true;
            try
            {
                int count = runtimeIds.Count;
                for (int i = 0; i < count; i++)
                {
                    if (activeBuffs.TryGetValue(runtimeIds[i], out BuffEffect buff)
                        && !buff.IsDirty()) buff.Trigger(eventType, context);
                }
            }
            finally
            {
                iterating = false;
                if (rented) BuffTriggerContext.Return(context);
                FlushPending();
            }
        }

        public void UpdateTriggerBuffs(int buffId, float deltaTime)
        {
            if (!runtimeBuffIdsByBuffId.TryGetValue(buffId, out List<int> runtimeIds)) return;
            iterating = true;
            int count = runtimeIds.Count;
            for (int i = 0; i < count; i++)
            {
                if (activeBuffs.TryGetValue(runtimeIds[i], out BuffEffect buff)
                    && !buff.IsDirty()) buff.UpdateTrigger(deltaTime);
            }
            iterating = false;
            FlushPending();
        }

        public void Update(float deltaTime)
        {
            iterating = true;
            activeBuffs.Foreach(deltaTime, static (buff, elapsed) => buff.Update(elapsed));
            iterating = false;
            FlushPending();
        }

        public bool IsHasBuff(int buffId)
        {
            return HasActiveBuffId(buffId);
        }

        public bool IsHasBuffState(EBuffStateType stateType)
        {
            bool result = false;
            activeBuffs.Foreach(buff =>
            {
                if (buff.StateType != stateType) return false;
                result = true;
                return true;
            });
            return result;
        }

        /// <summary>阶段属性重建后，仅重新写入仍存活的持续效果，不重复播放表现或触发即时逻辑。</summary>
        public void ReapplyPersistentAttributes()
        {
            activeBuffs.Foreach(static buff => buff.Reapply());
        }

        internal Entity ResolveEffectTarget(EBuffEffectTarget target, BuffTriggerContext context)
        {
            // if (target == EBuffEffectTarget.Owner) return entity;
            // if (target == EBuffEffectTarget.TriggerTarget)
            //     return world.TryGetEntity(context?.triggerTargetEid ?? 0, out Entity triggerTarget) ? triggerTarget : null;
            // int planetEid = BattleEntityMedium.GetOwnerPlanetEid(entity);
            // return world.TryGetEntity(planetEid, out Entity planet) ? planet : null;
            return null;
        }

        internal void OnMountedEffectDestroyed(int runtimeBuffId, int effectEid)
        {
            if (TryGetBuff(runtimeBuffId, out BuffEffect buff)) buff.ClearMountedEffect(effectEid);
        }

        private int AllocateRuntimeBuffId()
        {
            do
            {
                nextRuntimeBuffId++;
                if (nextRuntimeBuffId <= 0) nextRuntimeBuffId = 1;
            }
            while (activeBuffs.ContainsKey(nextRuntimeBuffId));
            return nextRuntimeBuffId;
        }

        private bool HasActiveBuffId(int buffId)
        {
            return runtimeBuffIdsByBuffId.TryGetValue(buffId, out List<int> runtimeIds)
                && runtimeIds.Count > 0;
        }

        private void FlushPending()
        {
            if (iterating || flushing) return;
            flushing = true;

            int removeIndex = 0;
            while (removeIndex < pendingRemoveIds.Count)
            {
                int runtimeBuffId = pendingRemoveIds[removeIndex++];
                if (!activeBuffs.TryGetValue(runtimeBuffId, out BuffEffect buff)) continue;
                int buffId = buff.BuffId;
                if (!recovering)
                {
                    BattleLog.LocalBuff(entity, buff.SourceEntityEid, buff.SourceOwnerPlayerEid,
                        buff.RuntimeBuffId, buff.BuffId, buff.SourceAffixId, 0,
                        "action=Remove destroy=" + buff.BuffConfig.destroyType.GetType().Name
                        + " stack=" + buff.StackCount);
                }
                buff.Destroy();
                activeBuffs.Remove(runtimeBuffId);
                if (runtimeBuffIdsByBuffId.TryGetValue(buffId, out List<int> runtimeIds))
                {
                    runtimeIds.Remove(runtimeBuffId);
                    if (runtimeIds.Count == 0) runtimeBuffIdsByBuffId.Remove(buffId);
                }
                RefObjectPool<BuffEffect>.Instance.Recycle(buff);
                if (!HasActiveBuffId(buffId))
                    entity.GetComponent<BuffTriggerComponent>(ECompType.BuffTrigger).UnregisterBuff(buffId);
            }
            pendingRemoveIds.Clear();

            processingPendingAdds = true;
            int addIndex = 0;
            while (addIndex < pendingAdds.Count)
            {
                PendingBuffAdd add = pendingAdds[addIndex++];
                AddBuff(add.buffId, add.sourceEntityEid, add.sourceAffixId);
            }
            pendingAdds.Clear();
            processingPendingAdds = false;
            flushing = false;
        }

        public override void OnRecover()
        {
            // 回收实体时不再接受遍历期间积压的新 Buff；先提交 Dirty 删除，避免对象池泄漏。
            recovering = true;
            activeBuffs.Foreach(static buff => buff.SetLogSuppressed(true));
            iterating = false;
            flushing = false;
            processingPendingAdds = false;
            pendingAdds.Clear();
            FlushPending();
            recoverRuntimeIds.Clear();
            activeBuffs.Foreach(recoverRuntimeIds,
                static (buff, ids) => ids.Add(buff.RuntimeBuffId));
            for (int i = 0; i < recoverRuntimeIds.Count; i++)
            {
                if (!activeBuffs.TryGetValue(recoverRuntimeIds[i], out BuffEffect buff)) continue;
                buff.Destroy();
                RefObjectPool<BuffEffect>.Instance.Recycle(buff);
            }
            activeBuffs.Clear();
            runtimeBuffIdsByBuffId.Clear();
            recoverRuntimeIds.Clear();
            pendingRemoveIds.Clear();
            pendingAdds.Clear();
            nextRuntimeBuffId = 0;
            iterating = false;
            flushing = false;
            processingPendingAdds = false;
            recovering = false;
        }

        protected override string GetString()
        {
            return $"[Buff生效:{activeBuffs.Count}]";
        }
    }
}
