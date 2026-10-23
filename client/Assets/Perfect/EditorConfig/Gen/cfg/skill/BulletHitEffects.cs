using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.skill
{
[System.Serializable]
public  class BulletHitEffects : BulletHitEffectType
 {

    public System.Collections.Generic.List<cfg.skill.BulletHitEffectType> effects  = new System.Collections.Generic.List<cfg.skill.BulletHitEffectType>();
     



    public const int TYPE_ID = 7475;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public BulletHitEffects(Perfect.Marshal.Octets os) : base(os)  {

        effects = cfg.Extensions.unmarshal_list_cfg_skill_BulletHitEffectType(os); 
         
    }

    public BulletHitEffects()
    {
    }

    public BulletHitEffects(XmlElement e)
    {
        foreach (XmlElement ele in e.GetChildren())
        {
            switch (ele.Name)
            {
            case "effects":  effects = cfg.Extensions.xml_unmarshal_list_cfg_skill_BulletHitEffectType(ele); break; 
             
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "BulletHitEffects");
        ele.WriteStartElement("effects"); cfg.Extensions.xml_marshal_list_cfg_skill_BulletHitEffectType(effects,ele); ele.WriteEndElement();
         
    }


}
}
}
