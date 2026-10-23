using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.bonus
{
[System.Serializable]
public  class RandomBonus : Bonus
 {

    public System.Collections.Generic.List<cfg.bonus.Bonus> bonuses  = new System.Collections.Generic.List<cfg.bonus.Bonus>();
     



    public const int TYPE_ID = 6547;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public RandomBonus(Perfect.Marshal.Octets os) : base(os)  {

        bonuses = cfg.Extensions.unmarshal_list_cfg_bonus_Bonus(os); 
         
    }

    public RandomBonus()
    {
    }

    public RandomBonus(XmlElement e)
    {
        foreach (XmlElement ele in e.GetChildren())
        {
            switch (ele.Name)
            {
            case "bonuses":  bonuses = cfg.Extensions.xml_unmarshal_list_cfg_bonus_Bonus(ele); break; 
             
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "RandomBonus");
        ele.WriteStartElement("bonuses"); cfg.Extensions.xml_marshal_list_cfg_bonus_Bonus(bonuses,ele); ele.WriteEndElement();
         
    }


}
}
}
