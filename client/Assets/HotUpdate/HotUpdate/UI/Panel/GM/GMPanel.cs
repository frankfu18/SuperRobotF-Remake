using db.parser;
using editor.cfg.bonus;
using System.Collections.Generic;
using UnityEngine;

public partial class GMPanel : UIPanel
{
    protected override void OnRegisterListener()
    {
        base.OnRegisterListener();

        AddButtonClick(this.m_Button_Currency, OnClickCurrency);
        AddButtonClick(this.m_Button_Item, OnClickItem);
        AddButtonClick(this.m_Button_AllItem, OnClickAllItem);
        AddButtonClick(this.m_Button_AddGold, OnClickAddGold);
        AddButtonClick(this.m_Button_AddDiamond, OnClickAddDiamond);
        AddButtonClick(this.m_Button_DBClear, OnClickDBClear);
        AddButtonClick(this.m_Button_AddScore1, OnClickAddScore1);
        AddButtonClick(this.m_Button_AddScore10, OnClickAddScore10);
        AddButtonClick(this.m_Button_SetScore, OnClickSetScore);
    }


    private void OnClickCurrency()
    {
        if(int.TryParse(this.m_TMP_InputField_CurrencyType.text, out int currencyType))
        {
            if (int.TryParse(this.m_TMP_InputField_CurrencyAmount.text, out int currencyAmount))
            {
                GmModule.Instance.AddCurrency((editor.cfg.common.ECurrencyType)currencyType, currencyAmount);
            }
        }
    }

    private void OnClickItem()
    {
        if (int.TryParse(this.m_TMP_InputField_ItemId.text, out int itemId))
        {
            if (int.TryParse(this.m_TMP_InputField_ItemAmount.text, out int itemAmount))
            {
                GmModule.Instance.AddItem(itemId, itemAmount);
            }
        }
    }
    private void OnClickAllItem()
    {
        var itemCfg = ConfigSystem.CurrentData.GetItemList();
        foreach (var item in itemCfg)
        {
            GmModule.Instance.AddItem(item.id, 1);
        }
    }

    private void OnClickAddGold()
    {
        GmModule.Instance.AddCurrency(editor.cfg.common.ECurrencyType.Gold, 100000);
    }

    private void OnClickAddDiamond()
    {
        GmModule.Instance.AddCurrency(editor.cfg.common.ECurrencyType.Diamond, 1000);
    }

    private void OnClickAddScore1()
    {
        GmModule.Instance.AddScore(1);
    }

    private void OnClickAddScore10()
    {
        GmModule.Instance.AddScore(10);
    }

    private void OnClickSetScore()
    {
        if (int.TryParse(this.m_TMP_InputField_Score.text, out int score))
        {
            GmModule.Instance.SetScore(score);
        }
    }

    private void OnClickDBClear()
    {
    }
}
