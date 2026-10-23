using System.Collections;
using System.Collections.Generic;

public static class ErrorHandler
{
    public static bool SendError(int _errorCode)
    {
        if(_errorCode == EErrorCode.OK)
        {
            return true;
        }

        var errorCfg = ConfigSystem.CurrentData.GetError("error_code_" + _errorCode);
        if (errorCfg == null)
        {
            FlyTextManager.Instance.AddText("错误码" + _errorCode);
        }
        else
        {
            SendError(errorCfg.desc);
        }

        return false;
    }

    public static bool SendError(string _errorCode, params object[] _params)
    {
        string text = LanguageSystem.Instance.GetLanguage(_errorCode, _params);
        if (text == null)
        {
            text = _errorCode;
        }

        FlyTextManager.Instance.AddText(text);

        return false;
    }
}
