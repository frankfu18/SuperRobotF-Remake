using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrankGame
{
    public class TimerManager : TimeSinglet<TimerManager>
    {
        private static int Timer_Instance_Id = 1;

        public Dictionary<int, int> timeIdToIndexDic = new Dictionary<int, int>();
        public List<Timer> totalTimers = new List<Timer>();
        public List<int> usingTimerIndexList = new List<int>();
        public Queue<int> freeTimerIndexQueue = new Queue<int>();
        public List<int> addTimerIndexList = new List<int>();
        public List<int> removeTimerIndexList = new List<int>();

        private Timer GetTimer()
        {
            Timer timer = null;
            int index;

            if (freeTimerIndexQueue.Count > 0)
            {
                index = freeTimerIndexQueue.Dequeue();
                timer = totalTimers[index];
                timer.TimerId = Timer_Instance_Id++;

                timeIdToIndexDic.Add(timer.TimerId, index);

                addTimerIndexList.Add(index);
            }
            else
            {
                timer = new Timer(Timer_Instance_Id++);

                index = totalTimers.Count;
                totalTimers.Add(timer);

                timeIdToIndexDic.Add(timer.TimerId, index);
                addTimerIndexList.Add(index);
            }

            return timer;
        }

        public void Update(float _deltaTime, float _unscaledDeltaTime)
        {
            int romoveCount = removeTimerIndexList.Count;
            if (romoveCount > 0)
            {
                for (int i = 0; i < romoveCount; i++)
                {
                    int index = removeTimerIndexList[i];

                    if (usingTimerIndexList.Remove(index))
                    {
                        Timer timer = totalTimers[index];

                        timer.Clear();

                        freeTimerIndexQueue.Enqueue(index);
                    }
                }

                removeTimerIndexList.Clear();
            }

            int addCount = addTimerIndexList.Count;

            if (addCount > 0)
            {
                for (int i = 0; i < addCount; i++)
                {
                    int index = addTimerIndexList[i];

                    usingTimerIndexList.Add(index);
                }

                addTimerIndexList.Clear();
            }

            int timeCount = usingTimerIndexList.Count;
            if (timeCount > 0)
            {
                for (int i = 0; i < timeCount; i++)
                {
                    int index = usingTimerIndexList[i];
                    Timer timer = totalTimers[index];

                    if (timer.ignoreScale)
                    {
                        timer.Update(_unscaledDeltaTime);
                    }
                    else
                    {
                        timer.Update(_deltaTime);
                    }

                    if (timer.dirty)
                    {
                        removeTimerIndexList.Add(index);
                    }
                }
            }
        }

        public int AddDelay(float _delay, OnTimerCallback _func, bool _ignoreScale = false)
        {
            if(_delay <= 0)
            {
                _func?.Invoke();
                return -1;
            }

            Timer timer = GetTimer();
            timer.ignoreScale = _ignoreScale;
            timer.duration = _delay;
            timer.count = 1;
            timer.callback = _func;

            timer.Start();

            return timer.TimerId;
        }

        public int AddIntervalUpdate(float _interval, OnTimerCallback _func, bool _immediately = false, bool _ignoreScale = false)
        {
            Timer timer = GetTimer();
            timer.duration = _interval;
            timer.ignoreScale = _ignoreScale;
            timer.count = -1;
            timer.callback = _func;

            timer.Start(_immediately);

            return timer.TimerId;
        }

        /// <summary>
        /// 持续时间内间隔刷新
        /// </summary>
        /// <param name="_interval"></param>
        /// <param name="_duration"></param>
        /// <param name="_func"></param>
        /// <param name="_immediately"></param>
        /// <returns></returns>
        public int AddIntervalUpdate(float _interval, float _duration, OnTimerCallback _func, bool _immediately = false)
        {
            Timer timer = GetTimer();
            timer.duration = _interval;
            timer.count = Mathf.CeilToInt(_duration / _interval);
            timer.callback = _func;

            timer.Start(_immediately);

            return timer.TimerId;
        }

        public int AddCount(float _interval, int _count, OnTimerCallback _func, OnTimerCallback _complete = null)
        {
            Timer timer = GetTimer();
            timer.duration = _interval;
            timer.count = _count;
            timer.callback = _func;
            timer.complete = _complete;

            timer.Start();

            return timer.TimerId;
        }

        public void RemoveTimer(int _timerId)
        {
            if (timeIdToIndexDic.TryGetValue(_timerId, out int index))
            {
                if (addTimerIndexList.Contains(index))
                {
                    addTimerIndexList.Remove(index);
                }
                else
                {
                    if (usingTimerIndexList.Contains(index))
                    {
                        removeTimerIndexList.Add(index);

                        timeIdToIndexDic.Remove(_timerId);
                    }
                }
            }
        }
    }
}