using editor.cfg.common;
using FrankGame;
using FrankGame.Battle;
using System.Collections.Generic;
using UnityEngine;

public partial class MainPanel : UIPanel
{
    private int selectArenaLevel;//当前选择的竞技场
    private int maxShowArenaLevel;

    protected override void OnRegisterListener()
    {
        base.OnRegisterListener();

        AddButtonClick(this.m_Button_GM, OnGMClick);
        AddButtonClick(this.m_Button_Setting, OnClickSetting);
        AddButtonClick(this.m_Button_Match, OnClickMatch);
        AddButtonClick(this.m_Button_Head, OnClickHead);

        AddEventListener(EventConst.Match_Success, OnMatchSuccess);
        AddEventListener(EventConst.User_ScoreRefresh, OnRefreshScore);
        AddEventListener(EventConst.User_ChangeHeadSuccess, OnRefreshHead);
        AddEventListener(EventConst.User_ChangeNameSuccess, OnRefreshUserName);
        AddEventListener(EventConst.Item_Refresh, OnRefreshItem);
        AddEventListener(EventConst.Currency_Refresh, OnRefreshICurrency);

        GameConfig.AddDebugCallback(OnShowGM);
    }

    protected override void OnCreate()
    {
        base.OnCreate();

        this.m_Item_CommonCurrencyItem_Gold.Show(ECurrencyType.Gold);
    }

    protected override void OnShow()
    {
        base.OnShow();

    }

    

    private void RefreshUserInfo()
    {
        // this.m_Item_UserInfoItem_Self.ShowView(UserModule.Instance.GetMyUserInfo());
    }

    

    private void OnRefreshScore(params object[] _params)
    {
        RefreshUserInfo();
    }

    private void OnRefreshHead(params object[] _params)
    {
        RefreshUserInfo();
    }

    private void OnRefreshUserName(params object[] _params)
    {
        RefreshUserInfo();
    }

    private void OnRefreshItem(params object[] _params)
    {
        //int itemId = (int)_params[0];
        //if(ConfigSystem.CurrentData.GetItem(itemId).itemType == editor.cfg.item.EItemType.CardFragment)
        //{
        //    SetActive(this.m_GameObject_CardRedPoint, CardModule.Instance.IsHasCardUpgrade());
        //}
    }

    private void OnRefreshICurrency(params object[] _params)
    {
        //ECurrencyType currencyType = (ECurrencyType)_params[0];
        //if (currencyType == ECurrencyType.Gold)
        //{
        //    SetActive(this.m_GameObject_CardRedPoint, CardModule.Instance.IsHasCardUpgrade());
        //}
    }

    private void OnMatchSuccess(params object[] _params)
    {
        this.Close();
    }

    private void OnGMClick()
    {
        UISystem.Instance.Open(UIConfig.GMPanel);
    }

    private void OnClickSetting()
    {
        UISystem.Instance.Open(UIConfig.SettingPanel);
    }

    private void OnClickMatch()
    {

        this.Close();
    }

    private void OnClickHead()
    {
        UISystem.Instance.Open(UIConfig.UserPanel);
    }

    private void OnShowGM(bool _active)
    {
        SetActive(this.m_Button_GM, _active);
    }

    protected override void OnClose()
    {
        base.OnClose();

        GameConfig.RemoveDebugCallback(OnShowGM);
    }
}
