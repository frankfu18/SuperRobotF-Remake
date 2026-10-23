using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.bonus
{
[System.Serializable]
public  class BonusData :   cfg.EditorWriter 
 {

    public int id ;
     public cfg.bonus.Bonus bonus ;
     



    public const int TYPE_ID = 5832;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public BonusData(Perfect.Marshal.Octets os) {

        id = os.ReadInt(); 
         bonus = cfg.Extensions.unmarshal_cfg_bonus_Bonus(os); 
         
    }

    public BonusData()
    {
    }

    public BonusData(XmlElement e)
    {
        foreach (XmlElement ele in e.GetChildren())
        {
            switch (ele.Name)
            {
            case "id":  id = ele.ReadInt(); break; 
             case "bonus":  bonus = cfg.Extensions.xml_unmarshal_cfg_bonus_Bonus(ele); break; 
             
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "BonusData");
        ele.WriteStartElement("id"); ele.WriteInt(id); ele.WriteEndElement();
         ele.WriteStartElement("bonus"); cfg.Extensions.xml_marshal_cfg_bonus_Bonus(bonus,ele); ele.WriteEndElement();
         
    }


}
}
}
