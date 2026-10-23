using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.common
{
[System.Serializable]
public  class AdCost : Cost
 {




    public const int TYPE_ID = 37017;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public AdCost(Perfect.Marshal.Octets os) : base(os)  {

    }

    public AdCost()
    {
    }

    public AdCost(XmlElement e)
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
        ele.WriteAttributeString("type", "AdCost");
    }


}
}
}
