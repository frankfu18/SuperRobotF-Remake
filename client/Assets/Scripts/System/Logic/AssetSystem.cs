using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FrankGame;

public class AssetSystem : SystemBase<AssetSystem>
{

    protected override void OnInit()
    {

    }

    protected override void OnInitDone()
    {
        EventNativeSystem.Instance.SendEvent(SysEvent.Asset_Load_Done);
    }

    public void LoadSound(string _name, System.Action<AudioClip> _onLoadDone)
    {
        LoadAsset<AudioClip>(EResType.Audio, _name, ResConfig.Audio_Suffix, (asset) =>
        {
            _onLoadDone?.Invoke(asset);
        });
    }

    public void UnloadSound(string _name, int _unuseCount = 1, bool _unloadBundle = false)
    {
        UnloadAsset(EResType.Audio, _name, ResConfig.Audio_Suffix, _unuseCount, _unloadBundle);
    }

    public void LoadAsset<T>(EResType _resType, string _assetPath, string _suffix, System.Action<T> _onLoadDone) where T : Object
    {
        var resInfo = ResConfig.ResInfoDic[_resType];

        string bundleName = resInfo.path.TrimEnd('/').ToLower();

        if (resInfo.layer <= 0)
        {
            bundleName = bundleName + ".ab";
        }
        else
        {
            string[] splits = _assetPath.Split('/');

            if (splits.Length == 0)
            {
                bundleName += "/" + _assetPath;
            }
            else
            {
                for (int i = 0; i < resInfo.layer; i++)
                {
                    if (i < splits.Length)
                    {
                        bundleName += "/" + splits[i];
                    }
                }
            }

            bundleName = bundleName.ToLower() + ".ab";
        }

        string path = GameConfig.Path.LocalResRoot + resInfo.path + _assetPath + _suffix;

        AssetManager.Instance.LoadAsset(path.ToLower(), bundleName, _onLoadDone);
    }

    public void LoadSubAssets(EResType _resType, string _assetPath, string _suffix, System.Action<object[]> _onLoadDone)
    {
        var resInfo = ResConfig.ResInfoDic[_resType];
        string path = GameConfig.Path.LocalResRoot + resInfo.path + _assetPath + _suffix;

        AssetManager.Instance.LoadSubAssets(path.ToLower(), resInfo.layer, _onLoadDone);
    }

    public T LoadResources<T>(string _path) where T : Object
    {
        return AssetManager.Instance.LoadResources<T>(_path);
    }

    public void UnloadAsset(EResType _resType, string _assetPath, string _suffix, int _unuseCount = 1, bool _unloadBundle = false)
    {
        if (GameConfig.Package.ResLoadMode == FrankGame.ELoadMode.Local)
        {
            return;
        }

        var resInfo = ResConfig.ResInfoDic[_resType];

        string bundleName = resInfo.path.TrimEnd('/').ToLower();

        if (resInfo.layer <= 0)
        {
            bundleName = bundleName + ".ab";
        }
        else
        {
            string[] splits = _assetPath.Split('/');

            if (splits.Length == 0)
            {
                bundleName += "/" + _assetPath;
            }
            else
            {
                for (int i = 0; i < resInfo.layer; i++)
                {
                    if (i < splits.Length)
                    {
                        bundleName += "/" + splits[i];
                    }
                }
            }

            bundleName = bundleName.ToLower() + ".ab";
        }

        string path = GameConfig.Path.LocalResRoot + resInfo.path + _assetPath + _suffix;

        AssetManager.Instance.UnloadAsset(path.ToLower(), bundleName, _unuseCount);

        if (_unloadBundle)
        {
            AssetManager.Instance.UnloadBundle(bundleName, true);
        }
    }

    public void UnloadAllUnuse()
    {
        if (GameConfig.Package.ResLoadMode == FrankGame.ELoadMode.Local)
        {
            return;
        }

        AssetManager.Instance.UnloadAllUnuse();
    }
}
