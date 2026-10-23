using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrankGame
{
    [CreateAssetMenu(menuName = "FrankGame/ScriptableObject/PathConfig", fileName = "PathConfig")]
    public class PathConfig : SingleScriptableObject<PathConfig>
    {
        [Header("本地资源路径")]
        public string LocalResRoot = "Assets/Res/";

        [Header("资源包后缀")]
        public string AssetBundleSuffix = ".ab";

        [Header("头文件名字")]
        public string HeadFileName = "resIndex.h";

        [Header("头文件后缀")]
        public string HeadFileSuffix = ".h";

        public static string GetPlatformDefines()
        {
            string s = "";
#if UNITY_STANDALONE_LINUX //directive for compiling/executing code specifically for Linux standalone applications.
                s = "UNITY_STANDALONE_LINUX";
#endif
#if UNITY_STANDALONE //directive for compiling/executing code for any standalone platform (Mac OS X, Windows or Linux).
                s = "UNITY_STANDALONE";
#endif
#if UNITY_WII //directive for compiling/executing code for the Wii console.
                s = "UNITY_WII";
#endif
#if UNITY_IOS //directive for compiling/executing code for the iOS platform.
                s = "UNITY_IOS";
#endif
#if UNITY_PS4 //directive for running PlayStation 4 code
                s = UNITY_PS4"";
#endif
#if UNITY_XBOXONE //directive for executing Xbox One code.
                s = "UNITY_XBOXONE";
#endif
#if UNITY_LUMIN //define directive for the Magic Leap OS platform. You can also use PLATFORM_LUMIN.
                s = "UNITY_LUMIN";
#endif
#if UNITY_TIZEN //directive for the Tizen platform.
                s = "UNITY_TIZEN";
#endif
#if UNITY_TVOS //directive for the Apple TV platform.
                s = "UNITY_TVOS";
#endif
#if UNITY_WSA //directive for Universal Windows Platform. Additionally, NETFX_CORE is defined when compiling C# files against .NET Core and using .NET scripting backend.
                s = "UNITY_WSA";
#endif
#if UNITY_WSA_10_0 //directive for Universal Windows Platform. Additionally WINDOWS_UWP is defined when compiling C# files against .NET Core.
                s = "UNITY_WSA_10_0";
#endif
#if UNITY_WINRT //UNITY_WSA
                s = "UNITY_WINRT";
#endif
#if UNITY_WINRT_10_0 //Equivalent to UNITY_WSA_10_0
                s = "UNITY_WINRT_10_0";
#endif
#if UNITY_WEBGL //directive for WebGL
            s = "UNITY_WEBGL";
#endif
#if UNITY_FACEBOOK //directive for the Facebook platform (WebGL or Windows standalone).
                s = "UNITY_FACEBOOK";
#endif
#if UNITY_ANALYTICS //directive for calling Unity Analytics methods from your game code. Version 5.2 and above
                s = "UNITY_ANALYTICS";
#endif
#if UNITY_XBOX360//XBOX360平台 
                s = "UNITY_XBOX360";
#endif
#if UNITY_IPHONE//IPHONE平台
                s = "UNITY_IPHONE";
#endif
#if UNITY_ANDROID//ANDROID平台
                s = "UNITY_ANDROID";
#endif
#if UNITY_STANDALONE_OSX//OSX平台
                s = "UNITY_STANDALONE_OSX";
#endif
#if UNITY_STANDALONE_WIN//Windows平台
                s = "UNITY_STANDALONE_WIN";
#endif

            return s;


        }
    }
}