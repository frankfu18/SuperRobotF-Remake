using editor.cfg.battle;
using UnityEngine;

namespace FrankGame.Battle
{
    /// <summary>
    /// 管理战斗实体资源的异步加载、显隐与对象池归还。
    /// 所有加载完成的战斗资源实例统一挂到当前 World 的 MapRoot 下。
    /// </summary>
    public class AssetComponent : ComponentBase
    {
        private enum ELoadState
        {
            Unload,
            Loading,
            Loaded,
        }

        public override ECompType ComponentType => ECompType.Asset;
        public bool active = true;
        public string AssetId => assetId;

        private string assetId;
        private BattleAsset battleAssetConfig;
        private GameObject insGo;
        private ELoadState loadState = ELoadState.Unload;
        private System.Action<GameObject> onLoadDone;
        private int loadVersion;

        public AssetComponent(Entity entity) : base(entity) { }

        /// <summary>切换资源时先释放旧实例，并用版本号拒绝已经过期的异步回调。</summary>
        public AssetComponent SetAsset(string newAssetId)
        {
            if (assetId == newAssetId && loadState != ELoadState.Unload)
            {
                return this;
            }

            ReleaseAsset();
            loadVersion++;
            assetId = newAssetId;

            if (string.IsNullOrWhiteSpace(assetId) || assetId == "null")
            {
                return this;
            }

            battleAssetConfig = ConfigSystem.CurrentData.GetBattleAsset(assetId);
            if (battleAssetConfig == null)
            {
                BattleLog.Error("战斗资源配置不存在，assetId:" + assetId);
                return this;
            }

            LoadAsset();
            return this;
        }

        private void LoadAsset()
        {
            if (loadState != ELoadState.Unload || battleAssetConfig == null)
            {
                return;
            }

            loadState = ELoadState.Loading;
            int requestVersion = loadVersion;
            string requestAssetId = assetId;

            if (battleAssetConfig.assetType == EBattleAssetType.Effect)
            {
                if (battleAssetConfig.assetInfo is EffectAssetInfo effectInfo
                    && entity.TryGetComponent(ECompType.Effect, out EffectComponent effectCom))
                {
                    effectCom.SetDuration(effectInfo.duration, effectInfo.delay);
                }

                BattleAssetManager.Instance.LoadEffect(assetId,
                    go => OnLoadDone(go, requestVersion, requestAssetId));
            }
            else
            {
                BattleAssetManager.Instance.LoadGameObject(assetId,
                    go => OnLoadDone(go, requestVersion, requestAssetId));
            }
        }

        private void OnLoadDone(GameObject go, int requestVersion, string requestAssetId)
        {
            if (requestVersion != loadVersion || IsDirty())
            {
                if (go != null)
                {
                    BattleAssetManager.Instance.Unuse(requestAssetId, go);
                }
                return;
            }

            insGo = go;
            if (insGo == null)
            {
                loadState = ELoadState.Unload;
                System.Action<GameObject> failedCallback = onLoadDone;
                onLoadDone = null;
                failedCallback?.Invoke(null);
                return;
            }

            // 资源根统一归属 MapRoot，避免对象池实例散落在 BattleNode 或场景根节点。
            if (world.mapRoot != null)
            {
                insGo.transform.SetParent(world.mapRoot.transform, false);
            }
            else
            {
                BattleLog.Error("战斗资源加载完成时 MapRoot 为空，assetId:" + assetId);
            }

            insGo.SetActive(active);
            loadState = ELoadState.Loaded;
            System.Action<GameObject> callback = onLoadDone;
            onLoadDone = null;
            callback?.Invoke(insGo);
        }

        /// <summary>加载中会登记回调，已加载则同步返回当前实例。</summary>
        public void GetAsset(System.Action<GameObject> onGet)
        {
            if (battleAssetConfig == null)
            {
                onGet?.Invoke(null);
                return;
            }

            if (loadState == ELoadState.Loaded)
            {
                onGet?.Invoke(insGo);
                return;
            }

            onLoadDone += onGet;
            if (loadState == ELoadState.Unload)
            {
                LoadAsset();
            }
        }

        public void SetActive(bool value)
        {
            if (active == value)
            {
                return;
            }
            active = value;
            if (insGo != null)
            {
                insGo.SetActive(value);
            }
        }

        /// <summary>
        /// 同步读取已经完成加载的资源实例。逐帧表现更新应使用该接口，
        /// 只有真正等待异步加载结果时才登记GetAsset回调。
        /// </summary>
        public bool TryGetLoadedAsset(out GameObject asset)
        {
            asset = loadState == ELoadState.Loaded ? insGo : null;
            return asset != null;
        }

        public bool IsLoaded()
        {
            return loadState == ELoadState.Loaded;
        }
        public bool HasAssetRequest()
        {
            return loadState != ELoadState.Unload;
        }

        /// <summary>先归还模型并清空 SceneItem，再归还固定场景根；重复调用安全。</summary>
        public void ReleaseAsset()
        {
            loadVersion++;
            onLoadDone = null;
            
            if (insGo != null)
            {
                BattleAssetManager.Instance.Unuse(assetId, insGo);
                insGo = null;
            }

            loadState = ELoadState.Unload;
            battleAssetConfig = null;
        }

        protected override void OnDestroy()
        {
            ReleaseAsset();
        }

        public override void OnRecover()
        {
            assetId = null;
            active = true;
            battleAssetConfig = null;
            insGo = null;
            loadState = ELoadState.Unload;
            onLoadDone = null;
        }

        protected override string GetString()
        {
            return $"assetId:{assetId},loadState:{loadState},active:{active}";
        }
    }
}
