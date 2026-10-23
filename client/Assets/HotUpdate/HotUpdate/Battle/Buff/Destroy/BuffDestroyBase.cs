using System;
using editor.cfg.skill;

namespace FrankGame.Battle
{
    public abstract class BuffDestroyBase<T> : IBuffDestroy, IClear where T : BuffDestroyType
    {
        private Action onDestoryFunc;

        public virtual void SetData(T _buffDestroyType, Action _onDestoryFunc)
        {
            this.onDestoryFunc = _onDestoryFunc;
        }

        public void Update(float _deltaTime)
        {
            OnUpdate(_deltaTime);
        }

        protected virtual void OnUpdate(float _deltaTime)
        {

        }

        public void Reset()
        {
            OnReset();
        }
        protected abstract void OnReset();

        public void CheckDestroy()
        {
            if (OnCheckDestroy())
            {
                ExecuteDestroy();
            }
        }

        protected abstract bool OnCheckDestroy();

        public void ExecuteDestroy()
        {
            this.onDestoryFunc?.Invoke();
        }

        public virtual void StartRound(int _round)
        {
            
        }

        public virtual void EndRound(int _round)
        {

        }

        public override string ToString()
        {
            return GetString();
        }

        protected abstract string GetString();

        public void Clear()
        {
            OnClear();
            this.onDestoryFunc = null;
        }

        protected abstract void OnClear();
    }
}
