using System;
using UnityEngine;
using FrankGame;

public class GameRoot : GameEntry
{
    public event Action onUpdate;

    private SystemManager sysManager;
    protected override void OnInit()
    {
#if UNITY_EDITOR
        this.gameObject.AddComponent<DebugRunner>();
#endif

        sysManager = new SystemManager();
        sysManager.Init(this);

        this.gameLauncher.AddCustomProcedure(typeof(SystemInitProcedure));
        this.gameLauncher.AddCustomProcedure(typeof(FontLocalizationProcedure));
        this.gameLauncher.AddCustomProcedure(typeof(HotUpdateProcedure));
        this.gameLauncher.AddCustomProcedure(typeof(LoginProcedure));

        this.onLoadStartFunc = OnLoadStart;
        this.onLoadFinishFunc = OnLoadFinish;


        Input.multiTouchEnabled = true;//禁止多点触控
    }

    protected override void OnUpdate()
    {
        sysManager.Update(Time.deltaTime, Time.unscaledDeltaTime);

        onUpdate?.Invoke();
    }

    protected override void OnLaunchComplete()
    {
        base.OnLaunchComplete();
    }

    public void SystemRun()
    {
        sysManager.Run();
    }

    private void OnLoadStart()
    {
        SdkManager.Instance.InitSdk();
        SdkManager.Instance.EmitNoPublicData("Loading_Start");
    }

    private void OnLoadFinish(float _duration)
    {
        SdkManager.Instance.Emit("Loading_End", "Time", _duration);
    }
}
