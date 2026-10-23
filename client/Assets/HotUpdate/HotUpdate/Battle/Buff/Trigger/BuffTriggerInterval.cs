using editor.cfg.skill;
using UnityEngine;

namespace FrankGame.Battle
{
    public sealed class BuffTriggerInterval : BuffTriggerBase<IntervalTrigger>
    {
        private float elapsedSecond;

        public override bool Update(float deltaTime, BuffTriggerContext context)
        {
            elapsedSecond += deltaTime;
            float interval = Mathf.Max(0.01f, triggerConfig.second);
            if (elapsedSecond < interval) return false;
            elapsedSecond -= interval;
            return CheckProbability();
        }

        protected override void OnClear()
        {
            elapsedSecond = 0f;
        }
    }
}
