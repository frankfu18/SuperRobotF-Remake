using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.common
{
[System.Serializable]
public  class XFloat : XNumber
 {

    public float amount ;
     



    public const int TYPE_ID = 24592;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public XFloat(Perfect.Marshal.Octets os) : base(os)  {

        amount = os.ReadFloat(); 
         
    }

    public XFloat()
    {
    }

    public XFloat(XmlElement e)
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
        ele.WriteAttributeString("type", "XFloat");
        ele.WriteStartElement("amount"); ele.WriteFloat(amount); ele.WriteEndElement();
         
    }


}
}
}
