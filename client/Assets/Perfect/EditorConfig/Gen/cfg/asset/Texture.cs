using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.asset
{
[System.Serializable]
public  class Texture : AssetInfo
 {

    public string path  = "";
     



    public const int TYPE_ID = 6998;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public Texture(Perfect.Marshal.Octets os) : base(os)  {

        path = os.ReadString(); 
         
    }

    public Texture()
    {
    }

    public Texture(XmlElement e)
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
        ele.WriteAttributeString("type", "Texture");
        ele.WriteStartElement("path"); ele.WriteString2(path); ele.WriteEndElement();
         
    }


}
}
}
