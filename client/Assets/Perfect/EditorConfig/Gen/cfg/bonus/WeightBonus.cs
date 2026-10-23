using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.bonus
{
[System.Serializable]
public  class WeightBonus : Bonus
 {

    public cfg.bonus.Bonus bonus ;
     public int weight ;
     



    public const int TYPE_ID = 11336;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public WeightBonus(Perfect.Marshal.Octets os) : base(os)  {

        bonus = cfg.Extensions.unmarshal_cfg_bonus_Bonus(os); 
         weight = os.ReadInt(); 
         
    }

    public WeightBonus()
    {
    }

    public WeightBonus(XmlElement e)
    {
        foreach (XmlElement ele in e.GetChildren())
        {
            switch (ele.Name)
            {
            case "bonus":  bonus = cfg.Extensions.xml_unmarshal_cfg_bonus_Bonus(ele); break; 
             case "weight":  weight = ele.ReadInt(); break; 
             
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "WeightBonus");
        ele.WriteStartElement("bonus"); cfg.Extensions.xml_marshal_cfg_bonus_Bonus(bonus,ele); ele.WriteEndElement();
         ele.WriteStartElement("weight"); ele.WriteInt(weight); ele.WriteEndElement();
         
    }


}
}
}
