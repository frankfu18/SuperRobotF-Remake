using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.skill
{
[System.Serializable]
public  class TargetInRangeRelease : SkillReleaseRule
 {

    public float radius ;
     



    public const int TYPE_ID = 31335;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public TargetInRangeRelease(Perfect.Marshal.Octets os) : base(os)  {

        radius = os.ReadFloat(); 
         
    }

    public TargetInRangeRelease()
    {
    }

    public TargetInRangeRelease(XmlElement e)
    {
        foreach (XmlElement ele in e.GetChildren())
        {
            switch (ele.Name)
            {
            case "radius":  radius = ele.ReadFloat(); break; 
             
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "TargetInRangeRelease");
        ele.WriteStartElement("radius"); ele.WriteFloat(radius); ele.WriteEndElement();
         
    }


}
}
}
