using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.global
{
[System.Serializable]
public  class GString : GlobalInfo
 {

    public string value  = "";
     



    public const int TYPE_ID = 11475;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public GString(Perfect.Marshal.Octets os) : base(os)  {

        value = os.ReadString(); 
         
    }

    public GString()
    {
    }

    public GString(XmlElement e)
    {
        foreach (XmlElement ele in e.GetChildren())
        {
            switch (ele.Name)
            {
            case "value":  value = ele.ReadString2(); break; 
             
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "GString");
        ele.WriteStartElement("value"); ele.WriteString2(value); ele.WriteEndElement();
         
    }


}
}
}
