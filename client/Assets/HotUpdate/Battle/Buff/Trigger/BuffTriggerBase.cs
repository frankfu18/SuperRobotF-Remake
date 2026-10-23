using editor.cfg.skill;

namespace FrankGame.Battle
{
    public abstract class BuffTriggerBase<T> : IBuffTrigger, IClear where T : BuffTriggerType
    {
        protected T triggerConfig;
        protected BuffEffect buffEffect;

        public virtual void SetData(T config, BuffEffect effect)
        {
            triggerConfig = config;
            buffEffect = effect;
        }

        public virtual bool CheckTrigger(EBuffTriggerEvent eventType, BuffTriggerContext context)
        {
            return false;
        }
        public virtual bool Update(float deltaTime, BuffTriggerContext context)
        {
            return false;
        }

        protected bool CheckProbability()
        {
            int ratio = triggerConfig?.ratio ?? 0;
            if (ratio <= 0) return false;
            return ratio >= 10000 || RandomUtils.RandomPercentageByPermyriad(ratio);
        }

        protected bool MatchesTarget(ETargetEntityType targetType, BuffTriggerContext context)
        {
            // if (targetType == ETargetEntityType.None || targetType == ETargetEntityType.Any) return true;
            // return buffEffect.World.TryGetEntity(context?.triggerTargetEid ?? 0, out Entity target)
            //     && BattleEntityMedium.MatchesTargetType(target, targetType);
            //TODO
            return true;
        }

        public void Clear()
        {
            OnClear();
            triggerConfig = null;
            buffEffect = null;
        }

        protected virtual void OnClear() { }
    }
}
