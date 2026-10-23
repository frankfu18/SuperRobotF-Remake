using System;

namespace FrankGame
{
    public static class TimeUtility
    {
        private static readonly long Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).Ticks;

        public static long NowMillisecond()
        {
            return (DateTime.UtcNow.Ticks - Epoch) / 10000;
        }

        public static long NowSecond()
        {
            return (DateTime.UtcNow.Ticks - Epoch) / 10000 / 1000;
        }

        public static long GetTimestamp(DateTime _dateTime)
        {
            return (_dateTime.Ticks - Epoch) / 10000;
        }

        public static DateTime GetLoaclDateTime(long _timestamp)
        {
            return GetUtcDateTime(_timestamp).ToLocalTime();
        }

        public static DateTime GetUtcDateTime(long _timestamp)
        {
            return new DateTime(Epoch + _timestamp * 10000, DateTimeKind.Utc);
        }

        public static long GetNextDayZeroHour()
        {
            return GetNextDayZeroHour(NowMillisecond());
        }

        /// <summary>
        /// 下一天0点
        /// </summary>
        /// <param name="_millSecTimestamp"></param>
        /// <returns></returns>
        public static long GetNextDayZeroHour(long _millSecTimestamp)
        {
            // 先解析为 UTC 时间
            DateTimeOffset dateTime = DateTimeOffset.FromUnixTimeMilliseconds(_millSecTimestamp);

            // 当天 UTC 0点
            DateTimeOffset utcMidnight = new DateTimeOffset(
                dateTime.Year, dateTime.Month, dateTime.Day,
                0, 0, 0, TimeSpan.Zero);

            // 下一天 UTC 0点 = 当天 0 点 + 1 天
            DateTimeOffset nextUtcMidnight = utcMidnight.AddDays(1);

            return nextUtcMidnight.ToUnixTimeMilliseconds();
        }

        public static long GetNowDayZeroHour()
        {
            return GetNowDayZeroHour(NowMillisecond());
        }

        public static long GetNowDayZeroHour(long _millSecTimestamp)
        {
            DateTimeOffset dateTime = DateTimeOffset.FromUnixTimeMilliseconds(_millSecTimestamp);
            DateTimeOffset utcMidnight = new DateTimeOffset(
                dateTime.Year, dateTime.Month, dateTime.Day,
                0, 0, 0, TimeSpan.Zero); // UTC 0点

            return utcMidnight.ToUnixTimeMilliseconds();
        }

        //获取下周一的0点时间戳
        public static long GetNextMondayZeroHour()
        {
            DateTime today = DateTime.Today;
            // 计算到下周一的间隔天数
            int daysUntilNextMonday = ((int)DayOfWeek.Monday - (int)today.DayOfWeek + 7) % 7;
            // 若当天是周一则加7天
            DateTime nextMonday = daysUntilNextMonday == 0 ? today.AddDays(7) : today.AddDays(daysUntilNextMonday);

            // 转换为UTC时间
            DateTime nextMondayUtc = nextMonday.ToUniversalTime();
            // 或计算毫秒级时间戳
            long timestampMilliseconds = new DateTimeOffset(nextMondayUtc).ToUnixTimeMilliseconds();

            return timestampMilliseconds;
        }

        public static long GetNextMondayZeroHour(long _nowTimestamp)
        {
            // 先获取当前时间戳所在周的周一 0 点
            long thisWeekStart = GetNowMondayZeroHour(_nowTimestamp);

            // 下一周 = 当前周一 + 7 天
            DateTimeOffset nextWeekStart = DateTimeOffset.FromUnixTimeMilliseconds(thisWeekStart).AddDays(7);

            return nextWeekStart.ToUnixTimeMilliseconds();
        }

        //获取当前周周一0点的时间戳
        public static long GetNowMondayZeroHour()
        {
            return GetNowMondayZeroHour(NowMillisecond());
        }

        //获取当前周周一0点的时间戳
        public static long GetNowMondayZeroHour(long _nowTimestamp)
        {
            // 把时间戳转换成 UTC 时间
            DateTimeOffset utcTime = DateTimeOffset.FromUnixTimeMilliseconds(_nowTimestamp);

            // 获取这一天是星期几（Monday = 1, Sunday = 0）
            int daysSinceMonday = (int)utcTime.DayOfWeek - 1;
            if (daysSinceMonday < 0) daysSinceMonday += 7; // Sunday 补正为 6

            // 得到本周周一的 0 点
            DateTimeOffset weekStartUtcMidnight = new DateTimeOffset(
                utcTime.Year, utcTime.Month, utcTime.Day,
                0, 0, 0, TimeSpan.Zero
            ).AddDays(-daysSinceMonday);

            return weekStartUtcMidnight.ToUnixTimeMilliseconds();
        }

        public static string GetNowTimeString()
        {
            return DateTime.UtcNow.ToString("HH:mm:ss");
        }
    }
}