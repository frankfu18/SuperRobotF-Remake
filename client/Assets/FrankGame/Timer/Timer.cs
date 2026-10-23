using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrankGame
{
    public delegate void OnTimerCallback();
    public delegate void OnGameTimerCallback(int _fixedTime, int _nowTime);

    public class Timer : ITimer
    {
        public int TimerId { get; set; }

        public ETimerType TimerType
        {
            get
            {
                return ETimerType.RealTime;
            }
        }

        public bool ignoreScale;//是否忽略缩放
        public float duration;
        public OnTimerCallback callback;
        public OnTimerCallback complete;
        public bool dirty;
        public int count;

        private float curTime;
        private bool start;
        private bool loop;

        public Timer(int _timeId)
        {
            TimerId = _timeId;
        }

        public void Start(bool _immediately = false)
        {
            dirty = false;

            if (count >= 0)
            {
                loop = false;
            }
            else
            {
                loop = true;
            }

            if (duration > 0)
            {
                start = true;
                curTime = 0;

                if (_immediately)
                {
                    ExecuteCallback();
                }
            }
            else
            {
                dirty = true;

                ExecuteCallback();
            }
        }

        public void Update(float _deltaTime)
        {
            if (dirty || !start)
            {
                return;
            }

            curTime += _deltaTime;
            if (curTime >= duration)
            {
                if (loop)
                {
                    curTime = 0;
                }
                else
                {
                    count--;
                }

                ExecuteCallback();
            }

            if (!loop)
            {
                if (count == 0)
                {
                    complete?.Invoke();
                }

                if (count <= 0)
                {
                    dirty = true;
                    start = false;
                }
            }
        }

        private void ExecuteCallback()
        {
            callback?.Invoke();
        }

        public void Clear()
        {
            ignoreScale = false;
            duration = 0;
            count = 0;
            dirty = false;
            start = false;
            complete = null;
            callback = null;
        }
    }
}