using editor.cfg.skill;

namespace FrankGame.Battle
{
    public sealed class BuffTriggerDealEffectiveDamage : BuffTriggerBase<DealEffectiveDamageTrigger>
    {
        public override bool CheckTrigger(EBuffTriggerEvent eventType, BuffTriggerContext context)
        {
            return eventType == EBuffTriggerEvent.DealEffectiveDamage && MatchesTarget(triggerConfig.targetEntityType, context) && CheckProbability();
        }
    }
}
