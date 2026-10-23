using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FrankGame;
using Spine.Unity;

public class AssetHandler : Singlet<AssetHandler>
{
    public void SetSpriteById(string _assetId, Image _img, bool _nativeSize = false)
    {
        if (string.IsNullOrEmpty(_assetId))
        {
            LogUtil.Error("资源id为空！");
            return;
        }

        var assetCfg = ConfigSystem.Instance.GetData().GetAsset(_assetId);
        if (assetCfg == null)
        {
            LogUtil.Error("资源id没找到：" + _assetId);
            return;
        }

        if (assetCfg.assetInfo is editor.cfg.asset.Texture)
        {
            SetTexture(((editor.cfg.asset.Texture)assetCfg.assetInfo).path, _img, _nativeSize);
        }
        else if (assetCfg.assetInfo is editor.cfg.asset.Sprite)
        {
            SetSprite(((editor.cfg.asset.Sprite)assetCfg.assetInfo).altasName, ((editor.cfg.asset.Sprite)assetCfg.assetInfo).spriteName, _img, _nativeSize);
        }
    }

    public void SetSpriteById(string _assetId, SpriteRenderer _render)
    {
        if (string.IsNullOrEmpty(_assetId))
        {
            LogUtil.Error("资源id为空！");
            return;
        }

        var assetCfg = ConfigSystem.Instance.GetData().GetAsset(_assetId);
        if (assetCfg == null)
        {
            LogUtil.Error("资源id没找到：" + _assetId);
            return;
        }

        if (assetCfg.assetInfo is editor.cfg.asset.Texture)
        {
            SetTexture(((editor.cfg.asset.Texture)assetCfg.assetInfo).path, _render);
        }
        else if (assetCfg.assetInfo is editor.cfg.asset.Sprite)
        {
            SetSprite(((editor.cfg.asset.Sprite)assetCfg.assetInfo).altasName, ((editor.cfg.asset.Sprite)assetCfg.assetInfo).spriteName, _render);
        }
    }

    public void SetMatTextureById(string _assetId, Material _material, Transform trm)
    {
        if (string.IsNullOrEmpty(_assetId))
        {
            LogUtil.Error("资源id为空！");
            return;
        }

        var assetCfg = ConfigSystem.Instance.GetData().GetAsset(_assetId);
        if (assetCfg == null)
        {
            LogUtil.Error("资源id没找到：" + _assetId);
            return;
        }
        if (assetCfg.assetInfo is editor.cfg.asset.Texture)
        {
            SetTexture(((editor.cfg.asset.Texture)assetCfg.assetInfo).path, _material, trm);
        }
    }

    public void SetSprite(string _atlasName, string _spriteName, Image _img, bool _nativeSize = false)
    {
        LoadSprite(_atlasName, _spriteName, (sprite) =>
        {
            _img.sprite = sprite;
            if (_nativeSize)
            {
                _img.SetNativeSize();
            }
        });
    }


    public void SetSprite(string _atlasName, string _spriteName, RawImage _img, bool _nativeSize = false)
    {
        LoadSprite(_atlasName, _spriteName, (sprite) =>
        {
            _img.texture = sprite.texture;
            if (_nativeSize)
            {
                _img.SetNativeSize();
            }
        });
    }

    public void SetSprite(string _atlasName, string _spriteName, SpriteRenderer _renderer)
    {
        LoadSprite(_atlasName, _spriteName, (sprite) =>
        {
            _renderer.sprite = sprite;
        });
    }

    public void SetSpineById(string _assetId, SkeletonGraphic _skeletonGraphic, bool _scale = false)
    {
        if (string.IsNullOrEmpty(_assetId))
        {
            LogUtil.Error("资源id为空！");
            return;
        }

        var assetCfg = ConfigSystem.Instance.GetData().GetAsset(_assetId);
        if (assetCfg == null)
        {
            LogUtil.Error("资源id没找到：" + _assetId);
            return;
        }

        if (!(assetCfg.assetInfo is editor.cfg.asset.Spine))
        {
            LogUtil.Error("Spine资源类型错误：" + _assetId);
            return;
        }

        var spineInfo = (editor.cfg.asset.Spine)assetCfg.assetInfo;

        if (_scale)
        {
            _skeletonGraphic.transform.localScale = spineInfo.scale * Vector3.one;
        }

        SetSpine(spineInfo.path, _skeletonGraphic, spineInfo.defaultAnim);
    }

