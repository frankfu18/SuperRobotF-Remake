using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.bonus
{
[System.Serializable]
public  class RefBonus : Bonus
 {

    public int id ;
     



    public const int TYPE_ID = 31944;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public RefBonus(Perfect.Marshal.Octets os) : base(os)  {

        id = os.ReadInt(); 
         
    }

    public RefBonus()
    {
    }

    public RefBonus(XmlElement e)
    {
        foreach (XmlElement ele in e.GetChildren())
        {
            switch (ele.Name)
            {
            case "id":  id = ele.ReadInt(); break; 
             
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "RefBonus");
        ele.WriteStartElement("id"); ele.WriteInt(id); ele.WriteEndElement();
         
    }


}
}
}
