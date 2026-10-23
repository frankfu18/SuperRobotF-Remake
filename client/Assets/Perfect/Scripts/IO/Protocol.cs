using System;
using Perfect.Marshal;

namespace Perfect.IO
{
    public abstract class Protocol : IMarshal
    {
        public object Context { get; set; }

        public abstract void Reset();
        public abstract object Clone();

        public abstract int GetTypeId();
        public abstract void Marshal(Octets os);
        public abstract void Unmarshal(Octets os);
        public void MarshalCompatible(Octets os)
        {
            throw new NotImplementedException();
        }
        public void UnmarshalCompatible(Octets os)
        {
            throw new NotImplementedException();
        }
    }


    public delegate Protocol IProtocolFactory();
}
