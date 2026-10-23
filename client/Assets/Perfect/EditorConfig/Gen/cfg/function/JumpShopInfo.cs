using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.function
{
[System.Serializable]
public  class JumpShopInfo : JumpInfo
 {




    public const int TYPE_ID = 22130;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public JumpShopInfo(Perfect.Marshal.Octets os) : base(os)  {

    }

    public JumpShopInfo()
    {
    }

    public JumpShopInfo(XmlElement e)
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
        ele.WriteAttributeString("type", "JumpShopInfo");
    }


}
}
}
