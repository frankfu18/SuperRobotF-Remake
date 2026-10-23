using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.localize
{
[System.Serializable]
public  class TexAssetElement : TexLocalizeElement
 {

    public string asset  = "";
     public bool nativeSize ;
     



    public const int TYPE_ID = 31510;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public TexAssetElement(Perfect.Marshal.Octets os) : base(os)  {

        asset = os.ReadString(); 
         nativeSize = os.ReadBool(); 
         
    }

    public TexAssetElement()
    {
    }

    public TexAssetElement(XmlElement e)
    {
        foreach (XmlElement ele in e.GetChildren())
        {
            switch (ele.Name)
            {
            case "asset":  asset = ele.ReadString2(); break; 
             case "nativeSize":  nativeSize = ele.ReadBool(); break; 
             
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "TexAssetElement");
        ele.WriteStartElement("asset"); ele.WriteString2(asset); ele.WriteEndElement();
         ele.WriteStartElement("nativeSize"); ele.WriteBool(nativeSize); ele.WriteEndElement();
         
    }


}
}
}
