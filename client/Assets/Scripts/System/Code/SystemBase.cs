using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SystemBase<T> : ISystem where T : new() 
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new T();

            }
            return _instance;
        }
    }

    protected MonoBehaviour root;

    public void InitDone()
    {
        OnInitDone();
    }

    public void Init(MonoBehaviour _mono)
    {
        root = _mono;

        OnInit();
    }

    public void Update(float _deltaTime, float _unscaledDeltaTime)
    {
        OnUpdate(_deltaTime, _unscaledDeltaTime);
    }

    public void Dispose()
    {
        OnDispose();
    }

    protected virtual void OnInit()
    {

    }
    protected virtual void OnInitDone()
    {

    }

    protected virtual void OnUpdate(float _deltaTime, float _unscaledDeltaTime)
    {

    }

    protected virtual void OnDispose()
    {

    }

    public void StartCoroutine(IEnumerator _ie)
    {
        root.StartCoroutine(_ie);
    }

    public void StopCoroutine(IEnumerator _ie)
    {
        root.StopCoroutine(_ie);
    }
}
