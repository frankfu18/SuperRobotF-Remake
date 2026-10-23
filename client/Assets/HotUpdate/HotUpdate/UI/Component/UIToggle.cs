using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIToggle : MonoBehaviour
{
    public Button OnBtn;
    public Button OffBtn;
    public Transform Flag;
    public Vector3 FlagOnPos;
    public Vector3 FlagOffPos;
    public UIToggleGroup ToggleGroup;
    public GameObject OnShow;
    public GameObject OffShow;

    private bool select;
    private System.Action<bool, object> onToggleFunc;
    public bool isCancel = true; //是否支持取消
    public bool isEffect = true;//是否生效
    public System.Action onNoEffectFunc;//不生效的回调

    private void Awake()
    {

        OnBtn.onClick.AddListener(Toggle);
        OffBtn.onClick.AddListener(Toggle);

        ToggleGroup?.AddToggle(this);
    }

    public void AddListener(System.Action<bool, object> _onToggleFunc)
    {
        onToggleFunc = _onToggleFunc;
    }

    public void Set(bool _select, object _param = null)
    {
        select = _select;

        onToggleFunc?.Invoke(select, _param);

        if (_select)
        {
            OnBtn.gameObject.SetActive(true);
            OffBtn.gameObject.SetActive(false);
            if (OnShow != null)
            {
                OnShow.SetActive(true);
            }
            if (OffShow != null)
            {
                OffShow.SetActive(false);
            }
            
            if (Flag != null)
            {
                Flag.localPosition = FlagOnPos;
            }
        }
        else
        {
            OnBtn.gameObject.SetActive(false);
            OffBtn.gameObject.SetActive(true);
            if (OnShow != null)
            {
                OnShow.SetActive(false);
            }
            if (OffShow != null)
            {
                OffShow.SetActive(true);
            }

            if (Flag != null)
            {
                Flag.localPosition = FlagOffPos;
            }
        }

        if (select)
        {
            ToggleGroup?.Select(this);
        }
    }

    public void Toggle()
    {
        Toggle(null);
    }

    public void Toggle(object _param)
    {
        if (!isEffect)
        {
            onNoEffectFunc?.Invoke();
            return;
        }
        if (!select || isCancel)
        {
            Set(!select, _param);
        }
    }

    public void On(object _param = null)
    {
        Set(true, _param);
    }

    public void Off(object _param = null)
    {
        Set(false, _param);
    }

    public bool IsOn()
    {
        return select;
    }

    private void OnDestroy()
    {
        OnBtn.onClick.RemoveAllListeners();
        OffBtn.onClick.RemoveAllListeners();
    }
}
