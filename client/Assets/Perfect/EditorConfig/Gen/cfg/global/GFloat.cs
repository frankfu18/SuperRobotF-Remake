using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.global
{
[System.Serializable]
public  class GFloat : GlobalInfo
 {

    public float value ;
     



    public const int TYPE_ID = 16276;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public GFloat(Perfect.Marshal.Octets os) : base(os)  {

        value = os.ReadFloat(); 
         
    }

    public GFloat()
    {
    }

    public GFloat(XmlElement e)
    {
        foreach (XmlElement ele in e.GetChildren())
        {
            switch (ele.Name)
            {
            case "value":  value = ele.ReadFloat(); break; 
             
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "GFloat");
        ele.WriteStartElement("value"); ele.WriteFloat(value); ele.WriteEndElement();
         
    }


}
}
}
