using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.condition
{
[System.Serializable]
public  class OwnItem : Condition
 {

    public int itemId ;
     



    public const int TYPE_ID = 23955;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public OwnItem(Perfect.Marshal.Octets os) : base(os)  {

        itemId = os.ReadInt(); 
         
    }

    public OwnItem()
    {
    }

    public OwnItem(XmlElement e)
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
        ele.WriteAttributeString("type", "OwnItem");
        ele.WriteStartElement("itemId"); ele.WriteInt(itemId); ele.WriteEndElement();
         
    }


}
}
}
