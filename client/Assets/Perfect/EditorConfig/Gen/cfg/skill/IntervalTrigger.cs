using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.skill
{
[System.Serializable]
public  class IntervalTrigger : BuffTriggerType
 {

    public float second ;
     



    public const int TYPE_ID = 8067;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public IntervalTrigger(Perfect.Marshal.Octets os) : base(os)  {

        second = os.ReadFloat(); 
         
    }

    public IntervalTrigger()
    {
    }

    public IntervalTrigger(XmlElement e)
    {
        foreach (XmlElement ele in e.GetChildren())
        {
            switch (ele.Name)
            {
            case "ratio":  ratio = ele.ReadInt(); break; 
             case "second":  second = ele.ReadFloat(); break; 
             
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "IntervalTrigger");
        ele.WriteStartElement("ratio"); ele.WriteInt(ratio); ele.WriteEndElement();
         ele.WriteStartElement("second"); ele.WriteFloat(second); ele.WriteEndElement();
         
    }


}
}
}
