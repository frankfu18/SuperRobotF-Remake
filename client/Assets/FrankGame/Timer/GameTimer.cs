using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace FrankGame
{
    //游戏时间定时器
    public class GameTimer : ITimer
    {
        public int TimerId { get; set; }

        public ETimerType TimerType
        {
            get
            {
                return ETimerType.GameTime;
            }
        }

        public int fixedTime;//定时，游戏时间(s)
        public int registerTime;//注册的时间
        public int interval;//执行的间隔，如果次数-1或者大于0
        public int count;
        public bool nowDay;//今天是否执行

        public Action Canncel;
        public OnGameTimerCallback onCallback;//到时间执行的回调

        public GameTimer()
        {
            
        }
    }
}