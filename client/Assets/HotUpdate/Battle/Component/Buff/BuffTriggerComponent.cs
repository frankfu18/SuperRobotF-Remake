using System.Collections.Generic;
using editor.cfg.skill;
using UnityEngine;

namespace FrankGame.Battle
{
    public enum EBuffTriggerEvent
    {
        Immediate,
        Interval,
        CollisionHitTarget,
        BeforeCollisionDamage,
        BeCollided,
        DealEffectiveDamage,
        HpRatioLessInterval,
    }

    public sealed class BuffTriggerContext : IClear
    {
        public int triggerTargetEid;
        public Vector2 collisionPoint;
        public bool hasCollisionPoint;
        public float effectiveDamage;
        // public DamageRequest damageRequest;
        public bool cancelCurrentDamage;

        public static BuffTriggerContext Rent()
        {
            return RefObjectPool<BuffTriggerContext>.Instance.Get();
        }

        public static void Return(BuffTriggerContext context)
        {
            if (context != null)
            {
                RefObjectPool<BuffTriggerContext>.Instance.Recycle(context);
            }
        }

        public void Clear()
        {
            triggerTargetEid = 0;
            collisionPoint = Vector2.zero;
            hasCollisionPoint = false;
            effectiveDamage = 0f;
            // damageRequest = default;
            cancelCurrentDamage = false;
        }
    }

    /// <summary>只保存按运行时事件分类的 Buff ID 索引，不拥有 BuffEffect 或 BuffConfig。</summary>
    public sealed class BuffTriggerComponent : ComponentBase, ICompClone<BuffTriggerComponent>
    {
        private struct PendingTriggerRegister
        {
            public int runtimeBuffId;
            public int buffId;
        }

        public override ECompType ComponentType => ECompType.BuffTrigger;

        private readonly List<int> collisionHitTriggerBuffIdList = new();
        private readonly List<int> beforeCollisionDamageTriggerBuffIdList = new();
        private readonly List<int> beCollidedTriggerBuffIdList = new();
        private readonly List<int> damageTriggerBuffIdList = new();
        private readonly List<int> intervalTriggerBuffIdList = new();
        private readonly List<int> hpRatioTriggerBuffIdList = new();
        private readonly List<PendingTriggerRegister> pendingRegisters = new();
        private readonly List<int> pendingUnregisterBuffIds = new();
        private bool iterating;

        public BuffTriggerComponent(Entity entity) : base(entity) { }

        public void RegisterBuff(int runtimeBuffId, int buffId)
        {
            if (iterating)
            {
                pendingRegisters.Add(new PendingTriggerRegister { runtimeBuffId = runtimeBuffId, buffId = buffId });
                return;
            }

            BuffConfig config = ConfigSystem.CurrentData.GetBuffConfig(buffId);
            if (config == null) return;
            if (config.triggerType is ImmediateTrigger)
            {
                BuffTriggerContext context = BuffTriggerContext.Rent();
                try
                {
                    entity.GetComponent<BuffEffectComponent>(ECompType.BuffEffect)
                        .TriggerBuff(runtimeBuffId, EBuffTriggerEvent.Immediate, context);
                }
                finally
                {
                    BuffTriggerContext.Return(context);
                }
                return;
            }

            List<int> list = GetList(config.triggerType);
            if (list != null && !list.Contains(buffId)) list.Add(buffId);
        }

        public void UnregisterBuff(int buffId)
        {
            if (iterating)
            {
                if (!pendingUnregisterBuffIds.Contains(buffId)) pendingUnregisterBuffIds.Add(buffId);
                return;
            }

            BuffConfig config = ConfigSystem.CurrentData.GetBuffConfig(buffId);
            List<int> list = config == null ? null : GetList(config.triggerType);
            list?.Remove(buffId);
        }

        public void Update(float deltaTime)
        {
            DispatchUpdate(intervalTriggerBuffIdList, deltaTime);
            DispatchUpdate(hpRatioTriggerBuffIdList, deltaTime);
        }

