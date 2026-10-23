using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.battle
{
[System.Serializable]
public  class EffectAssetInfo : BattleAssetInfo
 {

    public int duration ;
     public int delay ;
     



    public const int TYPE_ID = 37120;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public EffectAssetInfo(Perfect.Marshal.Octets os) : base(os)  {

        duration = os.ReadInt(); 
         delay = os.ReadInt(); 
         
    }

    public EffectAssetInfo()
    {
    }

    public EffectAssetInfo(XmlElement e)
    {
        foreach (XmlElement ele in e.GetChildren())
        {
            switch (ele.Name)
            {
            case "duration":  duration = ele.ReadInt(); break; 
             case "delay":  delay = ele.ReadInt(); break; 
             
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "EffectAssetInfo");
        ele.WriteStartElement("duration"); ele.WriteInt(duration); ele.WriteEndElement();
         ele.WriteStartElement("delay"); ele.WriteInt(delay); ele.WriteEndElement();
         
    }


}
}
}
