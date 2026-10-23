using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.error
{
[System.Serializable]
public  class Error :   cfg.EditorWriter 
 {

    public string key  = "";
     public string desc  = "";
     



    public const int TYPE_ID = 54263;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public Error(Perfect.Marshal.Octets os) {

        key = os.ReadString(); 
         desc = os.ReadString(); 
         
    }

    public Error()
    {
    }

    public Error(XmlElement e)
    {
        foreach (XmlElement ele in e.GetChildren())
        {
            switch (ele.Name)
            {
            case "key":  key = ele.ReadString2(); break; 
             case "desc":  desc = ele.ReadString2(); break; 
             
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "Error");
        ele.WriteStartElement("key"); ele.WriteString2(key); ele.WriteEndElement();
         ele.WriteStartElement("desc"); ele.WriteString2(desc); ele.WriteEndElement();
         
    }


}
}
}
