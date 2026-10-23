using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.asset
{
[System.Serializable]
public  class Spine : AssetInfo
 {

    public string path  = "";
     public string defaultAnim  = "";
     public float scale ;
     



    public const int TYPE_ID = 42126;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public Spine(Perfect.Marshal.Octets os) : base(os)  {

        path = os.ReadString(); 
         defaultAnim = os.ReadString(); 
         scale = os.ReadFloat(); 
         
    }

    public Spine()
    {
    }

    public Spine(XmlElement e)
    {
        foreach (XmlElement ele in e.GetChildren())
        {
            switch (ele.Name)
            {
            case "path":  path = ele.ReadString2(); break; 
             case "defaultAnim":  defaultAnim = ele.ReadString2(); break; 
             case "scale":  scale = ele.ReadFloat(); break; 
             
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "Spine");
        ele.WriteStartElement("path"); ele.WriteString2(path); ele.WriteEndElement();
         ele.WriteStartElement("defaultAnim"); ele.WriteString2(defaultAnim); ele.WriteEndElement();
         ele.WriteStartElement("scale"); ele.WriteFloat(scale); ele.WriteEndElement();
         
    }


}
}
}
