using System;

namespace FrankGame.Battle
{
    [Flags]
    public enum EEntityType
    {
        None,
        FlyBullet,//飞行子弹
        TargetFlyBullet,//锁定目标飞行子弹
        ImmediateBullet,//瞬间子弹
        Skill,
        Buff,
        Effect,//特效
        Camera,
        Actor,
        Player,//玩家
        Planet,// 星噬模式星球实体
        Stone,// 星噬模式碎石与小型陨石实体
        Satellite,// 星噬模式伴星实体
        SatelliteBody,// 分裂伴星的独立碰撞身体
    }
}
