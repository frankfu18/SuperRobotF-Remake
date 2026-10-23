using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.sound
{
[System.Serializable]
public abstract class Audio :   cfg.EditorWriter 
 {

    public string path  = "";
     



    public const int TYPE_ID = 0;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public Audio(Perfect.Marshal.Octets os) {

        path = os.ReadString(); 
         
    }

    public Audio()
    {
    }

}
}
}
