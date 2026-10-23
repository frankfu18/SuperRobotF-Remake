using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FrankGame;

public class EventNativeSystem : Singlet<EventNativeSystem>
{
    public void SendEvent(SysEvent _evtName, params object[] _args)
    {
        EventManager.SendEvent((int)_evtName, _args);
    }

    public void SendEvent(int _evtName, params object[] _args)
    {
        EventManager.SendEvent(_evtName, _args);
    }

    public void AddEvent(SysEvent _evtName, object _target, CallBackParams _callback)
    {
        EventManager.AddEvent((int)_evtName, _target, _callback);
    }

    public void AddEvent(int _evtName, object _target, CallBackParams _callback)
    {
        EventManager.AddEvent(_evtName, _target, _callback);
    }

    public void RemoveEvent(SysEvent _evtName)
    {
        EventManager.RemoveEvent((int)_evtName);
    }

    public void RemoveEvent(object _target)
    {
        EventManager.RemoveEvent(_target);
    }

    public void RemoveEvent(SysEvent _evtName, object _target)
    {
        EventManager.RemoveEvent((int)_evtName, _target);
    }

    public void RemoveEvent(SysEvent _evtName, object _target, CallBackParams _callback)
    {
        EventManager.RemoveEvent((int)_evtName, _target, _callback);
    }
}
