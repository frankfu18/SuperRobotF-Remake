using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.item
{
[System.Serializable]
public  class Item :   cfg.EditorWriter 
 {

    public int id ;
     public cfg.item.EItemQuality quality ;
     public cfg.item.EItemType itemType ;
     public string name  = "";
     public string desc  = "";
     public string icon  = "";
     public cfg.item.ItemInfo itemInfo ;
     



    public const int TYPE_ID = 20513;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public Item(Perfect.Marshal.Octets os) {

        id = os.ReadInt(); 
         quality = (cfg.item.EItemQuality)(os.ReadInt()); 
         itemType = (cfg.item.EItemType)(os.ReadInt()); 
         name = os.ReadString(); 
         desc = os.ReadString(); 
         icon = os.ReadString(); 
         itemInfo = cfg.Extensions.unmarshal_cfg_item_ItemInfo(os); 
         
    }

    public Item()
    {
    }

    public Item(XmlElement e)
    {
        foreach (XmlElement ele in e.GetChildren())
        {
            switch (ele.Name)
            {
            case "id":  id = ele.ReadInt(); break; 
             case "quality":  quality = ele.ReadEnum<cfg.item.EItemQuality>(); break; 
             case "itemType":  itemType = ele.ReadEnum<cfg.item.EItemType>(); break; 
             case "name":  name = ele.ReadString2(); break; 
             case "desc":  desc = ele.ReadString2(); break; 
             case "icon":  icon = ele.ReadString2(); break; 
             case "itemInfo":  itemInfo = cfg.Extensions.xml_unmarshal_cfg_item_ItemInfo(ele); break; 
             
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "Item");
        ele.WriteStartElement("id"); ele.WriteInt(id); ele.WriteEndElement();
         ele.WriteStartElement("quality"); ele.WriteEnum(quality); ele.WriteEndElement();
         ele.WriteStartElement("itemType"); ele.WriteEnum(itemType); ele.WriteEndElement();
         ele.WriteStartElement("name"); ele.WriteString2(name); ele.WriteEndElement();
         ele.WriteStartElement("desc"); ele.WriteString2(desc); ele.WriteEndElement();
         ele.WriteStartElement("icon"); ele.WriteString2(icon); ele.WriteEndElement();
         ele.WriteStartElement("itemInfo"); cfg.Extensions.xml_marshal_cfg_item_ItemInfo(itemInfo,ele); ele.WriteEndElement();
         
    }


}
}
}
