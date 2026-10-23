using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrankGame
{
    public class AssetInfo
    {
        public int RefCount
        {
            get
            {
                return refCount;
            }
        }

        public string assetName;
        private BundleInfo bundleInfo;
        private int refCount;
        private ELoadState loadState;
        private object[] assets;
        private Dictionary<string, object> assetsDic = new Dictionary<string, object>();
        private object asset;
        private Queue<System.Action<object>> cacheAssetQueue;

        public AssetInfo(string _assetName, BundleInfo _bundleInfo)
        {
            this.assetName = _assetName;
            this.bundleInfo = _bundleInfo;
            this.loadState = ELoadState.NotLoad;
            this.cacheAssetQueue = new Queue<System.Action<object>>();
        }

        public void LoadAsset(bool _subAssets)
        {
            if (loadState != ELoadState.NotLoad)
            {
                return;
            }

            loadState = ELoadState.Loading;
            bundleInfo.GetAssetBundle((assetBundle) =>
            {
                assets = assetBundle.LoadAssetWithSubAssets(assetName);
                asset = assetBundle.LoadAsset(assetName);

                loadState = ELoadState.LoadDone;

                while (cacheAssetQueue.Count > 0)
                {
                    if (_subAssets)
                    {
                        cacheAssetQueue.Dequeue()(assets);
                    }
                    else
                    {
                        cacheAssetQueue.Dequeue()(asset);
                    }
                }
            });
        }

        public bool IsLoadDone()
        {
            return this.loadState == ELoadState.LoadDone;
        }

        public void GetAsset<T>(System.Action<T> _onGetAsset) where T : Object
        {
            if (loadState == ELoadState.LoadDone)
            {
                if (_onGetAsset != null)
                {
                    AddRef();

                    if(asset == null)
                    {
                        Log.Error(ELogType.Asset, "Asset is not find: " + assetName + ", type: " + typeof(T).FullName);
                        _onGetAsset(null);
                    }
                    else
                    {
                        if (asset is T)
                        {
                            _onGetAsset((T)asset);
                        }
                        else
                        {
                            Object mainAsset = (Object)asset;
                            foreach (var a in assets)
                            {
                                if (a is T)
                                {
                                    T t = (T)a;
                                    if (t.name == mainAsset.name)
                                    {
                                        _onGetAsset(t);
                                        return;
                                    }
                                }
                            }

                            _onGetAsset(null);
                        }
                    }
                    
                }
            }
            else
            {
                if (_onGetAsset != null)
                {
                    cacheAssetQueue.Enqueue((obj) =>
                    {
                        AddRef();

                        _onGetAsset((T)obj);
                    });
                }
            }
        }

        public void GetAsset(System.Action<object> _onGetAsset)
        {
            if (loadState == ELoadState.LoadDone)
            {
                if (_onGetAsset != null)
                {
                    AddRef();

                    _onGetAsset(asset);
                }
            }
            else
            {
                if (_onGetAsset != null)
                {
                    cacheAssetQueue.Enqueue((obj) =>
                    {
                        AddRef();

                        _onGetAsset(obj);
                    });
                }
            }
        }

        public void GetAssets(System.Action<object[]> _onGetAsset)
        {
            if (loadState == ELoadState.LoadDone)
            {
                if (_onGetAsset != null)
                {
                    AddRef();

                    _onGetAsset(assets);
                }
            }
            else
            {
                if (_onGetAsset != null)
                {
                    cacheAssetQueue.Enqueue((obj) =>
                    {
                        AddRef();

                        _onGetAsset(obj as Object[]);
                    });
                }
            }
        }

        private void AddRef()
        {
            refCount++;

            bundleInfo.AddRef();

            //Log.Debug(ELogType.Asset, "Asset引用： " + assetName + ", 引用次数：" + refCount);
        }

        public void Unuse(int _unuseCount)
        {
            if (refCount >= _unuseCount)
            {
                refCount-= _unuseCount;
            }
            else
            {
                refCount = 0;
            }

            //Log.Debug(ELogType.Asset, "Asset释放： " + assetName + ", 释放次数：" + _unuseCount + ", 剩余引用：" + refCount);
        }

        public void Dispose(bool _force = false)
        {
            if (this.loadState == ELoadState.NotLoad)
            {
                return;
            }

            if (!_force && refCount > 0)
            {
                Log.Error(ELogType.Asset, "Refrence count is not zero：" + this.assetName);
            }

            refCount = 0;

            asset = null;
            assets = null;

            assetsDic.Clear();

            cacheAssetQueue.Clear();

            loadState = ELoadState.NotLoad;
        }
    }
}