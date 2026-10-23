using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.function
{
[System.Serializable]
public abstract class JumpInfo :   cfg.EditorWriter 
 {




    public const int TYPE_ID = 0;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public JumpInfo(Perfect.Marshal.Octets os) {

    }

    public JumpInfo()
    {
    }

}
}
}
