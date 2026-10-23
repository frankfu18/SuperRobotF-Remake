using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace FrankGame
{
    public static class LogCollector
    {
        private static string Log_File_Path = Application.persistentDataPath + "/Logs/log_{0}";
        private static string Flag_Format = "[{0}][{1}]{2}";

        private static string RealPath;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Init()
        {
#if UNITY_EDITOR || UNITY_ANDROID
            Application.logMessageReceived += OnLogMessageReceived;

            RealPath = System.DateTime.Now.Month < 10 ? ("0" + System.DateTime.Now.Month) : System.DateTime.Now.Month.ToString();
            RealPath += System.DateTime.Now.Day < 10 ? ("0" + System.DateTime.Now.Day) : System.DateTime.Now.Day.ToString();

            RealPath = string.Format(Log_File_Path, RealPath);

            string dirPath = Path.GetDirectoryName(RealPath);
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            if (!File.Exists(RealPath))
            {
                File.Create(RealPath);
            }
#endif
        }

        private static void OnLogMessageReceived(string logMessage, string stackTrace, LogType logType)
        {
            if (logType == LogType.Assert)
            {
                logType = LogType.Error;
            }

            WriteLog(logMessage, stackTrace, logType.ToString());
        }

        private static void WriteLog(string logMessage, string stackTrace, string logType)
        {
            if (!LogConfig.Instance.WriteLocal)
            {
                return;
            }

            using (StreamWriter writer = new StreamWriter(RealPath, true))
            {
                writer.WriteLine(string.Format(Flag_Format, System.DateTime.Now.ToString(), logType, logMessage));
                writer.WriteLine(stackTrace);

                writer.Dispose();
            }
        }
    }
}