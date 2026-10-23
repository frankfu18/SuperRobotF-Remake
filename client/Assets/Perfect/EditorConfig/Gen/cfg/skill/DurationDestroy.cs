using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.skill
{
[System.Serializable]
public  class DurationDestroy : BuffDestroyType
 {

    public float durationSecond ;
     



    public const int TYPE_ID = 20871;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public DurationDestroy(Perfect.Marshal.Octets os) : base(os)  {

        durationSecond = os.ReadFloat(); 
         
    }

    public DurationDestroy()
    {
    }

    public DurationDestroy(XmlElement e)
    {
        foreach (XmlElement ele in e.GetChildren())
        {
            switch (ele.Name)
            {
            case "durationSecond":  durationSecond = ele.ReadFloat(); break; 
             
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "DurationDestroy");
        ele.WriteStartElement("durationSecond"); ele.WriteFloat(durationSecond); ele.WriteEndElement();
         
    }


}
}
}
