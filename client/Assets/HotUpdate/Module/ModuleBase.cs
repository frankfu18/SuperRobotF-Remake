using System;
using System.Collections.Generic;

public abstract class ModuleBase<T> : Singlet<T>, IModule where T : new()
{
    public void Init()
    {
        AddEventListeners();
        OnInit();
    }

    protected virtual void OnInit()
    {

    }

    public void LoadConfigDone()
    {
        OnLoadConfigDone();
    }


    protected virtual void OnLoadConfigDone()
    {

    }

    public void Login()
    {
        OnLogin();
    }

    protected virtual void OnLogin()
    {

    }

    public void LoginDone()
    {
        OnLoginDone();

    }

    protected virtual void OnLoginDone()
    {

    }


    protected virtual void AddEventListeners()
    {

    }

    protected void SendEvent(EventConst _event, params object[] _args)
    {
        EventCenterSystem.Instance.SendEvent(_event, _args);
    }

    protected void SendEvent(SysEvent _event, params object[] _args)
    {
        EventCenterSystem.Instance.SendEvent(_event, _args);
    }

    protected void SendEvent(int _event, params object[] _args)
    {
        EventCenterSystem.Instance.SendEvent(_event, _args);
    }

    protected void AddEvent(EventConst _event, FrankGame.CallBackParams _callback)
    {
        EventCenterSystem.Instance.AddEvent(_event, this, _callback);
    }

    protected void AddEvent(SysEvent _event, FrankGame.CallBackParams _callback)
    {
        EventCenterSystem.Instance.AddEvent(_event, this, _callback);
    }

    protected void AddEvent(int _event, FrankGame.CallBackParams _callback)
    {
        EventCenterSystem.Instance.AddEvent(_event, this, _callback);
    }

    protected void AddEvents(FrankGame.CallBackParams _callback, params int[] events)
    {
        if (events == null || events.Length == 0)
        {
            return;
        }

        foreach (int _event in events)
        {
            AddEvent(_event, _callback);
        }
    }

    public void Logout()
    {
        OnLogout();
    }

    protected virtual void OnLogout()
    {

    }

    public void Dispose()
    {
        OnDispose();
        EventNativeSystem.Instance.RemoveEvent(this);
    }

    protected virtual void OnDispose()
    {

    }
}
