using System;

namespace Perfect.Marshal
{
    public interface IMarshal : ICloneable
    {
        int GetTypeId();
        void Marshal(Perfect.Marshal.Octets os);
        void Unmarshal(Perfect.Marshal.Octets os);
        void MarshalCompatible(Perfect.Marshal.Octets os);
        void UnmarshalCompatible(Perfect.Marshal.Octets os);
    }



    public static class Tag
    {
        public const int INT = 0, LONG = 1, STRING = 2, MAP = 3, BEAN = 4, BOOL = 5, FLOAT = 6, DOUBLE = 7;
        public const int SET = 8, LIST = 9, BYTE = 10, SHORT = 11, SINT = 13, SLONG = 14, FINT = 12, FLONG = 15;

        public const int OCTETS = STRING;
        public const int ARRAY = LIST;

        public const int TAG_SHIFT = 4;
        public const int TAG_MASK = (1 << TAG_SHIFT) - 1;

        // TODO
        // 优化 bean 的序列化
        public static void SkipUnknownField(int tid, Octets os)
        {
            int tag = tid & TAG_MASK;
            switch (tag)
            {
            case Tag.BOOL:
                os.ReadBool();
                break;
            case Tag.BYTE:
                os.ReadByte();
                break;
            case Tag.SHORT:
                os.ReadInt();
                break;
            case Tag.INT:
                os.ReadInt();
                break;
            case Tag.LONG:
                os.ReadLong();
                break;
            case Tag.FLOAT:
                os.ReadFloat();
                break;
            case Tag.DOUBLE:
                os.ReadDouble();
                break;
            // case Tag.OCTETS:
            case Tag.STRING:
            case Tag.ARRAY:
            //case Tag.LIST:
            case Tag.SET:
            case Tag.MAP:
            case Tag.BEAN:
                os.SkipBytes();
                break;
            case Tag.SINT:
                os.ReadSint();
                break;
            case Tag.SLONG:
                os.ReadSlong();
                break;
            default:
                throw new MarshalException("unknown xdb.tag:" + tag);
            }
        }
    }

}
