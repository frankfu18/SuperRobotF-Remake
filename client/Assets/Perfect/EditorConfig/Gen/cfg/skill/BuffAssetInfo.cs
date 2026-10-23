using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.skill
{
[System.Serializable]
public abstract class BuffAssetInfo :   cfg.EditorWriter 
 {




    public const int TYPE_ID = 0;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public BuffAssetInfo(Perfect.Marshal.Octets os) {

    }

    public BuffAssetInfo()
    {
    }

}
}
}
