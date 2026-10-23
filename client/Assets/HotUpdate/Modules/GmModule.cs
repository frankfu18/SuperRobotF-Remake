using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using editor.cfg.common;
using editor.cfg.item;
using db.parser;

public class GmModule : ModuleBase<GmModule>
{
    public void AddCurrency(ECurrencyType _currencyType, long _addCount)
    {
        CurrencyModule.Instance.AddCurrency(_currencyType, _addCount);
    }

    public void AddItem(int _itemId, int _amount)
    {
        var itemConfig = ConfigSystem.CurrentData.GetItem(_itemId);
        if (itemConfig == null)
        {
            LogUtil.Error("物品不存在：" + _itemId);
            return;
        }

        ItemModule.Instance.AddItem(_itemId, _amount, EReasonType.GM);
    }

    public void AddScore(int _addScore)
    {
        //UserModule.Instance.Send_AddScore(_addScore, out int real);
    }

    public void SetScore(int _score)
    {
        UserDataParser.Ins.Score = Mathf.Max(0, _score);

        SendEvent(EventConst.User_ScoreRefresh);
    }
}
