using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace FrankGame
{
    public class AssetManager : Singlet<AssetManager>
    {
        private const char Path_Separator = '/';

        public void LoadAsset<T>(string _assetPath, string _bundleName, System.Action<T> _onLoadDone) where T : Object
        {
            float delay = GameConfig.Debug.GetAsynIntervalTime();
            if (delay > 0)
            {
                GameEntry.Instance.StartCoroutine(IELoadAsset(delay, _assetPath, _bundleName, _onLoadDone));
            }
            else
            {
                OnLoadAsset(_assetPath, _bundleName, _onLoadDone);
            }
        }

        public void LoadSubAssets(string _assetPath, int _layer, System.Action<object[]> _onLoadDone)
        {
            float delay = GameConfig.Debug.GetAsynIntervalTime();
            if (delay > 0)
            {
                GameEntry.Instance.StartCoroutine(IELoadSubAssets(delay, _assetPath, _layer, _onLoadDone));
            }
            else
            {
                OnLoadSubAssets(_assetPath, _layer, _onLoadDone);
            }
        }

        public T LoadResources<T>(string _path) where T : Object
        {
            var t = Resources.Load<T>(_path);
            if (t == null)
            {
                Log.Error(ELogType.Asset, "Asset not find in Resources :" + _path);
            }

            return t;
        }

        public void UnloadAsset(string _assetPath, string _bundleName, int _unuseCount = 1)
        {
            if (GameConfig.Package.ResLoadMode == ELoadMode.Local)
            {
                return;
            }

            BundleManager.Instance.UnloadAsset(_bundleName, _assetPath, _unuseCount);
        }

        public void UnloadBundle(string _abName, bool _unloadDeps = false)
        {
            if (GameConfig.Package.ResLoadMode == ELoadMode.Local)
            {
                return;
            }

            BundleManager.Instance.UnloadBundle(_abName, _unloadDeps);
        }

        public void UnloadAllUnuse()
        {
            if (GameConfig.Package.ResLoadMode == ELoadMode.Local)
            {
                return;
            }

            Resources.UnloadUnusedAssets();

            BundleManager.Instance.UnloadAllBundles();
        }

        private void OnLoadAsset<T>(string _assetPath, string _bundleName, System.Action<T> _onLoadDone) where T : Object
        {
            if (GameConfig.Package.ResLoadMode == ELoadMode.Local)
            {
#if UNITY_EDITOR
                var asset = AssetDatabase.LoadAssetAtPath<T>(_assetPath);
                if (asset == null)
                {
                    Log.Error(ELogType.Asset, "Asset not find:" + _assetPath);
                }

                _onLoadDone.Invoke(asset);
#endif
            }
            else
            {
                string assetName = _assetPath.ToLower();

                BundleManager.Instance.LoadAsset(_bundleName, assetName, _onLoadDone);
            }
        }

        private void OnLoadSubAssets(string _assetPath, int _layer, System.Action<object[]> _onLoadDone)
        {
            if (GameConfig.Package.ResLoadMode == ELoadMode.Local)
            {
#if UNITY_EDITOR
                var asset = AssetDatabase.LoadAllAssetRepresentationsAtPath(_assetPath);
                if (asset == null)
                {
                    Log.Error(ELogType.Asset, "Asset not find:" + _assetPath);
                }

                _onLoadDone.Invoke(asset);
#endif
            }
            else
            {
                string bundleName = _assetPath.TrimEnd(Path_Separator);
                if (_layer <= 0)
                {
                    bundleName = bundleName.ToLower() + PathConfig.Instance.AssetBundleSuffix;
                }
                else
                {
                    string[] splits = _assetPath.Split(Path_Separator);

                    if (splits.Length == 0)
                    {
                        bundleName += Path_Separator + _assetPath;
                    }
                    else
                    {
                        for (int i = 0; i < _layer; i++)
                        {
                            if (i < splits.Length)
                            {
                                bundleName += Path_Separator + splits[i];
                            }
                        }
                    }

                    bundleName = bundleName.ToLower() + PathConfig.Instance.AssetBundleSuffix;
                }

                string assetName = _assetPath.ToLower();

                BundleManager.Instance.LoadSubAssets(bundleName, assetName, _onLoadDone);
            }
        }

        private IEnumerator IELoadAsset<T>(float _delay, string _assetPath, string _bundleName, System.Action<T> _onLoadDone) where T : Object
        {
            yield return new WaitForSecondsRealtime(_delay);

            OnLoadAsset(_assetPath, _bundleName, _onLoadDone);
        }

        private IEnumerator IELoadSubAssets(float _delay, string _assetPath, int _layer, System.Action<object[]> _onLoadDone)
        {
            yield return new WaitForSecondsRealtime(_delay);

            OnLoadSubAssets(_assetPath, _layer, _onLoadDone);
        }
    }
}