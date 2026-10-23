using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg
{
[System.Serializable]
public  class Vector2Int :   cfg.EditorWriter 
 {

    public int x ;
     public int y ;
     



    public const int TYPE_ID = 15453;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public Vector2Int(Perfect.Marshal.Octets os) {

        x = os.ReadInt(); 
         y = os.ReadInt(); 
         
    }

    public Vector2Int()
    {
    }

    public Vector2Int(XmlElement e)
    {
        foreach (XmlElement ele in e.GetChildren())
        {
            switch (ele.Name)
            {
            case "x":  x = ele.ReadInt(); break; 
             case "y":  y = ele.ReadInt(); break; 
             
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "Vector2Int");
        ele.WriteStartElement("x"); ele.WriteInt(x); ele.WriteEndElement();
         ele.WriteStartElement("y"); ele.WriteInt(y); ele.WriteEndElement();
         
    }


}
}
}
