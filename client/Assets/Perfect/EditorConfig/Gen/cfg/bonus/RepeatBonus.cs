using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.bonus
{
[System.Serializable]
public  class RepeatBonus : Bonus
 {

    public cfg.bonus.Bonus bonus ;
     public int num ;
     



    public const int TYPE_ID = 56236;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public RepeatBonus(Perfect.Marshal.Octets os) : base(os)  {

        bonus = cfg.Extensions.unmarshal_cfg_bonus_Bonus(os); 
         num = os.ReadInt(); 
         
    }

    public RepeatBonus()
    {
    }

    public RepeatBonus(XmlElement e)
    {
        foreach (XmlElement ele in e.GetChildren())
        {
            switch (ele.Name)
            {
            case "bonus":  bonus = cfg.Extensions.xml_unmarshal_cfg_bonus_Bonus(ele); break; 
             case "num":  num = ele.ReadInt(); break; 
             
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "RepeatBonus");
        ele.WriteStartElement("bonus"); cfg.Extensions.xml_marshal_cfg_bonus_Bonus(bonus,ele); ele.WriteEndElement();
         ele.WriteStartElement("num"); ele.WriteInt(num); ele.WriteEndElement();
         
    }


}
}
}
