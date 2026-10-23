using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.item
{
[System.Serializable]
public  class None : ItemInfo
 {




    public const int TYPE_ID = 7166;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public None(Perfect.Marshal.Octets os) : base(os)  {

    }

    public None()
    {
    }

    public None(XmlElement e)
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
        ele.WriteAttributeString("type", "None");
    }


}
}
}
