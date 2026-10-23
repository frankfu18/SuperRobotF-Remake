using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.common
{
[System.Serializable]
public  class Currency :   cfg.EditorWriter 
 {

    public cfg.common.ECurrencyType currencyType ;
     public string name  = "";
     public string desc  = "";
     public cfg.item.EItemQuality quality ;
     public string icon  = "";
     



    public const int TYPE_ID = 32997;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public Currency(Perfect.Marshal.Octets os) {

        currencyType = (cfg.common.ECurrencyType)(os.ReadInt()); 
         name = os.ReadString(); 
         desc = os.ReadString(); 
         quality = (cfg.item.EItemQuality)(os.ReadInt()); 
         icon = os.ReadString(); 
         
    }

    public Currency()
    {
    }

    public Currency(XmlElement e)
    {
        foreach (XmlElement ele in e.GetChildren())
        {
            switch (ele.Name)
            {
            case "currencyType":  currencyType = ele.ReadEnum<cfg.common.ECurrencyType>(); break; 
             case "name":  name = ele.ReadString2(); break; 
             case "desc":  desc = ele.ReadString2(); break; 
             case "quality":  quality = ele.ReadEnum<cfg.item.EItemQuality>(); break; 
             case "icon":  icon = ele.ReadString2(); break; 
             
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "Currency");
        ele.WriteStartElement("currencyType"); ele.WriteEnum(currencyType); ele.WriteEndElement();
         ele.WriteStartElement("name"); ele.WriteString2(name); ele.WriteEndElement();
         ele.WriteStartElement("desc"); ele.WriteString2(desc); ele.WriteEndElement();
         ele.WriteStartElement("quality"); ele.WriteEnum(quality); ele.WriteEndElement();
         ele.WriteStartElement("icon"); ele.WriteString2(icon); ele.WriteEndElement();
         
    }


}
}
}
