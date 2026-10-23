using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.skill
{
[System.Serializable]
public  class SkillReleaseSplitSatellite : SkillReleaseType
 {

    public int count ;
     public bool sharedHp ;
     public float angle ;
     public float childRadiusRatio ;
     



    public const int TYPE_ID = 35087;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public SkillReleaseSplitSatellite(Perfect.Marshal.Octets os) : base(os)  {

        count = os.ReadInt(); 
         sharedHp = os.ReadBool(); 
         angle = os.ReadFloat(); 
         childRadiusRatio = os.ReadFloat(); 
         
    }

    public SkillReleaseSplitSatellite()
    {
    }

    public SkillReleaseSplitSatellite(XmlElement e)
    {
        foreach (XmlElement ele in e.GetChildren())
        {
            switch (ele.Name)
            {
            case "count":  count = ele.ReadInt(); break; 
             case "sharedHp":  sharedHp = ele.ReadBool(); break; 
             case "angle":  angle = ele.ReadFloat(); break; 
             case "childRadiusRatio":  childRadiusRatio = ele.ReadFloat(); break; 
             
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "SkillReleaseSplitSatellite");
        ele.WriteStartElement("count"); ele.WriteInt(count); ele.WriteEndElement();
         ele.WriteStartElement("sharedHp"); ele.WriteBool(sharedHp); ele.WriteEndElement();
         ele.WriteStartElement("angle"); ele.WriteFloat(angle); ele.WriteEndElement();
         ele.WriteStartElement("childRadiusRatio"); ele.WriteFloat(childRadiusRatio); ele.WriteEndElement();
         
    }


}
}
}
