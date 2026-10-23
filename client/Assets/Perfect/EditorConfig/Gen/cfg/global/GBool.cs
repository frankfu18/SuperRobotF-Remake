using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.global
{
[System.Serializable]
public  class GBool : GlobalInfo
 {

    public bool value ;
     



    public const int TYPE_ID = 44923;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public GBool(Perfect.Marshal.Octets os) : base(os)  {

        value = os.ReadBool(); 
         
    }

    public GBool()
    {
    }

    public GBool(XmlElement e)
    {
        foreach (XmlElement ele in e.GetChildren())
        {
            switch (ele.Name)
            {
            case "value":  value = ele.ReadBool(); break; 
             
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "GBool");
        ele.WriteStartElement("value"); ele.WriteBool(value); ele.WriteEndElement();
         
    }


}
}
}
