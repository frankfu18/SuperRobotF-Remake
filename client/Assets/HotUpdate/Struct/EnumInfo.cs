using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EDialogRoleType
{
    User,
    Character,
}

public enum ETaskState
{
    NoDone = 0,//未完成
    Complete,//已完成未领取
    Receive,//已领取
}
