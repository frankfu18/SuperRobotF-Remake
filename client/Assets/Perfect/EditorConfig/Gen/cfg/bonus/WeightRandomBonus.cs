using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.bonus
{
[System.Serializable]
public  class WeightRandomBonus : Bonus
 {

    public System.Collections.Generic.List<cfg.bonus.WeightBonus> bonuses  = new System.Collections.Generic.List<cfg.bonus.WeightBonus>();
     



    public const int TYPE_ID = 17310;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public WeightRandomBonus(Perfect.Marshal.Octets os) : base(os)  {

        bonuses = cfg.Extensions.unmarshal_list_cfg_bonus_WeightBonus(os); 
         
    }

    public WeightRandomBonus()
    {
    }

    public WeightRandomBonus(XmlElement e)
    {
        foreach (XmlElement ele in e.GetChildren())
        {
            switch (ele.Name)
            {
            case "bonuses":  bonuses = cfg.Extensions.xml_unmarshal_list_cfg_bonus_WeightBonus(ele); break; 
             
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "WeightRandomBonus");
        ele.WriteStartElement("bonuses"); cfg.Extensions.xml_marshal_list_cfg_bonus_WeightBonus(bonuses,ele); ele.WriteEndElement();
         
    }


}
}
}
