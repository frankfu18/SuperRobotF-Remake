using editor.cfg.skill;

namespace FrankGame.Battle
{
    public static class BuffTriggerFactory
    {
        public static IBuffTrigger Create(BuffEffect effect)
        {
            BuffTriggerType config = effect.BuffConfig.triggerType;
            switch (config.GetTypeId())
            {
                case ImmediateTrigger.TYPE_ID: return Create<BuffTriggerImmediate, ImmediateTrigger>(config, effect);
                case IntervalTrigger.TYPE_ID: return Create<BuffTriggerInterval, IntervalTrigger>(config, effect);
                case CollisionHitTargetTrigger.TYPE_ID: return Create<BuffTriggerCollisionHitTarget, CollisionHitTargetTrigger>(config, effect);
                case BeforeCollisionDamageTrigger.TYPE_ID: return Create<BuffTriggerBeforeCollisionDamage, BeforeCollisionDamageTrigger>(config, effect);
                case BeCollidedTrigger.TYPE_ID: return Create<BuffTriggerBeCollided, BeCollidedTrigger>(config, effect);
                case DealEffectiveDamageTrigger.TYPE_ID: return Create<BuffTriggerDealEffectiveDamage, DealEffectiveDamageTrigger>(config, effect);
                default: throw new System.Exception("未找到 BuffTriggerType：" + config.GetType().Name);
            }
        }

        public static void Remove(BuffEffect effect, IBuffTrigger trigger)
        {
            switch (effect.BuffConfig.triggerType.GetTypeId())
            {
                case ImmediateTrigger.TYPE_ID: RefObjectPool<BuffTriggerImmediate>.Instance.Recycle((BuffTriggerImmediate)trigger); break;
                case IntervalTrigger.TYPE_ID: RefObjectPool<BuffTriggerInterval>.Instance.Recycle((BuffTriggerInterval)trigger); break;
                case CollisionHitTargetTrigger.TYPE_ID: RefObjectPool<BuffTriggerCollisionHitTarget>.Instance.Recycle((BuffTriggerCollisionHitTarget)trigger); break;
                case BeforeCollisionDamageTrigger.TYPE_ID: RefObjectPool<BuffTriggerBeforeCollisionDamage>.Instance.Recycle((BuffTriggerBeforeCollisionDamage)trigger); break;
                case BeCollidedTrigger.TYPE_ID: RefObjectPool<BuffTriggerBeCollided>.Instance.Recycle((BuffTriggerBeCollided)trigger); break;
                case DealEffectiveDamageTrigger.TYPE_ID: RefObjectPool<BuffTriggerDealEffectiveDamage>.Instance.Recycle((BuffTriggerDealEffectiveDamage)trigger); break;
                default: throw new System.Exception("未找到 BuffTriggerType：" + effect.BuffConfig.triggerType.GetType().Name);
            }
        }

        private static TTrigger Create<TTrigger, TConfig>(BuffTriggerType config, BuffEffect effect)
            where TTrigger : BuffTriggerBase<TConfig>, new()
            where TConfig : BuffTriggerType
        {
            TTrigger trigger = RefObjectPool<TTrigger>.Instance.Get();
            trigger.SetData((TConfig)config, effect);
            return trigger;
        }
    }
}
