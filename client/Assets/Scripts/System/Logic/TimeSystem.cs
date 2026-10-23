using System.Collections.Generic;
using UnityEngine;
using FrankGame;

public class TimeSystem : SystemBase<TimeSystem>
{
    public const int SecondsToMillisecond = 1000;
    public const int MinuteToSeconds = 60;
    public const int HourToSeconds = MinuteToSeconds * 60;

    public System.Action onRequestServerTime;
    public System.Action onRequestServerTimeFail;
    public System.Action onRequestServerTimeSuccess;

    protected override void OnInit()
    {
        base.OnInit();

        NetTimeManager.Instance.Init(OnDailyRefresh, OnWeekRefresh, OnRequestServerTime, OnRequestServerTimeFail);
    }

    private void OnDailyRefresh()
    {
        EventNativeSystem.Instance.SendEvent(SysEvent.Daily_Refresh);
    }

    private void OnWeekRefresh()
    {
        EventNativeSystem.Instance.SendEvent(SysEvent.Week_Refresh);
    }

    protected override void OnUpdate(float _deltaTime, float _unscaledDeltaTime)
    {
        NetTimeManager.Instance.Update(_deltaTime, _unscaledDeltaTime);
        TimerManager.Instance.Update(_deltaTime, _unscaledDeltaTime);
    }

    private void OnRequestServerTime(System.Action<long, long, long> _onRequestSuccess)
    {
        Log.Debug(ELogType.Net, "重新请求检测服务器。");

        GameEntry.Instance.StartCoroutine(DownloadUtil.RequestServerTime(GameConfig.Version.VersionInfo.VersionUrl, (t, d, s, p) =>
        {
            Log.Debug(ELogType.Net, "重连检测服务器时间戳：" + t + "，收发时间间隔：" + d + "，发送时间：" + s + "，当前时间：" + TimeUtility.NowMillisecond() + "，服务器比本地时间快：" + (t - s) + "ms");
            
            this.onRequestServerTimeSuccess?.Invoke();

            _onRequestSuccess?.Invoke(t, d, s);
        }, (e, p) =>
        {
            OnRequestServerTimeFail();
        }));

        this.onRequestServerTime?.Invoke();
    }

    private void OnRequestServerTimeFail()
    {
        Log.Error(ELogType.Net, "重连检测服务器时间失败！");

        NetTimeManager.Instance.Stop();

        this.onRequestServerTimeFail?.Invoke();
    }

    public int AddDelay(float _delay, OnTimerCallback _func, bool _ignoreScale = false)
    {
        if(_delay <= 0)
        {
            _func?.Invoke();
            return -1;
        }

        return TimerManager.Instance.AddDelay(_delay, _func, _ignoreScale);
    }

    public int AddIntervalUpdate(float _interval, OnTimerCallback _func, bool _immediately = false, bool _ignoreScale = false)
    {
        return TimerManager.Instance.AddIntervalUpdate(_interval, _func, _immediately, _ignoreScale);
    }

    /// <summary>
    /// 持续时间内间隔刷新
    /// </summary>
    /// <param name="_interval"></param>
    /// <param name="_duration"></param>
    /// <param name="_func"></param>
    /// <param name="_immediately"></param>
    /// <returns></returns>
    public int AddIntervalUpdate(float _interval, float _duration, OnTimerCallback _func, bool _immediately = false)
    {
        return TimerManager.Instance.AddIntervalUpdate(_interval, _duration, _func, _immediately);
    }

    public int AddCount(float _interval, int _count, OnTimerCallback _func, OnTimerCallback _complete = null)
    {
        return TimerManager.Instance.AddCount(_interval, _count, _func, _complete);
    }

    public void RemoveTimer(int _timeId)
    {
        TimerManager.Instance.RemoveTimer(_timeId);
    }
}
