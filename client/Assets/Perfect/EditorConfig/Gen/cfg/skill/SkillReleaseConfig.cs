using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.skill
{
[System.Serializable]
public  class SkillReleaseConfig :   cfg.EditorWriter 
 {

    public int id ;
     public string name  = "";
     public string desc  = "";
     public string icon  = "";
     public bool playAnim ;
     public cfg.skill.SkillShowEffect showEffect ;
     public cfg.skill.ETargetEntityType targetEntityType ;
     public cfg.skill.ESkillTargetType targetType ;
     public cfg.skill.SkillReleaseType releaseType ;
     public cfg.skill.SkillReleaseRule releaseRule ;
     



    public const int TYPE_ID = 17610;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public SkillReleaseConfig(Perfect.Marshal.Octets os) {

        id = os.ReadInt(); 
         name = os.ReadString(); 
         desc = os.ReadString(); 
         icon = os.ReadString(); 
         playAnim = os.ReadBool(); 
         showEffect = cfg.Extensions.unmarshal_cfg_skill_SkillShowEffect(os); 
         targetEntityType = (cfg.skill.ETargetEntityType)(os.ReadInt()); 
         targetType = (cfg.skill.ESkillTargetType)(os.ReadInt()); 
         releaseType = cfg.Extensions.unmarshal_cfg_skill_SkillReleaseType(os); 
         releaseRule = cfg.Extensions.unmarshal_cfg_skill_SkillReleaseRule(os); 
         
    }

    public SkillReleaseConfig()
    {
    }

    public SkillReleaseConfig(XmlElement e)
    {
        foreach (XmlElement ele in e.GetChildren())
        {
            switch (ele.Name)
            {
            case "id":  id = ele.ReadInt(); break; 
             case "name":  name = ele.ReadString2(); break; 
             case "desc":  desc = ele.ReadString2(); break; 
             case "icon":  icon = ele.ReadString2(); break; 
             case "playAnim":  playAnim = ele.ReadBool(); break; 
             case "showEffect":  showEffect = cfg.Extensions.xml_unmarshal_cfg_skill_SkillShowEffect(ele); break; 
             case "targetEntityType":  targetEntityType = ele.ReadEnum<cfg.skill.ETargetEntityType>(); break; 
             case "targetType":  targetType = ele.ReadEnum<cfg.skill.ESkillTargetType>(); break; 
             case "releaseType":  releaseType = cfg.Extensions.xml_unmarshal_cfg_skill_SkillReleaseType(ele); break; 
             case "releaseRule":  releaseRule = cfg.Extensions.xml_unmarshal_cfg_skill_SkillReleaseRule(ele); break; 
             
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "SkillReleaseConfig");
        ele.WriteStartElement("id"); ele.WriteInt(id); ele.WriteEndElement();
         ele.WriteStartElement("name"); ele.WriteString2(name); ele.WriteEndElement();
         ele.WriteStartElement("desc"); ele.WriteString2(desc); ele.WriteEndElement();
         ele.WriteStartElement("icon"); ele.WriteString2(icon); ele.WriteEndElement();
         ele.WriteStartElement("playAnim"); ele.WriteBool(playAnim); ele.WriteEndElement();
         ele.WriteStartElement("showEffect"); cfg.Extensions.xml_marshal_cfg_skill_SkillShowEffect(showEffect,ele); ele.WriteEndElement();
         ele.WriteStartElement("targetEntityType"); ele.WriteEnum(targetEntityType); ele.WriteEndElement();
         ele.WriteStartElement("targetType"); ele.WriteEnum(targetType); ele.WriteEndElement();
         ele.WriteStartElement("releaseType"); cfg.Extensions.xml_marshal_cfg_skill_SkillReleaseType(releaseType,ele); ele.WriteEndElement();
         ele.WriteStartElement("releaseRule"); cfg.Extensions.xml_marshal_cfg_skill_SkillReleaseRule(releaseRule,ele); ele.WriteEndElement();
         
    }


}
}
}
