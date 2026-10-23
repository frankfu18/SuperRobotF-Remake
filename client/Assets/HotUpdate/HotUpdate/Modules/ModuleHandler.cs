using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleHandler : Singlet<ModuleHandler>
{
    private List<IModule> moduleList = new List<IModule>()
    {
        LoginModule.Instance,//登录放到第一个
        AttributeModule.Instance,
        ConditionModule.Instance,
        CostModule.Instance,
        CurrencyModule.Instance,
        GlobalDataModule.Instance,
        GmModule.Instance,
        ItemModule.Instance,
        RoleModule.Instance,
    };

    public void Run()
    {
        foreach (var module in moduleList)
        {
            module.Init();
        }

        foreach (var module in moduleList)
        {
            module.LoadConfigDone();
        }
    }

    public void Login()
    {
        LoginDone();
    }

    private void LoginDone()
    {
        EventCenterSystem.Instance.SendEvent(SysEvent.Login_Success);

        foreach (var module in moduleList)
        {
            module.Login();
        }

        foreach (var module in moduleList)
        {
            module.LoginDone();
        }

        EventCenterSystem.Instance.SendEvent(SysEvent.Module_Load_Done);
    }

    public void Logout()
    {
        foreach (var module in moduleList)
        {
            module.Logout();
        }
    }

    public void Dispose()
    {
        foreach (var module in moduleList)
        {
            module.Dispose();
        }
    }
}
