using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.attribute
{
[System.Serializable]
public  class Attribute :   cfg.EditorWriter 
 {

    public cfg.attribute.EAttributeType attributeType ;
     public string name  = "";
     public string desc  = "";
     public cfg.attribute.EAttrNumberType attrNumberType ;
     



    public const int TYPE_ID = 50555;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public Attribute(Perfect.Marshal.Octets os) {

        attributeType = (cfg.attribute.EAttributeType)(os.ReadInt()); 
         name = os.ReadString(); 
         desc = os.ReadString(); 
         attrNumberType = (cfg.attribute.EAttrNumberType)(os.ReadInt()); 
         
    }

    public Attribute()
    {
    }

    public Attribute(XmlElement e)
    {
        foreach (XmlElement ele in e.GetChildren())
        {
            switch (ele.Name)
            {
            case "attributeType":  attributeType = ele.ReadEnum<cfg.attribute.EAttributeType>(); break; 
             case "name":  name = ele.ReadString2(); break; 
             case "desc":  desc = ele.ReadString2(); break; 
             case "attrNumberType":  attrNumberType = ele.ReadEnum<cfg.attribute.EAttrNumberType>(); break; 
             
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "Attribute");
        ele.WriteStartElement("attributeType"); ele.WriteEnum(attributeType); ele.WriteEndElement();
         ele.WriteStartElement("name"); ele.WriteString2(name); ele.WriteEndElement();
         ele.WriteStartElement("desc"); ele.WriteString2(desc); ele.WriteEndElement();
         ele.WriteStartElement("attrNumberType"); ele.WriteEnum(attrNumberType); ele.WriteEndElement();
         
    }


}
}
}
