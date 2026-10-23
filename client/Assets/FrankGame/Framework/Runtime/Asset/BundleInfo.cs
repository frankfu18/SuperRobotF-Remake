using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
#if WX_GAME
using WeChatWASM;
#endif

namespace FrankGame
{
    public class BundleInfo
    {
        public int RefCount
        {
            get
            {
                return refCount;
            }
        }

        public bool IsDirty
        {
            get
            {
                return dirty;
            }
        }

        public string bundleName;
        public string hash;
        public string localPath;
        public string prefixPath;
        public List<string> dependNameList = new List<string>();//存的hash
        public Dictionary<string, AssetInfo> assetInfoDic = new Dictionary<string, AssetInfo>();
        public ELoadState loadState;
        public AssetBundle assetBundle;

        private Queue<Action<AssetBundle>> cacheBundleQueue = new Queue<Action<AssetBundle>>();
        private bool dirty;
        private int refCount;

        public BundleInfo(string _bundleName, string _hash, string _localPath, List<string> _dependencies, bool _isConfig)
        {
            this.bundleName = _bundleName;
            this.hash = _hash;

            if (_dependencies != null)
            {
                this.dependNameList.AddRange(_dependencies);
            }

            this.loadState = ELoadState.NotLoad;
            this.prefixPath = _isConfig? PathUtil.GetConfigPrefixPath() : PathUtil.GetResPrefixPath();
            this.localPath = (_isConfig ? PathUtil.GetLocalConfigPath() : PathUtil.GetLocalResPath()) + _localPath;
            this.dirty = false;
            this.refCount = 0;
        }

        public bool IsHasLoad()
        {
            return this.loadState == ELoadState.Loading || this.loadState == ELoadState.Downloading || this.loadState == ELoadState.LoadDone;
        }

        public bool IsDownloading()
        {
            return this.loadState == ELoadState.Downloading;
        }

        public bool IsLoadDone()
        {
            return this.loadState == ELoadState.LoadDone;
        }

        public void GetAssetBundle(Action<AssetBundle> _onLoadDone)
        {
            if (IsLoadDone())
            {
                _onLoadDone?.Invoke(this.assetBundle);
            }
            else
            {
                if (_onLoadDone != null)
                {
                    cacheBundleQueue.Enqueue(_onLoadDone);
                }
            }
        }

        public void LoadBundle()
        {
            if (this.loadState == ELoadState.Loading)
            {
                return;
            }

            this.loadState = ELoadState.Loading;
        }

        public void Downloading()
        {
            if (this.loadState != ELoadState.Loading)
            {
                return;
            }

            this.loadState = ELoadState.Downloading;
        }

        public void SetBundle(AssetBundle _assetBundle)
        {
            if (this.loadState != ELoadState.Loading && this.loadState != ELoadState.Downloading)
            {
                return;
            }

            Log.Debug(ELogType.Asset, "Bundle load done:" + this.bundleName);

            assetBundle = _assetBundle;
            this.loadState = ELoadState.LoadDone;

            this.dirty = false;

            string[] assetNames = assetBundle.GetAllAssetNames();

            assetInfoDic.Clear();
            foreach (string assetName in assetNames)
            {
                AssetInfo assetInfo = new AssetInfo(assetName, this);

                assetInfoDic.Add(assetName, assetInfo);
            }

            while (cacheBundleQueue.Count > 0)
            {
                cacheBundleQueue.Dequeue()?.Invoke(_assetBundle);
            }
        }

        public void LoadAsset<T>(string _assetName, Action<T> _onLoadDone) where T : UnityEngine.Object
        {
            GetAndLoadBundle((bundle) =>
            {
                if (assetInfoDic.TryGetValue(_assetName, out AssetInfo assetInfo))
                {
                    assetInfo.LoadAsset(false);
                    assetInfo.GetAsset<T>((asset) =>
                    {
                        _onLoadDone?.Invoke(asset);
                    });
                }
                else
                {
                    Log.Error(ELogType.Asset, string.Format("{0} is not find in {1} ", _assetName, this.bundleName));
                }
            });
        }

