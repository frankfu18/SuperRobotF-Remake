using editor.cfg.skill;

namespace FrankGame.Battle
{
    public sealed class BuffTriggerImmediate : BuffTriggerBase<ImmediateTrigger>
    {
        public override bool CheckTrigger(EBuffTriggerEvent eventType, BuffTriggerContext context)
        {
            return eventType == EBuffTriggerEvent.Immediate && CheckProbability();
        }
    }
}
