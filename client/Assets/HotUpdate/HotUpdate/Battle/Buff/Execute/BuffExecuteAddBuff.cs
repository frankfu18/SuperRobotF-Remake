using editor.cfg.skill;

namespace FrankGame.Battle
{
    public sealed class BuffExecuteAddBuff : BuffExecuteBase<AddBuff>
    {
        protected override void OnExecute(BuffTriggerContext context)
        {
            Entity target = buffEffect.ResolveEffectTarget(effectContent.target, context);
            if (target != null && target.TryGetComponent(ECompType.BuffEffect, out BuffEffectComponent effects))
            {
                effects.AddBuff(effectContent.buffId, buffEffect.SourceEntityEid, buffEffect.SourceAffixId);
                buffEffect.LogExecution("action=AddBuff targetEid=" + target.ID
                    + " addedBuffId=" + effectContent.buffId, target.ID);
            }
        }
    }
}
