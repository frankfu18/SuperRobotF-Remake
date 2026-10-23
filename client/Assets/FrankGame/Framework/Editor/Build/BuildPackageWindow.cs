using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace FrankGame
{
    public class BuildPackageWindow : EditorWindow
    {
        private const string AssetPath_SaveSub = "Assets/FrankGame/Framework/Editor/Build/PackageEditorConfig/";

        [MenuItem("FrankGame/打包/打包设置")]
        private static void OpenWindow()
        {
            BuildPackageWindow win = EditorWindow.CreateWindow<BuildPackageWindow>("打包参数设置");
            win.FindTotalEditorConfig();
            win.Show();
        }

        private List<PackageEditorConfigData> packageEditorConfigDataList = new List<PackageEditorConfigData>();
        private PackageEditorConfigData currentConfigData = null;
        private string tempConfigName = null;//临时名字
        private bool isEditorConfigName;
        private Vector2 configContentScrollPos;
        private Vector2 configListScrollPos;

        private void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("新建配置"))
            {
                PackageEditorConfigData configData = new PackageEditorConfigData();
                configData.configName = "新建配置_" + TimeUtility.NowSecond();
                configData.Read();

                packageEditorConfigDataList.Add(configData);

                currentConfigData = configData;
            }
            if (GUILayout.Button("删除当前配置"))
            {
                if (currentConfigData != null)
                {
                    DeleteConfig(currentConfigData);
                    currentConfigData = packageEditorConfigDataList.Count > 0 ? packageEditorConfigDataList[0] : null;
                }
            }
            if (GUILayout.Button("应用当前配置"))
            {
                if (currentConfigData == null)
                {
                    return;
                }

                currentConfigData.Apply();
            }
            if (GUILayout.Button("保存当前配置"))
            {
                SaveConfig(currentConfigData);
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("所有配置：");
            

            configListScrollPos = EditorGUILayout.BeginScrollView(configListScrollPos);
            EditorGUILayout.BeginHorizontal();
            if (packageEditorConfigDataList.Count > 0)
            {
                for (int i = 0; i < packageEditorConfigDataList.Count; i++)
                {
                    PackageEditorConfigData configData = packageEditorConfigDataList[i];

                    if (currentConfigData == configData)
                    {
                        GUI.color = Color.gray;
                        if (GUILayout.Button(configData.configName))
                        {
                            currentConfigData = configData;
                        }
                        GUI.color = Color.white;
                    }
                    else
                    {
                        if (GUILayout.Button(configData.configName))
                        {
                            currentConfigData = configData;
                        }
                    }
                }
            }
            else
            {
                EditorGUILayout.LabelField("暂无配置!");
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndScrollView();
            

            if (currentConfigData == null)
            {
                EditorGUILayout.LabelField("请选择配置！");
                return;
            }

            configContentScrollPos = EditorGUILayout.BeginScrollView(configContentScrollPos);

            EditorGUILayout.LabelField("=======================基本信息=======================");
            EditorGUILayout.LabelField("App名：" + Application.productName);
            EditorGUILayout.LabelField("包名：" + Application.identifier);
            EditorGUILayout.LabelField("App版本号：" + Application.version);

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("====================打包参数设置=======================");
            EditorGUILayout.BeginHorizontal();
            if (isEditorConfigName)
            {
                tempConfigName = EditorGUILayout.TextField("配置名称：", tempConfigName);
                if (GUILayout.Button("保存配置名称"))
                {
                    if (string.IsNullOrEmpty(tempConfigName))
                    {
                        return;
                    }

                    isEditorConfigName = false;

                    if (!tempConfigName.Equals(currentConfigData.configName))
                    {
                        ChangeConfigName(currentConfigData, tempConfigName);
                    }
                }
            }
            else
            {
                EditorGUILayout.LabelField("配置名称：" + currentConfigData.configName);
                if (GUILayout.Button("修改配置名称"))
                {
                    isEditorConfigName = true;
                    tempConfigName = currentConfigData.configName;
                }
            }
            
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("【声音】");
            currentConfigData.audio_audioPlayMode = (EAudioPlayMode)EditorGUILayout.EnumPopup("音频播放方式", currentConfigData.audio_audioPlayMode);
            currentConfigData.audio_netAudioTestUrl = EditorGUILayout.TextField("测试网络音频路径(仅用作本地测试)", currentConfigData.audio_netAudioTestUrl);

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("【数据库】");
            currentConfigData.db_isEncrypt = EditorGUILayout.Toggle("是否加密", currentConfigData.db_isEncrypt);

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("【调试】");
            currentConfigData.debug_enable = EditorGUILayout.Toggle("调试开关", currentConfigData.debug_enable);
            currentConfigData.debug_debugWindow = EditorGUILayout.Toggle("日志窗口开关", currentConfigData.debug_debugWindow);
            currentConfigData.debug_localDebug = EditorGUILayout.Toggle("本地调试开关", currentConfigData.debug_localDebug);

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("【字体】");
            currentConfigData.font_localizationConfigName = EditorGUILayout.TextField("字体本地化配置名称", currentConfigData.font_localizationConfigName);
            currentConfigData.font_loadAllLanguage = EditorGUILayout.Toggle("字体是否加载所有多语言", currentConfigData.font_loadAllLanguage);
            currentConfigData.font_clearAllFallbackFont = EditorGUILayout.Toggle("字体是否清除所有多语言", currentConfigData.font_clearAllFallbackFont);

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("【热更新】");
            currentConfigData.hotUpdate_checkVersion = EditorGUILayout.Toggle("检查版本", currentConfigData.hotUpdate_checkVersion);
            currentConfigData.hotUpdate_enable = EditorGUILayout.Toggle("热更新开关", currentConfigData.hotUpdate_enable);

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("【多语言】");
            currentConfigData.localization_defaultLanguage = (SystemLanguage)EditorGUILayout.EnumPopup("默认语言", currentConfigData.localization_defaultLanguage);
            currentConfigData.localization_followSystemLanguage = EditorGUILayout.Toggle("跟随系统语言", currentConfigData.localization_followSystemLanguage);

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("【日志】");
            currentConfigData.log_enable = EditorGUILayout.Toggle("日志开关", currentConfigData.log_enable);
            currentConfigData.log_writeLocal = EditorGUILayout.Toggle("是否写入本地", currentConfigData.log_writeLocal);
            currentConfigData.log_logLevel = (ELogLevel)EditorGUILayout.EnumFlagsField("显示级别", currentConfigData.log_logLevel);
            currentConfigData.log_logType = (ELogType)EditorGUILayout.EnumFlagsField("显示类别", currentConfigData.log_logType);

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("【打包】");
            currentConfigData.package_channelType = (EChannelType)EditorGUILayout.EnumFlagsField("渠道", currentConfigData.package_channelType);
            currentConfigData.package_resLoadMode = (ELoadMode)EditorGUILayout.EnumFlagsField("资源加载方式", currentConfigData.package_resLoadMode);
            currentConfigData.package_assetBundleOffset = (byte)EditorGUILayout.IntField("资源加密偏移", currentConfigData.package_assetBundleOffset);

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("【路径】");
            currentConfigData.path_localResRoot = EditorGUILayout.TextField("本地资源路径", currentConfigData.path_localResRoot);
            currentConfigData.path_assetBundleSuffix = EditorGUILayout.TextField("资源包后缀", currentConfigData.path_assetBundleSuffix);
            currentConfigData.path_headFileName = EditorGUILayout.TextField("头文件名字", currentConfigData.path_headFileName);
            currentConfigData.path_headFileSuffix = EditorGUILayout.TextField("头文件后缀", currentConfigData.path_headFileSuffix);

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("【SDK】");
            currentConfigData.sdk_playAd = EditorGUILayout.Toggle("广告开关", currentConfigData.sdk_playAd);
            currentConfigData.sdk_buyMoney = EditorGUILayout.Toggle("SDK支付开关", currentConfigData.sdk_buyMoney);

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("【服务器配置】");
            currentConfigData.server_hostPrefix = EditorGUILayout.TextField("URL前缀（空不开启）", currentConfigData.server_hostPrefix);
            currentConfigData.server_heartbeat = EditorGUILayout.IntField("心跳检测周期（秒）", currentConfigData.server_heartbeat);
            currentConfigData.server_md5Salt = EditorGUILayout.TextField("MD5盐值", currentConfigData.server_md5Salt);

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("【运行时设置】");
            currentConfigData.setting_frameRate = EditorGUILayout.IntField("帧率", currentConfigData.setting_frameRate);
            currentConfigData.setting_runInBackground = EditorGUILayout.Toggle("后台运行", currentConfigData.setting_runInBackground);

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("【表格设置】");
            currentConfigData.table_localTablePath = EditorGUILayout.TextField("本地资源路径", currentConfigData.table_localTablePath);
            currentConfigData.table_tableRoot = EditorGUILayout.TextField("加载相对路径", currentConfigData.table_tableRoot);
            currentConfigData.table_tableSuffix = EditorGUILayout.TextField("表格配置文件后缀", currentConfigData.table_tableSuffix);
            currentConfigData.table_tableLayer = EditorGUILayout.IntField("表格配置打包层级", currentConfigData.table_tableLayer);

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("【版本设置】");
            EditorGUILayout.BeginHorizontal();
            currentConfigData.version_currentVersion = (EVersionType)EditorGUILayout.EnumPopup("当前版本", currentConfigData.version_currentVersion);
            if (GUILayout.Button("增加"))
            {
                VersionInfo versionInfo = new VersionInfo();

                currentConfigData.version_versionInfos.Add(versionInfo);
            }
            EditorGUILayout.EndHorizontal();

            if (currentConfigData.version_versionInfos.Count > 0)
            {
                bool deleteVersion = false;
                for (int i = 0; i < currentConfigData.version_versionInfos.Count; i++)
                {
                    var versionInfo = currentConfigData.version_versionInfos[i];
                    EditorGUILayout.LabelField("(" + i + ")");

                    EditorGUILayout.BeginHorizontal();
                    versionInfo.VersionType = (EVersionType)EditorGUILayout.EnumPopup("版本", versionInfo.VersionType);
                    if (GUILayout.Button("删除"))
                    {
                        currentConfigData.version_versionInfos.RemoveAt(i);
                        deleteVersion = true;
                    }
                    EditorGUILayout.EndHorizontal();

                    if (deleteVersion)
                    {
                        break;
                    }
                    versionInfo.VersionUrl = EditorGUILayout.TextField("版本文件地址", versionInfo.VersionUrl);
                }
            }
            else
            {
                EditorGUILayout.LabelField("暂无版本设置");
            }
            
            EditorGUILayout.EndScrollView();
        }

        public void FindTotalEditorConfig()
        {
            packageEditorConfigDataList.Clear();

            string dirPath = FileUtils.AssetPathToFilePath(AssetPath_SaveSub);

            List<string> filePaths = FileUtils.FindFilesInDirectoryByFilter(dirPath, (fileName) => 
            {
                return !fileName.Contains(".meta") && fileName.Contains(".json");
            });

            if (filePaths.Count > 0)
            {
                foreach (var filePath in filePaths)
                {
                    string json = File.ReadAllText(filePath);

                    PackageEditorConfigData configData = JsonUtility.FromJson<PackageEditorConfigData>(json);

                    packageEditorConfigDataList.Add(configData);
                }

                currentConfigData = packageEditorConfigDataList[0];
            }
        }

        private void SaveConfig(PackageEditorConfigData _configData)
        {
            _configData.Save();

            string json = JsonUtility.ToJson(_configData);

            File.WriteAllText(GetConfigPath(_configData.configName), json);

            AssetDatabase.Refresh();
        }

        private void DeleteConfig(PackageEditorConfigData _configData)
        {
            packageEditorConfigDataList.Remove(_configData);

            string configPath = GetConfigPath(_configData.configName);
            if (File.Exists(configPath))
            {
                File.Delete (configPath);

                AssetDatabase.Refresh();
            }
        }

        private void ChangeConfigName(PackageEditorConfigData _configData, string _newName)
        {
            string oldName = _configData.configName;

            _configData.configName = _newName;
            _configData.Save();

            string json = JsonUtility.ToJson(_configData);

            File.WriteAllText(GetConfigPath(_configData.configName), json);

            string configPath = GetConfigPath(oldName);
            if (File.Exists(configPath))
            {
                File.Delete(configPath);
            }

            AssetDatabase.Refresh();
        }

        private string GetConfigPath(string _configName)
        {
            return FileUtils.AssetPathToFilePath(AssetPath_SaveSub + _configName + ".json");
        }
        
    }
}