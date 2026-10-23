using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

namespace FrankGame
{
    public class LocalizationManager : Singlet<LocalizationManager>
    {
        private const string Localization_Key = "Localization_Cache_Key";
        private const string Package_Localization_Table_Path = "FrankGame/PackageLocalizationTable";

        public SystemLanguage Lanuage
        {
            get { return curLanuage; }
        }

        private SystemLanguage curLanuage;
        private Dictionary<SystemLanguage, Dictionary<string, string>> packageLanguageTextDic = new Dictionary<SystemLanguage, Dictionary<string, string>>();

        public void CheckLanguage(SystemLanguage _defaultLanguage)
        {
            if (PlayerPrefs.HasKey(Localization_Key))
            {
                SetLanguage((SystemLanguage)PlayerPrefs.GetInt(Localization_Key));
            }
            else
            {
                SetLanguage(_defaultLanguage);
            }

            LoadPackageLocalizationTable();//加载包内多语言配置
        }

        private void SetLanguage(SystemLanguage _language)
        {
            curLanuage = _language;

            PlayerPrefs.SetInt(Localization_Key, (int)curLanuage);
        }

        public void ChangeLanguage(SystemLanguage _language)
        {
            if (curLanuage == _language)
            {
                return;
            }

            SetLanguage(_language);
        }

        public void SaveLanguageCache(SystemLanguage _language)
        {
            PlayerPrefs.SetInt(Localization_Key, (int)_language);
        }

        public SystemLanguage GetLanguageCache()
        {
            if (PlayerPrefs.HasKey(Localization_Key))
            {
                return (SystemLanguage)PlayerPrefs.GetInt(Localization_Key);
            }
            else
            {
                return curLanuage;
            }
        }

        public void LoadPackageLocalizationTable()
        {
            TextAsset xmlAsset = Resources.Load<TextAsset>(Package_Localization_Table_Path);
            if (xmlAsset == null)
            {
                Log.Error("未找到包内多语言!");
                return;
            }

            packageLanguageTextDic.Clear();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlAsset.text);

            XmlNodeList nodeList = xmlDoc.SelectNodes("PackageLocalization");
            foreach (XmlElement languageNode in nodeList)
            {
                var childNodeList = languageNode.ChildNodes;

                foreach (XmlNode keyNode in childNodeList)
                {
                    string key = ((XmlElement)keyNode).GetAttribute("key");

                    XmlNodeList list = keyNode.ChildNodes;
                    foreach (XmlElement item in list)
                    {
                        switch (item.Name)
                        {
                            case "chinese":
                                AddPackageLanguageText(SystemLanguage.Chinese, key, item.InnerText);
                                break;
                            case "chineseTraditional":
                                AddPackageLanguageText(SystemLanguage.ChineseTraditional, key, item.InnerText);
                                break;
                            case "english":
                                AddPackageLanguageText(SystemLanguage.English, key, item.InnerText);
                                break;
                            case "japanese":
                                AddPackageLanguageText(SystemLanguage.Japanese, key, item.InnerText);
                                break;
                            case "korean":
                                AddPackageLanguageText(SystemLanguage.Korean, key, item.InnerText);
                                break;
                            case "vietnamese":
                                AddPackageLanguageText(SystemLanguage.Vietnamese, key, item.InnerText);
                                break;
                            default:
                                Log.Error("加载本地化语言未找到对应语言：" + item.Name);
                                break;
                        }
                    }
                }
            }
        }

        private void AddPackageLanguageText(SystemLanguage _language, string _key, string _value)
        {
            Dictionary<string, string> languageTextDic;
            if (!packageLanguageTextDic.TryGetValue(_language, out languageTextDic))
            {
                languageTextDic = new Dictionary<string, string>();

                packageLanguageTextDic.Add(_language, languageTextDic);
            }

            languageTextDic.Add(_key, _value);
        }

        public string GetPackageLanguageText(string _key, params object[] _params)
        {
            string text;
            if (GetPackageLanguageText(curLanuage, _key, out text, _params))
            {
                return text;
            }
            else if (GetPackageLanguageText(GameConfig.Localization.DefaultLanguage, _key, out text, _params))
            {
                return text;
            }

            return _key;
        }

        public bool GetPackageLanguageText(SystemLanguage _language, string _key, out string _text, params object[] _params)
        {
            if (_language == SystemLanguage.ChineseSimplified)
            {
                _language = SystemLanguage.Chinese;
            }

            if (packageLanguageTextDic.TryGetValue(_language, out Dictionary<string, string> languageTextDic))
            {
                if (languageTextDic.TryGetValue(_key, out _text))
                {
                    if (_params != null && _params.Length > 0)
                    {
                        _text = string.Format(_text, _params);
                    }

                    return true;
                }
                else
                {
                    Log.Error("未找到本地化key： language = " + _language + ", key = " + _key);
                }
            }
            else
            {
                Log.Error("未找到本地化语言：" + _language);
            }

            _text = _key;

            return false;
        }
    }
}