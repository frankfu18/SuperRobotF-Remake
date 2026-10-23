using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.skill
{
[System.Serializable]
public  class DealEffectiveDamageTrigger : BuffTriggerType
 {

    public cfg.skill.ETargetEntityType targetEntityType ;
     



    public const int TYPE_ID = 22375;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public DealEffectiveDamageTrigger(Perfect.Marshal.Octets os) : base(os)  {

        targetEntityType = (cfg.skill.ETargetEntityType)(os.ReadInt()); 
         
    }

    public DealEffectiveDamageTrigger()
    {
    }

    public DealEffectiveDamageTrigger(XmlElement e)
    {
        foreach (XmlElement ele in e.GetChildren())
        {
            switch (ele.Name)
            {
            case "ratio":  ratio = ele.ReadInt(); break; 
             case "targetEntityType":  targetEntityType = ele.ReadEnum<cfg.skill.ETargetEntityType>(); break; 
             
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "DealEffectiveDamageTrigger");
        ele.WriteStartElement("ratio"); ele.WriteInt(ratio); ele.WriteEndElement();
         ele.WriteStartElement("targetEntityType"); ele.WriteEnum(targetEntityType); ele.WriteEndElement();
         
    }


}
}
}
