using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.bonus
{
[System.Serializable]
public  class ItemId : Bonus
 {

    public int itemId ;
     



    public const int TYPE_ID = 21920;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public ItemId(Perfect.Marshal.Octets os) : base(os)  {

        itemId = os.ReadInt(); 
         
    }

    public ItemId()
    {
    }

    public ItemId(XmlElement e)
    {
        foreach (XmlElement ele in e.GetChildren())
        {
            switch (ele.Name)
            {
            case "itemId":  itemId = ele.ReadInt(); break; 
             
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "ItemId");
        ele.WriteStartElement("itemId"); ele.WriteInt(itemId); ele.WriteEndElement();
         
    }


}
}
}
