using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.skill
{
[System.Serializable]
public  class FlyBullet : BulletType
 {

    public float speed ;
     public float maxDistance ;
     



    public const int TYPE_ID = 41809;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public FlyBullet(Perfect.Marshal.Octets os) : base(os)  {

        speed = os.ReadFloat(); 
         maxDistance = os.ReadFloat(); 
         
    }

    public FlyBullet()
    {
    }

    public FlyBullet(XmlElement e)
    {
        foreach (XmlElement ele in e.GetChildren())
        {
            switch (ele.Name)
            {
            case "speed":  speed = ele.ReadFloat(); break; 
             case "maxDistance":  maxDistance = ele.ReadFloat(); break; 
             
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "FlyBullet");
        ele.WriteStartElement("speed"); ele.WriteFloat(speed); ele.WriteEndElement();
         ele.WriteStartElement("maxDistance"); ele.WriteFloat(maxDistance); ele.WriteEndElement();
         
    }


}
}
}