    public void SetTextureById(string _assetId, RawImage _img, bool _nativeSize = false)
    {
        if (string.IsNullOrEmpty(_assetId))
        {
            LogUtil.Error("资源id为空！");
            return;
        }

        var assetCfg = ConfigSystem.Instance.GetData().GetAsset(_assetId);
        if (assetCfg == null)
        {
            LogUtil.Error("资源id没找到：" + _assetId);
            return;
        }

        if (assetCfg.assetInfo is editor.cfg.asset.Texture)
        {
            SetTexture(((editor.cfg.asset.Texture)assetCfg.assetInfo).path, _img, _nativeSize);
        }
        else if (assetCfg.assetInfo is editor.cfg.asset.Sprite)
        {
            SetSprite(((editor.cfg.asset.Sprite)assetCfg.assetInfo).altasName, ((editor.cfg.asset.Sprite)assetCfg.assetInfo).spriteName, _img, _nativeSize);
        }
    }

    public void SetTextureById(string _assetId, Image _img, bool _nativeSize = false)
    {
        if (string.IsNullOrEmpty(_assetId))
        {
            LogUtil.Error("资源id为空！");
            return;
        }

        var assetCfg = ConfigSystem.Instance.GetData().GetAsset(_assetId);
        if (assetCfg == null)
        {
            LogUtil.Error("资源id没找到：" + _assetId);
            return;
        }

        if (assetCfg.assetInfo is editor.cfg.asset.Texture)
        {
            SetTexture(((editor.cfg.asset.Texture)assetCfg.assetInfo).path, _img, _nativeSize);
        }
        else if (assetCfg.assetInfo is editor.cfg.asset.Sprite)
        {
            SetSprite(((editor.cfg.asset.Sprite)assetCfg.assetInfo).altasName, ((editor.cfg.asset.Sprite)assetCfg.assetInfo).spriteName, _img, _nativeSize);
        }
    }

    public void SetTexture(string _texName, RawImage _rawImg, bool _nativeSize = false)
    {
        LoadTexture(_texName, (tex) =>
        {
            _rawImg.texture = tex;
            if (_nativeSize)
            {
                _rawImg.SetNativeSize();
            }
        });
    }

    public void SetSpine(string _data, SkeletonGraphic _skeletonGraphic, string _defaultAnim)
    {
        LoadSpine(_data, (dataAsset) =>
        {
            _skeletonGraphic.skeletonDataAsset = dataAsset;
            _skeletonGraphic.Initialize(true);
            _skeletonGraphic.AnimationState.SetAnimation(0, _defaultAnim, true);
        });
    }

    public void SetTexture(string _texName, Image _img, bool _nativeSize = false)
    {
        LoadTexture(_texName, (tex) =>
        {
            _img.sprite = tex;
            if (_nativeSize)
            {
                _img.SetNativeSize();
            }
        });
    }

    public void SetTexture(string _texName, SpriteRenderer _renderer)
    {
        LoadTexture(_texName, (tex) =>
        {
            _renderer.sprite = tex;
        });
    }

    public void SetTexture(string _texName, Material _material, Transform trm)
    {
        LoadTexture(_texName, (tex) =>
        {
            _material.mainTexture = tex;
            trm.localScale = new Vector3((float)tex.width / 100, (float)tex.height / 100);
        });
    }

    public void LoadSound(string _name, System.Action<AudioClip> _onLoadDone)
    {
        AssetSystem.Instance.LoadSound(_name, _onLoadDone);
    }

    public void UnloadSound(string _name, int _unuseCount = 1, bool _unloadBundle = false)
    {
        AssetSystem.Instance.UnloadSound(_name, _unuseCount, _unloadBundle);
    }

    public void LoadSprite(string _atlasName, string _spriteName, System.Action<Sprite> _onLoadDone)
    {
        AssetSystem.Instance.LoadAsset<Sprite>(EResType.Sprite, _atlasName + "/" + _spriteName, ResConfig.Texture_Suffix, (asset) =>
        {
            _onLoadDone?.Invoke(asset);
        });
    }

    public void LoadTexture(string _texName, System.Action<Texture2D> _onLoadDone)
    {
        AssetSystem.Instance.LoadAsset<Texture2D>(EResType.Texture, _texName, ResConfig.Texture_Suffix, (asset) =>
        {
            _onLoadDone?.Invoke(asset);
        });
    }

    public void LoadTexture(string _texName, System.Action<Sprite> _onLoadDone)
    {
        AssetSystem.Instance.LoadAsset<Sprite>(EResType.Texture, _texName, ResConfig.Texture_Suffix, (asset) =>
        {
            if (asset == null)
            {
                _onLoadDone?.Invoke(null);
                return;
            }

            _onLoadDone?.Invoke(asset);
        });
    }

