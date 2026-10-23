using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.function
{
[System.Serializable]
public  class AdGetCurrencyInfo : JumpInfo
 {

    public cfg.common.ECurrencyType currency ;
     public long amount ;
     public int dailyLimitTime ;
     



    public const int TYPE_ID = 39943;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public AdGetCurrencyInfo(Perfect.Marshal.Octets os) : base(os)  {

        currency = (cfg.common.ECurrencyType)(os.ReadInt()); 
         amount = os.ReadLong(); 
         dailyLimitTime = os.ReadInt(); 
         
    }

    public AdGetCurrencyInfo()
    {
    }

    public AdGetCurrencyInfo(XmlElement e)
    {
        foreach (XmlElement ele in e.GetChildren())
        {
            switch (ele.Name)
            {
            case "currency":  currency = ele.ReadEnum<cfg.common.ECurrencyType>(); break; 
             case "amount":  amount = ele.ReadLong(); break; 
             case "dailyLimitTime":  dailyLimitTime = ele.ReadInt(); break; 
             
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "AdGetCurrencyInfo");
        ele.WriteStartElement("currency"); ele.WriteEnum(currency); ele.WriteEndElement();
         ele.WriteStartElement("amount"); ele.WriteLong(amount); ele.WriteEndElement();
         ele.WriteStartElement("dailyLimitTime"); ele.WriteInt(dailyLimitTime); ele.WriteEndElement();
         
    }


}
}
}
