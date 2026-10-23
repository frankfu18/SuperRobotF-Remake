using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace FrankGame
{
    /// <summary>
    /// 宏定义 ENABLE_LOG 开启打印log
    /// </summary>
    public static class Log
    {
        public const string ENABLE_LOG = "ENABLE_LOG";
        public const string ENABLE_ERROR_LOG = "ENABLE_ERROR_LOG";

        [Conditional(ENABLE_LOG)]
        public static void Debug(string _format, params object[] _params)
        {
            Debug(ELogType.Default, _format, _params);
        }

        [Conditional(ENABLE_LOG)]
        public static void Warning(string _format, params object[] _params)
        {
            Warning(ELogType.Default, _format, _params);
        }

        [Conditional(ENABLE_LOG)]
        [Conditional(ENABLE_ERROR_LOG)]
        public static void Error(string _format, params object[] _params)
        {
            Error(ELogType.Default, _format, _params);
        }

        [Conditional(ENABLE_LOG)]
        public static void Debug(ELogType _logType, string _format, params object[] _params)
        {
            if (!LogConfig.Instance.Enable)
            {
                return;
            }

            if (ELogLevel.Debug != (LogConfig.Instance.LogLevel & ELogLevel.Debug))
            {
                return;
            }

            if (_logType != (LogConfig.Instance.LogType & _logType))
            {
                return;
            }

            UnityEngine.Debug.Log(string.Format("[{0}] {1}", _logType, _format, _params));
        }

        [Conditional(ENABLE_LOG)]
        public static void Warning(ELogType _logType, string _format, params object[] _params)
        {
            if (!LogConfig.Instance.Enable)
            {
                return;
            }

            if (ELogLevel.Warning != (LogConfig.Instance.LogLevel & ELogLevel.Warning))
            {
                return;
            }

            if (_logType != (LogConfig.Instance.LogType & _logType))
            {
                return;
            }

            UnityEngine.Debug.LogWarning(string.Format("[{0}] {1}", _logType, _format, _params));
        }

        [Conditional(ENABLE_LOG)]
        [Conditional(ENABLE_ERROR_LOG)]
        public static void Error(ELogType _logType, string _format, params object[] _params)
        {
            if (!LogConfig.Instance.Enable)
            {
                return;
            }

            if (ELogLevel.Error != (LogConfig.Instance.LogLevel & ELogLevel.Error))
            {
                return;
            }

            if (_logType != (LogConfig.Instance.LogType & _logType))
            {
                return;
            }

            UnityEngine.Debug.LogError(string.Format("[{0}] {1}", _logType, _format, _params));
        }
    }
}