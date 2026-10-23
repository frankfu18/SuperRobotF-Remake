using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace FrankGame
{
    public enum ELoadMode
    {
        Local,
        AssetBundle,
        Net,
        MiniGame,
    }

    public enum EChannelType
    {
        Android,//安卓
        WX_Game,//微信小游戏
        Byte_Game,//抖音小游戏
    }

    public enum EAssemblyLoadMode
    {
        Editor,//编辑器加载
        Local,//本地加载
    }

    [CreateAssetMenu(menuName = "FrankGame/ScriptableObject/PackageConfig", fileName = "PackageConfig")]
    public class PackageConfig : SingleScriptableObject<PackageConfig>
    {
        [Header("渠道")]
        public EChannelType ChannelType = EChannelType.Android;

        [Header("资源加载方式")]
        public ELoadMode ResLoadMode = ELoadMode.Local;

        [Header("资源加密偏移")]
        public byte AssetBundleOffset;

        public string GetChannelDefineSymbol(EChannelType _channelType)
        {
            if (_channelType == EChannelType.WX_Game || _channelType == EChannelType.Byte_Game)
            {
                return _channelType.ToString().ToUpper();
            }

            return null;
        }

#if UNITY_EDITOR
        public void Apply(EChannelType _targetChannel)
        {
            if (ChannelType == _targetChannel)
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

            string curSymbol = GameConfig.Package.GetChannelDefineSymbol(ChannelType);
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

            string newSymbol = GameConfig.Package.GetChannelDefineSymbol(_targetChannel);

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

            ChannelType = _targetChannel;
        }
#endif
    }
}