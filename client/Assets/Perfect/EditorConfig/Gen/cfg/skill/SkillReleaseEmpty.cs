using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.skill
{
[System.Serializable]
public  class SkillReleaseEmpty : SkillReleaseType
 {




    public const int TYPE_ID = 52118;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public SkillReleaseEmpty(Perfect.Marshal.Octets os) : base(os)  {

    }

    public SkillReleaseEmpty()
    {
    }

    public SkillReleaseEmpty(XmlElement e)
    {
        foreach (XmlElement ele in e.GetChildren())
        {
            switch (ele.Name)
            {
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "SkillReleaseEmpty");
    }


}
}
}
