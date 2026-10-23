using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrankGame.Battle
{
    public class TimerHandler
    {
        private List<ITimer> timers = new List<ITimer>(100);
        private List<ITimer> removeTimers = new List<ITimer>(50);
        private List<ITimer> addTimers = new List<ITimer>(50);
        private Stack<DelayTimer> freeDelayTimers = new Stack<DelayTimer>();
        private Stack<IntervalTimer> freeIntervalTimers = new Stack<IntervalTimer>();

        public TimerHandler()
        {
            
        }

        public int AddDelayTimer(float _delay, System.Action _onComplete)
        {
            if(_delay <= 0)
            {
                _onComplete?.Invoke();
                return -1;
            }

            DelayTimer timer = null;
            if (freeDelayTimers.Count > 0)
            {
                timer = freeDelayTimers.Pop();
                timer.SetData(_delay, _onComplete);
            }
            else
            {
                timer = new DelayTimer(_delay, _onComplete);
            }

            timer.SetIndex(this.timers.Count);
            timer.Start();

            this.addTimers.Add(timer);

            return timer.Index;
        }

        public int AddIntervalTimer(float _interval, System.Action _onComplete, int _repeatCount = -1)
        {
            if (_interval < 0 || _repeatCount == 0)
            {
                _onComplete?.Invoke();
                return -1;
            }

            IntervalTimer timer = null;

            if (freeIntervalTimers.Count > 0)
            {
                timer = freeIntervalTimers.Pop();
                timer.SetData(_interval, _onComplete, _repeatCount);
            }
            else
            {
                timer = new IntervalTimer(_interval, _onComplete, _repeatCount);
            }

            timer.SetIndex(this.timers.Count);
            timer.Start();

            this.addTimers.Add(timer);

            return timer.Index;
        }

        public void Update(float _deltaTime)
        {
            if(this.addTimers.Count > 0)
            {
                this.timers.AddRange(this.addTimers);

                this.addTimers.Clear();
            }

            int timerCount = this.timers.Count;
            for (int i = 0; i < timerCount; i++)
            {
                var timer = this.timers[i];
                if (timer.IsRunning())
                {
                    timer.Update(_deltaTime);
                }
                else
                {
                    this.removeTimers.Add(timer);
                }
            }

            if (this.removeTimers.Count > 0)
            {
                for (int i = 0; i < this.removeTimers.Count; i++)
                {
                    var timer = this.removeTimers[i];
                    int removeIdx = timer.Index;
                    int lastIdx = timerCount - 1;

                    if (removeIdx != lastIdx)
                    {
                        var temp = this.timers[lastIdx];
                        this.timers[lastIdx] = this.timers[removeIdx];
                        this.timers[removeIdx] = temp;
                        this.timers[removeIdx].SetIndex(removeIdx);
                    }

                    timer.Dispose();

                    this.timers.RemoveAt(lastIdx);
                    timerCount--;

                    if(timer is DelayTimer delayTimer)
                    {
                        this.freeDelayTimers.Push(delayTimer);
                    }
                }

                this.removeTimers.Clear();
            }
        }

        public void Cancel(int _timerIdx, bool _processComplete)
        {
            if(_timerIdx < 0 || _timerIdx >= this.timers.Count)
            {
                return;
            }

            var timer = this.timers[_timerIdx];

            timer.Cancel(_processComplete);

            this.removeTimers.Add(timer);
        }

        public void Dispose()
        {
            this.timers.Clear();
            this.removeTimers.Clear();
            this.addTimers.Clear();
            this.freeDelayTimers.Clear();
            this.freeIntervalTimers.Clear();
        }
    }
}
