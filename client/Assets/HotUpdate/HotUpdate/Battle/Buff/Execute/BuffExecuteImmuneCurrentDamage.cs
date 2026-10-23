using editor.cfg.skill;

namespace FrankGame.Battle
{
    public sealed class BuffExecuteImmuneCurrentDamage : BuffExecuteBase<ImmuneCurrentDamage>
    {
        protected override void OnExecute(BuffTriggerContext context)
        {
            if (context != null)
            {
                context.cancelCurrentDamage = true;
                buffEffect.LogExecution("action=ImmuneCurrentDamage result=True",
                    context.triggerTargetEid);
            }
        }
    }
}
