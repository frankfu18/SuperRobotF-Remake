using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.skill
{
[System.Serializable]
public  class ReleaseBulletAtCollisionPoint : BuffEffectContent
 {

    public int bulletId ;
     public cfg.skill.ETargetEntityType targetEntityType ;
     



    public const int TYPE_ID = 60019;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public ReleaseBulletAtCollisionPoint(Perfect.Marshal.Octets os) : base(os)  {

        bulletId = os.ReadInt(); 
         targetEntityType = (cfg.skill.ETargetEntityType)(os.ReadInt()); 
         
    }

    public ReleaseBulletAtCollisionPoint()
    {
    }

    public ReleaseBulletAtCollisionPoint(XmlElement e)
    {
        foreach (XmlElement ele in e.GetChildren())
        {
            switch (ele.Name)
            {
            case "bulletId":  bulletId = ele.ReadInt(); break; 
             case "targetEntityType":  targetEntityType = ele.ReadEnum<cfg.skill.ETargetEntityType>(); break; 
             
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "ReleaseBulletAtCollisionPoint");
        ele.WriteStartElement("bulletId"); ele.WriteInt(bulletId); ele.WriteEndElement();
         ele.WriteStartElement("targetEntityType"); ele.WriteEnum(targetEntityType); ele.WriteEndElement();
         
    }


}
}
}
