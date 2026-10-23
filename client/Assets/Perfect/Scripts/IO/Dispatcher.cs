using System;
using System.Collections.Generic;

namespace Perfect.IO
{
    public sealed class Dispatcher
    {

        private readonly Dictionary<int, Action<Protocol>> msgHandlers = new Dictionary<int, Action<Protocol>>();

        private readonly Dictionary<int, Action<long, Protocol>> msgIdHandlers = new Dictionary<int, Action<long, Protocol>>();

        public void Register(int typeId, Action<Protocol> handler)
        {
            if (!msgHandlers.ContainsKey(typeId))
                msgHandlers.Add(typeId, handler);
        }

        public void Register(int typeId, Action<long, Protocol> handler)
        {
            if (!msgIdHandlers.ContainsKey(typeId))
                msgIdHandlers.Add(typeId, handler);
        }

        public void Dispatch(Protocol p)
        {
            if (msgHandlers.TryGetValue(p.GetTypeId(), out Action<Protocol> handler))
            {
                handler(p);
            }
            else
            {
                UnityEngine.Debug.LogErrorFormat("unknown IProtocol:{0}", p);
            }
        }

        public void Dispatch(long id, Protocol p)
        {
            if (msgIdHandlers.TryGetValue(p.GetTypeId(), out Action<long, Protocol> handler))
            {
                handler(id, p);
            }
            else
            {
                UnityEngine.Debug.LogErrorFormat("unknown IProtocol:{0}", p);
            }
        }
    }
}
