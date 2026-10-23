using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FrankGame;

public class HotUpdateProcedure : GameLaunchProcedure
{
    protected override void OnEnter(params object[] _params)
    {
        base.OnEnter(_params);

        SetProgress(1, LocalizationManager.Instance.GetPackageLanguageText("asset_loading"));

        HotUpdateSystem.Instance.LoadHotUpdateAssembly();

        this.CompleteCurrentProcedure();
    }
}
