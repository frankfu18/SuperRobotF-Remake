using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.attribute
{
[System.Serializable]
public  class And : AttributeInfo
 {

    public System.Collections.Generic.List<cfg.attribute.AttributeInfo> attributes  = new System.Collections.Generic.List<cfg.attribute.AttributeInfo>();
     



    public const int TYPE_ID = 43740;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public And(Perfect.Marshal.Octets os) : base(os)  {

        attributes = cfg.Extensions.unmarshal_list_cfg_attribute_AttributeInfo(os); 
         
    }

    public And()
    {
    }

    public And(XmlElement e)
    {
        foreach (XmlElement ele in e.GetChildren())
        {
            switch (ele.Name)
            {
            case "attributes":  attributes = cfg.Extensions.xml_unmarshal_list_cfg_attribute_AttributeInfo(ele); break; 
             
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "And");
        ele.WriteStartElement("attributes"); cfg.Extensions.xml_marshal_list_cfg_attribute_AttributeInfo(attributes,ele); ele.WriteEndElement();
         
    }


}
}
}
