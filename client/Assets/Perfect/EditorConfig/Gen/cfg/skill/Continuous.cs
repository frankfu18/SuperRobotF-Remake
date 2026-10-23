using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.skill
{
[System.Serializable]
public  class Continuous : BulletReleaseCount
 {

    public int count ;
     public float intervalSecond ;
     



    public const int TYPE_ID = 65387;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public Continuous(Perfect.Marshal.Octets os) : base(os)  {

        count = os.ReadInt(); 
         intervalSecond = os.ReadFloat(); 
         
    }

    public Continuous()
    {
    }

    public Continuous(XmlElement e)
    {
        foreach (XmlElement ele in e.GetChildren())
        {
            switch (ele.Name)
            {
            case "count":  count = ele.ReadInt(); break; 
             case "intervalSecond":  intervalSecond = ele.ReadFloat(); break; 
             
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "Continuous");
        ele.WriteStartElement("count"); ele.WriteInt(count); ele.WriteEndElement();
         ele.WriteStartElement("intervalSecond"); ele.WriteFloat(intervalSecond); ele.WriteEndElement();
         
    }


}
}
}
