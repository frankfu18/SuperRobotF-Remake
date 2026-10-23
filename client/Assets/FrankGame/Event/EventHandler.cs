using System.Collections.Generic;

namespace FrankGame
{
    public class EventHandler
    {
        public class EventInfo
        {
            public object target;
            public CallBackParams onCallBack;
        }

        private Dictionary<int, List<EventInfo>> eventDic = new Dictionary<int, List<EventInfo>>();
        private Dictionary<object, List<int>> targetEventDic = new Dictionary<object, List<int>>();
        private Queue<List<EventInfo>> unuseEventInfoListQueue = new();
        private List<object> tempTargetList = new List<object>();

        public void Send(int _evtName, params object[] _args)
        {
            if (!eventDic.TryGetValue(_evtName, out List<EventInfo> evtInfoList))
            {
                return;
            }

            List<EventInfo> tempEventInfoList;
            if (unuseEventInfoListQueue.Count > 0)
            {
                tempEventInfoList = unuseEventInfoListQueue.Dequeue();
            }
            else
            {
                tempEventInfoList = new List<EventInfo>();
            }

            tempEventInfoList.AddRange(evtInfoList);

            foreach (var evt in tempEventInfoList)
            {
                evt.onCallBack?.Invoke(_args);
            }

            tempEventInfoList.Clear();
            unuseEventInfoListQueue.Enqueue(tempEventInfoList);
        }

        public EventInfo Add(int _evtName, object _target, CallBackParams _callback)
        {
            if (!eventDic.TryGetValue(_evtName, out List<EventInfo> evtInfoList))
            {
                evtInfoList = new List<EventInfo>();

                eventDic.Add(_evtName, evtInfoList);
            }

            var evt = evtInfoList.Find((info) =>
            {
                if (info.target == _target && info.onCallBack == _callback)
                {
                    return true;
                }

                return false;
            });

            if (evt == null)
            {
                evt = new EventInfo() { target = _target, onCallBack = _callback };
                evtInfoList.Add(evt);

                AddEvtName(_target, _evtName);
            }
            else
            {
                return null;
            }

            return evt;
        }

        public void Remove(int _evtName)
        {
            if (eventDic.Remove(_evtName))
            {
                RemoveTarget(_evtName);
            }
        }

        public void Remove(int _evtName, object _target)
        {
            if (!eventDic.TryGetValue(_evtName, out List<EventInfo> evtInfoList))
            {
                return;
            }

            evtInfoList.RemoveAll((info) =>
            {
                if (info.target == _target)
                {
                    return true;
                }

                return false;
            });

            RemoveTarget(_target, _evtName);
        }

        public void Remove(int _evtName, object _target, CallBackParams _callback)
        {
            if (!eventDic.TryGetValue(_evtName, out List<EventInfo> evtInfoList))
            {
                return;
            }

            var evt = evtInfoList.Find((info) =>
            {
                if (info.target == _target && info.onCallBack == _callback)
                {
                    return true;
                }

                return false;
            });

            if (evt != null)
            {
                evtInfoList.Remove(evt);
                RemoveTarget(_target, _evtName);
            }
        }

        /// <summary>
        /// 移除特定对象上的所有事件
        /// </summary>
        /// <param name="_target">目标对象</param>
        public void Remove(object _target)
        {
            List<int> evts = RemoveTarget(_target);
            if(evts != null)
            {
                foreach (var ev in evts)
                {
                    Remove(ev, _target);
                }
            }
        }

        private void AddEvtName(object _target, int _evtName)
        {
            if (!targetEventDic.TryGetValue(_target, out List<int> evtNameList))
            {
                evtNameList = new List<int>();

                targetEventDic.Add(_target, evtNameList);
            }

            if (evtNameList.IndexOf(_evtName) == -1)
            {
                evtNameList.Add(_evtName);
            }
        }

        private List<int> RemoveTarget(object _target)
        {
            if (!targetEventDic.TryGetValue(_target, out List<int> evtNameList))
            {
                return null;
            }

            targetEventDic.Remove(_target);
            return evtNameList;
        }


        private bool RemoveTarget(object _target, int _evtName)
        {
            if (!targetEventDic.TryGetValue(_target, out List<int> evtNameList))
            {
                return false;
            }

            if (evtNameList.Remove(_evtName))
            {
                if (evtNameList.Count == 0)
                {
                    targetEventDic.Remove(_target);
                }
                return true;
            }

            return false;
        }

        private void RemoveTarget(int _evtName)
        {
            tempTargetList.Clear();

            foreach (var o in targetEventDic)
            {
                o.Value.Remove(_evtName);
                if (o.Value.Count == 0)
                {
                    tempTargetList.Add(o.Key);
                }
            }

            foreach (var target in tempTargetList)
            {
                targetEventDic.Remove(target);
            }
        }

        public void Dispose()
        {
            eventDic.Clear();
            targetEventDic.Clear();
            unuseEventInfoListQueue.Clear();
            tempTargetList.Clear();
        }
    }
}