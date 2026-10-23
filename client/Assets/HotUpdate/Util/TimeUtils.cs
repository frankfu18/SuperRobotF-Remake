using System;
using System.Text;
using FrankGame;

public static class TimeUtils
{
    public static readonly DateTime ms_UTCBase = new DateTime(1970, 1, 1);

    private static long timeSub;
    private static readonly StringBuilder _timeStr;


    //public static DateTime ConvertSecondsToLocalTime(int seconds)
    //{
    //    TimeSpan timeSpan = TimeSpan.FromSeconds(seconds);
    //    DateTime utcTime = ms_UTCBase + timeSpan;
    //    DateTime localTime = TimeZoneInfo.ConvertTime(utcTime, TimeZoneInfo.Utc, TimeZoneInfo.Local);
    //    return localTime;
    //}

    //public static DateTime ConvertMillSceondsToLocalTime(long millSeconds)
    //{
    //    TimeSpan timeSpan = TimeSpan.FromMilliseconds(millSeconds);
    //    DateTime utcTime = ms_UTCBase + timeSpan;
    //    DateTime localTime = TimeZoneInfo.ConvertTime(utcTime, TimeZoneInfo.Utc, TimeZoneInfo.Local);
    //    return localTime;
    //}

    /// <summary>
    /// 将秒转成hh:mm:ss格式，如：03:12:26, 346:18:11
    /// </summary>
    /// <returns></returns>
    public static string FormatSecondsToHhmmss(int seconds, bool full = true)
    {
        if (seconds <= 0)
        {
            if (full)
            {
                return "00:00:00";
            }
            else
            {
                return "00:00";
            }
        }
        int remainSeconds = seconds;
        int hours = remainSeconds / 3600;
        remainSeconds %= 3600;
        int minutes = remainSeconds / 60;
        remainSeconds %= 60;
        int secs = remainSeconds;

        if (hours == 0 && !full)
        {
            return string.Format("{0}:{1}", FormatNumberTo2BitNumber(minutes), FormatNumberTo2BitNumber(secs));
        }

        return string.Format("{0}:{1}:{2}", FormatNumberTo2BitNumber(hours), FormatNumberTo2BitNumber(minutes), FormatNumberTo2BitNumber(secs));
    }

    public static string FormatSecondsToHhmm(int seconds, bool full = true)
    {
        if (seconds <= 0)
        {
            return "00:00:00";
        }
        int remainSeconds = seconds;
        int hours = remainSeconds / 3600;
        remainSeconds %= 3600;
        int minutes = remainSeconds / 60;
        remainSeconds %= 60;
        int secs = remainSeconds;

        if (hours == 0 && !full)
        {
            return string.Format("00:{0}", FormatNumberTo2BitNumber(minutes));
        }

        return string.Format("{0}:{1}", FormatNumberTo2BitNumber(hours), FormatNumberTo2BitNumber(minutes));
    }

    /// <summary>
    /// 从 HH:MM 格式的字符串解析出秒
    /// </summary>
    /// <param name="timeStr">时间字符串,HH:MM，如 08:02</param>
    /// <returns>秒数</returns>
    public static int ParseTimeToSecondsByHHMM(string timeStr) {
        if (timeStr == null || timeStr.Length != 5 || !timeStr.Contains(":")) {
            return 0;
        }
        var seconds = 0;
        var timeStrs = timeStr.Split(':');
        for (var i = 0;i < 2;i ++) {
            seconds += int.Parse(timeStrs[i]) * (i == 0 ? 3600 : 60);
        }
        return seconds;
    }

    /// <summary>
    /// 将秒转成d hh:mm:ss格式，如：03:12:26, 2d 20:18:11
    /// </summary>
    /// <param name="seconds"></param>
    /// <returns></returns>
    public static string FormatSecondsToDhhmmss(int seconds, bool full = false)
    {
        if (seconds <= 0)
        {
            return "00:00:00";
        }
        long remainSeconds = seconds;
        long day = 0;
        if(remainSeconds > 86400)
        {
            day = remainSeconds / 86400;
            remainSeconds %= 86400;
        }

        long hours = remainSeconds / 3600;
        remainSeconds %= 3600;
        long minutes = remainSeconds / 60;
        remainSeconds %= 60;
        long secs = remainSeconds;

        string formatTime = string.Empty;
        if (day > 0)
            if (full)
            {
                return string.Format("{0}d {1}:{2}:{3}", day, FormatNumberTo2BitNumber(hours), FormatNumberTo2BitNumber(minutes), FormatNumberTo2BitNumber(secs));
            }
            else
            {
                return string.Format("{0}d {1}:{2}", day, FormatNumberTo2BitNumber(hours), FormatNumberTo2BitNumber(minutes));
            }
        else
            return string.Format("{0}:{1}:{2}", FormatNumberTo2BitNumber(hours), FormatNumberTo2BitNumber(minutes), FormatNumberTo2BitNumber(secs));
    }

