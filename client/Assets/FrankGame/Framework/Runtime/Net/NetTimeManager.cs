using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrankGame
{
    public class NetTimeManager : Singlet<NetTimeManager>
    {
        public long NextRealDayTimestamp { get; private set; }
        public long NextMondayTimestamp { get; private set; }//下周一0点

        public long NowMillisecondTimestamp
        {
            get
            {
                return nowTimestamp;
            }
        }

        public long NowSecondTimestamp
        {
            get
            {
                return nowTimestamp / 1000;
            }
        }

        private long nowTimestamp;
        private Action onDailyRefresh;//隔天刷新事件
        private Action onWeekRefresh;//隔周刷新事件
        private Action<Action<long, long, long>> onRequestServerTime;//请求时间的方法
        private Action onTimeError;//时间错误方法

        private long serverMillSecTimestamp;//服务器时间戳
        private long serverSendReceiveDelatMillSecTime;//服务器时间戳收发间隔
        private long sendRequstTime;//发送请求的时间戳
        private float requestServerTimeDuration;//请求服务器时间的时长

        private bool isInit = false;
        private bool isRequestingTime = false;//是否正在请求时间
        private bool isStartTime = false;

        public void Init(Action _onDailyRefresh = null, Action _onWeekRefresh = null, Action<Action<long, long, long>> _onRequestServerTime = null, Action _onTimeError = null)
        {
            if (isInit)
            {
                return;
            }

            isInit = true;
            isStartTime = true;

            this.onDailyRefresh = _onDailyRefresh;
            this.onWeekRefresh = _onWeekRefresh;
            this.onRequestServerTime = _onRequestServerTime;
            this.onTimeError = _onTimeError;

            SetServerMillSecTimestamp(0, 0, 0);
        }

        public void SetServerMillSecTimestamp(long _serverMillSecTimestamp, long _sendReceiveDelatMillSecTime, long _sendRequstMillSecTime)
        {
            this.serverMillSecTimestamp = _serverMillSecTimestamp;
            this.serverSendReceiveDelatMillSecTime = _sendReceiveDelatMillSecTime;
            this.sendRequstTime = _sendRequstMillSecTime;

            NextRealDayTimestamp = GetTomorrowZeroHour();
            NextMondayTimestamp = GetNextMondayZeroHour();

            this.nowTimestamp = GetServerMillSecTimestamp();
            this.isRequestingTime = false;
            this.requestServerTimeDuration = 0;
        }

        /// <summary>
        /// 服务器时间戳（毫秒）
        /// </summary>
        /// <returns></returns>
        private long GetServerMillSecTimestamp()
        {
            if (this.serverMillSecTimestamp > 0)//服务器时间 = 本地时间 +（服务器时间 - 发送的时间）+ 网络延迟
            {
                return TimeUtility.NowMillisecond() + this.serverSendReceiveDelatMillSecTime - this.sendRequstTime + this.serverMillSecTimestamp;
            }
            else
            {
                return TimeUtility.NowMillisecond();
            }
        }

        public void Stop()
        {
            this.isStartTime = false;
        }


        public void Update(float _deltaTime, float _unscaledDeltaTime)
        {
            if (!this.isStartTime)
            {
                return;
            }

            if (GameConfig.HotUpdate.IsCheckServerTime())
            {
                //检测下时间有没有被修改，两次时间差太大，重新请求下服务器时间
                long newTime = GetServerMillSecTimestamp();
                int dTime = (int)(newTime - nowTimestamp) / 1000;

                if(dTime > GameConfig.HotUpdate.CheckTimeInterval || dTime < -GameConfig.HotUpdate.CheckTimeInterval)
                {
                    if (!isRequestingTime)
                    {
                        isRequestingTime = true;
                        requestServerTimeDuration = 0;
                        this.onRequestServerTime?.Invoke(SetServerMillSecTimestamp);
                    }
                    else
                    {
                        //超过时间没回强制下线
                        requestServerTimeDuration += _unscaledDeltaTime;
                        if(requestServerTimeDuration >= GameConfig.HotUpdate.CheckTimeInterval)
                        {
                            Stop();
                            this.onTimeError?.Invoke();
                        }
                    }

                    return;
                }
            }

            nowTimestamp = GetServerMillSecTimestamp();

            if (nowTimestamp > NextRealDayTimestamp)
            {
                NextRealDayTimestamp = GetTomorrowZeroHour();

                this.onDailyRefresh?.Invoke();
            }

            if (nowTimestamp > NextMondayTimestamp)
            {
                NextMondayTimestamp = GetNextMondayZeroHour();

                this.onWeekRefresh?.Invoke();
            }
        }


        /// <summary>
        /// 获取今日0点时间戳（毫秒）
        /// </summary>
        /// <returns></returns>
        public long GetTodayZeroHour()
        {
            return TimeUtility.GetNowDayZeroHour(GetServerMillSecTimestamp());
        }

        /// <summary>
        /// 获取明天0点时间戳（毫秒）
        /// </summary>
        /// <returns></returns>
        public long GetTomorrowZeroHour()
        {
            return TimeUtility.GetNextDayZeroHour(GetServerMillSecTimestamp());
        }

        /// <summary>
        /// 获取本周一0点时间戳（毫秒）
        /// </summary>
        /// <returns></returns>
        public long GetNowMondayZeroHour()
        {
            return TimeUtility.GetNowMondayZeroHour(GetServerMillSecTimestamp());
        }

        /// <summary>
        /// 获取下周一0点时间戳（毫秒）
        /// </summary>
        /// <returns></returns>
        public long GetNextMondayZeroHour()
        {
            return TimeUtility.GetNextMondayZeroHour(GetServerMillSecTimestamp());
        }
    }
}
