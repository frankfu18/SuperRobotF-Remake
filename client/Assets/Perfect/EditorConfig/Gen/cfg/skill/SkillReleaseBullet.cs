using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.skill
{
[System.Serializable]
public  class SkillReleaseBullet : SkillReleaseType
 {

    public int bulletId ;
     



    public const int TYPE_ID = 25001;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public SkillReleaseBullet(Perfect.Marshal.Octets os) : base(os)  {

        bulletId = os.ReadInt(); 
         
    }

    public SkillReleaseBullet()
    {
    }

    public SkillReleaseBullet(XmlElement e)
    {
        foreach (XmlElement ele in e.GetChildren())
        {
            switch (ele.Name)
            {
            case "bulletId":  bulletId = ele.ReadInt(); break; 
             
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "SkillReleaseBullet");
        ele.WriteStartElement("bulletId"); ele.WriteInt(bulletId); ele.WriteEndElement();
         
    }


}
}
}
