using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISwitch : MonoBehaviour, IUIComponent
{
    public GameObject Open;
    public GameObject Close;

    public bool isOpen;

    public System.Action<bool> onSwitchCallback;

    public void Call(bool _open)
    {
        this.isOpen = _open;

        this.Open.SetActive(_open);
        this.Close.SetActive(!_open);

        this.onSwitchCallback?.Invoke(_open);
    }

    public void Show(bool _open)
    {
        Call(_open);
    }
    
}
