using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleSystem : SystemBase<ModuleSystem>
{
    private bool isLogin;

    public void Login()
    {
        isLogin = true;

        HotUpdateSystem.Instance.Login();
    }

    public void Logout()
    {
        if (!isLogin)
        {
            return;
        }

        isLogin = false;

        HotUpdateSystem.Instance.Logout();

        EventNativeSystem.Instance.SendEvent(SysEvent.Login_Out);
    }
}
