using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.global
{
[System.Serializable]
public  class Global :   cfg.EditorWriter 
 {

    public string key  = "";
     public cfg.global.GlobalInfo info ;
     



    public const int TYPE_ID = 59114;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public Global(Perfect.Marshal.Octets os) {

        key = os.ReadString(); 
         info = cfg.Extensions.unmarshal_cfg_global_GlobalInfo(os); 
         
    }

    public Global()
    {
    }

    public Global(XmlElement e)
    {
        foreach (XmlElement ele in e.GetChildren())
        {
            switch (ele.Name)
            {
            case "key":  key = ele.ReadString2(); break; 
             case "info":  info = cfg.Extensions.xml_unmarshal_cfg_global_GlobalInfo(ele); break; 
             
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "Global");
        ele.WriteStartElement("key"); ele.WriteString2(key); ele.WriteEndElement();
         ele.WriteStartElement("info"); cfg.Extensions.xml_marshal_cfg_global_GlobalInfo(info,ele); ele.WriteEndElement();
         
    }


}
}
}
