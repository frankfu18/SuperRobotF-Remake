using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace FrankGame
{
    public class LanguageChange
    {
        [MenuItem("FrankGame/切换语言/中文")]
        private static void ChangeChinese()
        {
            LocalizationManager.Instance.ChangeLanguage(SystemLanguage.Chinese);
        }

        [MenuItem("FrankGame/切换语言/英文")]
        private static void ChangeEnglish()
        {
            LocalizationManager.Instance.ChangeLanguage(SystemLanguage.English);
        }
    }
}
