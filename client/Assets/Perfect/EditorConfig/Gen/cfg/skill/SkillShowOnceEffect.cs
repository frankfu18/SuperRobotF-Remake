using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.skill
{
[System.Serializable]
public  class SkillShowOnceEffect : SkillShowEffect
 {

    public string battleAssetId  = "";
     



    public const int TYPE_ID = 555;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public SkillShowOnceEffect(Perfect.Marshal.Octets os) : base(os)  {

        battleAssetId = os.ReadString(); 
         
    }

    public SkillShowOnceEffect()
    {
    }

    public SkillShowOnceEffect(XmlElement e)
    {
        foreach (XmlElement ele in e.GetChildren())
        {
            switch (ele.Name)
            {
            case "battleAssetId":  battleAssetId = ele.ReadString2(); break; 
             
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "SkillShowOnceEffect");
        ele.WriteStartElement("battleAssetId"); ele.WriteString2(battleAssetId); ele.WriteEndElement();
         
    }


}
}
}
