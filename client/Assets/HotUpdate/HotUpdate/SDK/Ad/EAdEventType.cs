using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EAdEventType
{
    None = 0,

    BuffReplaceAll,//选择特性换一批
    BuffGetAll,//选择特性全都要
    Relive,//复活
    ResultFailDoubleReward,//失败双倍奖励
    ResultVictoryDoubleReward,//胜利双倍奖励
    BattleSpeed,//看广告获得加速
    
    /// <summary>
    /// 战斗中的仙侣射覆游戏
    /// </summary>
    XianLvGame,

    ResultVictoryComfirm,//胜利点击确认
    ResultFailComfirm,//失败点击确认
    BuffSelect,//选择一个buff

    GetCellItem,//看广告获得物品格子
    GetEquipItem,//看广告获得高级装备
    BagRefreshItem,//看广告刷新物品
    GetTwoCellItem,//看广告获得两格子
    AddSliver,//银币不足时获得银币

    RestEventGetAll,//休息事件全都要
    SelectEventGetAll,//选择事件全都要

    ChapterSweep,//章节扫荡

    ShopDailyShopFreeGoods,//每日商店看广告获得的免费商品
    ShopGold,//商店金币
    ShopRefresh,//商店刷新
    ShopCommonBox,//商店普通宝箱
    ShopExquisiteBox,//商店高级宝箱
    ShopDailyPack,//商店每日礼包
    ShopWeeklyPack,//商店每周礼包
    
    // ShopAdFreeCoupon,   //  商店，免广告券
    ShopHeroFragment,   //  商店，英雄碎片

    GetStrength,//看广告获得体力
    
    HeroFreeUpgradeLevel,   //  看广告免费升级英雄
    HeroFreeClaimFragment,   //  看广告免费领取英雄碎片
    FightToEndDungeonsGetChance, //  看广告获取一战到底副本挑战机会
    
    CompanionAdGetGift,     //  仙侣，看广告获取礼物
    
}
