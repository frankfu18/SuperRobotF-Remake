using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.global
{
[System.Serializable]
public  class GInt64 : GlobalInfo
 {

    public long value ;
     



    public const int TYPE_ID = 31738;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public GInt64(Perfect.Marshal.Octets os) : base(os)  {

        value = os.ReadLong(); 
         
    }

    public GInt64()
    {
    }

    public GInt64(XmlElement e)
    {
        foreach (XmlElement ele in e.GetChildren())
        {
            switch (ele.Name)
            {
            case "value":  value = ele.ReadLong(); break; 
             
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "GInt64");
        ele.WriteStartElement("value"); ele.WriteLong(value); ele.WriteEndElement();
         
    }


}
}
}
