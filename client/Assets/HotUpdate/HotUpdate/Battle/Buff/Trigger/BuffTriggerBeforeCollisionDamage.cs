using editor.cfg.skill;

namespace FrankGame.Battle
{
    public sealed class BuffTriggerBeforeCollisionDamage : BuffTriggerBase<BeforeCollisionDamageTrigger>
    {
        public override bool CheckTrigger(EBuffTriggerEvent eventType, BuffTriggerContext context)
        {
            return eventType == EBuffTriggerEvent.BeforeCollisionDamage && MatchesTarget(triggerConfig.targetEntityType, context) && CheckProbability();
        }
    }
}
