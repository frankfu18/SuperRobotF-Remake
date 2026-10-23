using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsynLoadAsset
{
    private enum ELoadState
    {
        None,
        Loading,
        Done,
        Destroyed,
    }

    private GameObject asset;
    private ELoadState loadState;
    private Action<GameObject> onGetAsset;
    private string assetPath;
    private Action<string, Action<GameObject>> onLoadAsset;

    public AsynLoadAsset(string _assetPath, Action<string, Action<GameObject>> _onLoadAsset)
    {
        this.assetPath = _assetPath;
        this.onLoadAsset = _onLoadAsset;
    }

    public void Load(Action<GameObject> _onLoadDone = null)
    {
        this.onGetAsset += _onLoadDone;

        if(this.loadState == ELoadState.Done)
        {
            this.onGetAsset?.Invoke(this.asset);
            this.onGetAsset = null;
        }
        else
        {
            LoadAsset();
        }
    }

    private void LoadAsset()
    {
        if(this.loadState != ELoadState.None)
        {
            return;
        }

        this.loadState = ELoadState.Loading;

        this.onLoadAsset?.Invoke(this.assetPath, LoadAsset);
    }

    private void LoadAsset(GameObject _go)
    {
        if(this.loadState == ELoadState.Destroyed)
        {
            GameObject.Destroy(_go);
            this.loadState = ELoadState.None;
            return;
        }

        this.asset = _go;
        this.loadState = ELoadState.Done;

        this.onGetAsset?.Invoke(this.asset);
        this.onGetAsset = null;
    }

    public void GetAsset(Action<GameObject> _onGetAsset)
    {
        if (this.loadState == ELoadState.Done)
        {
            _onGetAsset?.Invoke(this.asset);
        }
        else if (this.loadState == ELoadState.Loading)
        {
            this.onGetAsset += _onGetAsset;
        }
        else
        {
            LoadAsset();
        }
    }

    public void Destroy()
    {
        if (this.loadState == ELoadState.Done)
        {
            GameObject.Destroy(this.asset);
            this.asset = null;
            this.onGetAsset = null;
            this.loadState = ELoadState.None;
        }
        else if (this.loadState == ELoadState.Loading)
        {
            this.onGetAsset = null;
            this.loadState = ELoadState.Destroyed;
        }
    }
}
