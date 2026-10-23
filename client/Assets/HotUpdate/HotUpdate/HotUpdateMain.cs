using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FrankGame;

public class HotUpdateMain
{
    public static void Run()
    {
        LogUtil.Log("启动热更新代码。");
                
        UISystem.Instance.Init();
        ConfigSystem.Instance.Init();
        ModuleHandler.Instance.Run();
    }

    public static void Login()
    {
        if (string.IsNullOrEmpty(GameConfig.SDK.LoginMode))//读取本地数据
        {
            DataBaseManager.Instance.LoadDB(ModuleHandler.Instance.Login);
        }
        else if (string.Equals(GameConfig.SDK.LoginMode, "google"))
        {
            DataBaseManager.Instance.LoadDB(ModuleHandler.Instance.Login);
        }
        else
        {
            LogUtil.Error("未找到的登录方式：" + GameConfig.SDK.LoginMode);
        }
    }

    public static void Logout()
    {
        ModuleHandler.Instance.Logout();
    }

    public static void Dispose()
    {
        ModuleHandler.Instance.Dispose();
    }
}
