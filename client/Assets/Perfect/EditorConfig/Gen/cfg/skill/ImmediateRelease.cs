using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.skill
{
[System.Serializable]
public  class ImmediateRelease : SkillReleaseRule
 {




    public const int TYPE_ID = 59032;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public ImmediateRelease(Perfect.Marshal.Octets os) : base(os)  {

    }

    public ImmediateRelease()
    {
    }

    public ImmediateRelease(XmlElement e)
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
        ele.WriteAttributeString("type", "ImmediateRelease");
    }


}
}
}
