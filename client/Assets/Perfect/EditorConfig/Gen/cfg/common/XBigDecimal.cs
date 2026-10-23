using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.common
{
[System.Serializable]
public  class XBigDecimal : XNumber
 {

    public string amount  = "";
     



    public const int TYPE_ID = 52174;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public XBigDecimal(Perfect.Marshal.Octets os) : base(os)  {

        amount = os.ReadString(); 
         
    }

    public XBigDecimal()
    {
    }

    public XBigDecimal(XmlElement e)
    {
        foreach (XmlElement ele in e.GetChildren())
        {
            switch (ele.Name)
            {
            case "amount":  amount = ele.ReadString2(); break; 
             
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "XBigDecimal");
        ele.WriteStartElement("amount"); ele.WriteString2(amount); ele.WriteEndElement();
         
    }


}
}
}