    public static string FormatSecondsTo_00d00h00m00s(int seconds)
    {
        if (seconds <= 0)
        {
            return "00s";
        }
        int remainSeconds = seconds;
        int day = remainSeconds / 86400;
        remainSeconds %= 86400;
        int hours = remainSeconds / 3600;
        remainSeconds %= 3600;
        int minutes = remainSeconds / 60;
        remainSeconds %= 60;
        int secs = remainSeconds;

        string str;
        //if (day > 0)
        //    str = string.Format("{0}{1}d {2}{3}h", day >= 10 ? string.Empty : "0", day, hours >= 10 ? string.Empty : "0", hours);
        //else if (hours > 0)
        //    str = string.Format("{0}{1}h {2}{3}m", hours >= 10 ? string.Empty : "0", hours, minutes >= 10 ? string.Empty : "0", minutes);
        //else if (minutes > 0)
        //    str = string.Format("{0}{1}m {2}{3}s", minutes >= 10 ? string.Empty : "0", minutes, secs >= 10 ? string.Empty : "0", secs);
        //else
        //    str = string.Format("{{0}{1}s", secs >= 10 ? string.Empty : "0", secs);

        if (day > 0)
        {
            str = string.Format("{0}d {1}{2}h {3}{4}m {5}{6}s", day, hours >= 10 ? string.Empty : "0", hours, minutes >= 10 ? string.Empty : "0", minutes, secs >= 10 ? string.Empty : "0", secs);
        }
        else if (hours > 0)
        {
            if (secs > 0 && minutes > 0)
            {
                str = string.Format("{0}{1}h {2}{3}m {4}{5}s", hours >= 10 ? string.Empty : "0", hours, minutes >= 10 ? string.Empty : "0", minutes, secs >= 10 ? string.Empty : "0", secs);
            }
            else
            {
                if (secs > 0)
                {
                    str = string.Format("{0}{1}h {2}{3}m {4}{5}s", hours >= 10 ? string.Empty : "0", hours, minutes >= 10 ? string.Empty : "0", minutes, secs >= 10 ? string.Empty : "0", secs);
                }
                else
                {
                    if (minutes > 0)
                    {
                        str = string.Format("{0}{1}h {2}{3}m", hours >= 10 ? string.Empty : "0", hours, minutes >= 10 ? string.Empty : "0", minutes);
                    }
                    else
                    {
                        str = string.Format("{0}{1}h", hours >= 10 ? string.Empty : "0", hours);
                    }
                }
            }
        }
        else if (minutes > 0)
        {
            if (secs > 0)
            {
                str = string.Format("{0}{1}m {2}{3}s", minutes >= 10 ? string.Empty : "0", minutes, secs >= 10 ? string.Empty : "0", secs);
            }
            else
            {
                str = string.Format("{0}{1}m", minutes >= 10 ? string.Empty : "0", minutes);
            }
        }
        else
        {
            str = string.Format("{0}{1}s", secs >= 10 ? string.Empty : "0", secs);
        }
            

        return str;
    }
    public static string FormatSecondsTo_00h00m00s(int seconds, bool full = false)
    {
        if (seconds <= 0)
        {
            return full ? "00h:00m:00s" : "00s";
        }
        int remainSeconds = seconds;
        int day = remainSeconds / 86400;
        remainSeconds %= 86400;
        int hours = remainSeconds / 3600;
        remainSeconds %= 3600;
        int minutes = remainSeconds / 60;
        remainSeconds %= 60;
        int secs = remainSeconds;

        string str;

        hours += day * 24;

        if (hours > 0)
        {
            if (secs > 0 && minutes > 0)
            {
                str = string.Format("{0}{1}h {2}{3}m {4}{5}s", hours >= 10 ? string.Empty : "0", hours, minutes >= 10 ? string.Empty : "0", minutes, secs >= 10 ? string.Empty : "0", secs);
            }
            else
            {
                if (secs > 0)
                {
                    str = string.Format("{0}{1}h {2}{3}m {4}{5}s", hours >= 10 ? string.Empty : "0", hours, minutes >= 10 ? string.Empty : "0", minutes, secs >= 10 ? string.Empty : "0", secs);
                }
                else
                {
                    if (minutes > 0)
                    {
                        str = string.Format("{0}{1}h {2}{3}m", hours >= 10 ? string.Empty : "0", hours, minutes >= 10 ? string.Empty : "0", minutes);
                        if (full)
                        {
                            str += " 00s";
                        }
                    }
                    else
                    {
                        str = string.Format("{0}{1}h", hours >= 10 ? string.Empty : "0", hours);
                        if (full)
                        {
                            str += " 00m 00s";
                        }
                    }
                }
            }
        }
        else if (minutes > 0)
        {
            if (secs > 0)
            {
                str = string.Format("{0}{1}m {2}{3}s", minutes >= 10 ? string.Empty : "0", minutes, secs >= 10 ? string.Empty : "0", secs);
                if (full)
                {
                    str = "00h " + str;
                }
            }
            else
            {
                str = string.Format("{0}{1}m", minutes >= 10 ? string.Empty : "0", minutes);
                if (full)
                {
                    str = "00h " + str + " 00s";
                }
            }
        }
        else
        {
            str = string.Format("{0}{1}s", secs >= 10 ? string.Empty : "0", secs);
            if (full)
            {
                str = "00h 00m " + str;
            }
        }
        return str.Replace(" ", "");
    }

