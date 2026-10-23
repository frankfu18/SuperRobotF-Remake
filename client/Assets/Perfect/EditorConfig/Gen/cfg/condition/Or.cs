using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.condition
{
[System.Serializable]
public  class Or : Condition
 {

    public System.Collections.Generic.List<cfg.condition.Condition> conditions  = new System.Collections.Generic.List<cfg.condition.Condition>();
     



    public const int TYPE_ID = 24596;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public Or(Perfect.Marshal.Octets os) : base(os)  {

        conditions = cfg.Extensions.unmarshal_list_cfg_condition_Condition(os); 
         
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
            case "conditions":  conditions = cfg.Extensions.xml_unmarshal_list_cfg_condition_Condition(ele); break; 
             
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "Or");
        ele.WriteStartElement("conditions"); cfg.Extensions.xml_marshal_list_cfg_condition_Condition(conditions,ele); ele.WriteEndElement();
         
    }


}
}
}
