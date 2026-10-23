using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.condition
{
[System.Serializable]
public  class Item : Condition
 {

    public int itemId ;
     public long amount ;
     



    public const int TYPE_ID = 65080;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public Item(Perfect.Marshal.Octets os) : base(os)  {

        itemId = os.ReadInt(); 
         amount = os.ReadLong(); 
         
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
             case "amount":  amount = ele.ReadLong(); break; 
             
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "Item");
        ele.WriteStartElement("itemId"); ele.WriteInt(itemId); ele.WriteEndElement();
         ele.WriteStartElement("amount"); ele.WriteLong(amount); ele.WriteEndElement();
         
    }


}
}
}
