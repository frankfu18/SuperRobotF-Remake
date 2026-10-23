using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.skill
{
[System.Serializable]
public  class NoneAsset : BuffAssetInfo
 {




    public const int TYPE_ID = 27089;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public NoneAsset(Perfect.Marshal.Octets os) : base(os)  {

    }

    public NoneAsset()
    {
    }

    public NoneAsset(XmlElement e)
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
        ele.WriteAttributeString("type", "NoneAsset");
    }


}
}
}
