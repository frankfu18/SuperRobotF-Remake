using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.function
{
[System.Serializable]
public  class JumpBattleInfo : JumpInfo
 {




    public const int TYPE_ID = 27082;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public JumpBattleInfo(Perfect.Marshal.Octets os) : base(os)  {

    }

    public JumpBattleInfo()
    {
    }

    public JumpBattleInfo(XmlElement e)
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
        ele.WriteAttributeString("type", "JumpBattleInfo");
    }


}
}
}
