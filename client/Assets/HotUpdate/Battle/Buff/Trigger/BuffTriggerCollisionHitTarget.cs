using editor.cfg.skill;

namespace FrankGame.Battle
{
    public sealed class BuffTriggerCollisionHitTarget : BuffTriggerBase<CollisionHitTargetTrigger>
    {
        public override bool CheckTrigger(EBuffTriggerEvent eventType, BuffTriggerContext context)
        {
            return eventType == EBuffTriggerEvent.CollisionHitTarget && MatchesTarget(triggerConfig.targetEntityType, context) && CheckProbability();
        }
    }
}
