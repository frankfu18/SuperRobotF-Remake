using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.common
{
[System.Serializable]
public  class Or : Cost
 {

    public System.Collections.Generic.List<cfg.common.Cost> costs  = new System.Collections.Generic.List<cfg.common.Cost>();
     



    public const int TYPE_ID = 23102;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public Or(Perfect.Marshal.Octets os) : base(os)  {

        costs = cfg.Extensions.unmarshal_list_cfg_common_Cost(os); 
         
    }

    public Or()
    {
    }

    public Or(XmlElement e)
    {
        foreach (XmlElement ele in e.GetChildren())
        {
            switch (ele.Name)
            {
            case "costs":  costs = cfg.Extensions.xml_unmarshal_list_cfg_common_Cost(ele); break; 
             
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "Or");
        ele.WriteStartElement("costs"); cfg.Extensions.xml_marshal_list_cfg_common_Cost(costs,ele); ele.WriteEndElement();
         
    }


}
}
}
