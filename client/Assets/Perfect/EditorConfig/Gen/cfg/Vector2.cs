using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg
{
[System.Serializable]
public  class Vector2 :   cfg.EditorWriter 
 {

    public float x ;
     public float y ;
     



    public const int TYPE_ID = 59242;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public Vector2(Perfect.Marshal.Octets os) {

        x = os.ReadFloat(); 
         y = os.ReadFloat(); 
         
    }

    public Vector2()
    {
    }

    public Vector2(XmlElement e)
    {
        foreach (XmlElement ele in e.GetChildren())
        {
            switch (ele.Name)
            {
            case "x":  x = ele.ReadFloat(); break; 
             case "y":  y = ele.ReadFloat(); break; 
             
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "Vector2");
        ele.WriteStartElement("x"); ele.WriteFloat(x); ele.WriteEndElement();
         ele.WriteStartElement("y"); ele.WriteFloat(y); ele.WriteEndElement();
         
    }


}
}
}