    public void LoadPrefab(string _prefabName, System.Action<GameObject> _onLoadDone)
    {
        AssetSystem.Instance.LoadAsset<GameObject>(EResType.UIPrefab, _prefabName, ResConfig.Prefab_Suffix, (asset) =>
        {
            _onLoadDone?.Invoke(GameObject.Instantiate(asset));
        });
    }

    public void LoadBattlePrefab(string _prefabName, System.Action<GameObject> _onLoadDone)
    {
        AssetSystem.Instance.LoadAsset<GameObject>(EResType.BattlePrefab, _prefabName, ResConfig.Prefab_Suffix, (asset) =>
        {
            _onLoadDone?.Invoke(GameObject.Instantiate(asset));
        });
    }

    public void LoadBattleSprite(string spritePath, System.Action<Sprite> _onLoadDone)
    {
        AssetSystem.Instance.LoadAsset<Sprite>(EResType.BattleSprite, spritePath, ResConfig.Texture_Suffix, (asset) =>
        {
            _onLoadDone?.Invoke(asset);
        });
    }

    public void LoadBattleSpine(string spinePath, System.Action<SkeletonDataAsset> _onLoadDone)
    {
        AssetSystem.Instance.LoadAsset<SkeletonDataAsset>(EResType.BattleSpine, spinePath, ResConfig.Asset_Suffix, (asset) =>
        {
            _onLoadDone?.Invoke(asset);
        });
    }

    public void LoadSpine(string _name, System.Action<SkeletonDataAsset> _onLoadDone)
    {
        AssetSystem.Instance.LoadAsset<SkeletonDataAsset>(EResType.Spine, _name, ResConfig.Asset_Suffix, (asset) =>
        {
            _onLoadDone?.Invoke(asset);
        });
    }

    public void LoadSpineMaterial(string _assetPath, System.Action<Material> _onLoadDone)
    {
        AssetSystem.Instance.LoadAsset<Material>(EResType.Spine, _assetPath, ResConfig.Material_Suffix, (asset) =>
        {
            _onLoadDone?.Invoke(asset);
        });
    }

    public void LoadMapPrefab(string _prefabName, System.Action<GameObject> _onLoadDone)
    {
        AssetSystem.Instance.LoadAsset<GameObject>(EResType.MapPrefab, _prefabName, ResConfig.Prefab_Suffix, (asset) =>
        {
            _onLoadDone?.Invoke(GameObject.Instantiate(asset));
        });
    }

    public void LoadMapData(string _prefabName, System.Action<TextAsset> _onLoadDone)
    {
        AssetSystem.Instance.LoadAsset<TextAsset>(EResType.MapData, _prefabName, ResConfig.Bytes_Suffix, (asset) =>
        {
            _onLoadDone?.Invoke(asset);
        });
    }
    
    public void LoadMapBytes(string _assetName, System.Action<byte[]> _onLoadDone)
    {
        AssetSystem.Instance.LoadAsset<TextAsset>(EResType.MapData, _assetName, ResConfig.Bytes_Suffix, (asset) =>
        {
            _onLoadDone?.Invoke(asset.bytes);
        });
    }

    public void UnloadUIPrefab(string _prefabName, bool _unloadBundle = false)
    {
        AssetSystem.Instance.UnloadAsset(EResType.UIPrefab, _prefabName, ResConfig.Prefab_Suffix, 1, _unloadBundle);
    }

    public void LoadUIScenePrefab(string _prefabName, System.Action<GameObject> _onLoadDone)
    {
        AssetSystem.Instance.LoadAsset<GameObject>(EResType.UIScene, _prefabName, ResConfig.Prefab_Suffix, (asset) =>
        {
            _onLoadDone?.Invoke(GameObject.Instantiate(asset));
        });
    }

    public void UnloadUIScenePrefab(string _prefabName, int _unuseCount = 1, bool _unloadBundle = false)
    {
        AssetSystem.Instance.UnloadAsset(EResType.UIScene, _prefabName, ResConfig.Prefab_Suffix, _unuseCount, _unloadBundle);
    }

    public void LoadMaterial(string _assetPath, System.Action<Material> _onLoadDone)
    {
        AssetSystem.Instance.LoadAsset<Material>(EResType.Material, _assetPath, ResConfig.Material_Suffix, (asset) =>
        {
            _onLoadDone?.Invoke(asset);
        });
    }
}
