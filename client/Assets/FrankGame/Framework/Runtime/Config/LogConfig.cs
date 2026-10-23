using UnityEngine;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace FrankGame
{
    [CreateAssetMenu(menuName = "FrankGame/ScriptableObject/LogConfig", fileName = "LogConfig")]
    public class LogConfig : SingleScriptableObject<LogConfig>
    {
        [Header("日志开关")] 
        public bool Enable = true;
        [Header("日志写入本地")] 
        public bool WriteLocal = true;
        [Header("日志显示级别")] 
        public ELogLevel LogLevel = ELogLevel.Debug | ELogLevel.Warning | ELogLevel.Error;
        [Header("日志显示类别")] 
        public ELogType LogType = ELogType.Default;

#if UNITY_EDITOR
        public void Apply(bool _enable)
        {
            if (Enable == _enable)
            {
                return;
            }

            UnityEditor.Build.NamedBuildTarget buildTarget;
            switch (PathConfig.GetPlatformDefines())
            {
                case "UNITY_WEBGL":
                    buildTarget = UnityEditor.Build.NamedBuildTarget.WebGL;
                    break;
                default:
                    buildTarget = UnityEditor.Build.NamedBuildTarget.Android;
                    break;
            }

            string[] oldDefineSymbols;
            PlayerSettings.GetScriptingDefineSymbols(buildTarget, out oldDefineSymbols);
            List<string> newDefineSymbolList = new List<string>();

            if (_enable)
            {
                bool findSymbol = false;
                foreach (var symbol in oldDefineSymbols)
                {
                    if (symbol == Log.ENABLE_LOG)
                    {
                        findSymbol = true;
                        break;
                    }
                }

                newDefineSymbolList.AddRange(oldDefineSymbols);
                if (!findSymbol)
                {
                    newDefineSymbolList.Add(Log.ENABLE_LOG);
                }
            }
            else
            {
                for (int i = 0; i < oldDefineSymbols.Length; i++)
                {
                    if (oldDefineSymbols[i] == Log.ENABLE_LOG)
                    {
                        oldDefineSymbols[i] = null;
                        break;
                    }
                }

                foreach (var symbol in oldDefineSymbols)
                {
                    if (!string.IsNullOrEmpty(symbol))
                    {
                        newDefineSymbolList.Add(symbol);
                    }
                }
            }

            PlayerSettings.SetScriptingDefineSymbols(buildTarget, newDefineSymbolList.ToArray());

            Enable = _enable;
        }
#endif
    }
}