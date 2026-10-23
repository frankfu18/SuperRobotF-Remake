using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.function
{
[System.Serializable]
public  class Jump :   cfg.EditorWriter 
 {

    public int id ;
     public string desc  = "";
     public cfg.function.JumpInfo jumpInfo ;
     



    public const int TYPE_ID = 51719;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public Jump(Perfect.Marshal.Octets os) {

        id = os.ReadInt(); 
         desc = os.ReadString(); 
         jumpInfo = cfg.Extensions.unmarshal_cfg_function_JumpInfo(os); 
         
    }

    public Jump()
    {
    }

    public Jump(XmlElement e)
    {
        foreach (XmlElement ele in e.GetChildren())
        {
            switch (ele.Name)
            {
            case "id":  id = ele.ReadInt(); break; 
             case "desc":  desc = ele.ReadString2(); break; 
             case "jumpInfo":  jumpInfo = cfg.Extensions.xml_unmarshal_cfg_function_JumpInfo(ele); break; 
             
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "Jump");
        ele.WriteStartElement("id"); ele.WriteInt(id); ele.WriteEndElement();
         ele.WriteStartElement("desc"); ele.WriteString2(desc); ele.WriteEndElement();
         ele.WriteStartElement("jumpInfo"); cfg.Extensions.xml_marshal_cfg_function_JumpInfo(jumpInfo,ele); ele.WriteEndElement();
         
    }


}
}
}
