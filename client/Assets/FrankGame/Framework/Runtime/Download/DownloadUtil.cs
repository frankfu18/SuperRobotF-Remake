using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Text;
#if WX_GAME
using WeChatWASM;
#endif

namespace FrankGame
{
    public static class DownloadUtil
    {
        private const int TimeOut_Duration = 5;//超时时间

        public static IEnumerator PostRequest(string url, Dictionary<string, string> paramsDic, Action<string> onResponse = null, Action<string> onError = null)
        {
            WWWForm from = new WWWForm();
            foreach (var dic in paramsDic)
            {
                from.AddField(dic.Key, dic.Value);
            }

            UnityWebRequest www = UnityWebRequest.Post(url, from);
            www.timeout = TimeOut_Duration;
            www.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");
            www.certificateHandler = new BypassCertificate();
            www.disposeCertificateHandlerOnDispose = true;

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                if (onError != null)
                {
                    onError(www.error);
                }
            }
            else
            {
                if (onResponse != null)
                {
                    onResponse(www.downloadHandler.text);
                }
            }

            www.Dispose();
        }

        public static IEnumerator GetRequest(string url, Dictionary<string, string> paramsDic, Action<string> onResponse = null, Action<string> onError = null)
        {
            string targetUrl = url;
            StringBuilder sb = new StringBuilder();

            if (paramsDic != null)
            {
                foreach (var Key in paramsDic.Keys)
                {
                    sb.Append(Key + "=" + paramsDic[Key] + "&");
                }
                if (url.Contains("?"))
                {
                    targetUrl = url + "&" + sb.ToString();
                }
                else
                {
                    targetUrl = url + "/" + sb.ToString();
                }

                targetUrl = targetUrl[targetUrl.Length - 1] == '&' ? targetUrl.Substring(0, targetUrl.Length - 1) : targetUrl;
            }

            UnityWebRequest www = UnityWebRequest.Get(targetUrl);
            www.timeout = TimeOut_Duration;
            www.certificateHandler = new BypassCertificate();

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                if (onError != null)
                {
                    onError(www.error);
                }
            }
            else
            {
                if (onResponse != null)
                {
                    onResponse(www.downloadHandler.text);
                }
            }

