using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using db.parser;
using FrankGame;

public class LoginModule : ModuleBase<LoginModule>
{
    public bool IsCurLoginCrossDay
    {
        get { return this.isCurLoginCrossDay; }
    }

    public bool IsCurLoginCrossWeek
    {
        get { return this.isCurLoginCrossWeek; }
    }

    private long _lastOnlineTime = 0;
    private bool isCurLoginCrossDay;//当前登录是否跨天了
    private bool isCurLoginCrossWeek;//当前登录是否跨周了

    protected override void AddEventListeners()
    {
        base.AddEventListeners();

        EventCenterSystem.Instance.AddEvent(SysEvent.Module_Load_Done, this, OnOpenMainPanel);
        EventCenterSystem.Instance.AddEvent(SysEvent.Daily_Refresh, this, OnDailyRefresh);
    }

    protected override void OnLoadConfigDone()
    {
        base.OnLoadConfigDone();
    }

    protected override void OnLogin()
    {
        base.OnLogin();

        if (IsCrossDay())//是否跨天了
        {
            this.isCurLoginCrossDay = true;

            OnDailyRefresh(null);

            long lastTime = UserDataParser.Ins.LastOnlineTimestamp;
            bool crossWeek = lastTime == 0;
            if (lastTime > 0)
            {
                long lastMondayTime = TimeUtility.GetNowMondayZeroHour(lastTime);
                long nowMondayTime = NetTimeManager.Instance.GetNowMondayZeroHour();

                if (nowMondayTime > lastMondayTime)
                {
                    crossWeek = true;
                    this.isCurLoginCrossWeek = crossWeek;
                }
            }

            this.isCurLoginCrossWeek = crossWeek;
        }
        else
        {
            this.isCurLoginCrossDay = false;
            this.isCurLoginCrossWeek = false;
        }

        UserDataParser.Ins.LastOnlineTimestamp = NetTimeManager.Instance.NowMillisecondTimestamp;//登录时间
    }

    protected override void OnLoginDone()
    {
        base.OnLoginDone();
        TimeSystem.Instance.AddIntervalUpdate(60, OnUpdateOnline, true);
    }

    private void OnDailyRefresh(params object[] _params)
    {
        ResetCount();
    }

    /// <summary>
    /// 重置次数
    /// </summary>
    public void ResetCount()
    {

    }

    private void OnOpenMainPanel(params object[] _params)
    {
        UISystem.Instance.Open(UIConfig.GuidePanel);
        UISystem.Instance.Open(UIConfig.MainPanel, true, (panel) => { ProcedureComplete(); });
    }

    private void ProcedureComplete()
    {
        SendEvent(SysEvent.Procedure_Complete);
    }

    //刷新在线时间
    private void OnUpdateOnline()
    {
        UserDataParser.Ins.LastOnlineTimestamp = NetTimeManager.Instance.NowMillisecondTimestamp;
        if (_lastOnlineTime > 0)
        {
            EventCenterSystem.Instance.SendEvent(EventConst.UpdateOnlineTime, NetTimeManager.Instance.NowMillisecondTimestamp - _lastOnlineTime);
        }

        _lastOnlineTime = NetTimeManager.Instance.NowMillisecondTimestamp;
    }

    //是否跨天了
    public bool IsCrossDay()
    {
        long nowZero = NetTimeManager.Instance.GetTodayZeroHour();
        bool result = UserDataParser.Ins.LastOnlineTimestamp < nowZero;

        return result;
    }
}
