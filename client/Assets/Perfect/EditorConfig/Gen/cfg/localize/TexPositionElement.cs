using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.localize
{
[System.Serializable]
public  class TexPositionElement : TexLocalizeElement
 {

    public cfg.Vector2 pos  = new cfg.Vector2();
     



    public const int TYPE_ID = 32342;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public TexPositionElement(Perfect.Marshal.Octets os) : base(os)  {

        pos = cfg.Extensions.unmarshal_cfg_Vector2(os); 
         
    }

    public TexPositionElement()
    {
    }

    public TexPositionElement(XmlElement e)
    {
        foreach (XmlElement ele in e.GetChildren())
        {
            switch (ele.Name)
            {
            case "pos":  pos = cfg.Extensions.xml_unmarshal_cfg_Vector2(ele); break; 
             
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "TexPositionElement");
        ele.WriteStartElement("pos"); cfg.Extensions.xml_marshal_cfg_Vector2(pos,ele); ele.WriteEndElement();
         
    }


}
}
}
