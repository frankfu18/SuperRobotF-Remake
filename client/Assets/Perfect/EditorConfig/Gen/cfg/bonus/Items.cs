using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.bonus
{
[System.Serializable]
public  class Items : Bonus
 {

    public System.Collections.Generic.List<cfg.bonus.Item> items  = new System.Collections.Generic.List<cfg.bonus.Item>();
     



    public const int TYPE_ID = 43728;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public Items(Perfect.Marshal.Octets os) : base(os)  {

        items = cfg.Extensions.unmarshal_list_cfg_bonus_Item(os); 
         
    }

    public Items()
    {
    }

    public Items(XmlElement e)
    {
        foreach (XmlElement ele in e.GetChildren())
        {
            switch (ele.Name)
            {
            case "items":  items = cfg.Extensions.xml_unmarshal_list_cfg_bonus_Item(ele); break; 
             
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "Items");
        ele.WriteStartElement("items"); cfg.Extensions.xml_marshal_list_cfg_bonus_Item(items,ele); ele.WriteEndElement();
         
    }


}
}
}
