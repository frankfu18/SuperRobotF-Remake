using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.skill
{
[System.Serializable]
public  class BeCollidedTrigger : BuffTriggerType
 {




    public const int TYPE_ID = 22894;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public BeCollidedTrigger(Perfect.Marshal.Octets os) : base(os)  {

    }

    public BeCollidedTrigger()
    {
    }

    public BeCollidedTrigger(XmlElement e)
    {
        foreach (XmlElement ele in e.GetChildren())
        {
            switch (ele.Name)
            {
            case "ratio":  ratio = ele.ReadInt(); break; 
             
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "BeCollidedTrigger");
        ele.WriteStartElement("ratio"); ele.WriteInt(ratio); ele.WriteEndElement();
         
    }


}
}
}
