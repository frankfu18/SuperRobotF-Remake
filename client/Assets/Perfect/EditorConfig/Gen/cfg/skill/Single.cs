using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.skill
{
[System.Serializable]
public  class Single : BulletReleaseCount
 {




    public const int TYPE_ID = 36854;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public Single(Perfect.Marshal.Octets os) : base(os)  {

    }

    public Single()
    {
    }

    public Single(XmlElement e)
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
        ele.WriteAttributeString("type", "Single");
    }


}
}
}