        public void LoadAsset(string _assetName, Action<object> _onLoadDone)
        {
            GetAndLoadBundle((bundle) =>
            {
                if (assetInfoDic.TryGetValue(_assetName, out AssetInfo assetInfo))
                {
                    assetInfo.LoadAsset(false);
                    assetInfo.GetAsset((asset) =>
                    {
                        _onLoadDone?.Invoke(asset);
                    });
                }
                else
                {
                    Log.Error(ELogType.Asset, "{{0} is not find in {1}", _assetName, this.bundleName);
                }
            });
        }

        public void LoadSubAssets(string _assetName, Action<object[]> _onLoadDone)
        {
            GetAndLoadBundle((bundle) =>
            {
                if (assetInfoDic.TryGetValue(_assetName, out AssetInfo assetInfo))
                {
                    assetInfo.LoadAsset(true);
                    assetInfo.GetAssets((assets) =>
                    {
                        _onLoadDone?.Invoke(assets);
                    });
                }
                else
                {
                    Log.Error(ELogType.Asset, "{0} 资源未在 {1} 中找到", _assetName, this.bundleName);
                }
            });
        }

        private void GetAndLoadBundle(Action<AssetBundle> _onLoadDone)
        {
            if (this.loadState == ELoadState.NotLoad)
            {
                BundleManager.Instance.LoadBundleByHash(this.hash, null);
            }
            GetAssetBundle(_onLoadDone);
        }

        public void GetAllAssets<T>(Action<Dictionary<string , T>> _onGetAsset) where T : UnityEngine.Object
        {
            GetAndLoadBundle((bundle) =>
            {
                Dictionary<string, T> tempAssetDic = new Dictionary<string, T>();
                int curCount = 0;
                int totalCount = assetInfoDic.Count;

                if (totalCount > 0)
                {
                    List<AssetInfo> assetInfos = new List<AssetInfo>(assetInfoDic.Values);
                    foreach (var assetInfo in assetInfos)
                    {
                        assetInfo.LoadAsset(false);
                        assetInfo.GetAsset<T>((asset) =>
                        {
                            tempAssetDic.Add(assetInfo.assetName, asset);

                            curCount++;
                            if (curCount == totalCount)
                            {
                                _onGetAsset?.Invoke(tempAssetDic);
                            }
                        });
                    }
                }
                else
                {
                    _onGetAsset?.Invoke(tempAssetDic);
                }
            });
        }

        public void AddRef()
        {
            refCount++;

            //Log.Debug(ELogType.Asset, "Bundle引用： " + bundleName + ", 引用次数：" + refCount);
        }

        public void RemoveRef(int count = 1)
        {
            if (refCount <= 0)
            {
                Log.Error(ELogType.Asset, "Refrence count error:" + bundleName);
                return;
            }

            refCount -= count;

            //Log.Debug(ELogType.Asset, "Bundle释放： " + bundleName + ", 剩余引用：" + refCount);

            if (refCount == 0)
            {
                dirty = true;
            }
        }

        public void UnloadAsset(string _assetName, int _unuseCount = 1)
        {
            if (assetInfoDic.TryGetValue(_assetName, out AssetInfo assetInfo))
            {
                assetInfo.Unuse(_unuseCount);

                RemoveRef(_unuseCount);
            }
        }

        public void Dispose(bool _force = false, bool _unloadDeps = false)
        {
            if (!_force && !this.dirty)
            {
                return;
            }

            refCount = 0;

            foreach (var assetInfo in assetInfoDic.Values)
            {
                assetInfo.Dispose(_force);
            }

            if (assetBundle != null)
            {
#if WX_GAME && !UNITY_EDITOR
                assetBundle.WXUnload(_force);
#else
                assetBundle.Unload(_force);
#endif
                assetBundle = null;
            }

            loadState = ELoadState.NotLoad;

            for (int i = 0; i < dependNameList.Count; i++)
            {
                string dep = dependNameList[i];
                bool depDirty = BundleManager.Instance.RemoveRefByName(dep);

                if (_unloadDeps || depDirty)
                {
                    BundleManager.Instance.UnloadBundle(dep, _unloadDeps);
                }
            }

            this.dirty = false;

            Log.Debug(ELogType.Asset, "Bundle dispose:" + this.bundleName);
        }
    }
}