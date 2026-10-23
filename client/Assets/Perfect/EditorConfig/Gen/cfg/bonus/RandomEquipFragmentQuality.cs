using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.bonus
{
[System.Serializable]
public  class RandomEquipFragmentQuality : Bonus
 {

    public cfg.item.EItemQuality quality ;
     public int count ;
     



    public const int TYPE_ID = 23945;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public RandomEquipFragmentQuality(Perfect.Marshal.Octets os) : base(os)  {

        quality = (cfg.item.EItemQuality)(os.ReadInt()); 
         count = os.ReadInt(); 
         
    }

    public RandomEquipFragmentQuality()
    {
    }

    public RandomEquipFragmentQuality(XmlElement e)
    {
        foreach (XmlElement ele in e.GetChildren())
        {
            switch (ele.Name)
            {
            case "quality":  quality = ele.ReadEnum<cfg.item.EItemQuality>(); break; 
             case "count":  count = ele.ReadInt(); break; 
             
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "RandomEquipFragmentQuality");
        ele.WriteStartElement("quality"); ele.WriteEnum(quality); ele.WriteEndElement();
         ele.WriteStartElement("count"); ele.WriteInt(count); ele.WriteEndElement();
         
    }


}
}
}
