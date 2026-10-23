using UnityEngine;

namespace FrankGame
{
    [CreateAssetMenu(menuName = "FrankGame/ScriptableObject/SettingConfig", fileName = "SettingConfig")]
    public class SettingConfig : SingleScriptableObject<SettingConfig>
    {
        [Header("帧率")] 
        public int FrameRate = 60;
        [Header("后台运行")]
        public bool RunInBackground = true;
        [Header("后台运行最大持续时间(秒，0代表不会超时)")]
        public float RunInBackgroundMaxDuration = 0;
    }
}