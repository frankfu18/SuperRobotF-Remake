using editor.cfg.skill;

namespace FrankGame.Battle
{
    public abstract class BuffExecuteBase<T> : IBuffExecute, IClear where T : BuffEffectContent
    {
        protected T effectContent;
        protected BuffEffect buffEffect;

        public virtual void SetData(T content, BuffEffect effect)
        {
            effectContent = content;
            buffEffect = effect;
        }

        public void Execute(BuffTriggerContext context)
        {
            OnExecute(context);
            buffEffect.LogExecution("action=Execute executor=" + GetType().Name
                + " effect=" + effectContent.GetType().Name,
                context?.triggerTargetEid ?? 0);
        }
        public void Reset()
        {
            OnReset();
        }
        public void Stack(int addCount)
        {
            OnStack(addCount);
        }
        public void Reapply()
        {
            OnReapply();
        }
        public void Destroy()
        {
            OnDestroy();
        }

        protected abstract void OnExecute(BuffTriggerContext context);
        protected virtual void OnReset() { }
        protected virtual void OnStack(int addCount) { }
        protected virtual void OnReapply() { }
        protected virtual void OnDestroy() { }

        public void Clear()
        {
            OnClear();
            effectContent = null;
            buffEffect = null;
        }

        protected virtual void OnClear() { }
    }
}
