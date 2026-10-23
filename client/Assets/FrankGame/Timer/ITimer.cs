using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrankGame
{
    public interface ITimer
    {
        int TimerId { get; set; }

        ETimerType TimerType { get; }
    }

    public enum ETimerType
    {
        RealTime,//真实时间
        GameTime,//游戏时间
    }
}