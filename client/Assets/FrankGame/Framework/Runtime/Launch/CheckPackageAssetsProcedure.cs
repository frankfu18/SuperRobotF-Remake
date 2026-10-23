using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace FrankGame
{
    public class CheckPackageAssetsProcedure : GameLaunchProcedure
    {
        private string filesPackagePath;
        private string filesLocalPath;
        private string filesTempPath;
        private ResIndexInfo localResIndexInfo;
        private int curCount;
        private int totalCount;
        private Queue<UpdateAssetInfo> updateAssetQueue = new Queue<UpdateAssetInfo>();

        protected override void OnInit()
        {
            base.OnInit();

            filesPackagePath = PathUtil.StreamingAsset_Path + GameConfig.Path.HeadFileName;
            filesLocalPath = PathUtil.PersistentData_Path + GameConfig.Path.HeadFileName;
            filesTempPath = PathUtil.PersistentData_Path + GameConfig.Path.HeadFileName.Replace(GameConfig.Path.HeadFileSuffix, ".tmp");

            Log.Debug("filesPackagePath:" + filesPackagePath);
            Log.Debug("filesLocalPath:" + filesLocalPath);
        }

        protected override void OnEnter(params object[] _params)
        {
            base.OnEnter(_params);

            SetProgress(0, LocalizationManager.Instance.GetPackageLanguageText("package_ckeckAsset"));

            if (File.Exists(filesLocalPath))//包含，直接走热更检测
            {
                localResIndexInfo = new ResIndexInfo();
                FileUtils.ReadBytes(filesLocalPath, localResIndexInfo.Read);

                if (GameConfig.HotUpdate.Enable)
                {
                    ChangeProcedure(typeof(CheckHotfixProcedure));
                }
                else
                {
#if UNITY_EDITOR_OSX || UNITY_IOS
                    if (File.Exists(filesPackagePath))
                    {
                        byte[] content = File.ReadAllBytes(filesPackagePath);
                        if (content == null)
                        {
                            OnReadFail(null, null);
                        }
                        else
                        {
                            OnReadSuccess(content);
                        }
                    }
                    else
                    {
                        OnReadFail(null, null);
                    }
#else
                    StartCoroutine(DownloadUtil.ReadStreamingAssetsFile(filesPackagePath, OnReadSuccess, OnReadFail));
#endif
                }
            }
            else//不包含，说明第一次安装，解压包内资源到本地
            {
#if UNITY_EDITOR_OSX || UNITY_IOS
                if (File.Exists(filesPackagePath))
                {
                    byte[] content = File.ReadAllBytes(filesPackagePath);
                    if (content == null)
                    {
                        OnReadFail(null, null);
                    }
                    else
                    {
                        OnReadSuccess(content);
                    }
                }
                else
                {
                    OnReadFail(null, null);
                }
#else
                StartCoroutine(DownloadUtil.ReadStreamingAssetsFile(filesPackagePath, OnReadSuccess, OnReadFail));
#endif
            }
        }

        private void OnReadSuccess(byte[] _bytes, object _param = null)
        {
            ResIndexInfo packageResIndexInfo = new ResIndexInfo();

            FileUtils.ReadBytes(_bytes, (br) => 
            {
                packageResIndexInfo.Read(br);
            });

            if (localResIndexInfo != null)
            {
                Dictionary<string, ResUpdateInfo> tempConfigDic = new Dictionary<string, ResUpdateInfo>();
                Dictionary<string, ResUpdateInfo> tempResDic = new Dictionary<string, ResUpdateInfo>();
                Dictionary<string, ResUpdateInfo> tempDllDic = new Dictionary<string, ResUpdateInfo>();

                //检查本地资源
                foreach (var info in localResIndexInfo.configList)
                {
                    tempConfigDic[info.hash] = info;
                }

                foreach (var info in localResIndexInfo.resList)
                {
                    tempResDic[info.hash] = info;
                }

                foreach (var info in localResIndexInfo.hotUpdateDllList)
                {
                    tempDllDic[info.hash] = info;
                }

                //对比包内资源
                foreach (var info in packageResIndexInfo.configList)
                {
                    if (tempConfigDic.ContainsKey(info.hash))
                    {
                        tempConfigDic.Remove(info.hash);
                    }
                    else
                    {
                        UpdateAssetInfo assetInfo = new UpdateAssetInfo();
                        assetInfo.resUpdateInfo = info;
                        assetInfo.type = EUpdateAssetType.Config;
                        assetInfo.state = EUpdateAssetState.Add;

                        updateAssetQueue.Enqueue(assetInfo);
                    }
                }

                foreach (var info in packageResIndexInfo.resList)
                {
                    if (tempResDic.ContainsKey(info.hash))
                    {
                        tempResDic.Remove(info.hash);
                    }
                    else
                    {
                        UpdateAssetInfo assetInfo = new UpdateAssetInfo();
                        assetInfo.resUpdateInfo = info;
                        assetInfo.type = EUpdateAssetType.Bundle;
                        assetInfo.state = EUpdateAssetState.Add;

                        updateAssetQueue.Enqueue(assetInfo);
                    }
                }

                foreach (var info in packageResIndexInfo.hotUpdateDllList)
                {
                    if (tempDllDic.ContainsKey(info.hash))
                    {
                        tempDllDic.Remove(info.hash);
                    }
                    else
                    {
                        UpdateAssetInfo assetInfo = new UpdateAssetInfo();
                        assetInfo.resUpdateInfo = info;
                        assetInfo.type = EUpdateAssetType.Dll;
                        assetInfo.state = EUpdateAssetState.Add;

                        updateAssetQueue.Enqueue(assetInfo);
                    }
                }

                //删除本地多余资源
                foreach (var info in tempConfigDic.Values)
                {
                    UpdateAssetInfo assetInfo = new UpdateAssetInfo();
                    assetInfo.resUpdateInfo = info;
                    assetInfo.type = EUpdateAssetType.Config;
                    assetInfo.state = EUpdateAssetState.Delete;

                    updateAssetQueue.Enqueue(assetInfo);
                }

                foreach (var info in tempResDic.Values)
                {
                    UpdateAssetInfo assetInfo = new UpdateAssetInfo();
                    assetInfo.resUpdateInfo = info;
                    assetInfo.type = EUpdateAssetType.Bundle;
                    assetInfo.state = EUpdateAssetState.Delete;

                    updateAssetQueue.Enqueue(assetInfo);
                }

                foreach (var info in tempDllDic.Values)
                {
                    UpdateAssetInfo assetInfo = new UpdateAssetInfo();
                    assetInfo.resUpdateInfo = info;
                    assetInfo.type = EUpdateAssetType.Dll;
                    assetInfo.state = EUpdateAssetState.Delete;

                    updateAssetQueue.Enqueue(assetInfo);
                }
            }
            else
            {
                localResIndexInfo = new ResIndexInfo();

                foreach (var info in packageResIndexInfo.configList)
                {
                    UpdateAssetInfo assetInfo = new UpdateAssetInfo();
                    assetInfo.resUpdateInfo = info;
                    assetInfo.type = EUpdateAssetType.Config;
                    assetInfo.state = EUpdateAssetState.Add;

                    updateAssetQueue.Enqueue(assetInfo);
                }

                foreach (var info in packageResIndexInfo.resList)
                {
                    UpdateAssetInfo assetInfo = new UpdateAssetInfo();
                    assetInfo.resUpdateInfo = info;
                    assetInfo.type = EUpdateAssetType.Bundle;
                    assetInfo.state = EUpdateAssetState.Add;

                    updateAssetQueue.Enqueue(assetInfo);
                }

                foreach (var info in packageResIndexInfo.hotUpdateDllList)
                {
                    UpdateAssetInfo assetInfo = new UpdateAssetInfo();
                    assetInfo.resUpdateInfo = info;
                    assetInfo.type = EUpdateAssetType.Dll;
                    assetInfo.state = EUpdateAssetState.Add;

                    updateAssetQueue.Enqueue(assetInfo);
                }
            }

            if (updateAssetQueue.Count > 0)//开始解压资源
            {
                curCount = 0;
                totalCount = updateAssetQueue.Count;

                StartCoroutine(IEWriteFiles());
            }
            else
            {
                if (GameConfig.HotUpdate.Enable)
                {
                    ChangeProcedure(typeof(CheckHotfixProcedure));
                }
                else
                {
                    ChangeProcedure(typeof(ResolveAssetsProcedure));
                }
            }
        }

        private void OnReadFail(string _error, object _param)
        {
            if (GameConfig.HotUpdate.Enable)
            {
                ChangeProcedure(typeof(CheckHotfixProcedure));
            }
            else
            {
                ChangeProcedure(typeof(ResolveAssetsProcedure));
            }
        }

        private IEnumerator IEWriteFiles()
        {
            while(updateAssetQueue.Count > 0)
            {
                var assetInfo = updateAssetQueue.Dequeue();
                if (assetInfo.state == EUpdateAssetState.Add)
                {
                    string path = PathUtil.StreamingAsset_Path + assetInfo.GetSubPath();
#if UNITY_EDITOR_OSX || UNITY_IOS
                    if (File.Exists(path))
                    {
                        byte[] content = File.ReadAllBytes(path);
                        if (content == null)
                        {
                            WriteFail("文件数据为空：" + path, assetInfo);
                        }
                        else
                        {
                            WriteBytes(content, assetInfo);
                        }
                    }
                    else
                    {
                        WriteFail("文件未找到：" + path, assetInfo);
                    }

                    yield return 0;
#else
                    yield return DownloadUtil.ReadStreamingAssetsFile(path, WriteBytes, WriteFail, assetInfo);
#endif
                }
                else if (assetInfo.state == EUpdateAssetState.Delete)
                {
                    string localPath = PathUtil.PersistentData_Path + assetInfo.GetSubPath();

                    if (File.Exists(localPath))
                    {
                        File.Delete(localPath);
                    }

                    if (assetInfo.type == EUpdateAssetType.Config)
                    {
                        localResIndexInfo.RemoveConfig(assetInfo.resUpdateInfo);
                    }
                    else if (assetInfo.type == EUpdateAssetType.Bundle)
                    {
                        localResIndexInfo.RemoveRes(assetInfo.resUpdateInfo);
                    }
                    else if (assetInfo.type == EUpdateAssetType.Dll)
                    {
                        localResIndexInfo.RemoveDll(assetInfo.resUpdateInfo);
                    }

                    Log.Debug(ELogType.Net, "删除文件======> " + assetInfo.resUpdateInfo.hash + " " + assetInfo.resUpdateInfo.name);

                    FileUtils.WriteBytes(filesTempPath, localResIndexInfo.Write);

                    FileUtils.MoveFile(filesTempPath, filesLocalPath, true);

                    curCount++;

                    SetProgress((float)curCount / totalCount, string.Format("正在解压包内资源({0}/{1})...", curCount, totalCount));

                    yield return 0;
                }
            }

            if (GameConfig.HotUpdate.Enable)
            {
                ChangeProcedure(typeof(CheckHotfixProcedure));
            }
            else
            {
                ChangeProcedure(typeof(ResolveAssetsProcedure));
            }
        }

        private void WriteBytes(byte[] _bytes, object _param)
        {
            UpdateAssetInfo assetInfo = _param as UpdateAssetInfo;
            string localPath = PathUtil.PersistentData_Path + assetInfo.GetSubPath();

            if (!File.Exists(localPath))
            {
                var dir = Path.GetDirectoryName(localPath);
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
            }

            File.WriteAllBytes(localPath, _bytes);

            if (assetInfo.type == EUpdateAssetType.Config)
            {
                localResIndexInfo.AddConfig(assetInfo.resUpdateInfo);
            }
            else if (assetInfo.type == EUpdateAssetType.Bundle)
            {
                localResIndexInfo.AddRes(assetInfo.resUpdateInfo);
            }
            else if (assetInfo.type == EUpdateAssetType.Dll)
            {
                localResIndexInfo.AddDll(assetInfo.resUpdateInfo);
            }

            FileUtils.WriteBytes(filesTempPath, (writer) =>
            {
                localResIndexInfo.Write(writer);
            });

            FileUtils.MoveFile(filesTempPath, filesLocalPath, true);

            Log.Debug(ELogType.Net, "解压文件======> " + assetInfo.resUpdateInfo.hash + " " + assetInfo.resUpdateInfo.name);

            curCount++;

            SetProgress((float)curCount / totalCount, LocalizationManager.Instance.GetPackageLanguageText("package_decompressAsset", curCount, totalCount));
        }

        private void WriteFail(string _error, object _param)
        {
            UpdateAssetInfo assetInfo = _param as UpdateAssetInfo;

            Log.Error(ELogType.Net, "write error: " + _error + ", name: " + assetInfo.resUpdateInfo.name);

            curCount++;

            SetProgress((float)curCount / totalCount, LocalizationManager.Instance.GetPackageLanguageText("package_decompressAsset", curCount, totalCount));
        }
    }
}