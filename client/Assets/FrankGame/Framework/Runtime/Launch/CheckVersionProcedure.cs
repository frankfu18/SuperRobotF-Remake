using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrankGame
{
    /// <summary>
    /// 检测Version文件
    /// </summary>
    public class CheckVersionProcedure : GameLaunchProcedure
    {
        private string versionUrl;

        protected override void OnEnter(params object[] _params)
        {
            base.OnEnter(_params);

            versionUrl = GameConfig.Version.VersionInfo.VersionUrl;

            Log.Debug(ELogType.Net, "检测版本信息：" + versionUrl);

            SetProgress(1, LocalizationManager.Instance.GetPackageLanguageText("version_checking"));

            if (NetworkCheck.Instance.IsReachability)
            {
                StartCoroutine(DownloadUtil.DownloadFile(versionUrl, OnDownloadVersionSuccess, OnDownloadVersionFail));
            }
            else
            {
                OnDownloadVersionFail("netError", null);

                LancherPanel lancherPanel = GameEntry.Instance.GetGameLauncher().GetLancherPanel();
                if (lancherPanel != null)
                {
                    lancherPanel.ShowOneButton(LocalizationManager.Instance.GetPackageLanguageText("prompt_networkError"));
                }
            }
        }

        private void OnDownloadVersionSuccess(string _text, object _param)
        {
            Log.Debug(ELogType.Net, "检测版本成功：" + _text);

            VersionIndexInfo versionInfo = JsonUtility.FromJson<VersionIndexInfo>(_text);

            GameConfig.ServerListUrl = versionInfo.serverList_url;
            GameConfig.AnnouncementUrl = versionInfo.announcement_url;

            bool isContainClientVersion = false;
            ClientVersionInfo clientVersionInfo = null;
            if (GameConfig.Package.ResLoadMode == ELoadMode.MiniGame)
            {
                isContainClientVersion = versionInfo.GetNewestClientVersion(out clientVersionInfo);
            }
            else
            {
                isContainClientVersion = versionInfo.GetCurrentClientVersion(out clientVersionInfo);
            }

            if (isContainClientVersion)
            {
                GameConfig.ResourceUrl = clientVersionInfo.res_url;
                GameConfig.FilesUrl = clientVersionInfo.files_url;
                GameConfig.IsDebug = clientVersionInfo.debug;

                if (GameConfig.Package.ResLoadMode == ELoadMode.Net || GameConfig.Package.ResLoadMode == ELoadMode.MiniGame)
                {
                    ChangeProcedure(typeof(ResolveAssetsProcedure));
                }
                else
                {
                    ChangeProcedure(typeof(CheckPackageAssetsProcedure));
                }
            }
            else
            {
//#if UNITY_EDITOR
                //ChangeProcedure(typeof(CheckPackageAssetsProcedure));
//#else
                Log.Error(ELogType.Net, "找不到客户端版本：" + ClientVersionInfo.GetClientVersion());

                SetProgress(1, LocalizationManager.Instance.GetPackageLanguageText("version_needNewClient"));

                LancherPanel lancherPanel = GameEntry.Instance.GetGameLauncher().GetLancherPanel();
                if (lancherPanel != null)
                {
                    lancherPanel.ShowTwoButton(LocalizationManager.Instance.GetPackageLanguageText("version_needNewClient"), () => 
                    {
                        //todo 商店地址

                        Application.Quit();
                    });
                }
//#endif
            }
        }

        private void OnDownloadVersionFail(string _error, object _param)
        {
            Log.Error(ELogType.Net, versionUrl + " error: " + _error);

            SetProgress(1, LocalizationManager.Instance.GetPackageLanguageText("version_checkError"));
        }
    }
}