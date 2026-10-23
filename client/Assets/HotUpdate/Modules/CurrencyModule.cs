using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using editor.cfg.common;
using db.parser;

public enum ECurrencyCostType
{
    Add, Use
}

public class CurrencyModule : ModuleBase<CurrencyModule>
{
    protected override void AddEventListeners()
    {
        base.AddEventListeners();
    }
    
    protected override void OnLoadConfigDone()
    {
        base.OnLoadConfigDone();
    }

    protected override void OnLoginDone()
    {
        base.OnLoginDone();

    }

    public long GetCurrencyCount(ECurrencyType _currencyType)
    {
        if (CurrencyDataParser.Ins.CurrencyDic.TryGetValue(_currencyType, out var amount))
        {
            return amount;
        }

        return 0;
    }

    public int AddCurrency(ECurrencyType _currencyType, long _addCount)
    {
        if (_addCount <= 0)
        {
            return EErrorCode.Currency_Amount_Error;
        }

        SetCurrencyCount(_currencyType, GetCurrencyCount(_currencyType) + _addCount);

        return EErrorCode.OK;
    }

    public void SetCurrencyCount(ECurrencyType _currencyType, long _currencyCount)
    {
        var beforeUpdateCurrencyCount = GetCurrencyCount(_currencyType);

        CurrencyDataParser.Ins.CurrencyDic[_currencyType] = _currencyCount;

        SendEvent(EventConst.Currency_Refresh, _currencyType, beforeUpdateCurrencyCount < _currencyCount ? ECurrencyCostType.Add : ECurrencyCostType.Use);
    }

    public bool CostCurrency(ECurrencyType _currencyType, long _costCount, EReasonType _costReason, bool _sendTips = false)
    {
        int errorCode = EErrorCode.OK;

        if (_costCount <= 0)
        {
            errorCode = EErrorCode.Currency_Amount_Error;
            if (_sendTips)
            {
                ErrorHandler.SendError(errorCode);
            }
            return false;
        }

        long curCount = GetCurrencyCount(_currencyType);
        if (curCount < _costCount)
        {
            errorCode = EErrorCode.Currency_Not_Enough;
            if (_sendTips)
            {
                ErrorHandler.SendError(errorCode);
            }
            return false;
        }

        SetCurrencyCount(_currencyType, curCount - _costCount);
        return true;
    }

    public bool Cost(Cost _cost, EReasonType _costReason, System.Action _onCostSuccess, bool _showTips = false)
    {
        bool result = false;
        switch (_cost.GetTypeId())
        {
            case None.TYPE_ID:
                result = true;

                _onCostSuccess?.Invoke();
                break;
            case AdCost.TYPE_ID:
                result = true;

                AdManager.Instance.PlayAd(_onCostSuccess);
                break;
            case CurrencyCost.TYPE_ID:
                {
                    CurrencyCost currency = (CurrencyCost)_cost;
                    result = CostCurrency(currency.currency, currency.amount, _costReason, _showTips);
                    if (result)
                    {
                        _onCostSuccess?.Invoke();
                    }
                }
                break;
            case ItemCost.TYPE_ID:
                {
                    ItemCost item = (ItemCost)_cost;
                    int errorCode = ItemModule.Instance.CostItem(item.itemId, item.itemCount, _costReason);
                    result = errorCode == EErrorCode.OK;
                    if (result)
                    {
                        _onCostSuccess?.Invoke();
                    }
                    else
                    {
                        if (_showTips)
                        {
                            ErrorHandler.SendError(errorCode);
                        }
                    }
                }
                break;
            case And.TYPE_ID:
                {
                    And and = (And)_cost;

                    result = IsEnough(_cost, _showTips);
                    if (result)
                    {
                        foreach (var cost in and.costs)
                        {
                            result = Cost(cost, _costReason, null, _showTips);
                            if (!result)
                            {
                                break;
                            }
                        }

                        _onCostSuccess?.Invoke();
                    }
                }
                break;
            case Or.TYPE_ID:
                {
                    Or or = (Or)_cost;
                    foreach (var cost in or.costs)
                    {
                        result = Cost(cost, _costReason, null, _showTips);
                        if (result)
                        {
                            break;
                        }
                    }

                    if (result)
                    {
                        _onCostSuccess?.Invoke();
                    }
                }
                break;
            default:
                result = false;
                break;
        }

        return result;
    }

    public bool IsEnough(Cost _cost, bool _showTips = false)
    {
        bool result = false;
        switch (_cost.GetTypeId())
        {
            case None.TYPE_ID:
                result = true;
                break;
            case AdCost.TYPE_ID:
                result = true;
                break;
            case CurrencyCost.TYPE_ID:
                {
                    CurrencyCost currency = (CurrencyCost)_cost;
                    result = IsCurrencyEnough(currency.currency, currency.amount, _showTips);
                }
                break;
            case ItemCost.TYPE_ID:
                {
                    ItemCost item = (ItemCost)_cost;
                    result = ItemModule.Instance.IsEnoughItemCount(item.itemId, item.itemCount, _showTips);
                }
                break;
            case And.TYPE_ID:
                {
                    And and = (And)_cost;
                    foreach (var cost in and.costs)
                    {
                        result = IsEnough(cost, _showTips);
                        if (!result)
                        {
                            break;
                        }
                    }
                }
                break;
            case Or.TYPE_ID:
                {
                    Or or = (Or)_cost;
                    foreach (var cost in or.costs)
                    {
                        result = IsEnough(cost, _showTips);
                        if (result)
                        {
                            break;
                        }
                    }
                }
                break;
            default:
                result = false;
                break;
        }

        return result;
    }

    public bool IsCurrencyEnough(ECurrencyType _currencyType, long _costCount, bool _sendTips = false)
    {
        int errorCode = EErrorCode.OK;

        if (_costCount <= 0)
        {
            errorCode = EErrorCode.Currency_Amount_Error;
            if (_sendTips)
            {
                ErrorHandler.SendError(errorCode);
            }
            return false;
        }

        long curCount = GetCurrencyCount(_currencyType);
        if (curCount < _costCount)
        {
            errorCode = EErrorCode.Currency_Not_Enough;
            if (_sendTips)
            {
                ErrorHandler.SendError(errorCode);
            }
            return false;
        }

        return true;
    }

    public string GetCurrencyName(ECurrencyType e)
    {
        return ConfigSystem.CurrentData.GetCurrency(e).name;
    }

    public string GetCurrencyDesc(ECurrencyType e)
    {
        return ConfigSystem.CurrentData.GetCurrency(e).desc;
    }
    
}