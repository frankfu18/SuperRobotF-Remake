using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Perfect.Marshal;

namespace editor.cfg
{

    public static class XmlWriterExtensions
    {
        public static void WriteBool(this XmlWriter os, bool x)
        {
            os.WriteString(x.ToString());
        }

        public static void WriteByte(this XmlWriter os, byte x)
        {
            os.WriteString(x.ToString());
        }
        public static void WriteDouble(this XmlWriter os, double x)
        {
            os.WriteString(x.ToString());
        }
        public static void WriteInt(this XmlWriter os, int x)
        {
            os.WriteString(x.ToString());
        }
        public static void WriteFloat(this XmlWriter os, float x)
        {
            os.WriteString(x.ToString());
        }
        public static void WriteLong(this XmlWriter os, long x)
        {
            os.WriteString(x.ToString());
        }
        public static void WriteShort(this XmlWriter os, short x)
        {
            os.WriteString(x.ToString());
        }
        public static void WriteString2(this XmlWriter os, string x)
        {
            os.WriteString(x);
        }
        public static void WriteOctets(this XmlWriter os, Octets x)
        {
            os.WriteString(x.ToString());
        }
        public static void WriteEnum<T>(this XmlWriter os, T x) where T : struct
        {
            os.WriteString(x.ToString());
        }
    }

    public static class XmlElementExtensions
    {
        public static bool ReadBool(this XmlElement ele)
        {
            return bool.Parse(ele.InnerText);
        }
        public static byte ReadByte(this XmlElement ele)
        {
            return byte.Parse(ele.InnerText);
        }
        public static double ReadDouble(this XmlElement ele)
        {
            return double.Parse(ele.InnerText);
        }
        public static int ReadInt(this XmlElement ele)
        {
            return int.Parse(ele.InnerText);
        }
        public static float ReadFloat(this XmlElement ele)
        {
            return float.Parse(ele.InnerText);
        }
        public static long ReadLong(this XmlElement ele)
        {
            return long.Parse(ele.InnerText);
        }
        public static Octets ReadOctets(this XmlElement ele)
        {
            return Octets.FromString(ele.InnerText);
        }
        public static string ReadString2(this XmlElement ele)
        {
            return ele.InnerText;
        }
        public static short ReadShort(this XmlElement ele)
        {
            return short.Parse(ele.InnerText);
        }
        public static T ReadEnum<T>(this XmlElement ele) where T : struct
        {
            //return Enum.Parse<T>(ele.InnerText);
            return (T)Enum.Parse(typeof(T), ele.InnerText);
        }
        public static List<XmlElement> GetChildren(this XmlElement parent)
        {
            var eles = new List<XmlElement>();
            foreach (XmlNode node in parent.ChildNodes)
            {
                if (node.NodeType == XmlNodeType.Element)
                {
                    eles.Add((XmlElement)node);
                }
            }

            return eles;
        }
    }
    
}
