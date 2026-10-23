using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 没有网络
/// </summary>
public class CheckNetWorkPanelUtil
{
    
    private static readonly List<string> TipDesc1List = new() 
    {
        "网络连接失败", 
        "NETWORK CONNECTION FAILED", 
        "網路連線失敗", 
        "ネットワーク接続に失敗しました", 
        "네트워크 연결에 실패했습니다." 
    };

    private static readonly List<string> TipDesc2List = new()
    {
        "请检查您的连接并重试。", 
        "Please check your connection and retry.", 
        "請檢查您的連線並重試。", 
        "接続を確認して再試行してください。",
        "연결을 확인하고 다시 시도해 주세요."
    };

    private static readonly List<string> TipDesc3List = new()
    {
        "再试一次", 
        "TRY AGAIN", 
        "再試一次", 
        "もう一度", 
        "다시 시도"
    };

    public static void Open()
    {
        var curLanguage = LanguageSystem.Instance.CurLanguage;
        var languageIndex = curLanguage switch
        {
            SystemLanguage.Korean => 4,
            SystemLanguage.Japanese => 3,
            SystemLanguage.ChineseTraditional => 2,
            SystemLanguage.Chinese => 0,
            _ => 1
        };
        var title = TipDesc1List[languageIndex];   
        var desc = TipDesc2List[languageIndex];   
        var buttonText = TipDesc3List[languageIndex];
        
        UISystem.Instance.Open(UIConfig.CommonPopupPanel, new stCommonPopupPanelData
        {
            title = title,
            content = desc,
            confirmBtnName = buttonText,
        });
    }
    
    private static void ClickTryAgain()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            Open();
        }
    }


}