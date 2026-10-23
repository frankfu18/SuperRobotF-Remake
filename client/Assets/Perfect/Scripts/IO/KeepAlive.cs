using Perfect.Marshal;

namespace Perfect.IO
{
    public sealed class KeepAlive : Protocol
    {
        public const int TYPE_ID = 0;

        public static readonly KeepAlive InsObj = new KeepAlive();
        public static readonly Octets InsData = new Octets(new byte[] { TYPE_ID });


        public KeepAlive()
        {
        }

        public override object Clone()
        {
            return this;
        }

        public Protocol NewObject()
        {
            return this;
        }

        public override int GetTypeId()
        {
            return TYPE_ID;
        }


        public override void Reset()
        {

        }

        public override void Marshal(Octets os)
        {

        }

        public override void Unmarshal(Octets os)
        {

        }
    }
}
