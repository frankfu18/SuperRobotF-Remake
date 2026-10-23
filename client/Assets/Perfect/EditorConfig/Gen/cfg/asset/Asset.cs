using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.asset
{
[System.Serializable]
public  class Asset :   cfg.EditorWriter 
 {

    public string assetId  = "";
     public cfg.asset.AssetInfo assetInfo ;
     



    public const int TYPE_ID = 32791;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public Asset(Perfect.Marshal.Octets os) {

        assetId = os.ReadString(); 
         assetInfo = cfg.Extensions.unmarshal_cfg_asset_AssetInfo(os); 
         
    }

    public Asset()
    {
    }

    public Asset(XmlElement e)
    {
        foreach (XmlElement ele in e.GetChildren())
        {
            switch (ele.Name)
            {
            case "assetId":  assetId = ele.ReadString2(); break; 
             case "assetInfo":  assetInfo = cfg.Extensions.xml_unmarshal_cfg_asset_AssetInfo(ele); break; 
             
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "Asset");
        ele.WriteStartElement("assetId"); ele.WriteString2(assetId); ele.WriteEndElement();
         ele.WriteStartElement("assetInfo"); cfg.Extensions.xml_marshal_cfg_asset_AssetInfo(assetInfo,ele); ele.WriteEndElement();
         
    }


}
}
}
