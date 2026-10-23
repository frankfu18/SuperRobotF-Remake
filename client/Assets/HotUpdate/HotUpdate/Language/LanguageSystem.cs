using FrankGame;
using UnityEngine;
using System.Collections.Generic;
using editor.cfg.localize;

public class LanguageSystem : Singlet<LanguageSystem>
{
    public static readonly Dictionary<SystemLanguage, string> LanguageToNameDic = new Dictionary<SystemLanguage, string>()
    {
        {SystemLanguage.English, "English" },
        {SystemLanguage.ChineseSimplified, "简体中文" },
    };

    public SystemLanguage CurLanguage
    {
        get
        {
            return LocalizationManager.Instance.Lanuage;
        }
    }

    public string GetLanguage(string _key, params object[] _params)
    {
        return GetLanguage(_key, CurLanguage, _params);
    }

    public string GetLanguage(string _key, SystemLanguage _language, params object[] _params)
    {
        if (ConfigSystem.Instance.GetData() == null)
        {
            if (_params.Length > 0)
            {
                return string.Format(_key, _params);
            }

            return _key;
        }

        string result;
        ConfigSystem.Instance.GetLanguage(_language, _key, out result);

        if (_params.Length > 0)
        {
            result = string.Format(result, _params);
        }

        return result.Replace("\\n", "\n");
    }

    public void SetLanguage(SystemLanguage _language)
    {
        LocalizationManager.Instance.ChangeLanguage(_language);
    }

    public void SetLanguageCache(SystemLanguage _language)
    {
        LocalizationManager.Instance.SaveLanguageCache(_language);
    }

    public SystemLanguage GetLanguageCache()
    {
        return LocalizationManager.Instance.GetLanguageCache();
    }
}
