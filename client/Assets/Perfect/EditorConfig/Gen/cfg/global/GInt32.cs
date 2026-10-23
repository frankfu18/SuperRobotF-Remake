using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.global
{
[System.Serializable]
public  class GInt32 : GlobalInfo
 {

    public int value ;
     



    public const int TYPE_ID = 31643;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public GInt32(Perfect.Marshal.Octets os) : base(os)  {

        value = os.ReadInt(); 
         
    }

    public GInt32()
    {
    }

    public GInt32(XmlElement e)
    {
        foreach (XmlElement ele in e.GetChildren())
        {
            switch (ele.Name)
            {
            case "value":  value = ele.ReadInt(); break; 
             
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "GInt32");
        ele.WriteStartElement("value"); ele.WriteInt(value); ele.WriteEndElement();
         
    }


}
}
}
