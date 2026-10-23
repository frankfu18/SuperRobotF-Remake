using System;
using editor.cfg.common;
using FrankGame;
using UnityEngine;

public class AdManager : Singlet<AdManager>
{
    public void PlayAd(Action _onPlayComplete, Action _onCancel = null)
    {
        LogUtil.Log("看广告");

        _onPlayComplete?.Invoke();
    }

    /// <summary>
    /// 广告按钮展示时调用
    /// </summary>
    /// <param name="_evtType"></param>
    public void ShowAd(EAdEventType _evtType)
    {
        if (AdEventKey.RvKeyDic.TryGetValue(_evtType, out string rvKey))
        {
            LogUtil.Log("展示广告 EAdEventType:" + _evtType + ", key : " + rvKey);
        }
    }

    public void PlayAd(EAdEventType _evtType, Action _onPlayComplete, Action _onCancel = null)
    {
        LogUtil.Log($"PlayAd, _evtType:{_evtType}");

        PlayAd(_onPlayComplete, _onCancel);
    }
}
