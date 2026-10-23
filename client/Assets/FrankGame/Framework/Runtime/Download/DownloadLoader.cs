using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace FrankGame
{
    public class DownloadLoader : SingleMonoBehaviour<DownloadLoader>
    {
        private enum ETaskType
        {
            File,
            AssetBundle,
        }

        private struct DownloadTask
        {
            public string url;
            public string name;
            public ETaskType taskType;
            public Action<byte[], object> onFileDone;
            public Action<AssetBundle, object> onAssetBundleDone;
            public float startDownloadTime;
            public int downloadCount;//下载尝试的次数
            public object param;

            public string GetUrl()
            {
                return url + name;
            }
        }

        private Queue<DownloadTask> taskQueue = new Queue<DownloadTask>();
        private bool isDownloading = false;

        public void DownloadFile(string url, string name, Action<byte[], object> onResponse, object param = null)
        {
            DownloadTask task = new DownloadTask();
            task.url = url;
            task.name = name;
            task.taskType = ETaskType.File;
            task.onFileDone = onResponse;
            task.param = param;

            taskQueue.Enqueue(task);

            StartDownload();
        }

        public void DownloadAssetBundle(string url, string name, Action<AssetBundle, object> onResponse, object param = null)
        {
            DownloadTask task = new DownloadTask();
            task.url = url;
            task.name = name;
            task.taskType = ETaskType.AssetBundle;
            task.onAssetBundleDone = onResponse;
            task.param = param;

            taskQueue.Enqueue(task);

            StartDownload();
        }

        private void StartDownload()
        {
            if (isDownloading)
            {
                return;
            }

            isDownloading = true;

            StartCoroutine(IEDownloadTask());
        }

        private IEnumerator IEDownloadTask()
        {
            while (true)
            {
                while (taskQueue.Count > 0)
                {
                    DownloadTask task = taskQueue.Dequeue();
                    if (task.downloadCount > 1)//超过失败次数大于1，隔帧执行一次
                    {
                        yield return new WaitForEndOfFrame();
                    }

                    yield return ExecuteTask(task);
                }

                yield return 0;
            }
        }

        private IEnumerator ExecuteTask(DownloadTask task)
        {
            task.startDownloadTime = Time.time;

            Log.Debug(ELogType.Net, "Download Start: " + task.GetUrl());

            if (task.taskType == ETaskType.AssetBundle)
            {
                yield return DownloadUtil.DownloadAssetBundle(task.GetUrl(), OnDownloadDone, OnDownloadFail, task);
            }
            else
            {
                yield return DownloadUtil.DownloadFile(task.GetUrl(), OnDownloadDone, OnDownloadFail, task);
            }
        }

        private void OnDownloadDone(byte[] _bytes, object _param)
        {
            DownloadTask task = (DownloadTask)_param;
            float duration = Time.time - task.startDownloadTime;
            Log.Debug(ELogType.Net, "Download duration: " + duration + "s" + ", url:" + task.GetUrl());

            task.onFileDone?.Invoke(_bytes, task.param);
        }

        private void OnDownloadDone(AssetBundle _bundle, object _param)
        {
            DownloadTask task = (DownloadTask)_param;
            float duration = Time.time - task.startDownloadTime;
            Log.Debug(ELogType.Net, "Download duration: " + duration + "s" + ", url:" + task.GetUrl() + ", bundle = " + _bundle);

            task.onAssetBundleDone?.Invoke(_bundle, task.param);
        }

        private void OnDownloadFail(string _error, object _param)
        {
            DownloadTask task = (DownloadTask)_param;
            Log.Error(ELogType.Net, task.GetUrl() + "  ,error:" + _error);

            task.downloadCount++;

            taskQueue.Enqueue(task);
        }
    }
}