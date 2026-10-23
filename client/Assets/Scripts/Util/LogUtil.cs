using UnityEngine;
using FrankGame;
using System.Diagnostics;

public static class LogUtil
{
    [Conditional(FrankGame.Log.ENABLE_LOG)]
    public static void Log(string _str, params object[] _params)
    {
        FrankGame.Log.Debug(_str, _params);
    }

    [Conditional(FrankGame.Log.ENABLE_LOG)]
    public static void Log(ELogType _logType, string _str, params object[] _params)
    {
        FrankGame.Log.Debug(_logType, _str, _params);
    }

    [Conditional(FrankGame.Log.ENABLE_LOG)]
    [Conditional(FrankGame.Log.ENABLE_ERROR_LOG)]
    public static void Error(string _str, params object[] _params)
    {
        FrankGame.Log.Error(_str, _params);
    }

    [Conditional(FrankGame.Log.ENABLE_LOG)]
    [Conditional(FrankGame.Log.ENABLE_ERROR_LOG)]
    public static void Error(ELogType _logType, string _str, params object[] _params)
    {
        FrankGame.Log.Error(_logType, _str, _params);
    }

    [Conditional(FrankGame.Log.ENABLE_LOG)]
    public static void Warn(string _str, params object[] _params)
    {
        FrankGame.Log.Warning(_str, _params);
    }

    


}