using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.skill
{
[System.Serializable]
public  class SingleTarget : BulletTargetType
 {




    public const int TYPE_ID = 56255;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public SingleTarget(Perfect.Marshal.Octets os) : base(os)  {

    }

    public SingleTarget()
    {
    }

    public SingleTarget(XmlElement e)
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
        ele.WriteAttributeString("type", "SingleTarget");
    }


}
}
}
