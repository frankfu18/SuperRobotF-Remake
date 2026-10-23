using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg
{
[System.Serializable]
public  class Vector3 :   cfg.EditorWriter 
 {

    public float x ;
     public float y ;
     public float z ;
     



    public const int TYPE_ID = 59241;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public Vector3(Perfect.Marshal.Octets os) {

        x = os.ReadFloat(); 
         y = os.ReadFloat(); 
         z = os.ReadFloat(); 
         
    }

    public Vector3()
    {
    }

    public Vector3(XmlElement e)
    {
        foreach (XmlElement ele in e.GetChildren())
        {
            switch (ele.Name)
            {
            case "x":  x = ele.ReadFloat(); break; 
             case "y":  y = ele.ReadFloat(); break; 
             case "z":  z = ele.ReadFloat(); break; 
             
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "Vector3");
        ele.WriteStartElement("x"); ele.WriteFloat(x); ele.WriteEndElement();
         ele.WriteStartElement("y"); ele.WriteFloat(y); ele.WriteEndElement();
         ele.WriteStartElement("z"); ele.WriteFloat(z); ele.WriteEndElement();
         
    }


}
}
}
