using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.skill
{
[System.Serializable]
public  class NoRelease : SkillReleaseRule
 {




    public const int TYPE_ID = 2326;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public NoRelease(Perfect.Marshal.Octets os) : base(os)  {

    }

    public NoRelease()
    {
    }

    public NoRelease(XmlElement e)
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
        ele.WriteAttributeString("type", "NoRelease");
    }


}
}
}
