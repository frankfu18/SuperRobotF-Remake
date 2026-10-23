using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.attribute
{
[System.Serializable]
public abstract class AttributeInfo :   cfg.EditorWriter 
 {




    public const int TYPE_ID = 0;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public AttributeInfo(Perfect.Marshal.Octets os) {

    }

    public AttributeInfo()
    {
    }

}
}
}
