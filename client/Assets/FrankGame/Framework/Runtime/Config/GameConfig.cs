using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrankGame
{
    public static class GameConfig
    {
        public readonly static PackageConfig Package = PackageConfig.Instance;
        public readonly static VersionConfig Version = VersionConfig.Instance;
        public readonly static LogConfig Log = LogConfig.Instance;
        public readonly static DebugConfig Debug = DebugConfig.Instance;
        public readonly static SettingConfig Setting = SettingConfig.Instance;
        public readonly static PathConfig Path = PathConfig.Instance;
        public readonly static TableConfig Table = TableConfig.Instance;
        public readonly static LocalizationConfig Localization = LocalizationConfig.Instance;
        public readonly static DBConfig DB = DBConfig.Instance;
        public readonly static AudioConfig Audio = AudioConfig.Instance;
        public readonly static SdkConfig SDK = SdkConfig.Instance;
        public readonly static HotUpdateConfig HotUpdate = HotUpdateConfig.Instance;
        public readonly static FontConfig Font = FontConfig.Instance;

        public static string ServerListUrl;
        public static string AnnouncementUrl;
        public static string ResourceUrl;
        public static string FilesUrl;
        public static string GotoShopUrl;
        public static string GotoWebUrl;

        public static bool IsDebug
        {
            get
            {
                if (HotUpdate.CheckVersion && !isDebug)
                {
                    return false;
                }

                return Debug.IsDebugEnable();
            }
            set
            {
                if (isDebug != value)
                {
                    isDebug = value;

                    onDebugChangeCallback?.Invoke(IsDebug);
                }
            }
        }

        private static bool isDebug;

        private static System.Action<bool> onDebugChangeCallback;

        public static void AddDebugCallback(System.Action<bool> _onCallback)
        {
            onDebugChangeCallback += _onCallback;

            _onCallback?.Invoke(IsDebug);
        }

        public static void RemoveDebugCallback(System.Action<bool> _onCallback)
        {
            onDebugChangeCallback -= _onCallback;
        }
    }
}