using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.attribute
{
[System.Serializable]
public  class AttributeLongNumber : AttributeInfo
 {

    public cfg.attribute.EAttributeType type ;
     public cfg.common.XLong number  = new cfg.common.XLong();
     



    public const int TYPE_ID = 35280;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public AttributeLongNumber(Perfect.Marshal.Octets os) : base(os)  {

        type = (cfg.attribute.EAttributeType)(os.ReadInt()); 
         number = cfg.Extensions.unmarshal_cfg_common_XLong(os); 
         
    }

    public AttributeLongNumber()
    {
    }

    public AttributeLongNumber(XmlElement e)
    {
        foreach (XmlElement ele in e.GetChildren())
        {
            switch (ele.Name)
            {
            case "type":  type = ele.ReadEnum<cfg.attribute.EAttributeType>(); break; 
             case "number":  number = cfg.Extensions.xml_unmarshal_cfg_common_XLong(ele); break; 
             
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "AttributeLongNumber");
        ele.WriteStartElement("type"); ele.WriteEnum(type); ele.WriteEndElement();
         ele.WriteStartElement("number"); cfg.Extensions.xml_marshal_cfg_common_XLong(number,ele); ele.WriteEndElement();
         
    }


}
}
}
