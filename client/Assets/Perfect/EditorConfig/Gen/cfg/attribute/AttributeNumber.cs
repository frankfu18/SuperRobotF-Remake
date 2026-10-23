using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.attribute
{
[System.Serializable]
public  class AttributeNumber : AttributeInfo
 {

    public cfg.attribute.EAttributeType type ;
     public cfg.common.XNumber number ;
     



    public const int TYPE_ID = 20666;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public AttributeNumber(Perfect.Marshal.Octets os) : base(os)  {

        type = (cfg.attribute.EAttributeType)(os.ReadInt()); 
         number = cfg.Extensions.unmarshal_cfg_common_XNumber(os); 
         
    }

    public AttributeNumber()
    {
    }

    public AttributeNumber(XmlElement e)
    {
        foreach (XmlElement ele in e.GetChildren())
        {
            switch (ele.Name)
            {
            case "type":  type = ele.ReadEnum<cfg.attribute.EAttributeType>(); break; 
             case "number":  number = cfg.Extensions.xml_unmarshal_cfg_common_XNumber(ele); break; 
             
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "AttributeNumber");
        ele.WriteStartElement("type"); ele.WriteEnum(type); ele.WriteEndElement();
         ele.WriteStartElement("number"); cfg.Extensions.xml_marshal_cfg_common_XNumber(number,ele); ele.WriteEndElement();
         
    }


}
}
}
