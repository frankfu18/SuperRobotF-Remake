using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.common
{
[System.Serializable]
public  class ItemCost : Cost
 {

    public int itemId ;
     public long itemCount ;
     



    public const int TYPE_ID = 46930;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public ItemCost(Perfect.Marshal.Octets os) : base(os)  {

        itemId = os.ReadInt(); 
         itemCount = os.ReadLong(); 
         
    }

    public ItemCost()
    {
    }

    public ItemCost(XmlElement e)
    {
        foreach (XmlElement ele in e.GetChildren())
        {
            switch (ele.Name)
            {
            case "itemId":  itemId = ele.ReadInt(); break; 
             case "itemCount":  itemCount = ele.ReadLong(); break; 
             
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "ItemCost");
        ele.WriteStartElement("itemId"); ele.WriteInt(itemId); ele.WriteEndElement();
         ele.WriteStartElement("itemCount"); ele.WriteLong(itemCount); ele.WriteEndElement();
         
    }


}
}
}
