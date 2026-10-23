using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.skill
{
[System.Serializable]
public  class EffectiveDamageToHeal : BuffEffectContent
 {

    public cfg.skill.EBuffEffectTarget target ;
     public float ratio ;
     



    public const int TYPE_ID = 7926;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public EffectiveDamageToHeal(Perfect.Marshal.Octets os) : base(os)  {

        target = (cfg.skill.EBuffEffectTarget)(os.ReadInt()); 
         ratio = os.ReadFloat(); 
         
    }

    public EffectiveDamageToHeal()
    {
    }

    public EffectiveDamageToHeal(XmlElement e)
    {
        foreach (XmlElement ele in e.GetChildren())
        {
            switch (ele.Name)
            {
            case "target":  target = ele.ReadEnum<cfg.skill.EBuffEffectTarget>(); break; 
             case "ratio":  ratio = ele.ReadFloat(); break; 
             
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "EffectiveDamageToHeal");
        ele.WriteStartElement("target"); ele.WriteEnum(target); ele.WriteEndElement();
         ele.WriteStartElement("ratio"); ele.WriteFloat(ratio); ele.WriteEndElement();
         
    }


}
}
}
