using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.localize
{
[System.Serializable]
public abstract class TexLocalizeElement :   cfg.EditorWriter 
 {




    public const int TYPE_ID = 0;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public TexLocalizeElement(Perfect.Marshal.Octets os) {

    }

    public TexLocalizeElement()
    {
    }

}
}
}
