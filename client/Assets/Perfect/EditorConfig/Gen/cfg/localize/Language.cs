using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.localize
{
[System.Serializable]
public  class Language :   cfg.EditorWriter 
 {

    public string key  = "";
     public string chinese  = "";
     public string english  = "";
     



    public const int TYPE_ID = 54420;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public Language(Perfect.Marshal.Octets os) {

        key = os.ReadString(); 
         chinese = os.ReadString(); 
         english = os.ReadString(); 
         
    }

    public Language()
    {
    }

    public Language(XmlElement e)
    {
        foreach (XmlElement ele in e.GetChildren())
        {
            switch (ele.Name)
            {
            case "key":  key = ele.ReadString2(); break; 
             case "chinese":  chinese = ele.ReadString2(); break; 
             case "english":  english = ele.ReadString2(); break; 
             
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "Language");
        ele.WriteStartElement("key"); ele.WriteString2(key); ele.WriteEndElement();
         ele.WriteStartElement("chinese"); ele.WriteString2(chinese); ele.WriteEndElement();
         ele.WriteStartElement("english"); ele.WriteString2(english); ele.WriteEndElement();
         
    }


}
}
}
