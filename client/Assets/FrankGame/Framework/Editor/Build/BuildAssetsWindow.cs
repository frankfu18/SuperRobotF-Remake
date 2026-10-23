using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using TMPro;

namespace FrankGame
{
    public class BuildAssetsWindow : EditorWindow
    {
        [MenuItem("FrankGame/打包/热更资源")]
        private static void OpenWindow()
        {
            BuildAssetsWindow win = EditorWindow.CreateWindow<BuildAssetsWindow>("热更资源");
            win.Show();
        }

        private static string AssetBundlePath
        {
            get
            {
                return Application.dataPath.Replace("Assets", "Output/AssetBundle/{0}/");
            }
        }
        private static readonly string AssetBundleSourcePath = AssetBundlePath + "{1}/Source/";
        private static readonly string AssetBundleReleasePath = AssetBundlePath + "{1}/Release/";
        private static readonly string ResPath = Application.dataPath + "/Res/";
        private static readonly string ConfigPath = Application.dataPath + "/Res/config/";

        private static string assetBundlePath;
        private static string targetSourcePath;
        private static string targetSourceConfigPath;
        private static string targetSourceResPath;
        private static string targetSourceDllPath;
        private static string targetReleasePath;
        private static string targetReleaseConfigPath;
        private static string targetReleaseResPath;
        private static string targetReleaseDllPath;

        private static List<string> AOTMetaAssemblyNames { get; } = new List<string>()
        {
            "Base",
            "DOTween",
            "HachiSdk",
            "LitJson",
            "System.Core",
            "System",
            "UnityEngine.CoreModule",
            "UnityEngine.UI",
            "mscorlib",
            "FrankGame",
            "FrankGame.Event",
            "FrankGame.Timer",
        };

        private BuildTarget activeTarget
        {
            get
            {
                return EditorUserBuildSettings.activeBuildTarget;
            }
        }

        private static string Key_Ver_Name = "HotUpdate_Ver";

        private string verName = "test";
        private static List<AssetImporter> assetImporterList = new List<AssetImporter>();
        private static Dictionary<string, int> bundleCountDic = new Dictionary<string, int>();

        private void OnEnable()
        {
            if (PlayerPrefs.HasKey(Key_Ver_Name))
            {
                verName = PlayerPrefs.GetString(Key_Ver_Name);
            }
        }

