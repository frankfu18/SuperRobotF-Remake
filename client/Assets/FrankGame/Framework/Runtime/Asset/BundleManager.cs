using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace FrankGame
{
    public class BundleManager : Singlet<BundleManager>
    {
        public Dictionary<string, BundleInfo> BundleNameDic
        {
            get { return bundleNameDic; }
        }

        private Dictionary<string, BundleInfo> bundleNameDic = new Dictionary<string, BundleInfo>();
        private Dictionary<string, BundleInfo> bundleHashDic = new Dictionary<string, BundleInfo>();
        private List<BundleInfo> configBundleList = new List<BundleInfo>();
        private Dictionary<string, byte[]> configDataDic = new Dictionary<string, byte[]>();
        private Dictionary<string, string> dllNameHashDic = new Dictionary<string, string>();

        public void ResolveDependInfoList(ResIndexInfo _resIndexInfo)
        {
            bundleNameDic.Clear();
            bundleHashDic.Clear();
            configBundleList.Clear();
            dllNameHashDic.Clear();

            foreach (ResUpdateInfo resUpdateInfo in _resIndexInfo.configList)
            {
                BundleInfo bundleInfo = new BundleInfo(resUpdateInfo.name, resUpdateInfo.hash, resUpdateInfo.localPath, resUpdateInfo.dependList, true);

                bundleNameDic.Add(bundleInfo.bundleName, bundleInfo);
                bundleHashDic.Add(bundleInfo.hash, bundleInfo);

                configBundleList.Add(bundleInfo);
            }

            foreach (ResUpdateInfo resUpdateInfo in _resIndexInfo.resList)
            {
                BundleInfo bundleInfo = new BundleInfo(resUpdateInfo.name, resUpdateInfo.hash,  resUpdateInfo.localPath, resUpdateInfo.dependList, false);

                bundleNameDic.Add(bundleInfo.bundleName, bundleInfo);
                bundleHashDic.Add(bundleInfo.hash, bundleInfo);
            }

            foreach (ResUpdateInfo resUpdateInfo in _resIndexInfo.hotUpdateDllList)
            {
                dllNameHashDic.Add(resUpdateInfo.name.Replace(".dll", ""), resUpdateInfo.hash);
            }
        }

        public void LoadTotalConfig(Action<Dictionary<string, byte[]>> _onLoadDone)
        {
            configDataDic.Clear();

            int curCount = 0;
            int totalCount = configBundleList.Count;

            if (totalCount > 0)
            {
                for (int i = 0; i < configBundleList.Count; i++)
                {
                    BundleInfo bundleInfo = configBundleList[i];
                    bundleInfo.GetAllAssets<TextAsset>((assetDic) =>
                    {
                        foreach (var dic in assetDic)
                        {
                            string cfgName = dic.Key.Substring(dic.Key.LastIndexOf('/') + 1);
                            configDataDic.Add(cfgName, dic.Value.bytes);
                        }

                        bundleInfo.Dispose(true);//取出完强制释放掉

                        curCount++;
                        if (curCount == totalCount)
                        {
                            _onLoadDone?.Invoke(configDataDic);

                            //configDataDic.Clear();//取出完清掉
                        }
                    });
                }
            }
            else
            {
                _onLoadDone?.Invoke(configDataDic);
            }
        }

        public string GetDllHash(string _dllName)
        {
            if (dllNameHashDic.TryGetValue(_dllName, out string hash))
            {
                return hash;
            }

            return null;
        }

        public Dictionary<string, string> GetDllNameHashDic()
        {
            return dllNameHashDic;
        }

        public byte[] GetConfigData(string _configName)
        {
            if (configDataDic.TryGetValue(_configName, out byte[] datas))
            {
                return datas;
            }

            return null;
        }

        public void LoadBundleByName(string _bundleName, Action<BundleInfo> _onLoadDone)
        {
            BundleInfo bundleInfo;
            if (!bundleNameDic.TryGetValue(_bundleName, out bundleInfo))
            {
                Log.Error(ELogType.Asset, "Bundle name not find:" + _bundleName);
                return;
            }

            LoadBundleInfo(bundleInfo, _onLoadDone, true);
        }

        public void LoadBundleByHash(string _hash, Action<BundleInfo> _onLoadDone)
        {
            BundleInfo bundleInfo;
            if (!bundleHashDic.TryGetValue(_hash, out bundleInfo))
            {
                Log.Error(ELogType.Asset, "Bundle hash not find:" + _hash);
                return;
            }

            LoadBundleInfo(bundleInfo, _onLoadDone, false);
        }

        private void LoadBundleInfo(BundleInfo _bundleInfo, Action<BundleInfo> _onLoadDone, bool _isDep)
        {
            if (_bundleInfo.IsHasLoad())
            {
                _bundleInfo.AddRef();
                _bundleInfo.GetAssetBundle((assetBundle) => 
                {
                    _onLoadDone?.Invoke(_bundleInfo);
                });
                return;
            }

            int totalDependCount = _bundleInfo.dependNameList.Count;
            int curDependCount = 0;

            //加载依赖
            if (totalDependCount > 0)
            {
                foreach (var hash in _bundleInfo.dependNameList)
                {
                    LoadBundleByName(hash, (bundleInfo) => 
                    {
                        curDependCount++;
                        if (curDependCount == totalDependCount)
                        {
                            if (_bundleInfo.IsHasLoad())
                            {
                                _bundleInfo.GetAssetBundle((assetBundle) =>
                                {
                                    _onLoadDone?.Invoke(_bundleInfo);
                                });
                            }
                            else
                            {
                                LoadAssetBundle(_bundleInfo, _onLoadDone, _isDep);
                            }
                        }
                    });
                }
            }
            else
            {
                LoadAssetBundle(_bundleInfo, _onLoadDone, _isDep);
            }
        }

        private void LoadAssetBundle(BundleInfo _bundleInfo, Action<BundleInfo> _onLoadDone, bool _isDep)
        {
            if (_isDep)
            {
                _bundleInfo.AddRef();
            }
            
            _bundleInfo.LoadBundle();

            BundleLoader.LoadBundle(_bundleInfo, (assetBundle) =>
            {
                _bundleInfo.SetBundle(assetBundle);

                _onLoadDone?.Invoke(_bundleInfo);
            }, 
            () => 
            {
                Log.Error(ELogType.Asset, "AssetBundle load fail:" + _bundleInfo.bundleName);
            });
        }

        public bool RemoveRefByName(string _abName)
        {
            if (bundleNameDic.TryGetValue(_abName, out BundleInfo bundleInfo))
            {
                bundleInfo.RemoveRef();

                return bundleInfo.IsDirty;
            }

            return false;
        }

        //用包名和资源名进行加载
        public void LoadAsset<T>(string _abName, string _assetName, System.Action<T> _onLoadDone) where T : UnityEngine.Object
        {
            BundleInfo bundleInfo;
            if (!bundleNameDic.TryGetValue(_abName, out bundleInfo))
            {
                Log.Error(ELogType.Asset, "Asset not find: abName = " + _abName + ", assetName = " + _assetName);
                return;
            }

            bundleInfo?.LoadAsset(_assetName, _onLoadDone);
        }

        //用包名和资源名进行加载
        public void LoadSubAssets(string _abName, string _assetName, System.Action<object[]> _onLoadDone)
        {
            BundleInfo bundleInfo;
            if (!bundleNameDic.TryGetValue(_abName, out bundleInfo))
            {
                Log.Error(ELogType.Asset, "Asset not find: abName = " + _abName + ", assetName = " + _assetName);
                return;
            }

            bundleInfo?.LoadSubAssets(_assetName, _onLoadDone);
        }

        public void UnloadAsset(string _abName, string _assetName, int _unuseCount = 1)
        {
            BundleInfo bundleInfo;
            if (!bundleNameDic.TryGetValue(_abName, out bundleInfo))
            {
                Log.Error(ELogType.Asset, "Asset not find: abName = " + _abName + ", assetName = " + _assetName);
                return;
            }

            bundleInfo.UnloadAsset(_assetName, _unuseCount);
        }

        public void UnloadBundle(string _abName, bool _unloadDeps = false)
        {
            BundleInfo bundleInfo;
            if (!bundleNameDic.TryGetValue(_abName, out bundleInfo))
            {
                Log.Error(ELogType.Asset, "Asset not find: abName = " + _abName);
                return;
            }

            bundleInfo.Dispose(false, _unloadDeps);
        }

        public void UnloadAllBundles()
        {
            foreach (var bundleInfo in bundleNameDic.Values)
            {
                bundleInfo.Dispose();
            }
        }
    }
}