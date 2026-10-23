using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.battle
{
[System.Serializable]
public  class None : BattleAssetInfo
 {




    public const int TYPE_ID = 26411;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public None(Perfect.Marshal.Octets os) : base(os)  {

    }

    public None()
    {
    }

    public None(XmlElement e)
    {
        foreach (XmlElement ele in e.GetChildren())
        {
            switch (ele.Name)
            {
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "None");
    }


}
}
}
