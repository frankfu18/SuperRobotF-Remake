using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.condition
{
[System.Serializable]
public  class BuyMonthlyCard : Condition
 {




    public const int TYPE_ID = 28544;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public BuyMonthlyCard(Perfect.Marshal.Octets os) : base(os)  {

    }

    public BuyMonthlyCard()
    {
    }

    public BuyMonthlyCard(XmlElement e)
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
        ele.WriteAttributeString("type", "BuyMonthlyCard");
    }


}
}
}
