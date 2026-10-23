using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.localize
{
[System.Serializable]
public  class TextureLanguage :   cfg.EditorWriter 
 {

    public string key  = "";
     public cfg.localize.TexLocalizeElement defaultEle ;
     public System.Collections.Generic.Dictionary<cfg.localize.ELanguageType,cfg.localize.TexLocalizeElement> localizeEleMap  = new System.Collections.Generic.Dictionary<cfg.localize.ELanguageType,cfg.localize.TexLocalizeElement>();
     



    public const int TYPE_ID = 632;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public TextureLanguage(Perfect.Marshal.Octets os) {

        key = os.ReadString(); 
         defaultEle = cfg.Extensions.unmarshal_cfg_localize_TexLocalizeElement(os); 
         localizeEleMap = cfg.Extensions.unmarshal_map_cfg_localize_ELanguageType_cfg_localize_TexLocalizeElement(os); 
         
    }

    public TextureLanguage()
    {
    }

    public TextureLanguage(XmlElement e)
    {
        foreach (XmlElement ele in e.GetChildren())
        {
            switch (ele.Name)
            {
            case "key":  key = ele.ReadString2(); break; 
             case "defaultEle":  defaultEle = cfg.Extensions.xml_unmarshal_cfg_localize_TexLocalizeElement(ele); break; 
             case "localizeEleMap":  localizeEleMap = cfg.Extensions.xml_unmarshal_map_cfg_localize_ELanguageType_cfg_localize_TexLocalizeElement(ele); break; 
             
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "TextureLanguage");
        ele.WriteStartElement("key"); ele.WriteString2(key); ele.WriteEndElement();
         ele.WriteStartElement("defaultEle"); cfg.Extensions.xml_marshal_cfg_localize_TexLocalizeElement(defaultEle,ele); ele.WriteEndElement();
         ele.WriteStartElement("localizeEleMap"); cfg.Extensions.xml_marshal_map_cfg_localize_ELanguageType_cfg_localize_TexLocalizeElement(localizeEleMap,ele); ele.WriteEndElement();
         
    }


}
}
}
