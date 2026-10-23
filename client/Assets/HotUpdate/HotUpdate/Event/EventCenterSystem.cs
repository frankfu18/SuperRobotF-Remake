using System;
using System.Collections.Generic;
using FrankGame;

public class EventCenterSystem : Singlet<EventCenterSystem>
{
    public void SendEvent(EventConst _evtName, params object[] _args)
    {
        SendEvent((int)_evtName, _args);
    }

    public void SendEvent(SysEvent _evtName, params object[] _args)
    {
        SendEvent((int)_evtName, _args);
    }

    public void SendEvent(int _evtName, params object[] _args)
    {
        EventManager.SendEvent(_evtName, _args);
    }

    public void AddEvent(EventConst _evtName, object _target, CallBackParams _callback)
    {
        AddEvent((int)_evtName, _target, _callback);
    }

    public void AddEvent(SysEvent _evtName, object _target, CallBackParams _callback)
    {
        AddEvent((int)_evtName, _target, _callback);
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

    public void RemoveEvent(EventConst _evtName)
    {
        EventManager.RemoveEvent((int)_evtName);
    }

    public void RemoveEvent(EventConst _evtName, object _target)
    {
        EventManager.RemoveEvent((int)_evtName, _target);
    }

    public void RemoveEvent(EventConst _evtName, object _target, CallBackParams _callback)
    {
        EventManager.RemoveEvent((int)_evtName, _target, _callback);
    }

    public void Broadcast(int eventType)
    {
        EventManager.Broadcast(eventType);
    }

    public void Broadcast<T>(int eventType, T arg)
    {
        EventManager.Broadcast(eventType, arg);
    }

    public void Broadcast<T, X>(int eventType, T arg1, X arg2)
    {
        EventManager.Broadcast(eventType, arg1, arg2);
    }

    public void Broadcast<T, X, Y>(int eventType, T arg1, X arg2, Y arg3)
    {
        EventManager.Broadcast(eventType, arg1, arg2, arg3);
    }

    public void Broadcast<T, X, Y, Z>(int eventType, T arg1, X arg2, Y arg3, Z arg4)
    {
        EventManager.Broadcast(eventType, arg1, arg2, arg3, arg4);
    }

    public void Broadcast<T, X, Y, Z, W>(int eventType, T arg1, X arg2, Y arg3, Z arg4, W arg5)
    {
        EventManager.Broadcast(eventType, arg1, arg2, arg3, arg4, arg5);
    }

    public void AddListener(int eventType, CallBack CallBack)
    {
        EventManager.AddListener(eventType, CallBack);
    }

    public void AddListener<T>(int eventType, CallBack<T> CallBack)
    {
        EventManager.AddListener(eventType, CallBack);
    }

    public void AddListener<T, X>(int eventType, CallBack<T, X> CallBack)
    {
        EventManager.AddListener(eventType, CallBack);
    }

    public void AddListener<T, X, Y>(int eventType, CallBack<T, X, Y> CallBack)
    {
        EventManager.AddListener(eventType, CallBack);
    }

    public void AddListener<T, X, Y, Z>(int eventType, CallBack<T, X, Y, Z> CallBack)
    {
        EventManager.AddListener(eventType, CallBack);
    }

    public void AddListener<T, X, Y, Z, W>(int eventType, CallBack<T, X, Y, Z, W> CallBack)
    {
        EventManager.AddListener(eventType, CallBack);
    }

    public void RemoveListener(int eventType, CallBack CallBack)
    {
        EventManager.RemoveListener(eventType, CallBack);
    }

    public void RemoveListener<T>(int eventType, CallBack<T> CallBack)
    {
        EventManager.RemoveListener(eventType, CallBack);
    }

    public void RemoveListener<T, X>(int eventType, CallBack<T, X> CallBack)
    {
        EventManager.RemoveListener(eventType, CallBack);
    }

    public void RemoveListener<T, X, Y>(int eventType, CallBack<T, X, Y> CallBack)
    {
        EventManager.RemoveListener(eventType, CallBack);
    }

    public void RemoveListener<T, X, Y, Z>(int eventType, CallBack<T, X, Y, Z> CallBack)
    {
        EventManager.RemoveListener(eventType, CallBack);
    }

    public void RemoveListener<T, X, Y, Z, W>(int eventType, CallBack<T, X, Y, Z, W> CallBack)
    {
        EventManager.RemoveListener(eventType, CallBack);
    }
}
