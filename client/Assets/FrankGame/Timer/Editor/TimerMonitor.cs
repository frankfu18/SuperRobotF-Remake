//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Diagnostics;
//using UnityEditor;
//using UnityEngine;

//namespace FrankGame
//{
//    public class TimerMonitor : EditorWindow
//    {
//        private class TimerLog
//        {
//            public float time;
//            public string message;
//            public string stackTrace;
//        }

//        public static TimerMonitor Instance => instance;

//        private static TimerMonitor instance;

//        private List<TimerLog> logList = new List<TimerLog>();
//        private Vector2 scrollPos;
//        private string curLogStackTrace;
//        private Vector2 m_StackScrollPosition;

//        [MenuItem("FrankGame/Monitor/Timer")]
//        public static void CreateWin()
//        {
//            if (instance == null)
//            {
//                instance = CreateWindow<TimerMonitor>("计时器监视器");
//            }

//            instance.Show();
//        }

//        public void AddTimer(int index, int timeId)
//        {
//            StackTrace stackTrace = new StackTrace(true);
//            string stackTraceString = stackTrace.ToString();

//            TimerLog timeLog = new TimerLog();
//            timeLog.time = Time.time;
//            timeLog.message = $"[{Time.time}]添加计时器 id:{timeId},index:{index}";
//            timeLog.stackTrace = stackTraceString;

//            logList.Add(timeLog);
//        }

//        public void RemoveTimer(int index, int timeId)
//        {
//            StackTrace stackTrace = new StackTrace(true);
//            string stackTraceString = stackTrace.ToString();

//            TimerLog timeLog = new TimerLog();
//            timeLog.time = Time.time;
//            timeLog.message = $"[{Time.time}]移除计时器 id:{timeId},index:{index}";
//            timeLog.stackTrace = stackTraceString;

//            logList.Add(timeLog);
//        }

//        public void RecycleTimer(int index, int timeId)
//        {
//            StackTrace stackTrace = new StackTrace(true);
//            string stackTraceString = stackTrace.ToString();

//            TimerLog timeLog = new TimerLog();
//            timeLog.time = Time.time;
//            timeLog.message = $"[{Time.time}]Recycle 计时器 id:{timeId},index:{index}";
//            timeLog.stackTrace = stackTraceString;

//            logList.Add(timeLog);
//        }

//        public void GetFreeTimer(int index, int timeId)
//        {
//            StackTrace stackTrace = new StackTrace(true);
//            string stackTraceString = stackTrace.ToString();

//            TimerLog timeLog = new TimerLog();
//            timeLog.time = Time.time;
//            timeLog.message = $"[{Time.time}]Get Free 计时器 id:{timeId},index:{index}";
//            timeLog.stackTrace = stackTraceString;

//            logList.Add(timeLog);
//        }

//        public void GetNewTimer(int index, int timeId)
//        {
//            StackTrace stackTrace = new StackTrace(true);
//            string stackTraceString = stackTrace.ToString();

//            TimerLog timeLog = new TimerLog();
//            timeLog.time = Time.time;
//            timeLog.message = $"[{Time.time}]New 计时器 id:{timeId},index:{index}";
//            timeLog.stackTrace = stackTraceString;

//            logList.Add(timeLog);
//        }

//        public void TimerDone(int index, int timeId)
//        {
//            StackTrace stackTrace = new StackTrace(true);
//            string stackTraceString = stackTrace.ToString();

//            TimerLog timeLog = new TimerLog();
//            timeLog.time = Time.time;
//            timeLog.message = $"[{Time.time}]结束计时器 id:{timeId},index:{index}";
//            timeLog.stackTrace = stackTraceString;

//            logList.Add(timeLog);
//        }

//        public void TimerRuning(int index, int timeId)
//        {
//            StackTrace stackTrace = new StackTrace(true);
//            string stackTraceString = stackTrace.ToString();

//            TimerLog timeLog = new TimerLog();
//            timeLog.time = Time.time;
//            timeLog.message = $"[{Time.time}]执行计时器 id:{timeId},index:{index}";
//            timeLog.stackTrace = stackTraceString;

//            logList.Add(timeLog);
//        }

//        public void OnGUI()
//        {

//            EditorGUILayout.LabelField("所有计时器：");
//            for (int i = 0; i < TimerManager.Instance.totalTimers.Count; i++)
//            {
//                DrawTimer(i, TimerManager.Instance.totalTimers[i]);
//            }
//            EditorGUILayout.Space();

//            EditorGUILayout.LabelField("ID索引对照：");
//            foreach (var kv in TimerManager.Instance.timeIdToIndexDic)
//            {
//                EditorGUILayout.LabelField($"ID:{kv.Key} Index:{kv.Value}");
//            }
//            EditorGUILayout.Space();

//            EditorGUILayout.LabelField("正在使用的计时器索引：");
//            foreach (var timerIndex in TimerManager.Instance.usingTimerIndexList)
//            {
//                EditorGUILayout.LabelField("计时器索引：" + timerIndex);
//            }
//            EditorGUILayout.Space();

//            EditorGUILayout.LabelField("空闲的计时器索引：");
//            foreach (var timerIndex in TimerManager.Instance.freeTimerIndexQueue)
//            {
//                EditorGUILayout.LabelField("计时器索引：" + timerIndex);
//            }
//            EditorGUILayout.Space();

//            EditorGUILayout.LabelField("日志：");
//            scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Height(200f));
//            foreach (var log in logList)
//            {
//                EditorGUILayout.BeginHorizontal();
//                if (GUILayout.Button("选中"))
//                {
//                    curLogStackTrace = log.stackTrace;
//                }
//                EditorGUILayout.LabelField(log.message);
//                EditorGUILayout.EndHorizontal();
//            }
//            EditorGUILayout.EndScrollView();
//            EditorGUILayout.Space();

//            m_StackScrollPosition = EditorGUILayout.BeginScrollView(m_StackScrollPosition, GUILayout.Height(200f));
//            if (GUILayout.Button(curLogStackTrace, "label"))
//            {

//            }
//            EditorGUILayout.EndScrollView();
//        }

//        private void DrawTimer(int index, Timer timer)
//        {
//            EditorGUILayout.LabelField($"[{timer.TimerId}] index:{index},dirty:{timer.dirty},duration:{timer.duration}");
//        }


//    }

//}