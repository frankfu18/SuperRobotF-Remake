using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.skill
{
[System.Serializable]
public  class AddBoostEnergy : BuffEffectContent
 {

    public int value ;
     



    public const int TYPE_ID = 8587;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public AddBoostEnergy(Perfect.Marshal.Octets os) : base(os)  {

        value = os.ReadInt(); 
         
    }

    public AddBoostEnergy()
    {
    }

    public AddBoostEnergy(XmlElement e)
    {
        foreach (XmlElement ele in e.GetChildren())
        {
            switch (ele.Name)
            {
            case "value":  value = ele.ReadInt(); break; 
             
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "AddBoostEnergy");
        ele.WriteStartElement("value"); ele.WriteInt(value); ele.WriteEndElement();
         
    }


}
}
}
