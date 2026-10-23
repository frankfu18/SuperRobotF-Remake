using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.skill
{
[System.Serializable]
public  class StackNum : BuffStatcking
 {

    public int num ;
     



    public const int TYPE_ID = 43531;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public StackNum(Perfect.Marshal.Octets os) : base(os)  {

        num = os.ReadInt(); 
         
    }

    public StackNum()
    {
    }

    public StackNum(XmlElement e)
    {
        foreach (XmlElement ele in e.GetChildren())
        {
            switch (ele.Name)
            {
            case "num":  num = ele.ReadInt(); break; 
             
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "StackNum");
        ele.WriteStartElement("num"); ele.WriteInt(num); ele.WriteEndElement();
         
    }


}
}
}
