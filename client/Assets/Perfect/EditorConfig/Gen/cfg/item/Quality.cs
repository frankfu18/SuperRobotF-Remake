using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.item
{
[System.Serializable]
public  class Quality :   cfg.EditorWriter 
 {

    public cfg.item.EItemQuality quality ;
     public string itemBg  = "";
     public string cardFrame  = "";
     public string equipBigBg  = "";
     public string equipColor  = "";
     



    public const int TYPE_ID = 29592;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public Quality(Perfect.Marshal.Octets os) {

        quality = (cfg.item.EItemQuality)(os.ReadInt()); 
         itemBg = os.ReadString(); 
         cardFrame = os.ReadString(); 
         equipBigBg = os.ReadString(); 
         equipColor = os.ReadString(); 
         
    }

    public Quality()
    {
    }

    public Quality(XmlElement e)
    {
        foreach (XmlElement ele in e.GetChildren())
        {
            switch (ele.Name)
            {
            case "quality":  quality = ele.ReadEnum<cfg.item.EItemQuality>(); break; 
             case "itemBg":  itemBg = ele.ReadString2(); break; 
             case "cardFrame":  cardFrame = ele.ReadString2(); break; 
             case "equipBigBg":  equipBigBg = ele.ReadString2(); break; 
             case "equipColor":  equipColor = ele.ReadString2(); break; 
             
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "Quality");
        ele.WriteStartElement("quality"); ele.WriteEnum(quality); ele.WriteEndElement();
         ele.WriteStartElement("itemBg"); ele.WriteString2(itemBg); ele.WriteEndElement();
         ele.WriteStartElement("cardFrame"); ele.WriteString2(cardFrame); ele.WriteEndElement();
         ele.WriteStartElement("equipBigBg"); ele.WriteString2(equipBigBg); ele.WriteEndElement();
         ele.WriteStartElement("equipColor"); ele.WriteString2(equipColor); ele.WriteEndElement();
         
    }


}
}
}