        private void OnGUI()
        {
            verName = EditorGUILayout.TextField("设置版本：", verName);

            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("表格配置：");
            if (GUILayout.Button("生成表格配置"))
            {
                if (string.IsNullOrEmpty(verName))
                {
                    Debug.LogError("请设置版本名称！！！");
                    return;
                }

                CreateDir();
                BuildConfig(1, 1);
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("资源：");
            if (GUILayout.Button("生成资源Bundle"))
            {
                if (string.IsNullOrEmpty(verName))
                {
                    Debug.LogError("请设置版本名称！！！");
                    return;
                }

                CreateDir();
                BuildAsset(1, 1);
            }
            EditorGUILayout.EndHorizontal();
#if ENABLE_HOTUPDATE
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("代码：");
            if (GUILayout.Button("仅生成代码库"))
            {
                if (string.IsNullOrEmpty(verName))
                {
                    Debug.LogError("请设置版本名称！！！");
                    return;
                }

                CreateDir();
                BuildHotUpdateDll(1, 1, false);
            }
            if (GUILayout.Button("生成全部代码配置"))
            {
                if (string.IsNullOrEmpty(verName))
                {
                    Debug.LogError("请设置版本名称！！！");
                    return;
                }

                CreateDir();
                BuildHotUpdateDll(1, 1, true);
            }
            EditorGUILayout.EndHorizontal();
#endif

            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("流程：");
            if (GUILayout.Button("打包所有"))
            {
                if (string.IsNullOrEmpty(verName))
                {
                    Debug.LogError("请设置版本名称！！！");
                    return;
                }

                CreateDir();
                BuildConfig(1, 3);
                BuildAsset(2, 3);
#if ENABLE_HOTUPDATE
                BuildHotUpdateDll(3, 3, true);
#endif
            }
            if (GUILayout.Button("合并目录文件"))
            {
                CreateDir();
                MergeTotalIndexFile();
            }
            if (GUILayout.Button("拷贝资源到StreamingAssets"))
            {
                CreateDir();
                CopyToStreaming();
            }
            if (GUILayout.Button("拷贝Sdk配置"))
            {
                CreateDir();
                CopySdkFiles();
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("一键打包：");
            if (GUILayout.Button("【仅生成不拷贝】"))
            {
                CreateDir();
                BuildConfig(1, 3);
                BuildAsset(2, 3);
#if ENABLE_HOTUPDATE
                BuildHotUpdateDll(3, 3, true);
#endif
                MergeTotalIndexFile();
                Debug.Log("打包全部完成。");
            }
            if (GUILayout.Button("【生成并拷贝到StreamingAssets】"))
            {
                CreateDir();
                BuildConfig(1, 3);
                BuildAsset(2, 3);
#if ENABLE_HOTUPDATE
                BuildHotUpdateDll(3, 3, true);
#endif
                MergeTotalIndexFile();
                CopyToStreaming();
                CopySdkFiles();
                Debug.Log("打包全部完成。");
            }
            EditorGUILayout.EndHorizontal();
        }

        private void CreateDir()
        {
            Debug.Log("Build Target:" + activeTarget);

            PlayerPrefs.SetString(Key_Ver_Name, verName);

            assetBundlePath = string.Format(AssetBundlePath, activeTarget);
            targetSourcePath = string.Format(AssetBundleSourcePath, activeTarget, verName);
            targetSourceConfigPath = targetSourcePath + "config/";
            targetSourceResPath = targetSourcePath + "bundle/";
            targetSourceDllPath = targetSourcePath + "dll/";
            targetReleasePath = string.Format(AssetBundleReleasePath, activeTarget, verName);
            targetReleaseConfigPath = targetReleasePath + "config/";
            targetReleaseResPath = targetReleasePath + "bundle/";
            targetReleaseDllPath = targetReleasePath + "dll/";

            if (!Directory.Exists(targetSourcePath))
            {
                Directory.CreateDirectory(targetSourcePath);
            }

            if (!Directory.Exists(targetReleasePath))
            {
                Directory.CreateDirectory(targetReleasePath);
            }
        }

#region 表格

        //构建表格配置Bundle
        private void BuildConfig(int _curProgress, int _totalProgerss)
        {
            EditorUtility.DisplayProgressBar("打包", "打包表格中...", _curProgress / (float)_totalProgerss);

            if (!Directory.Exists(targetSourceConfigPath))
            {
                Directory.CreateDirectory(targetSourceConfigPath);
            }
            else
            {
                FileUtils.ClearDirectory(targetSourceConfigPath);
            }

            List<AssetBundleBuild> configBuildList = new List<AssetBundleBuild>();

            var configs = FileUtils.FindFilesInDirectoryByFilter(ConfigPath, (fileName) =>
            {
                return fileName.EndsWith(GameConfig.Table.TableSuffix);
            });

            if (GameConfig.Table.TableLayer > 0)
            {
                foreach (var configFileName in configs)
                {
                    FileInfo fileInfo = new FileInfo(configFileName);

                    AssetBundleBuild configBuild = new AssetBundleBuild();

                    configBuild.assetBundleName = fileInfo.Name.Replace(fileInfo.Extension, "") + GameConfig.Path.AssetBundleSuffix;
                    configBuild.assetNames = new string[] { FileUtils.FilePathToAssetPath(fileInfo.FullName) };

                    configBuildList.Add(configBuild);
                }
            }
            else
            {
                AssetBundleBuild configBuild = new AssetBundleBuild();
                configBuild.assetBundleName = "config" + GameConfig.Path.AssetBundleSuffix;

                string[] configAssets = new string[configs.Count];

                for (int i = 0; i < configs.Count; i++)
                {
                    FileInfo fileInfo = new FileInfo(configs[i]);

                    configAssets[i] = FileUtils.FilePathToAssetPath(fileInfo.FullName);
                }

                configBuild.assetNames = configAssets;

                configBuildList.Add(configBuild);
            }

            BuildPipeline.BuildAssetBundles(targetSourceConfigPath, configBuildList.ToArray(), BuildAssetBundleOptions.AppendHashToAssetBundleName | BuildAssetBundleOptions.ChunkBasedCompression, activeTarget);

            Debug.Log("表格配置构建完成，表格数量：" + configs.Count);

            CreateTableIndex();
            CreateReleaseConfig();
            Encrypt(targetReleaseConfigPath);

            EditorUtility.ClearProgressBar();
        }

        private ResIndexInfo CreateTableIndex()
        {
            ResIndexInfo resIndexInfo = new ResIndexInfo();
            resIndexInfo.configList = new List<ResUpdateInfo>();

            FileUtils.FindFilesInDirectoryByFilter(targetSourceConfigPath, (file) =>
            {
                if (file.Extension == GameConfig.Path.AssetBundleSuffix)
                {
                    ResUpdateInfo bundleUpdateInfo = new ResUpdateInfo();

                    string fileName = file.Name.Replace("\\", "/").Replace(file.Extension, "");

                    int flagIdx = fileName.LastIndexOf('_');
                    string newName = fileName.Substring(flagIdx + 1);

                    string bundleName = file.FullName.Replace("\\", "/").Replace(file.Extension, "").Replace(targetSourceConfigPath, "");
                    flagIdx = bundleName.LastIndexOf('_');
                    bundleName = bundleName.Substring(0, flagIdx);
                    bundleName += GameConfig.Path.AssetBundleSuffix;

                    bundleUpdateInfo.localPath = file.Name;
                    bundleUpdateInfo.hash = newName;
                    bundleUpdateInfo.name = bundleName;
                    bundleUpdateInfo.size = file.Length;

                    resIndexInfo.configList.Add(bundleUpdateInfo);
                }
            });

            string jsonFilePath = targetSourceConfigPath.Replace(targetSourcePath, "");
            jsonFilePath = jsonFilePath.Substring(0, jsonFilePath.Length - 1) + ResConfig.Json_Suffix;
            jsonFilePath = targetSourcePath + jsonFilePath;

            string content = JsonUtility.ToJson(resIndexInfo, true);

            File.WriteAllText(jsonFilePath, content);

            Debug.Log("生成配置索引文件成功：" + jsonFilePath);

            return resIndexInfo;
        }

        private void CreateReleaseConfig()
        {
            if (!Directory.Exists(targetReleaseConfigPath))
            {
                Directory.CreateDirectory(targetReleaseConfigPath);
            }
            else
            {
                FileUtils.ClearDirectory(targetReleaseConfigPath);
            }

            FileUtils.FindFilesInDirectoryByFilter(targetSourceConfigPath, (file) =>
            {
                if (file.Extension == GameConfig.Path.AssetBundleSuffix)//资源包，改名成hash
                {
                    string newName = file.Name.Replace(file.Extension, "");
                    int flagIdx = newName.LastIndexOf('_');
                    newName = newName.Substring(flagIdx + 1);
                    newName += GameConfig.Path.AssetBundleSuffix;

                    newName = file.Name;
                    FileUtils.CopyFile(file.FullName, targetReleaseConfigPath + newName);
                }
            });
        }

#endregion

#region 资源Bundle

        //构建资源Bundle
        private void BuildAsset(int _curProgress, int _totalProgerss)
        {
            EditorUtility.DisplayProgressBar("打包", "打包资源中...", _curProgress / (float)_totalProgerss);

            if (!Directory.Exists(targetSourceResPath))
            {
                Directory.CreateDirectory(targetSourceResPath);
            }
            else
            {
                FileUtils.ClearDirectory(targetSourceResPath);
            }

            SetBundleNames();
            ClearFontFallbackData();

            try
            {
                BuildPipeline.BuildAssetBundles(targetSourceResPath, BuildAssetBundleOptions.None | BuildAssetBundleOptions.AppendHashToAssetBundleName | BuildAssetBundleOptions.ChunkBasedCompression, activeTarget);
            }
            catch(System.Exception e)
            {
                EditorUtility.DisplayDialog("错误", "打包资源失败，错误信息：" + e.Message, "确定");
            }
            
            CreateBundlesIndex();
            CreateReleaseBundles();
            Encrypt(targetReleaseResPath);

            EditorUtility.ClearProgressBar();
        }

        //清除字体的fallback
        private void ClearFontFallbackData()
        {
            if (!GameConfig.Font.ClearAllFallbackFont)
            {
                return;
            }

            string fontDirPath = GameConfig.Path.LocalResRoot + ResConfig.Font_Root;

            fontDirPath = FileUtils.AssetPathToFilePath(fontDirPath);
            List<string> fontFileNameList = FileUtils.FindFilesInDirectoryByFilter(fontDirPath, (filePath) =>
            {
                if (!filePath.Contains(".meta") && filePath.LastIndexOf(ResConfig.Asset_Suffix) >= 0)
                {
                    return true;
                }

                return false;
            });

            foreach (var fontFilePath in fontFileNameList)
            {
                string fontAssetPath = FileUtils.FilePathToAssetPath(fontFilePath);

                var asset = AssetDatabase.LoadAssetAtPath<ScriptableObject>(fontAssetPath);
                if (asset != null && asset is TMP_FontAsset)
                {
                    ((TMP_FontAsset)asset).fallbackFontAssetTable.Clear();

                    AssetDatabase.SaveAssetIfDirty(asset);
                }
            }
        }

        private ResIndexInfo CreateBundlesIndex()
        {
            ResIndexInfo resIndexInfo = new ResIndexInfo();

            //查找所有依赖
            var bundle = AssetBundle.LoadFromFile(targetSourceResPath + "bundle");
            var manifest = bundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
            string[] bundles = manifest.GetAllAssetBundles();
            Dictionary<string, List<string>> bundleDependDic = new Dictionary<string, List<string>>();

            foreach (var bundleName in bundles)
            {
                string[] deps = manifest.GetAllDependencies(bundleName);

                for (int i = 0; i < deps.Length; i++)
                {
                    string dep = deps[i];

                    int flagIdx = dep.LastIndexOf('_');
                    dep = dep.Substring(0, flagIdx);
                    dep += GameConfig.Path.AssetBundleSuffix;

                    deps[i] = dep;
                }

                bundleDependDic[bundleName] = new List<string>(deps);
            }
            bundle.Unload(false);

            resIndexInfo.resList = new List<ResUpdateInfo>();

            FileUtils.FindFilesInDirectoryByFilter(targetSourceResPath, (file) =>
            {
                if (file.Extension == GameConfig.Path.AssetBundleSuffix)//资源包
                {
                    ResUpdateInfo bundleUpdateInfo = new ResUpdateInfo();

                    string fileName = file.Name.Replace("\\", "/").Replace(file.Extension, "");

                    int flagIdx = fileName.LastIndexOf('_');
                    string newName = fileName.Substring(flagIdx + 1);

                    string bundleName = file.FullName.Replace("\\", "/").Replace(file.Extension, "").Replace(targetSourceResPath, "");
                    flagIdx = bundleName.LastIndexOf('_');
                    bundleName = bundleName.Substring(0, flagIdx);
                    bundleName += GameConfig.Path.AssetBundleSuffix;

                    bundleUpdateInfo.localPath = file.Name;
                    bundleUpdateInfo.hash = newName;
                    bundleUpdateInfo.name = bundleName;
                    bundleUpdateInfo.size = file.Length;

                    string oldBundleName = file.FullName.Replace("\\", "/").Replace(targetSourceResPath, "");
                    bundleDependDic.TryGetValue(oldBundleName, out bundleUpdateInfo.dependList);

                    resIndexInfo.resList.Add(bundleUpdateInfo);
                }
            });

            if (resIndexInfo.resList.Count > 0)
            {
                BuildPackageConfig buildPackageConfig = AssetDatabase.LoadAssetAtPath<BuildPackageConfig>("Assets/Editor/Framework/BuildPackageConfig.asset");
                if (buildPackageConfig != null)
                {
                    foreach (var bundleName in buildPackageConfig.PreloadBundleList)
                    {
                        var bundleInfo = resIndexInfo.resList.Find((b) =>
                        {
                            return (bundleName + GameConfig.Path.AssetBundleSuffix).ToLower() == b.name;
                        });

                        if (bundleInfo != null)
                        {
                            resIndexInfo.preloadHashList.Add(bundleInfo.hash);
                        }
                    }
                }

                CheckCircularDependency(resIndexInfo);//检测循环依赖
            }

            string jsonFilePath = targetSourceResPath.Replace(targetSourcePath, "");
            jsonFilePath = jsonFilePath.Substring(0, jsonFilePath.Length - 1) + ResConfig.Json_Suffix;
            jsonFilePath = targetSourcePath + jsonFilePath;

            string content = JsonUtility.ToJson(resIndexInfo, true);

            File.WriteAllText(jsonFilePath, content);

            Debug.Log("生成资源索引文件成功：" + jsonFilePath);

            return resIndexInfo;
        }

        //检查循环依赖
        private void CheckCircularDependency(ResIndexInfo abDependInfoList)
        {
            Dictionary<string, List<string>> nameDependcyDic = new Dictionary<string, List<string>>();

            foreach (var item in abDependInfoList.resList)
            {
                if (!nameDependcyDic.ContainsKey(item.name))
                {
                    nameDependcyDic.Add(item.name, item.dependList);
                }
            }

            foreach (var key in nameDependcyDic.Keys)
            {
                CheckOneCircularDependency(nameDependcyDic, key, key);
            }

            Debug.Log("循环依赖检测完毕.");
        }

        //检查循环依赖
        private void CheckOneCircularDependency(Dictionary<string, List<string>> nameDependcyDic, string resName, string key)
        {
            if (nameDependcyDic.TryGetValue(key, out List<string> depList))
            {
                if (depList != null && depList.Count > 0)
                {
                    foreach (var item in depList)
                    {
                        if (item == resName)
                        {
                            Debug.LogError("有循环依赖");
                            Debug.LogError("資源名是" + resName);
                            Debug.LogError("依賴包名是" + key);
                            Debug.Log("------------------------");
                            break;
                        }
                        CheckOneCircularDependency(nameDependcyDic, resName, item);
                    }
                }
            }
        }

        //设置名字
        private void SetBundleNames()
        {
            assetImporterList.Clear();
            bundleCountDic.Clear();

            foreach (var dic in ResConfig.ResInfoDic)
            {
                if (dic.Key != EResType.Config)
                {
                    SetBundleName(dic.Value, dic.Value.layer, ResPath + dic.Value.path);
                }
            }

            Debug.Log("资源构建完成，资源包数量：" + bundleCountDic.Count);
        }

        //根据配置设置名字
        private void SetBundleName(ResConfig.ResInfo _resInfo, int _layer, string _dirPath, string _abName = "")
        {
            if (!Directory.Exists(_dirPath))
            {
                return;
            }

            _dirPath = _dirPath.Replace("\\", "/");

            if (string.IsNullOrEmpty(_abName))
            {
                if (_resInfo.layer <= 0)//只有一个包
                {
                    string bundleName = GetBundleName(_resInfo.path);

                    string dirPath = FileUtils.AssetPathToFilePath(GameConfig.Path.LocalResRoot + _resInfo.path);

                    SetAssetBundleNameInDirectory(dirPath, bundleName);
                }
                else
                {
                    if (_layer == 0)
                    {
                        string subPath = _dirPath.Substring(_dirPath.IndexOf(_resInfo.path));

                        string bundleName = GetBundleName(subPath);

                        SetBundleName(_resInfo, 0, _dirPath, bundleName);
                    }
                    else
                    {
                        DirectoryInfo dInfo = new DirectoryInfo(_dirPath);

                        DirectoryInfo[] dInfos = dInfo.GetDirectories();
                        for (int i = 0; i < dInfos.Length; i++)
                        {
                            var dir = dInfos[i];

                            if (dir.GetDirectories().Length == 0)//没有子文件夹了
                            {
                                if (_layer > 1)
                                {
                                    FileInfo[] files = dir.GetFiles();
                                    foreach (var file in files)
                                    {
                                        string fileName = file.FullName.Replace(file.Extension, "").Replace("\\", "/");

                                        string subPath = fileName.Substring(fileName.IndexOf(_resInfo.path));

                                        string bundleName = GetBundleName(subPath);

                                        SetAssetBundleNameByFile(file.FullName, bundleName);
                                    }
                                }
                                else
                                {
                                    FileInfo[] files = dir.GetFiles();
                                    foreach (var file in files)
                                    {
                                        string fileName = dir.FullName.Replace("\\", "/");

                                        string subPath = fileName.Substring(fileName.IndexOf(_resInfo.path));

                                        string bundleName = GetBundleName(subPath);

                                        SetAssetBundleNameByFile(file.FullName, bundleName);
                                    }
                                }
                            }
                            else
                            {
                                SetBundleName(_resInfo, _layer - 1, dir.FullName);
                            }
                        }

                        FileInfo[] dFiles = dInfo.GetFiles();
                        for (int i = 0; i < dFiles.Length; i++)
                        {
                            var file = dFiles[i];

                            string fileName = file.FullName.Replace(file.Extension, "").Replace("\\", "/");

                            string subPath = fileName.Substring(fileName.IndexOf(_resInfo.path));

                            string bundleName = GetBundleName(subPath);

                            SetAssetBundleNameByFile(file.FullName, bundleName);
                        }
                    }
                }
            }
            else
            {
                SetAssetBundleNameInDirectory(_dirPath, _abName);
            }
        }

        private void SetAssetBundleNameByFile(string _filePath, string _bundleName)
        {
            if (_filePath.EndsWith(".meta") || _filePath.EndsWith(".DS_Store"))
            {
                return;
            }

            _filePath = _filePath.Replace("\\", "/");

            string assetPath = FileUtils.FilePathToAssetPath(_filePath);

            SetAssetImporter(assetPath, _bundleName);
        }

        private void SetAssetBundleNameInDirectory(string _dirPath, string _bundleName)
        {
            List<string> fileNameList = FileUtils.FindFilesInDirectoryByFilter(_dirPath, (fileName) =>
            {
                if (fileName.EndsWith(".meta") || fileName.EndsWith(".DS_Store"))
                {
                    return false;
                }

                return true;
            });

            if (fileNameList.Count == 0)
            {
                return;
            }

            //Debug.Log("_dirPath = " + _dirPath + ", _bundleName = " + _bundleName);

            foreach (var fileName in fileNameList)
            {
                string assetPath = FileUtils.FilePathToAssetPath(fileName);

                SetAssetImporter(assetPath, _bundleName);
            }
        }

        private void SetAssetImporter(string _assetPath, string _bundleName)
        {
            AssetImporter aImporter = AssetImporter.GetAtPath(_assetPath);

            if (aImporter == null)
            {
                Debug.LogError("导入错误, assetPath = " + _assetPath);
                return;
            }

            aImporter.assetBundleName = _bundleName;

            AddBundleCountDic(_bundleName);

            assetImporterList.Add(aImporter);
        }

        private void AddBundleCountDic(string _bundleName)
        {
            int assetCount;
            if (!bundleCountDic.TryGetValue(_bundleName, out assetCount))
            {
                assetCount = 0;

                bundleCountDic.Add(_bundleName, assetCount);
            }

            bundleCountDic[_bundleName] = ++assetCount;
        }

        private string GetBundleName(string _name)
        {
            string abName;
            if (_name.EndsWith("/"))
            {
                abName = _name.TrimEnd('/').ToLower() + GameConfig.Path.AssetBundleSuffix;
            }
            else
            {
                abName = _name.ToLower() + GameConfig.Path.AssetBundleSuffix;
            }

            return abName;
        }

        private void CreateReleaseBundles()
        {
            if (!Directory.Exists(targetReleaseResPath))
            {
                Directory.CreateDirectory(targetReleaseResPath);
            }
            else
            {
                FileUtils.ClearDirectory(targetReleaseResPath);
            }

            FileUtils.FindFilesInDirectoryByFilter(targetSourceResPath, (file) =>
            {
                if (file.Extension == GameConfig.Path.AssetBundleSuffix)//资源包，改名成hash
                {
                    string newName = file.Name.Replace(file.Extension, "");
                    int flagIdx = newName.LastIndexOf('_');
                    newName = newName.Substring(flagIdx + 1);
                    newName += GameConfig.Path.AssetBundleSuffix;

                    newName = file.Name;
                    FileUtils.CopyFile(file.FullName, targetReleaseResPath + newName);
                }
            });
        }
#endregion

#region 代码
#if ENABLE_HOTUPDATE
        //构建代码资源
        private void BuildHotUpdateDll(int _curProgress, int _totalProgerss, bool _genAll)
        {

            EditorUtility.DisplayProgressBar("打包", "打包代码中...", _curProgress / (float)_totalProgerss);

            if (!Directory.Exists(targetSourceDllPath))
            {
                Directory.CreateDirectory(targetSourceDllPath);
            }
            else
            {
                FileUtils.ClearDirectory(targetSourceDllPath);
            }

            Debug.Log("开始构建代码...");

            if (_genAll)
            {
                HybridCLR.Editor.Commands.PrebuildCommand.GenerateAll();
            }
            else
            {
                HybridCLR.Editor.Commands.CompileDllCommand.CompileDll(activeTarget);
            }

            string hotfixDllSrcDir = HybridCLR.Editor.SettingsUtil.GetHotUpdateDllsOutputDirByTarget(activeTarget);
            string aotDllSrcDir = HybridCLR.Editor.SettingsUtil.GetAssembliesPostIl2CppStripDir(activeTarget);
            List<string> hotUpdateDlls = HybridCLR.Editor.SettingsUtil.HotUpdateAssemblyNamesExcludePreserved;

            string hotUpdateOutPath = Path.Combine(Application.dataPath + "/..", hotfixDllSrcDir);

            foreach (var dll in hotUpdateDlls)
            {
                string dllPath = hotUpdateOutPath + "/" + dll + ".dll";
                string newPath = targetSourceDllPath + dll + ".dll";
                FileUtils.CopyFile(dllPath, newPath);

                Debug.Log("热更新dll: " + newPath);
            }

            string strippedDstDir = $"{HybridCLR.Editor.SettingsUtil.HybridCLRDataDir}/StrippedAOTAssembly2/{activeTarget}";
            string strippedAotOutPath = Path.Combine(Application.dataPath + "/..", strippedDstDir);

            foreach (var src in Directory.GetFiles(aotDllSrcDir, "*.dll"))
            {
                string dllName = Path.GetFileName(src);
                string dstFile = $"{strippedDstDir}/{dllName}";
                HybridCLR.Editor.AOT.AOTAssemblyMetadataStripper.Strip(src, dstFile);
            }

            foreach (var dll in AOTMetaAssemblyNames)
            {
                string dllPath = strippedAotOutPath + "/" + dll + ".dll";
                if (!File.Exists(dllPath))
                {
                    continue;
                }

                string newPath = targetSourceDllPath + dll + ".dll";

                FileUtils.CopyFile(dllPath, newPath);

                Debug.Log("AOT dll: " + newPath);
            }

            Debug.Log("构建代码完成。");

            CreateDllIndex();
            CreateReleaseDlls();

            EditorUtility.ClearProgressBar();
    }

    private ResIndexInfo CreateDllIndex()
        {
            ResIndexInfo resIndexInfo = new ResIndexInfo();
            resIndexInfo.hotUpdateDllList = new List<ResUpdateInfo>();

            string hotfixDllSrcDir = HybridCLR.Editor.SettingsUtil.GetHotUpdateDllsOutputDirByTarget(activeTarget);
            List<string> hotUpdateDlls = HybridCLR.Editor.SettingsUtil.HotUpdateAssemblyNamesExcludePreserved;

            FileUtils.FindFilesInDirectoryByFilter(targetSourceDllPath, (fileInfo) =>
            {
                ResUpdateInfo resUpdateInfo = new ResUpdateInfo();
                resUpdateInfo.hash = Utility.CreateMD5(fileInfo.FullName);
                resUpdateInfo.name = fileInfo.Name;
                resUpdateInfo.size = fileInfo.Length;
                resUpdateInfo.dependList = new List<string>();

                resIndexInfo.hotUpdateDllList.Add(resUpdateInfo);
            });

            string jsonFilePath = targetSourceDllPath.Replace(targetSourcePath, "");
            jsonFilePath = jsonFilePath.Substring(0, jsonFilePath.Length - 1) + ResConfig.Json_Suffix;
            jsonFilePath = targetSourcePath + jsonFilePath;

            string content = JsonUtility.ToJson(resIndexInfo, true);

            File.WriteAllText(jsonFilePath, content);

            Debug.Log("生成代码库索引文件成功：" + jsonFilePath);

            return resIndexInfo;
        }

        private void CreateReleaseDlls()
        {
            if (!Directory.Exists(targetReleaseDllPath))
            {
                Directory.CreateDirectory(targetReleaseDllPath);
            }
            else
            {
                FileUtils.ClearDirectory(targetReleaseDllPath);
            }

            ResIndexInfo resIndexInfo = JsonUtility.FromJson<ResIndexInfo>(File.ReadAllText(targetSourcePath + "dll.json")); ;

            foreach (var updateInfo in resIndexInfo.hotUpdateDllList)
            {
                string dllFilePath = targetSourceDllPath + updateInfo.name;
                string newDllFilePath = targetReleaseDllPath + updateInfo.hash;

                FileUtils.CopyFile(dllFilePath, newDllFilePath);
            }

            string jsonFilePath = assetBundlePath + GameConfig.Path.HeadFileName.Replace(GameConfig.Path.HeadFileSuffix, ResConfig.Json_Suffix);

            string filePath = targetReleasePath + GameConfig.Path.HeadFileName;

            FileUtils.WriteBytes(filePath, (writer) =>
            {
                resIndexInfo.Write(writer);
            });
        }
#endif

#endregion

        //简单加密
        private static void Encrypt(string _path)
        {
            if (GameConfig.Package.AssetBundleOffset > 0)
            {
                System.Action<FileInfo> action = (file) =>
                {
                    byte[] bytes = File.ReadAllBytes(file.FullName);

                    byte[] result = new byte[GameConfig.Package.AssetBundleOffset + bytes.Length];

                    for (int i = 0; i < result.Length; i++)
                    {
                        if (i < GameConfig.Package.AssetBundleOffset)
                        {
                            result[i] = (byte)(2 ^ GameConfig.Package.AssetBundleOffset);
                        }
                        else
                        {
                            result[i] = bytes[i - GameConfig.Package.AssetBundleOffset];
                        }
                    }

                    File.WriteAllBytes(file.FullName, result);
                };

                FileUtils.FindFilesInDirectoryByFilter(_path, action);

                Debug.Log("资源加密完成!");
            }
        }

        //合并索引文件
        private void MergeTotalIndexFile()
        {
            ResIndexInfo resIndexInfo = new ResIndexInfo();

            string configIndexFilePath = targetSourcePath + "config.json";
            string bundleIndexFilePath = targetSourcePath + "bundle.json";
            string dllIndexFilePath = targetSourcePath + "dll.json";

            if (File.Exists(configIndexFilePath))
            {
                ResIndexInfo configIndexInfo = JsonUtility.FromJson<ResIndexInfo>(File.ReadAllText(configIndexFilePath));

                resIndexInfo.Merge(configIndexInfo);
            }
            if (File.Exists(bundleIndexFilePath))
            {
                ResIndexInfo bundleIndexInfo = JsonUtility.FromJson<ResIndexInfo>(File.ReadAllText(bundleIndexFilePath));

                resIndexInfo.Merge(bundleIndexInfo);
            }
            if (File.Exists(dllIndexFilePath))
            {
                ResIndexInfo dllIndexInfo = JsonUtility.FromJson<ResIndexInfo>(File.ReadAllText(dllIndexFilePath));

                resIndexInfo.Merge(dllIndexInfo);
            }

            string sourceJsonPath = Path.Combine(targetSourcePath, "../resIndex.json");
            Debug.Log(sourceJsonPath);
            File.WriteAllText(sourceJsonPath, JsonUtility.ToJson(resIndexInfo));

            FileUtils.WriteBytes(targetReleasePath + GameConfig.Path.HeadFileName, (writer) =>
            {
                resIndexInfo.Write(writer);
            });

            Debug.Log("生成目录文件：" + sourceJsonPath);
        }

        //拷贝到StreamingAssets下
        private static void CopyToStreaming()
        {
            Debug.Log("拷贝到StreamingAssets。");
            FileUtils.ClearDirectory(Application.streamingAssetsPath);

            FileUtils.CopyDirectory(targetReleasePath, Application.streamingAssetsPath, (fileInfo) =>
            {
                return true;
            });

            AssetDatabase.Refresh();
        }

        private static void CopySdkFiles()
        {
            //拷贝配置文件到StreamingAssets文件夹下
            string dirPath = Application.dataPath + "/配置文件/";
            if (Directory.Exists(dirPath))
            {
                FileUtils.CopyDirectory(dirPath, Application.streamingAssetsPath);
                AssetDatabase.Refresh();
            }
        }
    }
}