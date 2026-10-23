using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HttpUtil
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

    public class BypassCertificate : CertificateHandler
    {
        protected override bool ValidateCertificate(byte[] certificateData)
        {
            //Simply return true no matter what
            return true;
        }
    }
}

