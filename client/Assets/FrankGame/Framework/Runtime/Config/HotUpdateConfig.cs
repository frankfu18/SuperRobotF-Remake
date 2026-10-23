using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrankGame
{
    [CreateAssetMenu(menuName = "FrankGame/ScriptableObject/HotUpdateConfig", fileName = "HotUpdateConfig")]
    public class HotUpdateConfig : SingleScriptableObject<HotUpdateConfig>
    {
        [Header("检查版本")]
        public bool CheckVersion;

        [Header("热更新开关")]
        public bool Enable;

        [Header("检测服务器时间间隔（秒）")]
        public int CheckTimeInterval = 60;

        /// <summary>
        /// 是否检测服务器时间
        /// </summary>
        /// <returns></returns>
        public bool IsCheckServerTime()
        {
            return CheckVersion && CheckTimeInterval > 0;
        }
    }
}
