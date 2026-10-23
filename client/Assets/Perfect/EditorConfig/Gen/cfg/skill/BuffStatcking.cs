using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.skill
{
[System.Serializable]
public abstract class BuffStatcking :   cfg.EditorWriter 
 {




    public const int TYPE_ID = 0;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public BuffStatcking(Perfect.Marshal.Octets os) {

    }

    public BuffStatcking()
    {
    }

}
}
}
