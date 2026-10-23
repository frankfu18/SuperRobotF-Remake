using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.skill
{
[System.Serializable]
public  class BulletHitBuffEffect : BulletHitEffectType
 {

    public int buffId ;
     



    public const int TYPE_ID = 22420;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public BulletHitBuffEffect(Perfect.Marshal.Octets os) : base(os)  {

        buffId = os.ReadInt(); 
         
    }

    public BulletHitBuffEffect()
    {
    }

    public BulletHitBuffEffect(XmlElement e)
    {
        foreach (XmlElement ele in e.GetChildren())
        {
            switch (ele.Name)
            {
            case "buffId":  buffId = ele.ReadInt(); break; 
             
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "BulletHitBuffEffect");
        ele.WriteStartElement("buffId"); ele.WriteInt(buffId); ele.WriteEndElement();
         
    }


}
}
}
