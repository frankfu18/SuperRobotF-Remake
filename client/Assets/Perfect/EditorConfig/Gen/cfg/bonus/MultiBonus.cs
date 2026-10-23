using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.bonus
{
[System.Serializable]
public  class MultiBonus : Bonus
 {

    public System.Collections.Generic.List<cfg.bonus.Bonus> bonuses  = new System.Collections.Generic.List<cfg.bonus.Bonus>();
     



    public const int TYPE_ID = 57487;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public MultiBonus(Perfect.Marshal.Octets os) : base(os)  {

        bonuses = cfg.Extensions.unmarshal_list_cfg_bonus_Bonus(os); 
         
    }

    public MultiBonus()
    {
    }

    public MultiBonus(XmlElement e)
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
        ele.WriteAttributeString("type", "MultiBonus");
        ele.WriteStartElement("bonuses"); cfg.Extensions.xml_marshal_list_cfg_bonus_Bonus(bonuses,ele); ele.WriteEndElement();
         
    }


}
}
}
