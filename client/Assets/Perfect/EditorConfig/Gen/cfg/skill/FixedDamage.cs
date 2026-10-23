using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.skill
{
[System.Serializable]
public  class FixedDamage : BuffEffectContent
 {

    public int damage ;
     public cfg.attribute.EDamageType damageType ;
     



    public const int TYPE_ID = 4920;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public FixedDamage(Perfect.Marshal.Octets os) : base(os)  {

        damage = os.ReadInt(); 
         damageType = (cfg.attribute.EDamageType)(os.ReadInt()); 
         
    }

    public FixedDamage()
    {
    }

    public FixedDamage(XmlElement e)
    {
        foreach (XmlElement ele in e.GetChildren())
        {
            switch (ele.Name)
            {
            case "damage":  damage = ele.ReadInt(); break; 
             case "damageType":  damageType = ele.ReadEnum<cfg.attribute.EDamageType>(); break; 
             
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "FixedDamage");
        ele.WriteStartElement("damage"); ele.WriteInt(damage); ele.WriteEndElement();
         ele.WriteStartElement("damageType"); ele.WriteEnum(damageType); ele.WriteEndElement();
         
    }


}
}
}
