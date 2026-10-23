using editor.cfg;
using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class ConfigSystem : Singlet<ConfigSystem>
{
    public static CfgMgr CurrentData => Instance.GetData();

    public void Init()
    {
        EventNativeSystem.Instance.AddEvent(SysEvent.Config_Load_Done, this, (args) =>
        {
            EventNativeSystem.Instance.SendEvent(SysEvent.Loading_Open);
        });

        LoadConfigs();
    }

    private void LoadConfigs()
    {
        CfgMgr.onLoadConfig = OnConfigLoad;
        CfgMgr.Load();

        EventNativeSystem.Instance.SendEvent(SysEvent.Config_Load_Done);
    }

    private byte[] OnConfigLoad(string _file)
    {
        if (FrankGame.GameConfig.Package.ResLoadMode == FrankGame.ELoadMode.Local)
        {
            return File.ReadAllBytes(Path.Combine(Application.dataPath.Replace("Assets", "") + FrankGame.GameConfig.Table.LocalTablePath, _file));
        }
        else
        {
            return FrankGame.BundleManager.Instance.GetConfigData(_file);
        }
    }

    public CfgMgr GetData()
    {
        return CfgMgr.Ins;
    }

    public List<T> GetConfigsByFilter<T>(List<T> _list, System.Func<T, bool> _filter) where T : editor.cfg.EditorWriter
    {
        List<T> result = new List<T>();

        foreach (var item in _list)
        {
            if (_filter == null || _filter(item))
            {
                result.Add(item);
            }
        }

        return result;
    }

    public bool HasGlobal(string _key)
    {
        var cfg = CfgMgr.Ins.GetGlobal(_key);
        return cfg != null;
    }

    public object GetGlobal(string _key)
    {
        var cfg = CfgMgr.Ins.GetGlobal(_key);
        if (cfg == null)
        {
            LogUtil.Error("global表没找到：" + _key);
            return null;
        }

        switch (cfg.info.GetTypeId())
        {
            case editor.cfg.global.GInt32.TYPE_ID:
                return ((editor.cfg.global.GInt32)cfg.info).value;
            case editor.cfg.global.GInt64.TYPE_ID:
                return ((editor.cfg.global.GInt64)cfg.info).value;
            case editor.cfg.global.GString.TYPE_ID:
                return ((editor.cfg.global.GString)cfg.info).value;
            case editor.cfg.global.GFloat.TYPE_ID:
                return ((editor.cfg.global.GFloat)cfg.info).value;
            case editor.cfg.global.GBool.TYPE_ID:
                return ((editor.cfg.global.GBool)cfg.info).value;
        }

        return null;
    }

    public int GetGlobalInt32(string _key)
    {
        var value = GetGlobal(_key);

        return (int)value;
    }

    public long GetGlobalInt64(string _key)
    {
        var value = GetGlobal(_key);

        return (long)value;
    }

    public string GetGlobalString(string _key)
    {
        var value = GetGlobal(_key);

        return value.ToString();
    }

    public float GetGlobalFloat(string _key)
    {
        var value = GetGlobal(_key);

        return (float)value;
    }

    public bool GetGlobalBool(string _key, bool _defaultVal = true)
    {
        if (!HasGlobal(_key))
        {
            return _defaultVal;
        }
        var value = GetGlobal(_key);
        return (bool)value;
    }

    public bool GetLanguage(SystemLanguage _language, string _key, out string _value)
    {
        bool result = false;
        _value = _key;
        switch (_language)
        {
            case SystemLanguage.English:
                {
                    var cfg = GetData().GetLanguage(_key);
                    if (cfg != null)
                    {
                        _value = cfg.english;
                        result = true;
                    }
                }
                break;
            case SystemLanguage.Chinese:
            case SystemLanguage.ChineseSimplified:
                {
                    var cfg = GetData().GetLanguage(_key);
                    if (cfg != null)
                    {
                        _value = cfg.chinese;
                        result = true;
                    }
                }
                break;
            default:
                result = GetLanguage(SystemLanguage.English, _key, out _value);
                break;
        }

        return result;
    }
}
