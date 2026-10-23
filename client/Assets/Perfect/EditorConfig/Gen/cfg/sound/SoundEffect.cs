using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.sound
{
[System.Serializable]
public  class SoundEffect : Audio
 {




    public const int TYPE_ID = 11565;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public SoundEffect(Perfect.Marshal.Octets os) : base(os)  {

    }

    public SoundEffect()
    {
    }

    public SoundEffect(XmlElement e)
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
        ele.WriteAttributeString("type", "SoundEffect");
        ele.WriteStartElement("path"); ele.WriteString2(path); ele.WriteEndElement();
         
    }


}
}
}
