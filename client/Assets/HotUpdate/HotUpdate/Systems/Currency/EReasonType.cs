using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EReasonType
{
    None,

    User_Create,//创建角色获得

    GM,//GM命令获得

    UserNameChange,//改名

    Equip_Upgrade,//卡牌升级

    Role_Unlock,//角色解锁
    Role_Upgrade,//角色升级

    Arena_Battle_Result,//竞技场战斗结算
    Arena_Battle_Box,//竞技场战斗宝箱
}
