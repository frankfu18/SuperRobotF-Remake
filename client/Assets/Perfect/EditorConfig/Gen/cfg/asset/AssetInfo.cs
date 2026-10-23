using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.asset
{
[System.Serializable]
public abstract class AssetInfo :   cfg.EditorWriter 
 {




    public const int TYPE_ID = 0;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public AssetInfo(Perfect.Marshal.Octets os) {

    }

    public AssetInfo()
    {
    }

}
}
}
