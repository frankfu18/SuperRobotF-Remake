using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.item
{
[System.Serializable]
public abstract class ItemInfo :   cfg.EditorWriter 
 {




    public const int TYPE_ID = 0;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public ItemInfo(Perfect.Marshal.Octets os) {

    }

    public ItemInfo()
    {
    }

}
}
}
