using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrankGame.Battle
{
    public class IntervalTimer : ITimer
    {
        public int Index
        {
            get { return index; }
        }

        private int index;
        private System.Action onComplete;
        private float curTime;
        private bool running;
        private float interval;
        private int repeatCount;

        public IntervalTimer(float _interval, System.Action _onComplete, int _repeatCount)
        {
            SetData(_interval, _onComplete, _repeatCount);
        }

        public void SetData(float _interval, System.Action _onComplete, int _repeatCount)
        {
            this.interval = _interval;
            this.repeatCount = _repeatCount;
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
            if(this.interval < 0 || this.repeatCount == 0)
            {
                this.onComplete?.Invoke();
                return;
            }

            this.curTime = 0f;
            this.running = false;
        }

        public void Update(float _deltaTime)
        {
            if (!this.running)
            {
                return;
            }

            if(this.repeatCount != 0)
            {
                this.curTime += _deltaTime;
                if (this.curTime >= this.interval)
                {
                    Process();
                }
            }
        }

        private void Process()
        {
            this.onComplete?.Invoke();

            if(this.repeatCount > 0)//小于0表示无限循环
            {
                this.repeatCount--;
                if (this.repeatCount == 0)
                {
                    this.running = false;
                }
                else
                {
                    this.curTime = 0f;
                }
            }
        }

        public void Cancel(bool _processComplete)
        {
            if (!this.running)
            {
                return;
            }

            if (_processComplete)
            {
                this.onComplete?.Invoke();
            }

            this.running = false;
        }

        public bool IsRunning()
        {
            return this.running;
        }

        public void Dispose()
        {
            this.interval = 0;
            this.repeatCount = 0;
            this.onComplete = null;
            this.curTime = 0f;
            this.running = false;
        }
    }
}
