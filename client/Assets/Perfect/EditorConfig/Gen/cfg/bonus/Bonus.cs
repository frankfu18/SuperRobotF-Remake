using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.bonus
{
[System.Serializable]
public abstract class Bonus :   cfg.EditorWriter 
 {




    public const int TYPE_ID = 0;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public Bonus(Perfect.Marshal.Octets os) {

    }

    public Bonus()
    {
    }

}
}
}
