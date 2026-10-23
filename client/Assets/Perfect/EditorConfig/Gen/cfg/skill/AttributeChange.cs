using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.skill
{
[System.Serializable]
public  class AttributeChange : BuffEffectContent
 {

    public cfg.attribute.AttributeLongNumber baseAttr  = new cfg.attribute.AttributeLongNumber();
     



    public const int TYPE_ID = 32442;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public AttributeChange(Perfect.Marshal.Octets os) : base(os)  {

        baseAttr = cfg.Extensions.unmarshal_cfg_attribute_AttributeLongNumber(os); 
         
    }

    public AttributeChange()
    {
    }

    public AttributeChange(XmlElement e)
    {
        foreach (XmlElement ele in e.GetChildren())
        {
            switch (ele.Name)
            {
            case "baseAttr":  baseAttr = cfg.Extensions.xml_unmarshal_cfg_attribute_AttributeLongNumber(ele); break; 
             
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "AttributeChange");
        ele.WriteStartElement("baseAttr"); cfg.Extensions.xml_marshal_cfg_attribute_AttributeLongNumber(baseAttr,ele); ele.WriteEndElement();
         
    }


}
}
}
