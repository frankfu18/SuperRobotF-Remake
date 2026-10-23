using editor.cfg.battle;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrankGame.Battle
{
    public class BattleAssetManager : Singlet<BattleAssetManager>
    {
        public void LoadAsset<T>(string _assetId, System.Action<T> _onLoadDone) where T : Object
        {
            if (!IsAssetValid(_assetId, out BattleAsset battleAssetConfig))
            {
                return;
            }

            switch (battleAssetConfig.assetType)
            {
                case EBattleAssetType.Prefab:
                    LoadGameObject(_assetId, (go) =>
                    {
                        _onLoadDone?.Invoke(go as T);
                    });
                    break;
                case EBattleAssetType.Sprite:
                    LoadSprite(_assetId, (sprite) => 
                    {
                        _onLoadDone?.Invoke(sprite as T);
                    });
                    break;
                case EBattleAssetType.Spine:
                    LoadSpine(_assetId, (spine) =>
                    {
                        _onLoadDone?.Invoke(spine as T);
                    });
                    break;
                default:
                    BattleLog.Error("EBattleAssetType error :" + battleAssetConfig.assetType);
                    return;
            }
        }

        public void LoadGameObject(string _assetId, System.Action<GameObject> _onLoadDone)
        {
            if(!IsAssetValid(_assetId, out BattleAsset battleAssetConfig))
            {
                return;
            }

            BattleGameObjectPool.Get(battleAssetConfig.path, (go) =>
            {
                _onLoadDone?.Invoke(go);
            });
        }

        public void LoadSpine(string _assetId, System.Action<SkeletonDataAsset> _onLoadDone)
        {
            if (!IsAssetValid(_assetId, out BattleAsset battleAssetConfig))
            {
                return;
            }

            AssetHandler.Instance.LoadBattleSpine(battleAssetConfig.path, (spine) =>
            {
                _onLoadDone?.Invoke(spine);
            });
        }

        public void LoadSprite(string _assetId, System.Action<Sprite> _onLoadDone)
        {
            if (!IsAssetValid(_assetId, out BattleAsset battleAssetConfig))
            {
                return;
            }

            AssetHandler.Instance.LoadBattleSprite(battleAssetConfig.path, (sprite) =>
            {
                _onLoadDone?.Invoke(sprite);
            });
        }

        public void LoadEffect(string _assetId, System.Action<GameObject> _onLoadDone)
        {
            if (!IsAssetValid(_assetId, out BattleAsset battleAssetConfig))
            {
                return;
            }

            BattleGameObjectPool.Get(battleAssetConfig.path, (go) =>
            {
                _onLoadDone?.Invoke(go);
            });
        }

        private bool IsAssetValid(string _assetId, out BattleAsset _battleAssetConfig)
        {
            _battleAssetConfig = null;

            if (string.IsNullOrEmpty(_assetId))
            {
                BattleLog.Error("AssetComponent assetId is null or empty.");
                return false;
            }

            var assetConfig = ConfigSystem.CurrentData.GetBattleAsset(_assetId);
            if (assetConfig == null)
            {
                BattleLog.Error("BattleAsset not exist. assetId:" + _assetId);
                return false;
            }

            _battleAssetConfig = ConfigSystem.CurrentData.GetBattleAsset(_assetId);

            return true;
        }

        public void Unuse(string _assetId, GameObject _go)
        {
            if (!IsAssetValid(_assetId, out BattleAsset battleAssetConfig))
            {
                return;
            }

            BattleGameObjectPool.Unuse(battleAssetConfig.path, _go);
        }
    }
}