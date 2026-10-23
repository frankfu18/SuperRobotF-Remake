using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.skill
{
[System.Serializable]
public  class SkillReleaseBuff : SkillReleaseType
 {

    public int buffId ;
     



    public const int TYPE_ID = 7587;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public SkillReleaseBuff(Perfect.Marshal.Octets os) : base(os)  {

        buffId = os.ReadInt(); 
         
    }

    public SkillReleaseBuff()
    {
    }

    public SkillReleaseBuff(XmlElement e)
    {
        foreach (XmlElement ele in e.GetChildren())
        {
            switch (ele.Name)
            {
            case "buffId":  buffId = ele.ReadInt(); break; 
             
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "SkillReleaseBuff");
        ele.WriteStartElement("buffId"); ele.WriteInt(buffId); ele.WriteEndElement();
         
    }


}
}
}
