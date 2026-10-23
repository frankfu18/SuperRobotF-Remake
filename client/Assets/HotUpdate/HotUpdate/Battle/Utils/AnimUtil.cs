using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EModelAnimType
{
    Idle,
    IdleMove,
    Attack,
    Attack_1,
    Attack_2,
    Skill,
    Hurt,//被击
    Dead,
    HitBack,//击退
    TurnBack,//回退
    Show,//登场
}
public static class AnimUtil
{
    public class AnimData
    {
        public string animName;
    }

    public static readonly Dictionary<EModelAnimType, string> Anim_Data_Dic = new()
    {
        { EModelAnimType.Idle, "idle_01"},
        { EModelAnimType.IdleMove, "idle_02"},
        { EModelAnimType.Attack, "attack"},
        { EModelAnimType.Attack_1,"attack_01"},
        { EModelAnimType.Attack_2,"attack_02"},
        { EModelAnimType.Skill,"skill"},
        { EModelAnimType.Hurt,"hurt"},
        { EModelAnimType.Dead,"die" },
        { EModelAnimType.HitBack,"Displace_01" },
        { EModelAnimType.TurnBack,"Displace_02"},
        { EModelAnimType.Show,"in"},
    };

    public static string GetAnimName(EModelAnimType _animType)
    {
        if(Anim_Data_Dic.TryGetValue(_animType, out string animName))
        {
            return animName;
        }

        return null;
    }
}