        public void CollisionHitTarget(BuffTriggerContext context)
        {
            Dispatch(collisionHitTriggerBuffIdList, EBuffTriggerEvent.CollisionHitTarget, context);
        }

        public void BeforeCollisionDamage(BuffTriggerContext context)
        {
            Dispatch(beforeCollisionDamageTriggerBuffIdList, EBuffTriggerEvent.BeforeCollisionDamage, context);
        }

        public void BeCollided(BuffTriggerContext context)
        {
            Dispatch(beCollidedTriggerBuffIdList, EBuffTriggerEvent.BeCollided, context);
        }

        public void DealEffectiveDamage(BuffTriggerContext context)
        {
            Dispatch(damageTriggerBuffIdList, EBuffTriggerEvent.DealEffectiveDamage, context);
        }

        private void Dispatch(List<int> buffIds, EBuffTriggerEvent eventType, BuffTriggerContext context)
        {
            iterating = true;
            BuffEffectComponent effects = entity.GetComponent<BuffEffectComponent>(ECompType.BuffEffect);
            int count = buffIds.Count;
            for (int i = 0; i < count; i++) effects.TriggerBuffs(buffIds[i], eventType, context);
            iterating = false;
            FlushPending();
        }

        private void DispatchUpdate(List<int> buffIds, float deltaTime)
        {
            iterating = true;
            BuffEffectComponent effects = entity.GetComponent<BuffEffectComponent>(ECompType.BuffEffect);
            int count = buffIds.Count;
            for (int i = 0; i < count; i++) effects.UpdateTriggerBuffs(buffIds[i], deltaTime);
            iterating = false;
            FlushPending();
        }

        private List<int> GetList(BuffTriggerType triggerType)
        {
            if (triggerType is IntervalTrigger) return intervalTriggerBuffIdList;
            if (triggerType is CollisionHitTargetTrigger) return collisionHitTriggerBuffIdList;
            if (triggerType is BeforeCollisionDamageTrigger) return beforeCollisionDamageTriggerBuffIdList;
            if (triggerType is BeCollidedTrigger) return beCollidedTriggerBuffIdList;
            if (triggerType is DealEffectiveDamageTrigger) return damageTriggerBuffIdList;
            if (triggerType is HpRatioLessIntervalTrigger) return hpRatioTriggerBuffIdList;
            return null;
        }

        private void FlushPending()
        {
            if (iterating) return;
            int unregisterCount = pendingUnregisterBuffIds.Count;
            for (int i = 0; i < unregisterCount; i++)
            {
                UnregisterBuff(pendingUnregisterBuffIds[i]);
            }
            if (unregisterCount > 0)
            {
                pendingUnregisterBuffIds.RemoveRange(0, unregisterCount);
            }

            int registerCount = pendingRegisters.Count;
            for (int i = 0; i < registerCount; i++)
            {
                PendingTriggerRegister register = pendingRegisters[i];
                RegisterBuff(register.runtimeBuffId, register.buffId);
            }
            if (registerCount > 0)
            {
                pendingRegisters.RemoveRange(0, registerCount);
            }
        }

        public void Clone(BuffTriggerComponent target) { }

        public override void OnRecover()
        {
            collisionHitTriggerBuffIdList.Clear();
            beforeCollisionDamageTriggerBuffIdList.Clear();
            beCollidedTriggerBuffIdList.Clear();
            damageTriggerBuffIdList.Clear();
            intervalTriggerBuffIdList.Clear();
            hpRatioTriggerBuffIdList.Clear();
            pendingRegisters.Clear();
            pendingUnregisterBuffIds.Clear();
            iterating = false;
        }

        protected override string GetString()
        {
            return $"[Buff触发:{collisionHitTriggerBuffIdList.Count + beforeCollisionDamageTriggerBuffIdList.Count + beCollidedTriggerBuffIdList.Count + damageTriggerBuffIdList.Count + intervalTriggerBuffIdList.Count + hpRatioTriggerBuffIdList.Count}]";
        }
    }
}
