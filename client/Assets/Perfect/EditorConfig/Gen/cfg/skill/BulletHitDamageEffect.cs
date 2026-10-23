using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.skill
{
[System.Serializable]
public  class BulletHitDamageEffect : BulletHitEffectType
 {

    public float atkRatio ;
     public cfg.attribute.EDamageType damageType ;
     



    public const int TYPE_ID = 33271;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public BulletHitDamageEffect(Perfect.Marshal.Octets os) : base(os)  {

        atkRatio = os.ReadFloat(); 
         damageType = (cfg.attribute.EDamageType)(os.ReadInt()); 
         
    }

    public BulletHitDamageEffect()
    {
    }

    public BulletHitDamageEffect(XmlElement e)
    {
        foreach (XmlElement ele in e.GetChildren())
        {
            switch (ele.Name)
            {
            case "atkRatio":  atkRatio = ele.ReadFloat(); break; 
             case "damageType":  damageType = ele.ReadEnum<cfg.attribute.EDamageType>(); break; 
             
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "BulletHitDamageEffect");
        ele.WriteStartElement("atkRatio"); ele.WriteFloat(atkRatio); ele.WriteEndElement();
         ele.WriteStartElement("damageType"); ele.WriteEnum(damageType); ele.WriteEndElement();
         
    }


}
}
}
