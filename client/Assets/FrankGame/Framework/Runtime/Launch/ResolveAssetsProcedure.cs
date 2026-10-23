using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace FrankGame
{
    public class ResolveAssetsProcedure : GameLaunchProcedure
    {
        private Queue<UpdateAssetInfo> updateAssetQueue = new Queue<UpdateAssetInfo>();
        private ResIndexInfo localResIndexInfo;
        private string filesLocalPath;
        private string filesUrl;
        private int curCount;
        private int totalCount;

        protected override void OnInit()
        {
            base.OnInit();

            filesLocalPath = PathUtil.PersistentData_Path + GameConfig.Path.HeadFileName;
        }

        protected override void OnEnter(params object[] _params)
        {
            base.OnEnter(_params);

            filesUrl = GameConfig.FilesUrl;

            localResIndexInfo = new ResIndexInfo();

            SetProgress(0, LocalizationManager.Instance.GetPackageLanguageText("asset_loading"));

            if (GameConfig.Package.ResLoadMode == ELoadMode.Local)
            {
                CompleteAllStaticProcedure();
            }
            else if (GameConfig.Package.ResLoadMode == ELoadMode.Net || GameConfig.Package.ResLoadMode == ELoadMode.MiniGame)
            {
                StartCoroutine(DownloadUtil.DownloadFile(filesUrl, OnDownloadFilesSuccess, OnDownloadFilesFail));
            }
            else
            {
                if (File.Exists(filesLocalPath))
                {
                    FileUtils.ReadBytes(filesLocalPath, localResIndexInfo.Read);

                    BundleManager.Instance.ResolveDependInfoList(localResIndexInfo);

                    BundleManager.Instance.LoadTotalConfig((configDic) =>
                    {
                        CompleteAllStaticProcedure();
                    });
                }
                else
                {
                    Log.Error("缺少本地资源.");
                }
            }
        }

        protected override void OnExit()
        {
            base.OnExit();

            SetProgress(0, LocalizationManager.Instance.GetPackageLanguageText("asset_loading"));
        }

        private void OnDownloadFilesSuccess(byte[] _bytes, object _param)
        {
            ResIndexInfo resIndexInfo = new ResIndexInfo();

            FileUtils.ReadBytes(_bytes, (br) =>
            {
                resIndexInfo.Read(br);
            });

            BundleManager.Instance.ResolveDependInfoList(resIndexInfo);

            if (resIndexInfo.preloadHashList.Count > 0)
            {
                foreach (var hash in resIndexInfo.preloadHashList)
                {
                    ResUpdateInfo resUpdateInfo = resIndexInfo.resList.Find((bd) =>
                    {
                        return bd.hash == hash;
                    });

                    if (resUpdateInfo != null)
                    {
                        UpdateAssetInfo assetInfo = new UpdateAssetInfo();
                        assetInfo.resUpdateInfo = resUpdateInfo;
                        assetInfo.state = EUpdateAssetState.Add;
                        assetInfo.type = EUpdateAssetType.Bundle;

                        updateAssetQueue.Enqueue(assetInfo);
                    }
                }
            }

            if (updateAssetQueue.Count > 0)
            {
                totalCount = updateAssetQueue.Count;
                curCount = 0;

                while(updateAssetQueue.Count > 0)
                {
                    UpdateAssetInfo assetInfo = updateAssetQueue.Dequeue();

                    BundleManager.Instance.LoadBundleByHash(assetInfo.resUpdateInfo.hash, (bundleInfo) =>
                    {
                        curCount++;
                        if (curCount == totalCount)
                        {
                            BundleManager.Instance.LoadTotalConfig((configDic) =>
                            {
                                CompleteAllStaticProcedure();
                            });
                        }
                    });
                }
            }
            else
            {
                BundleManager.Instance.LoadTotalConfig((configDic) =>
                {
                    CompleteAllStaticProcedure();
                });
            }
        }

        private void OnDownloadFilesFail(string _error, object _param)
        {
            Log.Error(ELogType.Net, filesUrl + " error: " + _error);
        }
    }
}