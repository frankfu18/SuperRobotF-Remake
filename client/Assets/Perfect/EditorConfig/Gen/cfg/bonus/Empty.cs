using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.bonus
{
[System.Serializable]
public  class Empty : Bonus
 {




    public const int TYPE_ID = 18584;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public Empty(Perfect.Marshal.Octets os) : base(os)  {

    }

    public Empty()
    {
    }

    public Empty(XmlElement e)
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
        ele.WriteAttributeString("type", "Empty");
    }


}
}
}
