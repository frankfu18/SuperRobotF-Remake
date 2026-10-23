using editor.cfg.skill;

namespace FrankGame.Battle
{
    public sealed class BuffTriggerBeCollided : BuffTriggerBase<BeCollidedTrigger>
    {
        public override bool CheckTrigger(EBuffTriggerEvent eventType, BuffTriggerContext context)
        {
            return eventType == EBuffTriggerEvent.BeCollided && CheckProbability();
        }
    }
}
