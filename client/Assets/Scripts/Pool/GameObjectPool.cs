using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FrankGame;

public class GameObjectPool
{
    private Transform poolRoot;
    private Dictionary<EResType, Dictionary<string, Queue<GameObject>>> unusedDic;


    public GameObjectPool(Transform _poolRoot)
    {
        this.poolRoot = _poolRoot;
        this.unusedDic = new Dictionary<EResType, Dictionary<string, Queue<GameObject>>>();

        this.poolRoot.gameObject.SetActive(false);
    }

    public void GetGameObject(EResType _resType, string _assetName, System.Action<GameObject> _onGetFunc)
    {
        GameObject go = null;
        if (this.unusedDic.ContainsKey(_resType))
        {
            if (this.unusedDic[_resType].ContainsKey(_assetName))
            {
                if (this.unusedDic[_resType][_assetName].Count > 0)
                {
                    go = this.unusedDic[_resType][_assetName].Dequeue();
                }
            }
        }

        if (go == null)
        {
            AssetSystem.Instance.LoadAsset<Object>(_resType, _assetName, ResConfig.Prefab_Suffix, (obj) =>
            {
                go = GameObject.Instantiate(obj) as GameObject;

                _onGetFunc?.Invoke(go);
            });
        }
        else
        {
            _onGetFunc?.Invoke(go);
        }
    }

    public void Unused(EResType _resType, string _assetName, GameObject _go)
    {
        _go.transform.SetParent(this.poolRoot);
        _go.transform.position = Vector3.zero;
        _go.transform.eulerAngles = Vector3.zero;
        _go.transform.localScale = Vector3.one;
        if (!this.unusedDic.ContainsKey(_resType))
        {
            this.unusedDic.Add(_resType, new Dictionary<string, Queue<GameObject>>());
        }

        if (!this.unusedDic[_resType].ContainsKey(_assetName))
        {
            this.unusedDic[_resType].Add(_assetName, new Queue<GameObject>());
        }

        this.unusedDic[_resType][_assetName].Enqueue(_go);
        //AssetSystem.Instance.UnloadAsset(_resType,_assetName,ResConfig.Prefab_Suffix);
    }

    public void Destroy()
    {
        foreach (KeyValuePair<EResType, Dictionary<string, Queue<GameObject>>> item in unusedDic)
        {
            Dictionary<string, Queue<GameObject>> dic = item.Value;
            foreach (KeyValuePair<string,Queue<GameObject>> citem in dic)
            {
                AssetSystem.Instance.UnloadAsset(item.Key,citem.Key,ResConfig.Prefab_Suffix,citem.Value.Count);
            }
        }
        
        GameObject.Destroy(this.poolRoot.gameObject);
    }
}