            www.Dispose();
        }

        public static IEnumerator DownloadFile(string url, Action<byte[], object> onResponse = null, Action<string, object> onError = null, object param = null)
        {
            UnityWebRequest www = UnityWebRequest.Get(url);
            www.timeout = TimeOut_Duration;
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                if (onError != null)
                {
                    onError(www.error, param);
                }
            }
            else
            {
                if (onResponse != null)
                {
                    onResponse(www.downloadHandler.data, param);
                }
            }

            www.Dispose();
        }

        /// <summary>
        /// 获取服务器时间
        /// </summary>
        /// <param name="url"></param>
        /// <param name="onGetServerTime">返回时间戳，时间差，发送时的本地时间，参数</param>
        /// <param name="onError"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static IEnumerator RequestServerTime(string url, Action<long, long, long, object> onGetServerTime = null, Action<string, object> onError = null, object param = null)
        {
            UnityWebRequest www = UnityWebRequest.Head(url);
            www.timeout = TimeOut_Duration;
            long sendTime = TimeUtility.NowMillisecond();

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                if (onError != null)
                {
                    onError(www.error, param);
                }
            }
            else
            {
                if (onGetServerTime != null)
                {
                    string dateHeader = www.GetResponseHeader("Date");
                    if (!string.IsNullOrEmpty(dateHeader))
                    {
                        long receiveTime = TimeUtility.NowMillisecond();
                        long deltaTime = receiveTime - sendTime;

                        DateTime utcTime = DateTime.Parse(dateHeader).ToUniversalTime();
                        long millTimestamp = TimeUtility.GetTimestamp(utcTime);

                        onGetServerTime(millTimestamp, deltaTime, sendTime, param);
                    }
                    else
                    {
                        if (onError != null)
                        {
                            onError("Date is empty.", param);
                        }
                    }
                }
            }

            www.Dispose();
        }

        public static IEnumerator DownloadFile(string url, Action<string, object> onResponse = null, Action<string, object> onError = null, object param = null)
        {
            UnityWebRequest www = UnityWebRequest.Get(url);
            www.timeout = TimeOut_Duration;
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                if (onError != null)
                {
                    onError(www.error, param);
                }
            }
            else
            {
                if (onResponse != null)
                {
                    onResponse(www.downloadHandler.text, param);
                }
            }

            www.Dispose();
        }

        public static IEnumerator DownloadFileWithServerTime(string url, Action<string, object> onResponse = null, Action<string, object> onError = null, Action<long, long, long, object> onGetServerTime = null, object param = null)
        {
            UnityWebRequest www = UnityWebRequest.Get(url);
            www.timeout = TimeOut_Duration;
            yield return www.SendWebRequest();
            long sendTime = TimeUtility.NowMillisecond();

            if (www.result != UnityWebRequest.Result.Success)
            {
                if (onError != null)
                {
                    onError(www.error, param);
                }
            }
            else
            {
                if (onResponse != null)
                {
                    onResponse(www.downloadHandler.text, param);
                }

                if (onGetServerTime != null)
                {
                    long receiveTime = TimeUtility.NowMillisecond();
                    long deltaTime = receiveTime - sendTime;
                    long millTimestamp = 0;

                    string dateHeader = www.GetResponseHeader("Date");
                    if (!string.IsNullOrEmpty(dateHeader))
                    {
                        DateTime utcTime = DateTime.Parse(dateHeader).ToUniversalTime();
                       millTimestamp = TimeUtility.GetTimestamp(utcTime);
                    }

                    onGetServerTime(millTimestamp, deltaTime, sendTime, param);
                }
            }

            www.Dispose();
        }

        public static IEnumerator DownloadFile(string url, Action<float, ulong, ulong> onDownloadSize, Action<byte[], object> onResponse = null, Action<string, object> onError = null, object param = null)
        {
            //UnityWebRequest 经配置可传输 HTTP HEAD 请求的 UnityWebRequest。
            UnityWebRequest headRequest = UnityWebRequest.Head(url);
            //开始与远程服务器通信。
            yield return headRequest.SendWebRequest();

            if (!string.IsNullOrEmpty(headRequest.error))
            {
                if (onError != null)
                {
                    onError(headRequest.error, param);
                }
                yield break;
            }

            //获取文件总大小
            ulong totalLength = ulong.Parse(headRequest.GetResponseHeader("Content-Length"));
            headRequest.Dispose();

            UnityWebRequest www = UnityWebRequest.Get(url);
            www.SendWebRequest();

            while (!www.isDone)
            {
                onDownloadSize?.Invoke(www.downloadProgress, www.downloadedBytes, totalLength);
                yield return 0;
            }

            onDownloadSize?.Invoke(www.downloadProgress, www.downloadedBytes, totalLength);

            if (www.result != UnityWebRequest.Result.Success)
            {
                if (onError != null)
                {
                    onError(www.error, param);
                }
            }
            else
            {
                if (onResponse != null)
                {
                    onResponse(www.downloadHandler.data, param);
                }
            }

            www.Dispose();
        }

        public static IEnumerator ReadStreamingAssetsFile(string _url, System.Action<byte[], object> _onLoadDone, Action<string, object> _onError, object _param = null)
        {
            UnityWebRequest request = UnityWebRequest.Get(_url);
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                if (_onError != null)
                {
                    _onError(request.error, _param);
                }
            }
            else
            {
                if (_onLoadDone != null)
                {
                    _onLoadDone(request.downloadHandler.data, _param);
                }
            }

            request.Dispose();
        }

        public static IEnumerator DownloadAssetBundle(string url, Action<AssetBundle, object> onResponse = null, Action<string, object> onError = null, object param = null)
        {
#if WX_GAME && !UNITY_EDITOR
            UnityWebRequest www = WXAssetBundle.GetAssetBundle(url);
#else
            UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(url);
#endif

            www.timeout = TimeOut_Duration;
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                if (onError != null)
                {
                    onError(www.error, param);
                }
            }
            else
            {
                if (onResponse != null)
                {
#if WX_GAME && !UNITY_EDITOR
                    AssetBundle bundle = (www.downloadHandler as DownloadHandlerWXAssetBundle).assetBundle;
#else
                    AssetBundle bundle = (www.downloadHandler as DownloadHandlerAssetBundle).assetBundle;
#endif
                    onResponse(bundle, param);
                }
            }

            www.Dispose();
        }

        public class BypassCertificate : CertificateHandler
        {
            protected override bool ValidateCertificate(byte[] certificateData)
            {
                //Simply return true no matter what
                return true;
            }
        }
    }
}