using Perfect.Marshal;
using System.Xml;

namespace editor
{
namespace cfg.skill
{
[System.Serializable]
public  class StackReset : BuffStatcking
 {




    public const int TYPE_ID = 35619;

    public override int GetTypeId() {
        return TYPE_ID;
    }

    public StackReset(Perfect.Marshal.Octets os) : base(os)  {

    }

    public StackReset()
    {
    }

    public StackReset(XmlElement e)
    {
        foreach (XmlElement ele in e.GetChildren())
        {
            switch (ele.Name)
            {
            default: break;
            }
        }
    }

    public override void MarshalXml(XmlWriter ele)
    {
        ele.WriteAttributeString("type", "StackReset");
    }


}
}
}
