using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.skill
{
[System.Serializable]
public  class DirectionTarget : BulletTargetType
 {




    public const int TYPE_ID = 64845;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public DirectionTarget(Perfect.Marshal.Octets os) : base(os)  {

    }

    public DirectionTarget()
    {
    }

    public DirectionTarget(XmlElement e)
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
        ele.WriteAttributeString("type", "DirectionTarget");
    }


}
}
}
