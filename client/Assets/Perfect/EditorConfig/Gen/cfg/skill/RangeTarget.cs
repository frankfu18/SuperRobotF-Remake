using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.skill
{
[System.Serializable]
public  class RangeTarget : BulletTargetType
 {

    public float radius ;
     



    public const int TYPE_ID = 25258;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public RangeTarget(Perfect.Marshal.Octets os) : base(os)  {

        radius = os.ReadFloat(); 
         
    }

    public RangeTarget()
    {
    }

    public RangeTarget(XmlElement e)
    {
        foreach (XmlElement ele in e.GetChildren())
        {
            switch (ele.Name)
            {
            case "radius":  radius = ele.ReadFloat(); break; 
             
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "RangeTarget");
        ele.WriteStartElement("radius"); ele.WriteFloat(radius); ele.WriteEndElement();
         
    }


}
}
}
