using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FrankGame;

public class SystemInitProcedure : GameLaunchProcedure
{
    protected override void OnEnter(params object[] _params)
    {
        base.OnEnter(_params);

        (GameRoot.Instance as GameRoot).SystemRun();

        this.CompleteCurrentProcedure();
    }
}
