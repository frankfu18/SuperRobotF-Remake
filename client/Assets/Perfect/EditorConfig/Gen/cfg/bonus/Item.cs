using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.bonus
{
[System.Serializable]
public  class Item : Bonus
 {

    public int itemId ;
     public int amount ;
     



    public const int TYPE_ID = 9164;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public Item(Perfect.Marshal.Octets os) : base(os)  {

        itemId = os.ReadInt(); 
         amount = os.ReadInt(); 
         
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
            case "itemId":  itemId = ele.ReadInt(); break; 
             case "amount":  amount = ele.ReadInt(); break; 
             
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "Item");
        ele.WriteStartElement("itemId"); ele.WriteInt(itemId); ele.WriteEndElement();
         ele.WriteStartElement("amount"); ele.WriteInt(amount); ele.WriteEndElement();
         
    }


}
}
}
