using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SysEvent : uint
{
    None = 0,
    Asset_Load_Done,
    Config_Load_Done,
    Module_Load_Done,
    Loading_Open,
    Loading_Close,
    Login_Success,//登录成功 param: 是否是重新连接
    Login_Out,      //  退出登录
    ServerList_Success,//请求服务器列表成功
    Procedure_Complete,//流程结束
    Daily_Refresh,//自然天刷新，实际时间
    Week_Refresh,//自然周刷新，实际时间


    Max = 999,
}
