using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FrankGame;

public class EffectObject : MonoBehaviour
{
    public string effectName;
    public GameObject effectGo;
    public UIEffectLayer effectLayer;

    private bool goActive = true;
    private float delay = 0;
    private int timerId;

    public void SetGameObjectObject(GameObject _effGo)
    {
        effectGo = _effGo;

        effectGo.transform.SetParent(this.transform);
        effectGo.transform.localPosition = Vector3.zero;
        effectGo.transform.localEulerAngles = Vector3.zero;
        effectGo.transform.localScale = Vector3.one;

        effectLayer = effectGo.GetComponent<UIEffectLayer>();
        if (effectLayer != null)
        {
            var canvas = FindParentComponent<Canvas>(effectLayer.transform);
            effectLayer.SetLayer(canvas.sortingOrder + 1);
        }

        if (goActive)
        {
            Show(delay);
        }
        else
        {
            Hide();
        }
    }

    public void Show(float _delay = 0)
    {
        delay = _delay;

        goActive = true;

        if (effectGo != null)
        {
            effectGo.SetActive(false);
            if (_delay > 0)
            {
                if (timerId > 0)
                {
                    TimeSystem.Instance.RemoveTimer(timerId);
                    timerId = 0;
                }

                timerId = TimeSystem.Instance.AddDelay(_delay, () =>
                {
                    effectGo.SetActive(true);
                });
            }
            else
            {
                effectGo.SetActive(true);
            }
        }
    }

    public void Hide()
    {
        goActive = false;

        if (effectGo != null)
        {
            StopAllCoroutines();
            effectGo.SetActive(false);
        }
    }

    private T FindParentComponent<T>(Transform tf)
    {
        T t = tf.GetComponentInParent<T>();
        if (t == null)
        {
            if (tf.parent != null)
            {
                t = FindParentComponent<T>(tf.parent);
            }
        }

        return t;
    }
}
