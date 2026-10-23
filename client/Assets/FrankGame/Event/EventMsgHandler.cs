using System;
using System.Collections.Generic;

namespace FrankGame
{
    public class EventMsgHandler
    {
        //事件字典，一个事件对应一个回调
        private Dictionary<int, Delegate> m_EventDic = new Dictionary<int, Delegate>();

        private void OnListenerAdding(int eventType, Delegate callBack)
        {
            if (!m_EventDic.ContainsKey(eventType))
            {
                m_EventDic.Add(eventType, null);
            }
        }
        
        public void AddListener(int eventType, CallBack CallBack)
        {
            OnListenerAdding(eventType, CallBack);
            m_EventDic[eventType] = (CallBack)m_EventDic[eventType] + CallBack;
        }

        public void AddListener<T>(int eventType, CallBack<T> CallBack)
        {
            OnListenerAdding(eventType, CallBack);
            m_EventDic[eventType] = (CallBack<T>)m_EventDic[eventType] + CallBack;
        }

        public void AddListener<T, X>(int eventType, CallBack<T, X> CallBack)
        {
            OnListenerAdding(eventType, CallBack);
            m_EventDic[eventType] = (CallBack<T, X>)m_EventDic[eventType] + CallBack;
        }

        public void AddListener<T, X, Y>(int eventType, CallBack<T, X, Y> CallBack)
        {
            OnListenerAdding(eventType, CallBack);
            m_EventDic[eventType] = (CallBack<T, X, Y>)m_EventDic[eventType] + CallBack;
        }

        public void AddListener<T, X, Y, Z>(int eventType, CallBack<T, X, Y, Z> CallBack)
        {
            OnListenerAdding(eventType, CallBack);
            m_EventDic[eventType] = (CallBack<T, X, Y, Z>)m_EventDic[eventType] + CallBack;
        }

        public void AddListener<T, X, Y, Z, W>(int eventType, CallBack<T, X, Y, Z, W> CallBack)
        {
            OnListenerAdding(eventType, CallBack);
            m_EventDic[eventType] = (CallBack<T, X, Y, Z, W>)m_EventDic[eventType] + CallBack;
        }

        public void RemoveListener(int eventType, CallBack CallBack)
        {
            if (!m_EventDic.ContainsKey(eventType)) return;
            m_EventDic[eventType] = (CallBack)m_EventDic[eventType] - CallBack;
            OnListenerRemoved(eventType);
        }

        public void RemoveListener<T>(int eventType, CallBack<T> CallBack)
        {
            if (!m_EventDic.ContainsKey(eventType)) return;
            m_EventDic[eventType] = (CallBack<T>)m_EventDic[eventType] - CallBack;
            OnListenerRemoved(eventType);
        }

        public void RemoveListener<T, X>(int eventType, CallBack<T, X> CallBack)
        {
            if (!m_EventDic.ContainsKey(eventType)) return;
            m_EventDic[eventType] = (CallBack<T, X>)m_EventDic[eventType] - CallBack;
            OnListenerRemoved(eventType);
        }

        public void RemoveListener<T, X, Y>(int eventType, CallBack<T, X, Y> CallBack)
        {
            if (!m_EventDic.ContainsKey(eventType)) return;
            m_EventDic[eventType] = (CallBack<T, X, Y>)m_EventDic[eventType] - CallBack;
            OnListenerRemoved(eventType);
        }

        public void RemoveListener<T, X, Y, Z>(int eventType, CallBack<T, X, Y, Z> CallBack)
        {
            if (!m_EventDic.ContainsKey(eventType)) return;
            m_EventDic[eventType] = (CallBack<T, X, Y, Z>)m_EventDic[eventType] - CallBack;
            OnListenerRemoved(eventType);
        }

        public void RemoveListener<T, X, Y, Z, W>(int eventType, CallBack<T, X, Y, Z, W> CallBack)
        {
            if (!m_EventDic.ContainsKey(eventType)) return;
            m_EventDic[eventType] = (CallBack<T, X, Y, Z, W>)m_EventDic[eventType] - CallBack;
            OnListenerRemoved(eventType);
        }

        private void OnListenerRemoved(int eventType)
        {
            if (m_EventDic[eventType] == null)
            {
                m_EventDic.Remove(eventType);
            }
        }

        public void Broadcast(int eventType)
        {
            Delegate d;
            if (m_EventDic.TryGetValue(eventType, out d))
            {
                CallBack CallBack = d as CallBack;
                if (CallBack != null)
                {
                    CallBack();
                }
            }
        }

        public void Broadcast<T>(int eventType, T arg)
        {
            Delegate d;
            if (m_EventDic.TryGetValue(eventType, out d))
            {
                CallBack<T> CallBack = d as CallBack<T>;
                if (CallBack != null)
                {
                    CallBack(arg);
                }
            }
        }

        public void Broadcast<T, X>(int eventType, T arg1, X arg2)
        {
            Delegate d;
            if (m_EventDic.TryGetValue(eventType, out d))
            {
                CallBack<T, X> CallBack = d as CallBack<T, X>;
                if (CallBack != null)
                {
                    CallBack(arg1, arg2);
                }
            }
        }

        public void Broadcast<T, X, Y>(int eventType, T arg1, X arg2, Y arg3)
        {
            Delegate d;
            if (m_EventDic.TryGetValue(eventType, out d))
            {
                CallBack<T, X, Y> CallBack = d as CallBack<T, X, Y>;
                if (CallBack != null)
                {
                    CallBack(arg1, arg2, arg3);
                }
            }
        }

        public void Broadcast<T, X, Y, Z>(int eventType, T arg1, X arg2, Y arg3, Z arg4)
        {
            Delegate d;
            if (m_EventDic.TryGetValue(eventType, out d))
            {
                CallBack<T, X, Y, Z> CallBack = d as CallBack<T, X, Y, Z>;
                if (CallBack != null)
                {
                    CallBack(arg1, arg2, arg3, arg4);
                }
            }
        }

        public void Broadcast<T, X, Y, Z, W>(int eventType, T arg1, X arg2, Y arg3, Z arg4, W arg5)
        {
            Delegate d;
            if (m_EventDic.TryGetValue(eventType, out d))
            {
                CallBack<T, X, Y, Z, W> CallBack = d as CallBack<T, X, Y, Z, W>;
                if (CallBack != null)
                {
                    CallBack(arg1, arg2, arg3, arg4, arg5);
                }
            }
        }
    }
}