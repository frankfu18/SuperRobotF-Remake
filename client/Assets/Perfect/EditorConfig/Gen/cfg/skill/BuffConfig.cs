using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.skill
{
[System.Serializable]
public  class BuffConfig :   cfg.EditorWriter 
 {

    public int id ;
     public string desc  = "";
     public cfg.skill.BuffAssetInfo assetInfo ;
     public cfg.skill.BuffTriggerType triggerType ;
     public cfg.skill.BuffDestroyType destroyType ;
     public cfg.skill.EBuffStateType buffStateType ;
     public cfg.skill.BuffStatcking buffStacking ;
     public cfg.skill.BuffEffectContent effectContent ;
     



    public const int TYPE_ID = 65000;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public BuffConfig(Perfect.Marshal.Octets os) {

        id = os.ReadInt(); 
         desc = os.ReadString(); 
         assetInfo = cfg.Extensions.unmarshal_cfg_skill_BuffAssetInfo(os); 
         triggerType = cfg.Extensions.unmarshal_cfg_skill_BuffTriggerType(os); 
         destroyType = cfg.Extensions.unmarshal_cfg_skill_BuffDestroyType(os); 
         buffStateType = (cfg.skill.EBuffStateType)(os.ReadInt()); 
         buffStacking = cfg.Extensions.unmarshal_cfg_skill_BuffStatcking(os); 
         effectContent = cfg.Extensions.unmarshal_cfg_skill_BuffEffectContent(os); 
         
    }

    public BuffConfig()
    {
    }

    public BuffConfig(XmlElement e)
    {
        foreach (XmlElement ele in e.GetChildren())
        {
            switch (ele.Name)
            {
            case "id":  id = ele.ReadInt(); break; 
             case "desc":  desc = ele.ReadString2(); break; 
             case "assetInfo":  assetInfo = cfg.Extensions.xml_unmarshal_cfg_skill_BuffAssetInfo(ele); break; 
             case "triggerType":  triggerType = cfg.Extensions.xml_unmarshal_cfg_skill_BuffTriggerType(ele); break; 
             case "destroyType":  destroyType = cfg.Extensions.xml_unmarshal_cfg_skill_BuffDestroyType(ele); break; 
             case "buffStateType":  buffStateType = ele.ReadEnum<cfg.skill.EBuffStateType>(); break; 
             case "buffStacking":  buffStacking = cfg.Extensions.xml_unmarshal_cfg_skill_BuffStatcking(ele); break; 
             case "effectContent":  effectContent = cfg.Extensions.xml_unmarshal_cfg_skill_BuffEffectContent(ele); break; 
             
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "BuffConfig");
        ele.WriteStartElement("id"); ele.WriteInt(id); ele.WriteEndElement();
         ele.WriteStartElement("desc"); ele.WriteString2(desc); ele.WriteEndElement();
         ele.WriteStartElement("assetInfo"); cfg.Extensions.xml_marshal_cfg_skill_BuffAssetInfo(assetInfo,ele); ele.WriteEndElement();
         ele.WriteStartElement("triggerType"); cfg.Extensions.xml_marshal_cfg_skill_BuffTriggerType(triggerType,ele); ele.WriteEndElement();
         ele.WriteStartElement("destroyType"); cfg.Extensions.xml_marshal_cfg_skill_BuffDestroyType(destroyType,ele); ele.WriteEndElement();
         ele.WriteStartElement("buffStateType"); ele.WriteEnum(buffStateType); ele.WriteEndElement();
         ele.WriteStartElement("buffStacking"); cfg.Extensions.xml_marshal_cfg_skill_BuffStatcking(buffStacking,ele); ele.WriteEndElement();
         ele.WriteStartElement("effectContent"); cfg.Extensions.xml_marshal_cfg_skill_BuffEffectContent(effectContent,ele); ele.WriteEndElement();
         
    }


}
}
}
