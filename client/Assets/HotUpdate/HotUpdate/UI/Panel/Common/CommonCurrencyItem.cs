using editor.cfg.common;
using FrankGame;
using UnityEngine;

public partial class CommonCurrencyItem : UIItem
{
	private ECurrencyType currencyType;
    private int strengthTimerId;

    protected override void OnRegisterListener()
    {
        base.OnRegisterListener();

        AddEventListener(EventConst.Currency_Refresh, OnRefrehCurrency);

        AddButtonClick(this.m_Button_Add, OnClickAdd);
    }

    protected override void OnShow()
    {
        base.OnShow();

        this.currencyType = (ECurrencyType)this.data;

        SetSpriteById(this.m_Image_Icon, ConfigSystem.CurrentData.GetCurrency(this.currencyType).icon, true);

        RefreshView();
    }

    private void OnRefrehCurrency(params object[] _params)
    {
        ECurrencyType currency = (ECurrencyType)_params[0];
        if (currency == currencyType)
        {
            RefreshView();
        }
    }

    private void RefreshView()
    {
        if (strengthTimerId > 0)
        {
            RemoveTimer(strengthTimerId);
            strengthTimerId = 0;
        }

        SetActive(this.m_TMP_Text_Time, false);

        if (this.currencyType == ECurrencyType.Gold)
        {
            SetText(this.m_TMP_Text_Count, StringUtils.FormatFromNumberAbbreviation(CurrencyModule.Instance.GetCurrencyCount(this.currencyType)));
        }
        else
        {
            SetText(this.m_TMP_Text_Count, CurrencyModule.Instance.GetCurrencyCount(this.currencyType));
        }
    }

    private void OnClickAdd()
    {
        GameConfig.Debug.SetLocalDebug(!GameConfig.Debug.IsDebugEnable());
    }
}
