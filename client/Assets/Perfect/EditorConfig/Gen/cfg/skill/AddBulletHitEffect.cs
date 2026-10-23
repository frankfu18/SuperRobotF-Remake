using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.skill
{
[System.Serializable]
public  class AddBulletHitEffect : BuffEffectContent
 {

    public System.Collections.Generic.List<int> bulletIds  = new System.Collections.Generic.List<int>();
     public cfg.skill.BulletHitEffectType hitEffect ;
     



    public const int TYPE_ID = 60175;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public AddBulletHitEffect(Perfect.Marshal.Octets os) : base(os)  {

        bulletIds = cfg.Extensions.unmarshal_list_int(os); 
         hitEffect = cfg.Extensions.unmarshal_cfg_skill_BulletHitEffectType(os); 
         
    }

    public AddBulletHitEffect()
    {
    }

    public AddBulletHitEffect(XmlElement e)
    {
        foreach (XmlElement ele in e.GetChildren())
        {
            switch (ele.Name)
            {
            case "bulletIds":  bulletIds = cfg.Extensions.xml_unmarshal_list_int(ele); break; 
             case "hitEffect":  hitEffect = cfg.Extensions.xml_unmarshal_cfg_skill_BulletHitEffectType(ele); break; 
             
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "AddBulletHitEffect");
        ele.WriteStartElement("bulletIds"); cfg.Extensions.xml_marshal_list_int(bulletIds,ele); ele.WriteEndElement();
         ele.WriteStartElement("hitEffect"); cfg.Extensions.xml_marshal_cfg_skill_BulletHitEffectType(hitEffect,ele); ele.WriteEndElement();
         
    }


}
}
}
