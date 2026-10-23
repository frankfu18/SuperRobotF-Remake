using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.skill
{
[System.Serializable]
public  class ImmediateBullet : BulletType
 {




    public const int TYPE_ID = 4599;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public ImmediateBullet(Perfect.Marshal.Octets os) : base(os)  {

    }

    public ImmediateBullet()
    {
    }

    public ImmediateBullet(XmlElement e)
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
        ele.WriteAttributeString("type", "ImmediateBullet");
    }


}
}
}
