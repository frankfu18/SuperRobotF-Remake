using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.skill
{
[System.Serializable]
public  class BeforeCollisionDamageTrigger : BuffTriggerType
 {

    public cfg.skill.ETargetEntityType targetEntityType ;
     



    public const int TYPE_ID = 16931;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public BeforeCollisionDamageTrigger(Perfect.Marshal.Octets os) : base(os)  {

        targetEntityType = (cfg.skill.ETargetEntityType)(os.ReadInt()); 
         
    }

    public BeforeCollisionDamageTrigger()
    {
    }

    public BeforeCollisionDamageTrigger(XmlElement e)
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
        ele.WriteAttributeString("type", "BeforeCollisionDamageTrigger");
        ele.WriteStartElement("ratio"); ele.WriteInt(ratio); ele.WriteEndElement();
         ele.WriteStartElement("targetEntityType"); ele.WriteEnum(targetEntityType); ele.WriteEndElement();
         
    }


}
}
}
