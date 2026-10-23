using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FrankGame;

public class EffectSystem : Singlet<EffectSystem>
{
    private GameObject effectRoot;
    private GameObjectPool effectPool;

    private void Awake()
    {
        effectRoot = new GameObject("EffectRoot");
        effectPool = new GameObjectPool(effectRoot.transform);
    }

    public EffectObject LoadEffect(string _effectName, Transform _parent, Vector3 _pos, float _scale = 1)
    {
        GameObject go = new GameObject("Effect_" + _effectName);
        go.transform.SetParent(_parent);
        go.transform.localPosition = _pos;
        go.transform.localRotation = Quaternion.identity;
        go.transform.localScale = Vector3.one * _scale;

        EffectObject effect = go.AddComponent<EffectObject>();
        effect.effectName = _effectName;

        LoadEffectGameObject(_effectName, (effGo) =>
        {
            effect.SetGameObjectObject(effGo);
        });

        return effect;
    }

    private void LoadEffectGameObject(string _effectName, System.Action<GameObject> _onLoadDone)
    {
        effectPool.GetGameObject(EResType.Effect, _effectName, _onLoadDone);
    }

    public void UnusedEffect(EffectObject _eff)
    {
        if (_eff != null)
        {
            effectPool.Unused(EResType.Effect, _eff.effectName, _eff.effectGo);

            GameObject.Destroy(_eff);
        }
    }
}
