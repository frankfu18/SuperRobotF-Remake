using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.localize
{
[System.Serializable]
public  class TexLocalizeElements : TexLocalizeElement
 {

    public System.Collections.Generic.List<cfg.localize.TexLocalizeElement> elements  = new System.Collections.Generic.List<cfg.localize.TexLocalizeElement>();
     



    public const int TYPE_ID = 17934;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public TexLocalizeElements(Perfect.Marshal.Octets os) : base(os)  {

        elements = cfg.Extensions.unmarshal_list_cfg_localize_TexLocalizeElement(os); 
         
    }

    public TexLocalizeElements()
    {
    }

    public TexLocalizeElements(XmlElement e)
    {
        foreach (XmlElement ele in e.GetChildren())
        {
            switch (ele.Name)
            {
            case "elements":  elements = cfg.Extensions.xml_unmarshal_list_cfg_localize_TexLocalizeElement(ele); break; 
             
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "TexLocalizeElements");
        ele.WriteStartElement("elements"); cfg.Extensions.xml_marshal_list_cfg_localize_TexLocalizeElement(elements,ele); ele.WriteEndElement();
         
    }


}
}
}
