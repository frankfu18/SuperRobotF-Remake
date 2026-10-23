using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrankGame.Battle
{
    public enum ECompType
    {
        None,
        Asset,// 资源组件，加载和卸载资源
        Transform,//位置组件
        Collider,//碰撞组件，检测刚体碰撞
        Spine,//spine组件
        SpineUI,//spine组件
        Animator,//动画组件
        Movement,//移动组件
        Bullet, // 子弹数据组件
        BulletFlyState,//子弹飞行状态组件
        BulletTarget,//子弹目标
        Blood, // 血量组件
        Shield, // 护盾组件
        BloodInt, // 血量组件
        BloodAsset,//血条资源组件
        Attribute, // 属性组件
        SkillRelease,//技能释放组件
        Skill,//技能组件（挂在攻击者身上的）
        BuffTrigger,//buff触发组件
        BuffEffect,//buff生效组件
        Effect,//特效组件
        
        ModelPoint,//模型挂点
        Camera,//相机
        Tween,//Tween组件

        Player,
        Attack,//自动攻击组件
        AIControl,
        PlayerControl,// 玩家输入启用标识，具体命令统一由 PlayerSystem 处理

        // 星噬模式组件必须追加在末尾，避免改变已有对象池键值。
        SceneItemAsset,// 场景资源脚本缓存
        Planet,// 星球运行时数据
        PlanetPhysics,// 星球真实物理缓存
        DamageWindow,// 连续受伤与资源损失窗口
        SatelliteOwner,// 星球拥有的伴星集合
        Stone,// 碎石运行时数据
        StoneMotion,// 碎石运动与吸附状态
        Satellite,// 伴星轨道、生命与复活状态
        CollisionDamage,// 碰撞伤害来源与攻击者归属
        BulletModifier,// 子弹命中效果修正
        SatelliteAbility,// 伴星已安装能力与来源索引
        SatelliteSplit,// 黑铁伴星分裂运行状态
        SatelliteBody,// 分裂身体到逻辑伴星的引用
        RandomRotate,// 伴星模型的确定性随机摆动状态
    }
}
