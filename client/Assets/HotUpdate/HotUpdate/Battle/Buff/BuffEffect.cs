using System.Diagnostics;
using editor.cfg.skill;

namespace FrankGame.Battle
{
    /// <summary>一个独立来源的运行时 Buff，组合触发、执行和销毁三个策略对象。</summary>
    public sealed class BuffEffect : IDirty, IClear
    {
        public int RuntimeBuffId => runtimeBuffId;
        public int BuffId => buffId;
        public int SourceEntityEid => sourceEntityEid;
        /// <summary>Buff创建时保存的来源玩家EID，来源实体销毁后仍用于伤害归属与本地日志筛选。</summary>
        public int SourceOwnerPlayerEid => sourceOwnerPlayerEid;
        public int SourceAffixId => sourceAffixId;
        /// <summary>当前实际叠加层数，首层为1。</summary>
        public int StackCount => stackCount;
        public BuffConfig BuffConfig => buffConfig;
        public EBuffStateType StateType => buffConfig.buffStateType;
        public World World => ownerComponent?.OwnerEntity.World;

        private BuffEffectComponent ownerComponent;
        private int runtimeBuffId;
        private int buffId;
        private int sourceEntityEid;
        private int sourceOwnerPlayerEid; // 来源实体失效后不能再查询归属，因此在Buff创建时固定快照。
        private int sourceAffixId;
        private BuffConfig buffConfig;
        private IBuffTrigger buffTrigger;
        private IBuffExecute buffExecute;
        private IBuffDestroy buffDestroy;
        private int stackCount;
        private int mountedEffectEid;
        private bool dirty;
        private bool destroyed;
        private bool logSuppressed;

        public void SetData(BuffEffectComponent owner, int runtimeId, int configId,
            int sourceEid, int affixId, BuffConfig config)
        {
            ownerComponent = owner;
            runtimeBuffId = runtimeId;
            buffId = configId;
            sourceEntityEid = sourceEid;
            sourceOwnerPlayerEid = 0;
            if (owner?.OwnerEntity?.World != null
                && owner.OwnerEntity.World.TryGetEntity(sourceEid, out Entity source))
            {
                // sourceOwnerPlayerEid = BattleEntityMedium.GetOwnerPlayerEid(source);
            }
            sourceAffixId = affixId;
            buffConfig = config;
            stackCount = 1;
            mountedEffectEid = 0;
            dirty = false;
            destroyed = false;
            logSuppressed = false;
            buffTrigger = BuffTriggerFactory.Create(this);
            buffExecute = BuffExecuteFactory.Create(this);
            buffDestroy = BuffDestroyFactory.Create(config.destroyType, RequestDestroy);
        }

        public void Trigger(EBuffTriggerEvent eventType, BuffTriggerContext context)
        {
            if (dirty || !buffTrigger.CheckTrigger(eventType, context)) return;
            TakeEffect(eventType, context);
        }

        public void UpdateTrigger(float deltaTime)
        {
            if (dirty) return;
            BuffTriggerContext context = BuffTriggerContext.Rent();
            try
            {
                if (!buffTrigger.Update(deltaTime, context)) return;
                EBuffTriggerEvent eventType = buffConfig.triggerType is HpRatioLessIntervalTrigger
                    ? EBuffTriggerEvent.HpRatioLessInterval : EBuffTriggerEvent.Interval;
                TakeEffect(eventType, context);
            }
            finally
            {
                BuffTriggerContext.Return(context);
            }
        }

        public void Update(float deltaTime)
        {
            if (dirty) return;
            buffDestroy.Update(deltaTime);
        }

        public void Stack()
        {
            if (dirty) return;
            if (buffConfig.buffStacking is StackNone) return;
            if (buffConfig.buffStacking is StackReset)
            {
                buffExecute.Reset();
                buffDestroy.Reset();
                return;
            }
            if (buffConfig.buffStacking is StackNum stackRule
                && (stackRule.num < 0 || stackCount - 1 < stackRule.num))
            {
                stackCount++;
                PlayAsset();
                buffExecute.Stack(1);
            }
        }

        public void Reapply()
        {
            if (!dirty) buffExecute.Reapply();
        }

        public bool GetOwnEntity(out Entity ownEntity)
        {
            ownEntity = ownerComponent?.OwnerEntity;
            return ownEntity != null && !ownEntity.IsDirty();
        }

        public bool GetReleaserEntity(out Entity releaserEntity)
        {
            releaserEntity = null;
            return World != null && World.TryGetEntity(sourceEntityEid, out releaserEntity) && !releaserEntity.IsDirty();
        }

