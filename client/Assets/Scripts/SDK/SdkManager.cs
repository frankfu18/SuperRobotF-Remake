using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SdkManager : Singlet<SdkManager>
{
    public bool hachiSDKIsInited = false;

    private Action onPublicDataFunc;//公共属性回调

    public void Login()
    {
        ModuleSystem.Instance.Login();
    }

    public void InitSdk(Action _initCallback = null)
    {
        _initCallback?.Invoke();
    }

    public void OpenPrivacy()
    {
        Application.OpenURL("https://peekaboo.games/ios-privacy.html");
    }

    public void ReView()
    {
        
    }

    public void SetPublicDataFunc(Action _onPublicDataFunc)
    {
        this.onPublicDataFunc = _onPublicDataFunc;
    }

    public void EmitNoPublicData(string _eventType)
    {

    }

    public void Emit(string _eventType, string _key, object _value)
    {

    }
}
