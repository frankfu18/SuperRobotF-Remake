using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.skill
{
[System.Serializable]
public  class HpRatioLessIntervalTrigger : BuffTriggerType
 {

    public float hpRatio ;
     public float intervalSecond ;
     



    public const int TYPE_ID = 20225;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public HpRatioLessIntervalTrigger(Perfect.Marshal.Octets os) : base(os)  {

        hpRatio = os.ReadFloat(); 
         intervalSecond = os.ReadFloat(); 
         
    }

    public HpRatioLessIntervalTrigger()
    {
    }

    public HpRatioLessIntervalTrigger(XmlElement e)
    {
        foreach (XmlElement ele in e.GetChildren())
        {
            switch (ele.Name)
            {
            case "ratio":  ratio = ele.ReadInt(); break; 
             case "hpRatio":  hpRatio = ele.ReadFloat(); break; 
             case "intervalSecond":  intervalSecond = ele.ReadFloat(); break; 
             
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "HpRatioLessIntervalTrigger");
        ele.WriteStartElement("ratio"); ele.WriteInt(ratio); ele.WriteEndElement();
         ele.WriteStartElement("hpRatio"); ele.WriteFloat(hpRatio); ele.WriteEndElement();
         ele.WriteStartElement("intervalSecond"); ele.WriteFloat(intervalSecond); ele.WriteEndElement();
         
    }


}
}
}
