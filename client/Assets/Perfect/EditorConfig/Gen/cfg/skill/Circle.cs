using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.skill
{
[System.Serializable]
public  class Circle : BulletReleaseCount
 {

    public int count ;
     



    public const int TYPE_ID = 58314;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public Circle(Perfect.Marshal.Octets os) : base(os)  {

        count = os.ReadInt(); 
         
    }

    public Circle()
    {
    }

    public Circle(XmlElement e)
    {
        foreach (XmlElement ele in e.GetChildren())
        {
            switch (ele.Name)
            {
            case "count":  count = ele.ReadInt(); break; 
             
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "Circle");
        ele.WriteStartElement("count"); ele.WriteInt(count); ele.WriteEndElement();
         
    }


}
}
}
