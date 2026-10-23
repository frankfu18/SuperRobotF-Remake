using editor.cfg.global;

/// <summary>
/// 记录一些都会用到的全局数据
/// </summary>
public class GlobalDataModule : ModuleBase<GlobalDataModule>
{
    protected override void OnLoginDone()
    {
        base.OnLoginDone();
    }
    
    public int GetInt(string _key)
    {
        var globalConfig = ConfigSystem.CurrentData.GetGlobal(_key);

        switch (globalConfig.info.GetTypeId())
        {
            case GInt32.TYPE_ID:
                return ((GInt32) globalConfig.info).value;
            case GInt64.TYPE_ID:
                return (int)((GInt64)globalConfig.info).value;
            case GFloat.TYPE_ID:
                return (int)((GFloat)globalConfig.info).value;
            default:
                return 0;
        }
    }

    public float GetFloat(string _key)
    {
        var globalConfig = ConfigSystem.CurrentData.GetGlobal(_key);

        switch (globalConfig.info.GetTypeId())
        {
            case GInt32.TYPE_ID:
                return ((GInt32)globalConfig.info).value;
            case GInt64.TYPE_ID:
                return ((GInt64)globalConfig.info).value;
            case GFloat.TYPE_ID:
                return ((GFloat)globalConfig.info).value;
            default:
                return 0;
        }
    }
}
