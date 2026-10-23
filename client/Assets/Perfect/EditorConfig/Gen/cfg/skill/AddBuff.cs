using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.skill
{
[System.Serializable]
public  class AddBuff : BuffEffectContent
 {

    public cfg.skill.EBuffEffectTarget target ;
     public int buffId ;
     



    public const int TYPE_ID = 53274;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public AddBuff(Perfect.Marshal.Octets os) : base(os)  {

        target = (cfg.skill.EBuffEffectTarget)(os.ReadInt()); 
         buffId = os.ReadInt(); 
         
    }

    public AddBuff()
    {
    }

    public AddBuff(XmlElement e)
    {
        foreach (XmlElement ele in e.GetChildren())
        {
            switch (ele.Name)
            {
            case "target":  target = ele.ReadEnum<cfg.skill.EBuffEffectTarget>(); break; 
             case "buffId":  buffId = ele.ReadInt(); break; 
             
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "AddBuff");
        ele.WriteStartElement("target"); ele.WriteEnum(target); ele.WriteEndElement();
         ele.WriteStartElement("buffId"); ele.WriteInt(buffId); ele.WriteEndElement();
         
    }


}
}
}