    private static string FormatNumberTo2BitNumber(long num, bool full = true)
    {
        return num < 10 ? (full ? string.Format("0{0}", num) : num.ToString()) : num.ToString();
    }

    //获取当前时间
    private static long GetLocalTimeMilliseconds(bool bflag = true)
    {
        TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0);
        long ret;
        if (bflag)
        {
            ret = Convert.ToInt64(ts.TotalSeconds);
        }
        else
        {
            ret = Convert.ToInt64(ts.TotalMilliseconds);
        }

        return ret;
    }
    /// <summary>
    /// 时间戳展示
    /// </summary>
    /// <param name="endTime"></param>
    /// <returns></returns>
    public static string GetTimeSecondsDes(long endTime)
    {
        DateTime ts = new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(endTime);
        return ts.ToString("yyyy-MM-dd HH:mm:ss");
    }
    public static string GetTimeSecondsDes(long endTime , string format)
    {
        //DateTime ts = new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(endTime);
        DateTime ts = TimeUtils.GetDateTimeSeconds(endTime);
        return ts.ToString(format);
    }

    /// <summary>
    /// 将指定的UTC时间 转化为当地时区的Utc时间
    /// </summary>
    /// <param name="timeStamp"></param>
    /// <returns></returns>
    public static DateTime ConvertTimeStampToDateTime(long timeStamp)
    {
        DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); // 当地时区
        DateTime dt = startTime.AddMilliseconds(timeStamp);
        return dt;
    }

    /// <summary>
    /// 将服务器的毫秒变为秒 并返还字符串
    /// </summary>
    /// <param name="serverTime"></param>
    /// <returns></returns>
    public static string GetTimeByServerMilliseconds(long serverTime)
    {
        serverTime = serverTime / 1000;
        return GetTimeSecondsDes((int)serverTime);
    }

    /// <summary>
    /// 抽卡倒计时 使用到 将获取的毫秒转化成 hh:mm:ss格式，如：
    /// </summary>
    /// <param name="Time"></param>
    /// <returns> 
    /// {0}d {1}h"
    /// {0}h {1}m
    /// {0}m {1}s
    /// </returns>
    public static string GetMillisecondsTOHHMMMSS(long Time, bool full = true)
    {
        int remainSeconds = (int)Time / 1000;
        if (remainSeconds <= 0)
        {
            return "00:00";
        }
        int day = remainSeconds / (3600 * 24);
        remainSeconds %= (3600 * 24);
        int hours = remainSeconds / 3600;
        remainSeconds %= 3600;
        int minutes = remainSeconds / 60;
        remainSeconds %= 60;
        int secs = remainSeconds;
        if (day >= 1)
        {
            if (full)
            {
                return string.Format("{0}d {1}h", FormatNumberTo2BitNumber(day, full), FormatNumberTo2BitNumber(hours, full));
            }

            if (hours >= 1)
            {
                return string.Format("{0}d {1}h", FormatNumberTo2BitNumber(day, full), FormatNumberTo2BitNumber(hours, full));
            }

            return string.Format("{0}d", FormatNumberTo2BitNumber(day, full));
        }
        else
        {
            if (hours >= 1)
            {
                if (full)
                {
                    return string.Format("{0}h {1}m", FormatNumberTo2BitNumber(hours, full), FormatNumberTo2BitNumber(minutes, full));
                }

                if (minutes >= 1)
                {
                    return string.Format("{0}h {1}m", FormatNumberTo2BitNumber(hours, full), FormatNumberTo2BitNumber(minutes, full));
                }

                return string.Format("{0}h", FormatNumberTo2BitNumber(hours, full));
            }
            else
            {
                if (minutes >= 1)
                {
                    if (full)
                    {
                        return string.Format("{0}m {1}s", FormatNumberTo2BitNumber(minutes, full), FormatNumberTo2BitNumber(secs, full));
                    }

                    if (secs >= 1)
                    {
                        return string.Format("{0}m {1}s", FormatNumberTo2BitNumber(minutes, full), FormatNumberTo2BitNumber(secs, full));
                    }

                    return string.Format("{0}m", FormatNumberTo2BitNumber(minutes, full));
                }

                return string.Format("{0}s", FormatNumberTo2BitNumber(secs, full));
            }
        }
    }

    //根据
    public static string GetMillisecondsCountdown(long Time)
    {
        string timeStr = "00:00:00";
        int remainSeconds = (int)Time / 1000;
        if (remainSeconds > 0)
        {
            int day = remainSeconds / (3600 * 24);
            remainSeconds %= (3600 * 24);
            int hours = remainSeconds / 3600 + day*24;
            remainSeconds %= 3600;
            int minutes = remainSeconds / 60;
            remainSeconds %= 60;
            int secs = remainSeconds;

            return string.Format("{0}:{1}:{2}", FormatNumberTo2BitNumber(hours, true), FormatNumberTo2BitNumber(minutes, true), FormatNumberTo2BitNumber(secs, true));
        }
        return timeStr;
    }
    /// <summary>
    /// 根据传入的秒 来判断当前需要显示的格式是什么 研究需要使用到
    /// </summary>
    /// <param name="remainSeconds">秒/// </param>
    /// <returns>
    /// 1  {0}小时{1}分钟
    /// 2  {0}分钟{1}秒
    /// 3  {0}秒</returns>
    public static int ISHourOrMinutesOrSeconds(int remainSeconds)
    {
        int hours = remainSeconds / 3600;
        remainSeconds %= 3600;
        int minutes = remainSeconds / 60;
        if (hours >= 1)
        {
            return 1;
        }
        else if (minutes >= 1)
        {
            return 2;
        }
        else
        {
            return 3;
        }
    }

    public static StringBuilder stringBuilderHourOrMinutesOrSeconds = new StringBuilder();
    /// <summary>
    /// 获取时间 或者分钟 秒的参数
    /// </summary>
    /// <param name="remainSeconds"></param>
    /// <returns></returns>
    public static string[] GetHourOrMinutesOrSecondsParams(int remainSeconds)
    {
        stringBuilderHourOrMinutesOrSeconds.Clear();
        int hours = remainSeconds / 3600;
        remainSeconds %= 3600;
        int minutes = remainSeconds / 60;
        remainSeconds %= 60;
        int secs = remainSeconds;
        if (hours >= 1)
        {
            stringBuilderHourOrMinutesOrSeconds.Append(hours + ",");
            stringBuilderHourOrMinutesOrSeconds.Append(minutes);
            return stringBuilderHourOrMinutesOrSeconds.ToString().Split(',');
        }
        else if (minutes >= 1)
        {
            stringBuilderHourOrMinutesOrSeconds.Append(minutes + ",");
            stringBuilderHourOrMinutesOrSeconds.Append(secs);
            return stringBuilderHourOrMinutesOrSeconds.ToString().Split(',');
        }
        else
        {
            stringBuilderHourOrMinutesOrSeconds.Append(secs);
            return stringBuilderHourOrMinutesOrSeconds.ToString().Split(',');
        }
    }

    /// <summary>
    /// 获取当前时间距离当天UTC  0点的毫秒时间
    /// </summary>
    /// <returns></returns>
    public static long GetCurrentToZeroMilliseconds()
    {
        return 86400000 - GetCurrentTimeMilliseconds() % 86400000;
    }

    /// <summary>
    /// 获取指定时间到当天UTC 0点的毫秒时间
    /// </summary>
    /// <returns></returns>
    public static long GetAssignTimeToZeroMilliseconds(long assignTime)
    {
        return assignTime - assignTime % 86400000;
    }

    /// <summary>
    /// 获取当天0点的时间戳 
    /// </summary>
    /// <param name="hour"></param>
    /// <returns></returns>
    public static long GetCurrentZeroTime()
    {
        long now = GetCurrentTimeMilliseconds();
        long daySecond = 86400000;
        long dayTime = now - (now + 28800000) % daySecond;
        return dayTime;
    }

    /// <summary>
    /// 获取当天0点时间 +[Num]个小时的时间戳
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public static long GetCurrentZeroToHourTime(int num)
    {
        long currentTime = GetCurrentZeroTime();
        return currentTime + (num * 3600000);
    }

    /// <summary>
    /// 获取当天0点时间 +[num]个常量表的固定数据
    /// </summary>
    /// <param name="fixedTime">固定时间</param>
    /// <param name="num">第几个</param>
    /// <returns></returns>
    public static long GetCurrentZeroToHourTime(long fixedTime, int num)
    {
        long currentTime = GetCurrentZeroTime();
        return currentTime + (fixedTime * num);
    }



    public static string GetTimeSecondsWithComma(int endTime)
    {
        DateTime ts = new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(endTime);
        return ts.ToString("yyyy.MM.dd");
    }

    public static int GetCurrentTimeSec()
    {
        return (int)(GetCurrentTimeMilliseconds() * 0.001d);
    }

    public static long GetCurrentTimeMilliseconds()
    {
        long localTime = GetLocalTimeMilliseconds(false);
        return localTime - timeSub;
    }
    public static void SetServerTime(long timeMillSec)
    {
        ServerTime = timeMillSec;
        long localTime = GetLocalTimeMilliseconds(false);
        timeSub = localTime - serverTime;
    }

    public static DateTime CurTime()
    {
        int curMilSec = (int)(GetCurrentTimeMilliseconds() * 0.001d);
        return curMilSec.ToDateTime();
    }

    private static long localTime;
    private static long serverTime;
    private static long ServerTime
    {
        set
        {
            localTime = DateTime.Now.ToUnixTimeMill();
            serverTime = value + 1;
        }
        get => serverTime + (DateTime.Now.ToUnixTimeMill() - localTime);
    }

    public static int GetLocalDate()
    {
        return DateTime.Now.Day;
    }

    //public static string GetDate(int hour)
    //{
    //    DateTime sever = ServerTime.ToDateTime();
    //    sever = sever.AddHours((hour == 0 ? 24 : hour) - sever.Hour);
    //    DateTime serverNew = new DateTime(sever.Year, sever.Month, sever.Day, sever.Hour, 0, 0);
    //    return GetTimeSecondsDes(serverNew.ToUnixTime());
    //}

    //public static string GetDateByDayofWeek(int week)
    //{
    //    DateTime sever = ServerTime.ToDateTime();
    //    sever = sever.AddDays(Mathf.Abs(week - (int)sever.DayOfWeek));
    //    DateTime serverNew = new DateTime(sever.Year, sever.Month, sever.Day);
    //    return GetTimeSecondsDes(serverNew.ToUnixTime());
    //}

    public static string GetDDHHMMSS(int seconds)
    {
        return GetTimeTwoDesc(seconds);
    }

    public static string GetTimeDesc(int seconds)
    {
        TimeSpan ts = new TimeSpan(0, 0, 0, seconds);

        int hour = (int)ts.TotalHours;
        int min = ts.Minutes;
        int sec = ts.Seconds;

        // if (hour > 0)
        // {
        //     return string.Format(Language.instance.GetLang("time_hour"), hour, min, sec);
        // }
        // else if (min > 0)
        // {
        //     return string.Format(Language.instance.GetLang("time_min"), min, sec);
        // }
        // else
        // {
        //     return string.Format(Language.instance.GetLang("time_sec"), sec);
        // }
        return "";
    }

    /// <summary>
    /// 获取2位时间节点显示 
    /// </summary>
    /// <param name="seconds"></param>
    /// <returns></returns>
    public static string GetTimeTwoDesc(int seconds)
    {
        TimeSpan ts = new TimeSpan(0, 0, 0, seconds);
        int tmpDay = (int)ts.TotalDays;
        if (tmpDay > 0)
        {
            return string.Format("{0}{1} {2}", tmpDay, "", ts.ToString(@"hh\:mm\:ss"));
        }
        else
        {

            int temp = (int)ts.TotalHours;
            if (temp != 0)
            {
                return ts.ToString(@"hh\:mm\:ss");
                //return temp + ":" + ts.ToString(@"mm\:ss");
            }
            else
            {
                return ts.ToString(@"mm\:ss");
                //return ts.ToString(@"m\:ss");
            }
        }
    }

    /// <summary>
    /// 格式：2019-03-19 12:00
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    //public static string GetDateAndTime(int _time)
    //{
    //    string timeStamp = _time.ToString();
    //    DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
    //    long lTime = long.Parse(timeStamp + "0000000");
    //    TimeSpan toNow = new TimeSpan(lTime);
    //    DateTime dtResult = dtStart.AddAndGets(toNow);
    //    UnityGameFramework.Runtime.Log.Debug(dtResult);
    //    string date = dtResult.ToShortDateString().ToString();
    //    string time = dtResult.ToLongTimeString().ToString();
    //    string[] date_arr = date.Split('/');
    //    string[] time_arr = time.Split(':');
    //    string result = date_arr[0] + "月" + date_arr[1] + "日" + " " + time_arr[0] + "时" + time_arr[1] + "分";
    //    return result;
    //}
    public static string FormatTime(int time, bool includeTime = false)
    {
        /*string result = "";
        int nowTime = TimeUtils.GetCurrentTimeSec();
        DateTime now = nowTime.ToDateTime();
        int nowYear = now.Year;
        int nowMonth = now.Month;
        int nowDay = now.Day;
        DateTime src = srcTime.ToDateTime();
        int srcYear = src.Year;
        int srcMonth = src.Month;
        int srcDay = src.Day;
        string timeExtraStr = includeTime ? "" : " " + src.ToString("HH:mm");
        if (nowYear == srcYear)
        {
            int delta = nowTime - srcTime;
            if (nowMonth == srcMonth && nowDay == srcDay)
            {
                //in 60s
                if (delta < 60)
                {
                    result = Language.instance.GetLang("common.ago.justnow");
                }
                else
                {
                    result = src.ToString("HH:mm");
                }
            }
            else
            {
                //yesterday
                DateTime yesterday = now.Subtract(new TimeSpan(1, 0, 0, 0));
                DateTime beforeYesterday = now.Subtract(new TimeSpan(2, 0, 0, 0));
                if (srcMonth == yesterday.Month && srcDay == yesterday.Day)
                {
                    result = Language.instance.GetLang("common.ago.yesterday") + timeExtraStr;
                }
                else if (srcMonth == beforeYesterday.Month && srcDay == beforeYesterday.Day)
                {
                    result = Language.instance.GetLang("common.ago.beforeyesterday") + timeExtraStr;
                }
                else
                {
                    int deltaHour = delta / 3600;
                    if (deltaHour < 7 * 24)
                    {
                        result = GetWeekDay(src) + timeExtraStr;
                    }
                    else
                    {
                        result = src.ToString("yyyy-M-d") + timeExtraStr;
                    }
                }
            }
        }
        else
        {
            result = src.ToString("yyyy-M-d") + timeExtraStr;
        }
        return result;*/
        var result = "";
        var SECOND = 1;
        var MINUTE = 60 * SECOND;
        var HOUR = 60 * MINUTE;
        var DAY = 24 * HOUR;
        var MONTH = 30 * DAY;
        var now = TimeUtils.GetCurrentTimeSec();
        var delta = now - time;
        // if(delta <= 1 * MINUTE) {
        // 	result = Language.instance.GetLang("common.ago.justnow");
        // }
        // else if(delta <= 60 * MINUTE) {
        // 	result = Language.instance.GetLang("common.ago.minutes", new Dictionary<string, object> () {{"time",(delta / MINUTE)}});
        // } else if(delta < 24 * HOUR) {
        // 	result = Language.instance.GetLang("common.ago.hours", new Dictionary<string, object> () {{"time",(delta / HOUR)}});
        // } else if(delta < 48 * HOUR) {
        // 	result = Language.instance.GetLang("common.ago.yesterday");
        // } else if(delta < 30 * DAY) {
        // 	result = Language.instance.GetLang("common.ago.days", new Dictionary<string, object> () {{"time",(delta / DAY)}});
        // } else if(delta < 12 * MONTH) {
        // 	var months = Mathf.FloorToInt ((delta / DAY) / 30f);
        // 	result = Language.instance.GetLang("common.ago.months", new Dictionary<string, object> () {{"time",(months <= 1 ? 1 : months)}});
        // }else{
        // 	var years = Mathf.FloorToInt((delta/DAY)/365f);
        // 	result = Language.instance.GetLang("common.ago.years", new Dictionary<string, object> () {{"time",(years <= 1 ? 1 : years)}});
        // }	

        return result;
    }

    public static string GetWeekDay(DateTime dt)
    {
        // string[] weekday = new string[]{Language.instance.GetLang("common.ago.date0"),
        //                                 Language.instance.GetLang("common.ago.date1"),
        //                                 Language.instance.GetLang("common.ago.date2"),
        //                                 Language.instance.GetLang("common.ago.date3"),
        //                                 Language.instance.GetLang("common.ago.date4"),
        //                                 Language.instance.GetLang("common.ago.date5"),
        //                                 Language.instance.GetLang("common.ago.date6")};
        // return weekday[(int)dt.DayOfWeek];
        return "";
    }

    public static string FormatTimeWithDay(long time)
    {
        if (time < 0)
        {
            return "0";
        }
        TimeSpan t = TimeSpan.FromSeconds(time);
        string result = "";
        long hours = 0L;
        if (t.Days > 0)
        {
            result += t.Days.ToString() + "D ";
        }
        if (t.Hours > 0)
        {
            hours = t.Hours + hours;
            result += (hours < 10 ? "0" + hours : hours.ToString()) + ":";
        }
        else
        {
            result += "00:";
        }
        if (t.Minutes > 0)
        {
            result += (t.Minutes < 10 ? "0" + t.Minutes : t.Minutes.ToString()) + ":";
        }
        else
        {
            result += "00:";
        }
        if (t.Seconds > 0)
        {
            result += t.Seconds < 10 ? "0" + t.Seconds : t.Seconds.ToString();
        }
        else
        {
            result += "00";
        }
        return result;
    }

    public static string FormatTimeWithNoDay(long time)
    {
        if (time < 0)
        {
            return "0";
        }
        TimeSpan t = TimeSpan.FromSeconds(time);
        string result = "";
        long hours = 0L;

        if (t.Hours > 0)
        {
            hours = t.Hours + hours;
            result += (hours < 10 ? "0" + hours : hours.ToString()) + ":";
        }
        else
        {
            result += "00:";
        }
        if (t.Minutes > 0)
        {
            result += (t.Minutes < 10 ? "0" + t.Minutes : t.Minutes.ToString()) + ":";
        }
        else
        {
            result += "00:";
        }
        if (t.Seconds > 0)
        {
            result += t.Seconds < 10 ? "0" + t.Seconds : t.Seconds.ToString();
        }
        else
        {
            result += "00";
        }
        return result;
    }
    public static bool isMaintance(int time)
    {
        return UnixTimeStamp() > time ? false : true;
    }
    private static int UnixTimeStamp()
    {
        return (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1, 0, 0, 0))).TotalSeconds;
    }

    public static bool HaveIntersectTime(int startTimeA, int endTimeA, int startTimeB, int endTimeB)
    {
        return (startTimeA >= startTimeB && startTimeA < endTimeB) || (endTimeA > startTimeB && endTimeA <= endTimeB) || (startTimeA <= startTimeB && endTimeA >= endTimeB);
    }

    /// <summary>
    /// 秒转天数（向上取整）
    /// </summary>
    /// <param name="upgradeTime"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    internal static int GetDayNum_Up(int upgradeTime)
    {
        return (upgradeTime % 86400 > 0 ? 1 : 0) + upgradeTime / 86400;
    }

    /// <summary>
    /// 时间戳转DateTime
    /// </summary>
    /// <param name="timestamp"></param>
    /// <returns></returns>
    public static DateTime GetDateTimeSeconds(long timestamp)
    {
        //long begtime = timestamp * 10000;
        //DateTime dt_1970 = new DateTime(1970, 1, 1, 0, 0, 0);
        //long tricks_1970 = dt_1970.Ticks;//1970年1月1日刻度
        //long time_tricks = tricks_1970 + begtime;//日志日期刻度 
        //DateTime dt = new DateTime(time_tricks);//转化为DateTime
        //return dt;
        //DateTime dt = TimeZoneInfo.ConvertTimeToUtc(new DateTime(1970, 1, 1));
        //long lTime =  long.Parse(timestamp + "0000");
        //TimeSpan toNow = new TimeSpan(lTime);
        //return dt.AddAndGets(toNow);

        return ConvertTimeStampToDateTime(timestamp);

    }

    public static TimeSpan DiffTimeSpan(DateTime startTime, DateTime endTime)
    {
        TimeSpan daysSpan = new TimeSpan(endTime.Ticks - startTime.Ticks);
        return daysSpan;
    }
    /// <summary>
    /// 时间差 
    /// </summary>
    /// <param name="startTime"></param>
    /// <param name="endTime"></param>
    /// <returns>相差的天数</returns>
    public static int DiffDays(DateTime startTime, DateTime endTime)
    {
        TimeSpan daysSpan = new TimeSpan(endTime.Ticks - startTime.Ticks);
        return daysSpan.Days;
    }

    public static DateTime ToDateTime(this int seconds)
    {
        var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        return epoch.AddSeconds(seconds);
    }

    public static int ToUnixTime(this DateTime date)
    {
        var time = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        return Convert.ToInt32((date - time).TotalSeconds);
    }

    public static long ToUnixTimeMill(this DateTime date)
    {
        var time = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        return Convert.ToInt64((date - time).TotalMilliseconds);
    }

    public static bool IsSameDay(long timestamp1, long timestamp2)
    {
        DateTime dateTime1 = DateTimeOffset.FromUnixTimeMilliseconds(timestamp1).DateTime;
        DateTime dateTime2 = DateTimeOffset.FromUnixTimeMilliseconds(timestamp2).DateTime;

        return dateTime1.Year == dateTime2.Year &&
               dateTime1.Month == dateTime2.Month &&
               dateTime1.Day == dateTime2.Day;
    }

    //返回星期几，返回值1-7
    public static int getWeekday()
    {
        long timestamp = TimeUtils.GetCurrentTimeMilliseconds();
        return getWeekday(timestamp);
    }

    public static int getWeekday(long timestamp)
    {
        DateTime dateTime = DateTimeOffset.FromUnixTimeMilliseconds(timestamp).DateTime;
        int index = (int)dateTime.DayOfWeek;
        return index == 0 ? 7 : index;
    }

    //判断一个日前相差几天
    public static int getDayNum(long timestamp1)
    {
        long timestamp = TimeUtils.GetCurrentTimeMilliseconds();
        //(timestamp - timestamp1)
        DateTime dt1 = DateTimeOffset.FromUnixTimeMilliseconds(timestamp1).DateTime;
        DateTime dt2 = DateTimeOffset.FromUnixTimeMilliseconds(timestamp).DateTime;

        TimeSpan span = dt2.Subtract(dt1);
        int days = span.Days;
        return days;
    }

    public static long GetNextMondayTime()
    {
        int num = 7 - TimeUtils.getWeekday() + 1;
        return TimeUtils.GetCurrentZeroTime() + num * 24 * 60 * 60 * 1000;
    }
    //判断两个时间戳还剩下几天
    public static int CalculateRemainingDays(long timestamp1, long timestamp2)
    {
        // 将毫秒时间戳转换为DateTime对象
        DateTime dateTime1 = DateTimeOffset.FromUnixTimeMilliseconds(timestamp1).DateTime;
        DateTime dateTime2 = DateTimeOffset.FromUnixTimeMilliseconds(timestamp2).DateTime;

        return DiffDays(dateTime1, dateTime2);
    }
    
    /// <summary>
    /// 秒转换为时间，只返回一部分时间
    /// </summary>
    /// <param name="seconds"></param>
    /// <returns></returns>
    public static string ReturnMaxUnitStr(long seconds)
    {
        if (seconds <= 0)
        {
            return "0秒前";
        }
        var remainSeconds = seconds;
        var day = 0L;
        if(remainSeconds > 86400)
        {
            day = remainSeconds / 86400;
            remainSeconds %= 86400;
        }
        var hours = remainSeconds / 3600;
        remainSeconds %= 3600;
        var minutes = remainSeconds / 60;
        remainSeconds %= 60;
        var secs = remainSeconds;
        //var formatTime = "";
        if (day > 0)
        {
            return $"{day}天前";
        }
        if (hours > 0)
        {
            return $"{hours}小时前";
        }
        if (minutes > 0)
        {
            return $"{minutes}分钟前";
        }
        return $"{remainSeconds}秒前";
    }

}
