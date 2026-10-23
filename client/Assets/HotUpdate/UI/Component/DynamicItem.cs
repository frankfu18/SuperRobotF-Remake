using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DynamicItem : MonoBehaviour
{
    private enum ELoadState
    {
        NotLoad,
        Loading,
        LoadDone,
    }

    public string assetUIPath;

    public GameObject ItemGameObject
    {
        get
        {
            return itemGameObject;
        }
    }

    public UIItem UIItem
    {
        get
        {
            return uiItem;
        }
    }

    private UIContainer container;
    private Func<GameObject, UIItem> onNewUIItem;
    private ELoadState loadState = ELoadState.NotLoad;
    private UIItem uiItem;
    private GameObject itemGameObject;
    private event Action<GameObject> onLoadDone;

    public void InitContainer<T>(UIContainer _container) where T : UIItem, new()
    {
        this.container = _container;
        this.onNewUIItem = (go) =>
        {
            return this.container.AddDynamicItem<T>(this, go, this.container);
        };
    }

    public void GetUIItem(Action<UIItem> _onGetFunc)
    {
        if (loadState == ELoadState.LoadDone)
        {
            _onGetFunc?.Invoke(uiItem);
            return;
        }

        if (_onGetFunc != null)
        {
            onLoadDone += (go) =>
            {
                _onGetFunc.Invoke(uiItem);
            };
        }

        LoadItemAsset();
    }

    private void LoadItemAsset()
    {
        if (loadState != ELoadState.NotLoad)
        {
            return;
        }

        loadState = ELoadState.Loading;

        AssetHandler.Instance.LoadPrefab(assetUIPath, (go) =>
        {
            this.itemGameObject = go;

            this.loadState = ELoadState.LoadDone;

            if (this.container.IsPanelHasClose())//如果界面已经关闭了，直接销毁
            {
                Dispose();
                return;
            }

            RectTransform rectTransform = go.transform as RectTransform;
            rectTransform.SetParent(this.transform);
            rectTransform.localScale = Vector3.one;

            rectTransform.pivot = new Vector2(0.5f, 0.5f);

            rectTransform.anchoredPosition = Vector2.zero;

            rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, 0);
            rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, 0);

            rectTransform.anchorMax = Vector2.one;
            rectTransform.anchorMin = Vector2.zero;

            uiItem = this.onNewUIItem.Invoke(go);

            if (onLoadDone != null)
            {
                onLoadDone.Invoke(go);
                onLoadDone = null;
            }
        });
    }

    public void Dispose()
    {
        if(this.uiItem != null)
        {
            this.uiItem.Destroy();
            this.uiItem = null;
        }

        if (this.itemGameObject != null)
        {
            GameObject.Destroy(this.itemGameObject);
            this.itemGameObject = null;

            this.loadState = ELoadState.NotLoad;

            AssetHandler.Instance.UnloadUIPrefab(this.assetUIPath);
        }
    }
}
