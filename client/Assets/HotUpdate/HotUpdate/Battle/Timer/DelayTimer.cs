using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrankGame.Battle
{
    public class DelayTimer : ITimer
    {
        public int Index
        {
            get { return index; }
        }

        private int index;
        private System.Action onComplete;
        private float curTime;
        private bool running;
        private float duration;

        public DelayTimer(float _delay, System.Action _onComplete)
        {
            SetData(_delay, _onComplete);
        }

        public void SetData(float _delay, System.Action _onComplete)
        {
            this.duration = _delay;
            this.onComplete = _onComplete;
            this.curTime = 0f;
            this.running = false;
        }

        public void SetIndex(int _index)
        {
            this.index = _index;
        }

        public void Start()
        {
            if(this.duration > 0)
            {
                this.running = true;
                this.curTime = 0f;
            }
            else
            {
                Complete();
            }
        }

        public void Update(float _deltaTime)
        {
            if (!this.running)
            {
                return;
            }

            this.curTime += _deltaTime;
            if (this.curTime >= this.duration)
            {
                Complete();
            }
        }

        private void Complete()
        {
            this.onComplete?.Invoke();
            this.running = false;
        }

        public bool IsRunning()
        {
            return this.running;
        }

        public void Cancel(bool _processComplete)
        {
            if (!this.running)
            {
                return;
            }

            if(_processComplete)
            {
                Complete();
            }

            this.running = false;
        }

        public void Dispose()
        {
            this.onComplete = null;
            this.running = false;
            this.curTime = 0f;
            this.duration = 0f;
        }
    }
}
