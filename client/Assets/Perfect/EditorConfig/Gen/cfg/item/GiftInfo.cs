using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.item
{
[System.Serializable]
public  class GiftInfo : ItemInfo
 {

    public int bonusId ;
     public bool autoOpen ;
     



    public const int TYPE_ID = 46305;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public GiftInfo(Perfect.Marshal.Octets os) : base(os)  {

        bonusId = os.ReadInt(); 
         autoOpen = os.ReadBool(); 
         
    }

    public GiftInfo()
    {
    }

    public GiftInfo(XmlElement e)
    {
        foreach (XmlElement ele in e.GetChildren())
        {
            switch (ele.Name)
            {
            case "bonusId":  bonusId = ele.ReadInt(); break; 
             case "autoOpen":  autoOpen = ele.ReadBool(); break; 
             
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "GiftInfo");
        ele.WriteStartElement("bonusId"); ele.WriteInt(bonusId); ele.WriteEndElement();
         ele.WriteStartElement("autoOpen"); ele.WriteBool(autoOpen); ele.WriteEndElement();
         
    }


}
}
}
