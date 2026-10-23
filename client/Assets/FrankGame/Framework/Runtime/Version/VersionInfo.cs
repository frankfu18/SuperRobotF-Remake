using UnityEngine;

namespace FrankGame
{
    [System.Serializable]
    public class VersionInfo
    {
        [Header("版本")] 
        public EVersionType VersionType;
        [Header("版本文件地址")]
        public string VersionUrl;
    }
}