using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if WX_GAME
using WeChatWASM;
#endif

namespace FrankGame
{
    public abstract class GameEntry : SingleMonoBehaviour<GameEntry>
    {
        public Canvas UICanvas;
        public UnityEngine.EventSystems.EventSystem EventSystem;

        public Action<bool, float> onApplicationPause;
        public Action<float> onApplicationOvertime;//切后台超时方法，参数切后台秒数

        protected GameLauncher gameLauncher;//启动器
        private DebugFloatWindow debugFloatWindow;

        protected System.Action onLoadStartFunc = null;
        protected System.Action<float> onLoadFinishFunc = null;

        private long startPasueTime;//开始暂停时间

        protected override void Awake()
        {
            GameConfig.AddDebugCallback((debug) => 
            {
                if (debug)
                {
                    if (GameConfig.Debug.DebugWindow)
                    {
                        if (debugFloatWindow == null)
                        {
                            debugFloatWindow = this.gameObject.AddComponent<DebugFloatWindow>();
                        }
                    }
                }
                else
                {
                    if (GameConfig.Debug.DebugWindow)
                    {
                        Destroy(debugFloatWindow);
                        debugFloatWindow = null;
                    }
                }
            });
            
#if UNITY_EDITOR

#elif WX_GAME
            if (EventSystem != null)
            {
                EventSystem.gameObject.AddComponent<WXTouchInputOverride>();
            }
#elif BYTE_GAME
            if (EventSystem != null)
            {
                EventSystem.gameObject.AddComponent<StarkSDKSpace.StarkInputOverrideBypass>();
            }
#endif

            Application.targetFrameRate = GameConfig.Setting.FrameRate;
            Application.runInBackground = GameConfig.Setting.RunInBackground;

            this.gameObject.AddComponent<NetworkCheck>();

            this.gameLauncher = new GameLauncher(this, OnLaunchComplete);
            this.gameLauncher.RegisterProcedure(typeof(StartUpProcedure));//启动流程
            this.gameLauncher.RegisterProcedure(typeof(CheckVersionProcedure));//检测版本
            this.gameLauncher.RegisterProcedure(typeof(CheckPackageAssetsProcedure));//检测包内资源
            this.gameLauncher.RegisterProcedure(typeof(CheckHotfixProcedure));//检测热更新
            this.gameLauncher.RegisterProcedure(typeof(ResolveAssetsProcedure));//解析资源

            OnInit();
        }


        private void Start()
        {
            this.gameLauncher.LoadPanel(onLoadStartFunc, onLoadFinishFunc);
            this.gameLauncher.ExecuteProcedure(typeof(StartUpProcedure));

#if BYTE_GAME
            if (Application.isEditor)//开启调试
                StarkSDKSpace.MockSetting.OpenAllMockModule();
#endif
        }

        protected virtual void OnInit()
        {

        }

        private void Update()
        {
            this.gameLauncher.Update(Time.deltaTime);

            OnUpdate();
        }

        protected virtual void OnUpdate()
        {

        }

        //流程启动完成
        protected virtual void OnLaunchComplete()
        {

        }

        //切后台
        private void OnApplicationPause(bool _pause)
        {
            if (_pause)
            {
                this.startPasueTime = TimeUtility.NowMillisecond();
                this.onApplicationPause?.Invoke(_pause, 0);
            }
            else
            {
                if(this.startPasueTime > 0)
                {
                    long deltaTime = TimeUtility.NowMillisecond() - this.startPasueTime;

                    float duration = deltaTime / 1000.0f;
                    if (deltaTime < 0)
                    {
                        duration = 0;
                    }
                    else
                    {
                        if (GameConfig.Setting.RunInBackgroundMaxDuration > 0 && duration > GameConfig.Setting.RunInBackgroundMaxDuration)
                        {
                            this.onApplicationOvertime?.Invoke(duration);
                        }
                        else
                        {
                            this.onApplicationPause?.Invoke(_pause, duration);
                        }
                    }
                }
            }
        }

        public GameLauncher GetGameLauncher()
        {
            return gameLauncher;
        }
    }
}