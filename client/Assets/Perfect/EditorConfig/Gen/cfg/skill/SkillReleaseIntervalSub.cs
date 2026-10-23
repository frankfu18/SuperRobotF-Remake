using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.skill
{
[System.Serializable]
public  class SkillReleaseIntervalSub : BuffEffectContent
 {

    public System.Collections.Generic.List<int> skillIds  = new System.Collections.Generic.List<int>();
     public float subRatio ;
     



    public const int TYPE_ID = 37682;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public SkillReleaseIntervalSub(Perfect.Marshal.Octets os) : base(os)  {

        skillIds = cfg.Extensions.unmarshal_list_int(os); 
         subRatio = os.ReadFloat(); 
         
    }

    public SkillReleaseIntervalSub()
    {
    }

    public SkillReleaseIntervalSub(XmlElement e)
    {
        foreach (XmlElement ele in e.GetChildren())
        {
            switch (ele.Name)
            {
            case "skillIds":  skillIds = cfg.Extensions.xml_unmarshal_list_int(ele); break; 
             case "subRatio":  subRatio = ele.ReadFloat(); break; 
             
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "SkillReleaseIntervalSub");
        ele.WriteStartElement("skillIds"); cfg.Extensions.xml_marshal_list_int(skillIds,ele); ele.WriteEndElement();
         ele.WriteStartElement("subRatio"); ele.WriteFloat(subRatio); ele.WriteEndElement();
         
    }


}
}
}
