using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AdEventKey
{
    public static readonly Dictionary<EAdEventType, string> RvKeyDic = new Dictionary<EAdEventType, string>()
    {
        {EAdEventType.BuffReplaceAll, "RV_ReplaceAll" },
        {EAdEventType.BuffGetAll, "RV_SelectAllSkill" },
        {EAdEventType.Relive, "RV_Revive" },
        {EAdEventType.ResultFailDoubleReward, "RV_FailMore" },
        {EAdEventType.ResultVictoryDoubleReward, "RV_SuccMore" },
        {EAdEventType.BattleSpeed, "RV_SpeedUp" },
        {EAdEventType.GetCellItem, "RV_AddGrid" },
        {EAdEventType.GetEquipItem, "RV_AddEquipment" },
        {EAdEventType.BagRefreshItem, "RV_Refresh" },
        {EAdEventType.GetTwoCellItem, "RV_TwoGrid" },
        {EAdEventType.AddSliver, "RV_AddGems" },
        {EAdEventType.RestEventGetAll, "RV_SelectAll" },
        {EAdEventType.SelectEventGetAll, "RV_SelectAll" },
        {EAdEventType.ChapterSweep, "RV_Sweep" },
        {EAdEventType.ShopDailyShopFreeGoods, "RV_ShopPanacea" },
        {EAdEventType.ShopGold, "RV_ShopCoins" },
        {EAdEventType.ShopRefresh, "RV_ShopRefresh" },
        {EAdEventType.ShopCommonBox, "RV_CommonChest" },
        {EAdEventType.ShopExquisiteBox, "RV_ExquisiteChest" },
        {EAdEventType.GetStrength, "RV_Energy" },
        
        {EAdEventType.ShopHeroFragment, "RV_ShopHeroFragment"},
        {EAdEventType.HeroFreeUpgradeLevel, "RV_HeroUpgrade"},
        {EAdEventType.HeroFreeClaimFragment, "RV_HeroFragment"},
        {EAdEventType.FightToEndDungeonsGetChance, "RV_DungeonTimes"},
        {EAdEventType.CompanionAdGetGift, "RV_GetGift"},
        
        {EAdEventType.XianLvGame, "RV_LoseReward"},
    };

    public static readonly Dictionary<EAdEventType, string> IvKeyDic = new Dictionary<EAdEventType, string>()
    {
        {EAdEventType.ResultVictoryComfirm, "IV_Succ" },
        {EAdEventType.ResultFailComfirm, "IV_Fail" },
        {EAdEventType.BuffSelect, "IV_SelectSkill" },
    };
}
