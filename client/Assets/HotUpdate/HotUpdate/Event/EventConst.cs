public enum EventConst : uint
{
    Min = 1000,

    Panel_Open,
    Panel_Close,
    
    PlayAd,              //  播放广告
    UpdateOnlineTime,    //  更新在线时间

    //物品
    Item_Refresh,//物品数量刷新, param:itemId
    Item_FirstGet,//物品第一次获得

    Currency_Refresh,//货币刷新 params: ECurrencyType
    Currency_UpdateMaxStrength,

    #region 用户
    User_ScoreRefresh,//分数刷新
    #endregion

    #region 战斗
    Battle_Pause,//暂停
    Battle_Resume,//继续

    Battle_HpChange,
    Battle_Start,
    Battle_Prepare,//进入准备阶段，外部完成准备表现后发送 Battle_FightStart
    
    Battle_FightStart,//开打
    Battle_FightStateChange,//战斗状态变化
    Battle_RoundChange,//回合改变
    Battle_PlayerStateChange,//玩家阶段改变
    Battle_PlayerHpChange,//玩家血量变化
    Battle_ArenaResourceChanged,// params: playerEid, stageResource, fusedResource, totalResource
    Battle_ArenaBoostEnergyChanged,// params: currentEnergy, maxEnergy；仅分发本地玩家能量
    Battle_ArenaPlanetStateChanged,// params: ArenaPlanetStateSnapshot
    Battle_ArenaSatelliteStateChanged,// params: ArenaSatelliteStateSnapshot
    Battle_ArenaMiniMapChanged,// params: ArenaMiniMapSnapshot；本地星球半径内敌方星球的相对位置变化
    Battle_EvolutionOptionsChanged,// params: EvolutionOptionSnapshot；affixIds 为空表示关闭当前三选一
    Battle_ArenaEnd,// params: ArenaResultSnapshot

    #endregion

    #region 引导
    Guide_TaskRefresh,//新手引导任务刷新 params: stGuideTaskInfo
    Guide_TaskHide,//新手引导任务隐藏
    #endregion

    #region 战斗界面
    BattlePanel_HpChange,// params: camp, currentHp, maxHp
    #endregion

    #region 匹配
    Match_Success,//匹配成功
    #endregion

    #region 主界面
    MainPanel_ShowTag,//主界面打开页签
    #endregion

    #region 装备
    Equip_UseSuccess,//上阵成功
    Equip_RemoveSuccess,//下阵成功
    Equip_UpgradeSuccess,//升级成功
    Equip_SlotUnlock,//槽位解锁
    Equip_NewUnlock,//新装备解锁
    #endregion

    #region 角色
    Role_UnlockSuccess,//角色解锁成功
    Role_UpgradeSuccess,//角色升级成功
    Role_GoInBattleSuccess,//角色上阵成功
    #endregion

    #region 用户
    User_ChangeNameSuccess,//改名成功
    User_ChangeHeadSuccess,//改头衔成功
    #endregion
}
