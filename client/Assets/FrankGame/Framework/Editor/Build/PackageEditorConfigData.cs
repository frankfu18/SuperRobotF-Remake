using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace FrankGame
{
    [System.Serializable]
    public class PackageEditorConfigData
    {
        public string configName;

        public EAudioPlayMode audio_audioPlayMode;
        public string audio_netAudioTestUrl;

        public bool db_isEncrypt;

        public bool debug_enable;
        public bool debug_debugWindow;
        public bool debug_localDebug;
        public float debug_asynIntervalTime;

        public string font_localizationConfigName;
        public bool font_loadAllLanguage;
        public bool font_clearAllFallbackFont;

        public bool hotUpdate_checkVersion;
        public bool hotUpdate_enable;

        public SystemLanguage localization_defaultLanguage;
        public bool localization_followSystemLanguage;

        public bool log_enable; 
        public bool log_writeLocal;
        public ELogLevel log_logLevel;
        public ELogType log_logType;

        public EChannelType package_channelType;
        public ELoadMode package_resLoadMode;
        public byte package_assetBundleOffset;

        public string path_localResRoot;
        public string path_assetBundleSuffix;
        public string path_headFileName;
        public string path_headFileSuffix;

        public bool sdk_playAd;
        public bool sdk_buyMoney;

        public string server_hostPrefix;
        public int server_heartbeat;
        public string server_md5Salt;

        public int setting_frameRate;
        public bool setting_runInBackground;
        public float setting_runInBackgroundMaxDuration;

        public string table_localTablePath;
        public string table_tableRoot;
        public string table_tableSuffix;
        public int table_tableLayer;

        public EVersionType version_currentVersion;
        public List<VersionInfo> version_versionInfos;


        public void Save()
        {
            
        }

        public void Read()
        {
            this.audio_audioPlayMode = AudioConfig.Instance.AudioPlayMode;
            this.audio_netAudioTestUrl = AudioConfig.Instance.NetAudioTestUrl;

            this.db_isEncrypt = DBConfig.Instance.IsEncrypt;

            this.debug_enable = DebugConfig.Instance.Enable;
            this.debug_debugWindow = DebugConfig.Instance.DebugWindow;
            this.debug_localDebug = DebugConfig.Instance.LocalDebug;
            this.debug_asynIntervalTime = DebugConfig.Instance.AsynIntervalTime;

            this.font_localizationConfigName = FontConfig.Instance.FontLocalizationConfigName;
            this.font_loadAllLanguage = FontConfig.Instance.LoadAllLanguage;
            this.font_clearAllFallbackFont = FontConfig.Instance.ClearAllFallbackFont;

            this.hotUpdate_checkVersion = HotUpdateConfig.Instance.CheckVersion;
            this.hotUpdate_enable = HotUpdateConfig.Instance.Enable;

            this.localization_defaultLanguage = LocalizationConfig.Instance.DefaultLanguage;
            this.localization_followSystemLanguage = LocalizationConfig.Instance.FollowSystemLanguage;

            this.log_enable = LogConfig.Instance.Enable;
            this.log_writeLocal = LogConfig.Instance.WriteLocal;
            this.log_logLevel = LogConfig.Instance.LogLevel;
            this.log_logType = LogConfig.Instance.LogType;

            this.package_channelType = PackageConfig.Instance.ChannelType;
            this.package_resLoadMode = PackageConfig.Instance.ResLoadMode;
            this.package_assetBundleOffset = PackageConfig.Instance.AssetBundleOffset;

            this.path_localResRoot = PathConfig.Instance.LocalResRoot;
            this.path_assetBundleSuffix = PathConfig.Instance.AssetBundleSuffix;
            this.path_headFileName = PathConfig.Instance.HeadFileName;
            this.path_headFileSuffix = PathConfig.Instance.HeadFileSuffix;

            this.sdk_playAd = SdkConfig.Instance.PlayAd;
            this.sdk_buyMoney = SdkConfig.Instance.BuyMoney;

            this.server_hostPrefix = ServerConfig.Instance.HostPrefix;
            this.server_heartbeat = ServerConfig.Instance.Heartbeat;
            this.server_md5Salt = ServerConfig.Instance.MD5Salt;

            this.setting_frameRate = SettingConfig.Instance.FrameRate;
            this.setting_runInBackground = SettingConfig.Instance.RunInBackground;

            this.table_localTablePath = TableConfig.Instance.LocalTablePath;
            this.table_tableRoot = TableConfig.Instance.TableRoot;
            this.table_tableSuffix = TableConfig.Instance.TableSuffix;
            this.table_tableLayer = TableConfig.Instance.TableLayer;

            this.version_currentVersion = VersionConfig.Instance.CurrentVersion;
            this.version_versionInfos = VersionConfig.Instance.VersionInfos;
        }

        public void Apply()
        {
            AudioConfig.Instance.AudioPlayMode = this.audio_audioPlayMode;
            AudioConfig.Instance.NetAudioTestUrl = this.audio_netAudioTestUrl;

            DBConfig.Instance.IsEncrypt = this.db_isEncrypt;

            DebugConfig.Instance.Enable = this.debug_enable;
            DebugConfig.Instance.DebugWindow = this.debug_debugWindow;
            DebugConfig.Instance.LocalDebug = this.debug_localDebug;
            DebugConfig.Instance.AsynIntervalTime = this.debug_asynIntervalTime;

            FontConfig.Instance.FontLocalizationConfigName = this.font_localizationConfigName;
            FontConfig.Instance.LoadAllLanguage = this.font_loadAllLanguage;
            FontConfig.Instance.ClearAllFallbackFont = this.font_clearAllFallbackFont;

            HotUpdateConfig.Instance.CheckVersion = this.hotUpdate_checkVersion;
            HotUpdateConfig.Instance.Enable = this.hotUpdate_enable;

            LocalizationConfig.Instance.DefaultLanguage = this.localization_defaultLanguage;
            LocalizationConfig.Instance.FollowSystemLanguage = this.localization_followSystemLanguage;

            LogConfig.Instance.Apply(this.log_enable);
            LogConfig.Instance.WriteLocal = this.log_writeLocal;
            LogConfig.Instance.LogLevel = this.log_logLevel;
            LogConfig.Instance.LogType = this.log_logType;

            PackageConfig.Instance.Apply(this.package_channelType);
            PackageConfig.Instance.ResLoadMode = this.package_resLoadMode;
            PackageConfig.Instance.AssetBundleOffset = this.package_assetBundleOffset;

            PathConfig.Instance.LocalResRoot = this.path_localResRoot;
            PathConfig.Instance.AssetBundleSuffix = this.path_assetBundleSuffix;
            PathConfig.Instance.HeadFileName = this.path_headFileName;
            PathConfig.Instance.HeadFileSuffix = this.path_headFileSuffix;

            SdkConfig.Instance.PlayAd = this.sdk_playAd;
            SdkConfig.Instance.BuyMoney = this.sdk_buyMoney;

            ServerConfig.Instance.HostPrefix = this.server_hostPrefix;
            ServerConfig.Instance.Heartbeat = this.server_heartbeat;
            ServerConfig.Instance.MD5Salt = this.server_md5Salt;

            SettingConfig.Instance.FrameRate = this.setting_frameRate;
            SettingConfig.Instance.RunInBackground = this.setting_runInBackground;
            SettingConfig.Instance.RunInBackgroundMaxDuration = this.setting_runInBackgroundMaxDuration;

            TableConfig.Instance.LocalTablePath = this.table_localTablePath;
            TableConfig.Instance.TableRoot = this.table_tableRoot;
            TableConfig.Instance.TableSuffix = this.table_tableSuffix;
            TableConfig.Instance.TableLayer = this.table_tableLayer;

            VersionConfig.Instance.CurrentVersion = this.version_currentVersion;
            VersionConfig.Instance.VersionInfos = this.version_versionInfos;

            EditorUtility.SetDirty(AudioConfig.Instance);
            EditorUtility.SetDirty(DBConfig.Instance);
            EditorUtility.SetDirty(DebugConfig.Instance);
            EditorUtility.SetDirty(FontConfig.Instance);
            EditorUtility.SetDirty(LocalizationConfig.Instance);
            EditorUtility.SetDirty(LogConfig.Instance);
            EditorUtility.SetDirty(PackageConfig.Instance);
            EditorUtility.SetDirty(PathConfig.Instance);
            EditorUtility.SetDirty(SettingConfig.Instance);
            EditorUtility.SetDirty(TableConfig.Instance);
            EditorUtility.SetDirty(VersionConfig.Instance);

            AssetDatabase.SaveAssets();
        }
    }
}