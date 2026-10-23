using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrankGame
{
    public static class BundleLoader
    {
        public static void LoadBundle(BundleInfo _bundleInfo, System.Action<AssetBundle> _onLoadDone, System.Action _onLoadFail = null)
        {
            if (GameConfig.Package.ResLoadMode == ELoadMode.Net || GameConfig.Package.ResLoadMode == ELoadMode.MiniGame)
            {
                if (_bundleInfo.IsDownloading())
                {
                    _bundleInfo.GetAssetBundle(_onLoadDone);
                }
                else
                {
                    _bundleInfo.Downloading();

                    DownloadLoader.Instance.DownloadAssetBundle(GameConfig.ResourceUrl + _bundleInfo.prefixPath, _bundleInfo.hash, (assetBundle, param) =>
                    {
                        _onLoadDone?.Invoke(assetBundle);
                    }, null);
                }
            }
            else
            {
                AssetBundle assetBundle = AssetBundle.LoadFromFile(_bundleInfo.localPath, 0, GameConfig.Package.AssetBundleOffset);
                if (assetBundle != null)
                {
                    _onLoadDone?.Invoke(assetBundle);
                }
                else
                {
                    _onLoadFail?.Invoke();
                }
            }
        }
    }
}
