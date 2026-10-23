using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.localize
{
[System.Serializable]
public  class TexScaleElement : TexLocalizeElement
 {

    public cfg.Vector2 scale  = new cfg.Vector2();
     



    public const int TYPE_ID = 23750;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public TexScaleElement(Perfect.Marshal.Octets os) : base(os)  {

        scale = cfg.Extensions.unmarshal_cfg_Vector2(os); 
         
    }

    public TexScaleElement()
    {
    }

    public TexScaleElement(XmlElement e)
    {
        foreach (XmlElement ele in e.GetChildren())
        {
            switch (ele.Name)
            {
            case "scale":  scale = cfg.Extensions.xml_unmarshal_cfg_Vector2(ele); break; 
             
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "TexScaleElement");
        ele.WriteStartElement("scale"); cfg.Extensions.xml_marshal_cfg_Vector2(scale,ele); ele.WriteEndElement();
         
    }


}
}
}
