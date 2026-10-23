using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrankGame
{
    public static class EventManager
    {
        private static EventHandler eventHandler = new EventHandler();
        private static EventMsgHandler eventMsgHandler = new EventMsgHandler();

        public static void SendEvent(int _evtName, params object[] _args)
        {
            eventHandler.Send(_evtName, _args);
        }

        public static void AddEvent(int _evtName, object _target, CallBackParams _callback)
        {
            eventHandler.Add(_evtName, _target, _callback);
        }

        public static void RemoveEvent(int _evtName)
        {
            eventHandler.Remove(_evtName);
        }

        public static void RemoveEvent(object _target)
        {
            eventHandler.Remove(_target);
        }

        public static void RemoveEvent(int _evtName, object _target)
        {
            eventHandler.Remove(_evtName, _target);
        }

        public static void RemoveEvent(int _evtName, object _target, CallBackParams _callback)
        {
            eventHandler.Remove(_evtName, _target, _callback);
        }

        public static void Broadcast(int eventType)
        {
            eventMsgHandler.Broadcast(eventType);
        }

        public static void Broadcast<T>(int eventType, T arg)
        {
            eventMsgHandler.Broadcast(eventType, arg);
        }

        public static void Broadcast<T, X>(int eventType, T arg1, X arg2)
        {
            eventMsgHandler.Broadcast(eventType, arg1, arg2);
        }

        public static void Broadcast<T, X, Y>(int eventType, T arg1, X arg2, Y arg3)
        {
            eventMsgHandler.Broadcast(eventType, arg1, arg2, arg3);
        }

        public static void Broadcast<T, X, Y, Z>(int eventType, T arg1, X arg2, Y arg3, Z arg4)
        {
            eventMsgHandler.Broadcast(eventType, arg1, arg2, arg3, arg4);
        }

        public static void Broadcast<T, X, Y, Z, W>(int eventType, T arg1, X arg2, Y arg3, Z arg4, W arg5)
        {
            eventMsgHandler.Broadcast(eventType, arg1, arg2, arg3, arg4, arg5);
        }

        public static void AddListener(int eventType, CallBack CallBack)
        {
            eventMsgHandler.AddListener(eventType, CallBack);
        }

        public static void AddListener<T>(int eventType, CallBack<T> CallBack)
        {
            eventMsgHandler.AddListener(eventType, CallBack);
        }

        public static void AddListener<T, X>(int eventType, CallBack<T, X> CallBack)
        {
            eventMsgHandler.AddListener(eventType, CallBack);
        }

        public static void AddListener<T, X, Y>(int eventType, CallBack<T, X, Y> CallBack)
        {
            eventMsgHandler.AddListener(eventType, CallBack);
        }

        public static void AddListener<T, X, Y, Z>(int eventType, CallBack<T, X, Y, Z> CallBack)
        {
            eventMsgHandler.AddListener(eventType, CallBack);
        }

        public static void AddListener<T, X, Y, Z, W>(int eventType, CallBack<T, X, Y, Z, W> CallBack)
        {
            eventMsgHandler.AddListener(eventType, CallBack);
        }

        public static void RemoveListener(int eventType, CallBack CallBack)
        {
            eventMsgHandler.RemoveListener(eventType, CallBack);
        }

        public static void RemoveListener<T>(int eventType, CallBack<T> CallBack)
        {
            eventMsgHandler.RemoveListener(eventType, CallBack);
        }

        public static void RemoveListener<T, X>(int eventType, CallBack<T, X> CallBack)
        {
            eventMsgHandler.RemoveListener(eventType, CallBack);
        }

        public static void RemoveListener<T, X, Y>(int eventType, CallBack<T, X, Y> CallBack)
        {
            eventMsgHandler.RemoveListener(eventType, CallBack);
        }

        public static void RemoveListener<T, X, Y, Z>(int eventType, CallBack<T, X, Y, Z> CallBack)
        {
            eventMsgHandler.RemoveListener(eventType, CallBack);
        }

        public static void RemoveListener<T, X, Y, Z, W>(int eventType, CallBack<T, X, Y, Z, W> CallBack)
        {
            eventMsgHandler.RemoveListener(eventType, CallBack);
        }
    }
}