using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrankGame
{
    public class StartUpProcedure : GameLaunchProcedure
    {
        protected override void OnEnter(params object[] _params)
        {
            base.OnEnter(_params);

            if (GameConfig.Localization.FollowSystemLanguage)
            {
                LocalizationManager.Instance.CheckLanguage(Application.systemLanguage);
            }
            else
            {
                LocalizationManager.Instance.CheckLanguage(GameConfig.Localization.DefaultLanguage);
            }


            this.launcher.GameEntry.gameObject.AddComponent<DownloadLoader>();

            if (GameConfig.HotUpdate.CheckVersion)
            {
                ChangeProcedure(typeof(CheckVersionProcedure));
            }
            else
            {
                GameConfig.Debug.SetLocalDebug(GameConfig.IsDebug);

                if (GameConfig.Package.ResLoadMode == ELoadMode.Net || GameConfig.Package.ResLoadMode == ELoadMode.MiniGame)
                {
                    ChangeProcedure(typeof(ResolveAssetsProcedure));
                }
                else
                {
                    ChangeProcedure(typeof(CheckPackageAssetsProcedure));
                }
            }
        }
    }
}