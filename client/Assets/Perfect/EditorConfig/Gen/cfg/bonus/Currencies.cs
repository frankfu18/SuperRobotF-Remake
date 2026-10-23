using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.bonus
{
[System.Serializable]
public  class Currencies : Bonus
 {

    public System.Collections.Generic.List<cfg.bonus.Currency> currencies  = new System.Collections.Generic.List<cfg.bonus.Currency>();
     



    public const int TYPE_ID = 19353;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public Currencies(Perfect.Marshal.Octets os) : base(os)  {

        currencies = cfg.Extensions.unmarshal_list_cfg_bonus_Currency(os); 
         
    }

    public Currencies()
    {
    }

    public Currencies(XmlElement e)
    {
        foreach (XmlElement ele in e.GetChildren())
        {
            switch (ele.Name)
            {
            case "currencies":  currencies = cfg.Extensions.xml_unmarshal_list_cfg_bonus_Currency(ele); break; 
             
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "Currencies");
        ele.WriteStartElement("currencies"); cfg.Extensions.xml_marshal_list_cfg_bonus_Currency(currencies,ele); ele.WriteEndElement();
         
    }


}
}
}
