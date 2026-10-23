using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.skill
{
[System.Serializable]
public abstract class BulletTargetType :   cfg.EditorWriter 
 {




    public const int TYPE_ID = 0;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public BulletTargetType(Perfect.Marshal.Octets os) {

    }

    public BulletTargetType()
    {
    }

}
}
}
