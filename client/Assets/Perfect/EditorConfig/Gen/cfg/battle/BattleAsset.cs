using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.battle
{
[System.Serializable]
public  class BattleAsset :   cfg.EditorWriter 
 {

    public string assetId  = "";
     public cfg.battle.EBattleAssetType assetType ;
     public string path  = "";
     public cfg.battle.BattleAssetInfo assetInfo ;
     



    public const int TYPE_ID = 25306;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public BattleAsset(Perfect.Marshal.Octets os) {

        assetId = os.ReadString(); 
         assetType = (cfg.battle.EBattleAssetType)(os.ReadInt()); 
         path = os.ReadString(); 
         assetInfo = cfg.Extensions.unmarshal_cfg_battle_BattleAssetInfo(os); 
         
    }

    public BattleAsset()
    {
    }

    public BattleAsset(XmlElement e)
    {
        foreach (XmlElement ele in e.GetChildren())
        {
            switch (ele.Name)
            {
            case "assetId":  assetId = ele.ReadString2(); break; 
             case "assetType":  assetType = ele.ReadEnum<cfg.battle.EBattleAssetType>(); break; 
             case "path":  path = ele.ReadString2(); break; 
             case "assetInfo":  assetInfo = cfg.Extensions.xml_unmarshal_cfg_battle_BattleAssetInfo(ele); break; 
             
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "BattleAsset");
        ele.WriteStartElement("assetId"); ele.WriteString2(assetId); ele.WriteEndElement();
         ele.WriteStartElement("assetType"); ele.WriteEnum(assetType); ele.WriteEndElement();
         ele.WriteStartElement("path"); ele.WriteString2(path); ele.WriteEndElement();
         ele.WriteStartElement("assetInfo"); cfg.Extensions.xml_marshal_cfg_battle_BattleAssetInfo(assetInfo,ele); ele.WriteEndElement();
         
    }


}
}
}
