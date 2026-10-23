using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrankGame
{
    [CreateAssetMenu(menuName = "FrankGame/ScriptableObject/LocalizationConfig", fileName = "LocalizationConfig")]
    public class LocalizationConfig : SingleScriptableObject<LocalizationConfig>
    {
        [Header("默认语言")]
        public SystemLanguage DefaultLanguage = SystemLanguage.English;

        [Header("是否跟随系统语言")]
        public bool FollowSystemLanguage = true;
    }
}