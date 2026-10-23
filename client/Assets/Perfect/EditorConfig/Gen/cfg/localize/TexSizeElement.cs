using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.localize
{
[System.Serializable]
public  class TexSizeElement : TexLocalizeElement
 {

    public cfg.Vector2 size  = new cfg.Vector2();
     



    public const int TYPE_ID = 14865;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public TexSizeElement(Perfect.Marshal.Octets os) : base(os)  {

        size = cfg.Extensions.unmarshal_cfg_Vector2(os); 
         
    }

    public TexSizeElement()
    {
    }

    public TexSizeElement(XmlElement e)
    {
        foreach (XmlElement ele in e.GetChildren())
        {
            switch (ele.Name)
            {
            case "size":  size = cfg.Extensions.xml_unmarshal_cfg_Vector2(ele); break; 
             
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "TexSizeElement");
        ele.WriteStartElement("size"); cfg.Extensions.xml_marshal_cfg_Vector2(size,ele); ele.WriteEndElement();
         
    }


}
}
}
