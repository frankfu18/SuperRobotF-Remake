using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.item
{
[System.Serializable]
public  class ArenaBoxInfo : ItemInfo
 {

    public int bonusId ;
     



    public const int TYPE_ID = 46661;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public ArenaBoxInfo(Perfect.Marshal.Octets os) : base(os)  {

        bonusId = os.ReadInt(); 
         
    }

    public ArenaBoxInfo()
    {
    }

    public ArenaBoxInfo(XmlElement e)
    {
        foreach (XmlElement ele in e.GetChildren())
        {
            switch (ele.Name)
            {
            case "bonusId":  bonusId = ele.ReadInt(); break; 
             
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "ArenaBoxInfo");
        ele.WriteStartElement("bonusId"); ele.WriteInt(bonusId); ele.WriteEndElement();
         
    }


}
}
}
