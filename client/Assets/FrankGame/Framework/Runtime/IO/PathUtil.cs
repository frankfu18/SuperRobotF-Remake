using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrankGame
{
    public static class PathUtil
    {
        public static string StreamingAsset_Path
        {
            get
            {
                string filepath = string.Empty;
#if UNITY_EDITOR
                filepath = Application.dataPath + "/StreamingAssets/";
#elif UNITY_IPHONE
        filepath = Application.dataPath +"/Raw/";
#elif UNITY_ANDROID
        filepath = "jar:file://" + Application.dataPath + "!/assets/";
#endif
                return filepath;
            }
        }

        public static string PersistentData_Path
        {
            get
            {
                return Application.persistentDataPath + "/";
            }
        }

        public static string GetResPrefixPath()
        {
            return "bundle/";
        }

        public static string GetConfigPrefixPath()
        {
            return "config/";
        }

        public static string GetLocalResPath()
        {
            return PersistentData_Path + GetResPrefixPath();
        }

        public static string GetLocalConfigPath()
        {
            return PersistentData_Path + GetConfigPrefixPath();
        }

        public static string GetLocalDllPath()
        {
            return PersistentData_Path + "dll/";
        }
    }
}