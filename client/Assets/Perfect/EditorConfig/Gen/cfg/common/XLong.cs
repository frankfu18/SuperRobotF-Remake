using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.common
{
[System.Serializable]
public  class XLong : XNumber
 {

    public long amount ;
     



    public const int TYPE_ID = 5234;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public XLong(Perfect.Marshal.Octets os) : base(os)  {

        amount = os.ReadLong(); 
         
    }

    public XLong()
    {
    }

    public XLong(XmlElement e)
    {
        foreach (XmlElement ele in e.GetChildren())
        {
            switch (ele.Name)
            {
            case "amount":  amount = ele.ReadLong(); break; 
             
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "XLong");
        ele.WriteStartElement("amount"); ele.WriteLong(amount); ele.WriteEndElement();
         
    }


}
}
}
