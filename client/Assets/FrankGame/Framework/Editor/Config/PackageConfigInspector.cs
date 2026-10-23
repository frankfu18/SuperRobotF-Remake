using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace FrankGame
{
    [CustomEditor(typeof(PackageConfig))]
    public class PackageConfigInspector : Editor
    {
        private PackageConfig packageConfig;
        private EChannelType curChannelType;

        protected void OnEnable()
        {
            packageConfig = (PackageConfig)target;
            curChannelType = packageConfig.ChannelType;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (curChannelType != packageConfig.ChannelType)
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

                string curSymbol = GameConfig.Package.GetChannelDefineSymbol(curChannelType);
                if (!string.IsNullOrEmpty(curSymbol))
                {
                    for (int i = 0; i < oldDefineSymbols.Length; i++)
                    {
                        if (oldDefineSymbols[i] == curSymbol)
                        {
                            oldDefineSymbols[i] = null;
                        }
                    }
                }

                string newSymbol = GameConfig.Package.GetChannelDefineSymbol(packageConfig.ChannelType);

                List<string> newDefineSymbolList = new List<string>();
                if (!string.IsNullOrEmpty(newSymbol))
                {
                    newDefineSymbolList.Add(newSymbol);
                }

                foreach (var symbol in oldDefineSymbols)
                {
                    if (!string.IsNullOrEmpty(symbol))
                    {
                        newDefineSymbolList.Add(symbol);
                    }
                }

                PlayerSettings.SetScriptingDefineSymbols(buildTarget, newDefineSymbolList.ToArray());

                curChannelType = packageConfig.ChannelType;
            }
        }
    }


}