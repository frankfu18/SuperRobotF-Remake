using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.common
{
[System.Serializable]
public  class XInt : XNumber
 {

    public int amount ;
     



    public const int TYPE_ID = 13651;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public XInt(Perfect.Marshal.Octets os) : base(os)  {

        amount = os.ReadInt(); 
         
    }

    public XInt()
    {
    }

    public XInt(XmlElement e)
    {
        foreach (XmlElement ele in e.GetChildren())
        {
            switch (ele.Name)
            {
            case "amount":  amount = ele.ReadInt(); break; 
             
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "XInt");
        ele.WriteStartElement("amount"); ele.WriteInt(amount); ele.WriteEndElement();
         
    }


}
}
}
