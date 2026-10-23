using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrankGame
{
    [CreateAssetMenu(menuName = "FrankGame/ScriptableObject/DebugConfig", fileName = "DebugConfig")]
    public class DebugConfig : SingleScriptableObject<DebugConfig>
    {
        private const string Local_Debug_Key = "Local_Debug_Key";

        [Header("调试模式")]
        public bool Enable = true;

        [Header("调试窗口")]
        public bool DebugWindow = true;

        [Header("本地控制开关")]
        public bool LocalDebug = true;

        [Header("模拟异步间隔时间(秒)")]
        public float AsynIntervalTime = 0;

        public bool IsDebugEnable()
        {
            if (LocalDebug && PlayerPrefs.HasKey(Local_Debug_Key))
            {
                return PlayerPrefs.GetInt(Local_Debug_Key) > 0;
            }

            return Enable;
        }

        //设置本地缓存的debug参数，仅不联网时生效
        public void SetLocalDebug(bool _debug)
        {
            if (!LocalDebug)
            {
                return;
            }

            PlayerPrefs.SetInt(Local_Debug_Key, _debug ? 1 : 0);

            GameConfig.IsDebug = _debug;
        }

        public float GetAsynIntervalTime()
        {
            if (IsDebugEnable())
            {
                return Mathf.Max(AsynIntervalTime, 0);
            }

            return 0;
        }
    }
}
