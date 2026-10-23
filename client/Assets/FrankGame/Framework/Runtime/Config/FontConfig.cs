using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrankGame
{
    [CreateAssetMenu(menuName = "FrankGame/ScriptableObject/FontConfig", fileName = "FontConfig")]
    public class FontConfig : SingleScriptableObject<FontConfig>
    {
        [Header("字体本地化配置名称")]
        public string FontLocalizationConfigName;

        [Header("字体是否加载所有多语言")]
        public bool LoadAllLanguage;

        [Header("字体是否清除所有多语言")]
        public bool ClearAllFallbackFont;
    }
}