        public Entity ResolveEffectTarget(EBuffEffectTarget target, BuffTriggerContext context)
        {
            return ownerComponent.ResolveEffectTarget(target, context);
        }

        /// <summary>由具体执行器补充本次Buff实际修改的目标和数值结果。</summary>
        [Conditional(FrankGame.Log.ENABLE_LOG)]
        internal void LogExecution(string message, int triggerTargetEid = 0)
        {
            if (logSuppressed || !GetOwnEntity(out Entity owner))
            {
                return;
            }

            BattleLog.LocalBuff(owner, sourceEntityEid, sourceOwnerPlayerEid,
                runtimeBuffId, buffId, sourceAffixId, triggerTargetEid, message);
        }

        /// <summary>实体整体回收时关闭Buff执行器的清理日志，避免World销毁产生批量噪音。</summary>
        internal void SetLogSuppressed(bool value)
        {
            logSuppressed = value;
        }

        public bool IsDirty()
        {
            return dirty;
        }
        public void SetDirty(bool value)
        {
            dirty = value;
        }

        public void Destroy()
        {
            if (destroyed) return;
            destroyed = true;
            DestroyMountedEffect();
            buffExecute?.Destroy();
        }

        public void ClearMountedEffect(int effectEid)
        {
            if (mountedEffectEid == effectEid) mountedEffectEid = 0;
        }

        private void TakeEffect(EBuffTriggerEvent eventType, BuffTriggerContext context)
        {
            if (GetOwnEntity(out Entity owner))
            {
                BattleLog.LocalBuff(owner, sourceEntityEid, sourceOwnerPlayerEid,
                    runtimeBuffId, buffId, sourceAffixId, context?.triggerTargetEid ?? 0,
                    "action=Trigger event=" + eventType
                    + " effect=" + buffConfig.effectContent.GetType().Name
                    + " destroy=" + buffConfig.destroyType.GetType().Name
                    + " stack=" + stackCount
                    + " effectiveDamage=" + (context?.effectiveDamage ?? 0f).ToString("0.##")
                    + " collisionPoint=" + (context != null && context.hasCollisionPoint
                        ? context.collisionPoint.ToString("F2") : "None"));
            }
            PlayAsset();
            buffExecute.Execute(context);
            buffDestroy.CheckDestroy();
        }

        private void RequestDestroy()
        {
            ownerComponent?.RemoveBuff(runtimeBuffId);
        }

        private void PlayAsset()
        {
            if (buffConfig.assetInfo is MountPoint mount)
            {
                if (string.IsNullOrWhiteSpace(mount.battleAssetId) || mount.battleAssetId == "null") return;
                if (mountedEffectEid > 0 && World.effectSys.TryGetEntity(mountedEffectEid, out EffectEntity current)
                    && !current.IsDirty()) return;
                if (!GetOwnEntity(out Entity own)) return;
                EffectEntity effect = World.effectSys.CreateMountedEffect(mount.battleAssetId, own);
                if (effect == null) return;
                mountedEffectEid = effect.ID;
                int runtimeId = runtimeBuffId;
                effect.SetDestoryCallback(effectEid => ownerComponent?.OnMountedEffectDestroyed(runtimeId, effectEid));
            }
            else if (buffConfig.assetInfo is OnceShow once
                && !string.IsNullOrWhiteSpace(once.battleAssetId) && once.battleAssetId != "null"
                && GetOwnEntity(out Entity own)
                && own.TryGetComponent(ECompType.Transform, out TransformComponent transform))
            {
                World.effectSys.CreateEffect(once.battleAssetId, transform.position);
            }
        }

        private void DestroyMountedEffect()
        {
            if (mountedEffectEid <= 0 || World == null) return;
            int effectEid = mountedEffectEid;
            mountedEffectEid = 0;
            World.effectSys.RemoveEntity(effectEid);
        }

        public void Clear()
        {
            if (buffExecute != null) BuffExecuteFactory.Remove(this, buffExecute);
            if (buffDestroy != null) BuffDestroyFactory.Remove(this, buffDestroy);
            if (buffTrigger != null) BuffTriggerFactory.Remove(this, buffTrigger);
            ownerComponent = null;
            runtimeBuffId = 0;
            buffId = 0;
            sourceEntityEid = 0;
            sourceOwnerPlayerEid = 0;
            sourceAffixId = 0;
            buffConfig = null;
            buffTrigger = null;
            buffExecute = null;
            buffDestroy = null;
            stackCount = 0;
            mountedEffectEid = 0;
            dirty = false;
            destroyed = false;
            logSuppressed = false;
        }
    }
}
