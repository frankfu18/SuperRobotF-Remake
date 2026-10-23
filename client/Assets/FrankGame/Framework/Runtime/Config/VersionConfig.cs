using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrankGame
{
    [CreateAssetMenu(menuName = "FrankGame/ScriptableObject/VersionConfig", fileName = "VersionConfig")]
    public class VersionConfig : SingleScriptableObject<VersionConfig>
    {
        [Header("当前版本")]
        public EVersionType CurrentVersion;
        [Header("版本列表")]
        public List<VersionInfo> VersionInfos = new List<VersionInfo>();

        public VersionInfo VersionInfo
        {
            get
            {
                foreach (var versionInfo in VersionInfos)
                {
                    if (versionInfo.VersionType == CurrentVersion)
                    {
                        return versionInfo;
                    }
                }

                return null;
            }
        }
    }
}