using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.skill
{
[System.Serializable]
public  class BulletConfig :   cfg.EditorWriter 
 {

    public int id ;
     public string assetId  = "";
     public string hitAssetId  = "";
     public cfg.skill.BulletType bulletType ;
     public cfg.skill.BulletTargetType targetType ;
     public cfg.skill.BulletReleaseCount releaseCount ;
     public cfg.skill.BulletHitEffectType hitEffect ;
     



    public const int TYPE_ID = 49182;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public BulletConfig(Perfect.Marshal.Octets os) {

        id = os.ReadInt(); 
         assetId = os.ReadString(); 
         hitAssetId = os.ReadString(); 
         bulletType = cfg.Extensions.unmarshal_cfg_skill_BulletType(os); 
         targetType = cfg.Extensions.unmarshal_cfg_skill_BulletTargetType(os); 
         releaseCount = cfg.Extensions.unmarshal_cfg_skill_BulletReleaseCount(os); 
         hitEffect = cfg.Extensions.unmarshal_cfg_skill_BulletHitEffectType(os); 
         
    }

    public BulletConfig()
    {
    }

    public BulletConfig(XmlElement e)
    {
        foreach (XmlElement ele in e.GetChildren())
        {
            switch (ele.Name)
            {
            case "id":  id = ele.ReadInt(); break; 
             case "assetId":  assetId = ele.ReadString2(); break; 
             case "hitAssetId":  hitAssetId = ele.ReadString2(); break; 
             case "bulletType":  bulletType = cfg.Extensions.xml_unmarshal_cfg_skill_BulletType(ele); break; 
             case "targetType":  targetType = cfg.Extensions.xml_unmarshal_cfg_skill_BulletTargetType(ele); break; 
             case "releaseCount":  releaseCount = cfg.Extensions.xml_unmarshal_cfg_skill_BulletReleaseCount(ele); break; 
             case "hitEffect":  hitEffect = cfg.Extensions.xml_unmarshal_cfg_skill_BulletHitEffectType(ele); break; 
             
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "BulletConfig");
        ele.WriteStartElement("id"); ele.WriteInt(id); ele.WriteEndElement();
         ele.WriteStartElement("assetId"); ele.WriteString2(assetId); ele.WriteEndElement();
         ele.WriteStartElement("hitAssetId"); ele.WriteString2(hitAssetId); ele.WriteEndElement();
         ele.WriteStartElement("bulletType"); cfg.Extensions.xml_marshal_cfg_skill_BulletType(bulletType,ele); ele.WriteEndElement();
         ele.WriteStartElement("targetType"); cfg.Extensions.xml_marshal_cfg_skill_BulletTargetType(targetType,ele); ele.WriteEndElement();
         ele.WriteStartElement("releaseCount"); cfg.Extensions.xml_marshal_cfg_skill_BulletReleaseCount(releaseCount,ele); ele.WriteEndElement();
         ele.WriteStartElement("hitEffect"); cfg.Extensions.xml_marshal_cfg_skill_BulletHitEffectType(hitEffect,ele); ele.WriteEndElement();
         
    }


}
}
}
