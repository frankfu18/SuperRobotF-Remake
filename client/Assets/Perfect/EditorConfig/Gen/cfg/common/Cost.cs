using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.common
{
[System.Serializable]
public abstract class Cost :   cfg.EditorWriter 
 {




    public const int TYPE_ID = 0;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public Cost(Perfect.Marshal.Octets os) {

    }

    public Cost()
    {
    }

}
}
}
