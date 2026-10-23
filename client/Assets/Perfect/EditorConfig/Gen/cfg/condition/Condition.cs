using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.condition
{
[System.Serializable]
public abstract class Condition :   cfg.EditorWriter 
 {




    public const int TYPE_ID = 0;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public Condition(Perfect.Marshal.Octets os) {

    }

    public Condition()
    {
    }

}
}
}
