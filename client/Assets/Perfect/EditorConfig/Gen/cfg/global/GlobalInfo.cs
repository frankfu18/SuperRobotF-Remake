using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.global
{
[System.Serializable]
public abstract class GlobalInfo :   cfg.EditorWriter 
 {




    public const int TYPE_ID = 0;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public GlobalInfo(Perfect.Marshal.Octets os) {

    }

    public GlobalInfo()
    {
    }

}
}
}
