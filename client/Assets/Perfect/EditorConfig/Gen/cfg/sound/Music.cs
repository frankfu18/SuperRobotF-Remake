using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.sound
{
[System.Serializable]
public  class Music : Audio
 {




    public const int TYPE_ID = 58755;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public Music(Perfect.Marshal.Octets os) : base(os)  {

    }

    public Music()
    {
    }

    public Music(XmlElement e)
    {
        foreach (XmlElement ele in e.GetChildren())
        {
            switch (ele.Name)
            {
            case "path":  path = ele.ReadString2(); break; 
             
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "Music");
        ele.WriteStartElement("path"); ele.WriteString2(path); ele.WriteEndElement();
         
    }


}
}
}
