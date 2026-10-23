using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/TMPLocalizationConfig", fileName = "TMPLocalizationConfig")]
public class TMPLocalizationConfig : ScriptableObject
{
    public List<TMPFontInfo> fontInfoList;
}

[System.Serializable]
public class TMPFontInfo
{
    public string baseFontAssetPath;
    public List<TMPLanguageInfo> languageInfoList;
}

[System.Serializable]
public class TMPLanguageInfo
{
    public SystemLanguage language;
    public string fontAssetPath;
}
