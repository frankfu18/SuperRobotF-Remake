using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.asset
{
[System.Serializable]
public  class Sprite : AssetInfo
 {

    public string altasName  = "";
     public string spriteName  = "";
     



    public const int TYPE_ID = 3106;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public Sprite(Perfect.Marshal.Octets os) : base(os)  {

        altasName = os.ReadString(); 
         spriteName = os.ReadString(); 
         
    }

    public Sprite()
    {
    }

    public Sprite(XmlElement e)
    {
        foreach (XmlElement ele in e.GetChildren())
        {
            switch (ele.Name)
            {
            case "altasName":  altasName = ele.ReadString2(); break; 
             case "spriteName":  spriteName = ele.ReadString2(); break; 
             
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "Sprite");
        ele.WriteStartElement("altasName"); ele.WriteString2(altasName); ele.WriteEndElement();
         ele.WriteStartElement("spriteName"); ele.WriteString2(spriteName); ele.WriteEndElement();
         
    }


}
}
}
