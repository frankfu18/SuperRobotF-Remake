using System;
using System.Collections.Generic;
using Perfect.Marshal;

namespace Perfect.IO
{
    public sealed class Codec
    {
        public static Dictionary<int, IProtocolFactory> MessageStubs { get; set; }

        public Codec()
        {
            message.maxSize = 512 * 1024;
        }

        private readonly Octets.Msg message = new Octets.Msg();
        public void Decode(Octets bs, Action<Protocol> onMsg)
        {
            while (bs.TryUnmarshalBytes(message) == Octets.UnmarshalError.OK)
            {
                var body = message.body;
                var typeId = body.ReadInt();
                Protocol p;
                if (typeId == KeepAlive.TYPE_ID)
                {
                    p = KeepAlive.InsObj;
                }
                else
                {
                    p = MessageStubs[typeId]();
                    p.Unmarshal(body);
                }
                try
                {
                    onMsg(p);
                }
                catch (Exception e)
                {
                    UnityEngine.Debug.LogException(e);
                }
            }
        }

        private readonly Octets temp = new Octets();
        public void Encode(Octets output, Protocol p)
        {
            temp.Clear();
            temp.WriteInt(p.GetTypeId());
            p.Marshal(temp);
            output.WriteOctets(temp);
        }
    }
}
