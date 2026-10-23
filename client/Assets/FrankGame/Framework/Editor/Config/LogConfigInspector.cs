using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace FrankGame
{
    [CustomEditor(typeof(LogConfig))]
    public class LogConfigInspector : Editor
    {
        private LogConfig logConfig;
        private bool openLog;

        protected void OnEnable()
        {
            logConfig = (LogConfig)target;
            openLog = logConfig.Enable;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (openLog != logConfig.Enable)
            {
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

                if (logConfig.Enable)
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

                openLog = logConfig.Enable;
            }
        }
    }


}