using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FrankGame;

public class LoginProcedure : GameLaunchProcedure
{
    protected override void OnEnter(params object[] _params)
    {
        base.OnEnter(_params);

        EventNativeSystem.Instance.AddEvent(SysEvent.Procedure_Complete, this, OnMainPanelOpen);

        SetProgress(1, LocalizationManager.Instance.GetPackageLanguageText("login_start"));

        SdkManager.Instance.Login();
    }

    private void OnMainPanelOpen(params object[] _params)
    {
        this.CompleteCurrentProcedure();
    }

    protected override void OnExit()
    {
        base.OnExit();

        EventNativeSystem.Instance.RemoveEvent(this);
    }
}
