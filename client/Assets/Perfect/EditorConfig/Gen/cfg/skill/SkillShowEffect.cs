using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.skill
{
[System.Serializable]
public abstract class SkillShowEffect :   cfg.EditorWriter 
 {




    public const int TYPE_ID = 0;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public SkillShowEffect(Perfect.Marshal.Octets os) {

    }

    public SkillShowEffect()
    {
    }

}
}
}
