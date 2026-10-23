using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.sound
{
[System.Serializable]
public  class Sound :   cfg.EditorWriter 
 {

    public string id  = "";
     public cfg.sound.Audio audio ;
     



    public const int TYPE_ID = 49179;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public Sound(Perfect.Marshal.Octets os) {

        id = os.ReadString(); 
         audio = cfg.Extensions.unmarshal_cfg_sound_Audio(os); 
         
    }

    public Sound()
    {
    }

    public Sound(XmlElement e)
    {
        foreach (XmlElement ele in e.GetChildren())
        {
            switch (ele.Name)
            {
            case "id":  id = ele.ReadString2(); break; 
             case "audio":  audio = cfg.Extensions.xml_unmarshal_cfg_sound_Audio(ele); break; 
             
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "Sound");
        ele.WriteStartElement("id"); ele.WriteString2(id); ele.WriteEndElement();
         ele.WriteStartElement("audio"); cfg.Extensions.xml_marshal_cfg_sound_Audio(audio,ele); ele.WriteEndElement();
         
    }


}
}
}
