using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.condition
{
[System.Serializable]
public  class Currency : Condition
 {

    public cfg.common.ECurrencyType currency ;
     public long amount ;
     



    public const int TYPE_ID = 53049;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public Currency(Perfect.Marshal.Octets os) : base(os)  {

        currency = (cfg.common.ECurrencyType)(os.ReadInt()); 
         amount = os.ReadLong(); 
         
    }

    public Currency()
    {
    }

    public Currency(XmlElement e)
    {
        foreach (XmlElement ele in e.GetChildren())
        {
            switch (ele.Name)
            {
            case "currency":  currency = ele.ReadEnum<cfg.common.ECurrencyType>(); break; 
             case "amount":  amount = ele.ReadLong(); break; 
             
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "Currency");
        ele.WriteStartElement("currency"); ele.WriteEnum(currency); ele.WriteEndElement();
         ele.WriteStartElement("amount"); ele.WriteLong(amount); ele.WriteEndElement();
         
    }


}
}
}
