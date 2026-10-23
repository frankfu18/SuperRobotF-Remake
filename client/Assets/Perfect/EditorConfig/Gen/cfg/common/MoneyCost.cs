using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.common
{
[System.Serializable]
public  class MoneyCost : Cost
 {

    public float amount ;
     



    public const int TYPE_ID = 50266;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public MoneyCost(Perfect.Marshal.Octets os) : base(os)  {

        amount = os.ReadFloat(); 
         
    }

    public MoneyCost()
    {
    }

    public MoneyCost(XmlElement e)
    {
        foreach (XmlElement ele in e.GetChildren())
        {
            switch (ele.Name)
            {
            case "amount":  amount = ele.ReadFloat(); break; 
             
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "MoneyCost");
        ele.WriteStartElement("amount"); ele.WriteFloat(amount); ele.WriteEndElement();
         
    }


}
}
}
