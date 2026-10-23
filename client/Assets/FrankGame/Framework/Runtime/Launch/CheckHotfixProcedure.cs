using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace FrankGame
{
    public enum EUpdateAssetState
    {
        Update,
        Delete,
        Add,
    }

    public enum EUpdateAssetType
    {
        Config,
        Bundle,
        Dll,
    }

    public class UpdateAssetInfo
    {
        public EUpdateAssetState state;
        public EUpdateAssetType type;
        public ResUpdateInfo resUpdateInfo;

        public string GetSubPath()
        {
            return type.ToString().ToLower() + "/" + resUpdateInfo.localPath;
        }
    }

    public class CheckHotfixProcedure : GameLaunchProcedure
    {
        private string resourceUrl;
        private string filesUrl;
        private string filesLocalPath;
        private string filesTempPath;
        private Queue<UpdateAssetInfo> updateAssetQueue = new Queue<UpdateAssetInfo>();
        private ResIndexInfo localResIndexInfo;//本地资源信息
        private long totalSize;
        private long downloadSize;

        protected override void OnInit()
        {
            base.OnInit();

            filesLocalPath = PathUtil.PersistentData_Path + GameConfig.Path.HeadFileName;
            filesTempPath = PathUtil.PersistentData_Path + GameConfig.Path.HeadFileName.Replace(GameConfig.Path.HeadFileSuffix, ".tmp");
        }

        protected override void OnEnter(params object[] _params)
        {
            base.OnEnter(_params);

            resourceUrl = GameConfig.ResourceUrl;
            filesUrl = GameConfig.FilesUrl;

            SetProgress(0, LocalizationManager.Instance.GetPackageLanguageText("hotupdate_check"));

            Log.Debug(filesUrl);

            StartCoroutine(DownloadUtil.DownloadFile(filesUrl, OnDownloadFilesSuccess, OnDownloadFilesFail));
        }

        private void OnDownloadFilesSuccess(byte[] _bytes, object _param)
        {
            ResIndexInfo onlineResIndexInfo = new ResIndexInfo();//线上资源信息

            FileUtils.ReadBytes(_bytes, (br) =>
            {
                onlineResIndexInfo.Read(br);
            });

            localResIndexInfo = new ResIndexInfo();

            if (File.Exists(filesLocalPath))
            {
                FileUtils.ReadBytes(filesLocalPath, localResIndexInfo.Read);

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

                //对比线上资源
                foreach (var info in onlineResIndexInfo.configList)
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
                        totalSize += info.size;

                        updateAssetQueue.Enqueue(assetInfo);
                    }
                }

                foreach (var info in onlineResIndexInfo.resList)
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
                        totalSize += info.size;

                        updateAssetQueue.Enqueue(assetInfo);
                    }
                }

                foreach (var info in onlineResIndexInfo.hotUpdateDllList)
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
                        totalSize += info.size;

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
                //下载线上资源
                foreach (var info in onlineResIndexInfo.configList)
                {
                    UpdateAssetInfo assetInfo = new UpdateAssetInfo();
                    assetInfo.resUpdateInfo = info;
                    assetInfo.type = EUpdateAssetType.Config;
                    assetInfo.state = EUpdateAssetState.Add;
                    totalSize += info.size;

                    updateAssetQueue.Enqueue(assetInfo);
                }

                foreach (var info in onlineResIndexInfo.resList)
                {
                    UpdateAssetInfo assetInfo = new UpdateAssetInfo();
                    assetInfo.resUpdateInfo = info;
                    assetInfo.type = EUpdateAssetType.Bundle;
                    assetInfo.state = EUpdateAssetState.Add;
                    totalSize += info.size;

                    updateAssetQueue.Enqueue(assetInfo);
                }

                foreach (var info in onlineResIndexInfo.hotUpdateDllList)
                {
                    UpdateAssetInfo assetInfo = new UpdateAssetInfo();
                    assetInfo.resUpdateInfo = info;
                    assetInfo.type = EUpdateAssetType.Dll;
                    assetInfo.state = EUpdateAssetState.Add;
                    totalSize += info.size;

                    updateAssetQueue.Enqueue(assetInfo);
                }
            }

            Log.Debug("检测更新： " + updateAssetQueue.Count);

            if (updateAssetQueue.Count > 0)
            {
                StartCoroutine(StartDownload());
            }
            else
            {
                ChangeProcedure(typeof(ResolveAssetsProcedure));
            }
        }

        private void OnDownloadFilesFail(string _error, object _param)
        {
            Log.Error(ELogType.Net, filesUrl + " error: " + _error);

            SetProgress(1, LocalizationManager.Instance.GetPackageLanguageText("hotupdate_error"));
        }

        private IEnumerator StartDownload()
        {
            while(updateAssetQueue.Count > 0)
            {
                yield return WriteFile(updateAssetQueue.Dequeue());
            }

            ChangeProcedure(typeof(ResolveAssetsProcedure));
        }

        private IEnumerator WriteFile(UpdateAssetInfo assetInfo)
        {
            string localPath = PathUtil.PersistentData_Path + assetInfo.GetSubPath();

            if (assetInfo.state == EUpdateAssetState.Delete)
            {
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

                yield return null;
            }
            else
            {
                yield return DownloadUtil.DownloadFile(resourceUrl + assetInfo.GetSubPath(), OnDownloadUpdate, WriteBytes, WriteFail, assetInfo);
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

            FileUtils.WriteBytes(filesTempPath, localResIndexInfo.Write);

            FileUtils.MoveFile(filesTempPath, filesLocalPath, true);

            downloadSize += assetInfo.resUpdateInfo.size;

            Log.Debug(ELogType.Net, "下载文件======>" + assetInfo.resUpdateInfo.hash + " " + assetInfo.resUpdateInfo.name + " , size = " + assetInfo.resUpdateInfo.size);
        }

        private void WriteFail(string _error, object _param)
        {
            UpdateAssetInfo assetInfo = _param as UpdateAssetInfo;

            Log.Error(ELogType.Net, assetInfo.resUpdateInfo.name + " error: " + _error);
        }

        private void OnDownloadUpdate(float _progress, ulong _size, ulong _total)
        {
            long curSize = downloadSize + (long)_size;

            SetProgress((float)downloadSize / totalSize, LocalizationManager.Instance.GetPackageLanguageText("hotupdate_downloading", ByteConversion(curSize), ByteConversion(totalSize)));
        }

        private string ByteConversion(double length)
        {
            string temp;

            if (length < 1024)
            {
                temp = System.Math.Round(length, 0) + "B";
            }

            else if (length < 1024 * 1024)
            {
                temp = System.Math.Round(length / (1024), 0) + "KB";
            }

            else if (length < 1024 * 1024 * 1024)
            {
                temp = System.Math.Round(length / (1024 * 1024), 2) + "MB";
            }

            else
            {
                temp = System.Math.Round(length / (1024 * 1024 * 1024), 2) + "GB";
            }

            return temp;
        }

        protected override void OnExit()
        {
            base.OnExit();
        }
    }

}