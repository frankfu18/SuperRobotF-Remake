using System;
using System.Collections.Generic;
using editor.cfg.common;

public class CostModule : ModuleBase<CostModule>
{

    public bool IsEnough(Cost cost)
    {
        if (cost.GetTypeId() == None.TYPE_ID)
        {
            return true;
        }
        if (cost.GetTypeId() == CurrencyCost.TYPE_ID)
        {
            var cc = (CurrencyCost)cost;
            return CurrencyModule.Instance.IsCurrencyEnough(cc.currency, cc.amount);
        }
        if (cost.GetTypeId() == ItemCost.TYPE_ID)
        {
            var ic = (ItemCost)cost;
            var itemCount = ItemModule.Instance.GetItemCount(ic.itemId);
            return itemCount >= ic.itemCount;
        }
        if (cost.GetTypeId() == And.TYPE_ID)
        {
            var and = (And)cost;
            var enough = true;
            foreach (var andCost in and.costs)
            {
                enough = enough && Instance.IsEnough(andCost);
            }
            return enough;
        }
        throw new NotImplementedException(cost.ToString());
    }

    public Dictionary<int, long> GetItemCostDic(Cost cost)
    {
        Dictionary<int, long> itemCountDic = new Dictionary<int, long>();

        if(cost.GetTypeId() == ItemCost.TYPE_ID)
        {
            ItemCost itemCost = (ItemCost)cost;
            if (itemCountDic.TryGetValue(itemCost.itemId, out long count))
            {
                itemCountDic[itemCost.itemId] = count + itemCost.itemCount;
            }
            else
            {
                itemCountDic.Add(itemCost.itemId, itemCost.itemCount);
            }
        }
        else if (cost.GetTypeId() == And.TYPE_ID)
        {
            And andCost = (And)cost;

            foreach (var cc in andCost.costs)
            {
                var dic = GetItemCostDic(cc);
                foreach (var kv in dic)
                {
                    if(itemCountDic.TryGetValue(kv.Key, out long count))
                    {
                        itemCountDic[kv.Key] = count + kv.Value;
                    }
                    else
                    {
                        itemCountDic.Add(kv.Key, kv.Value);
                    }
                }
            }
        }
        else if (cost.GetTypeId() == Or.TYPE_ID)
        {
            Or orCost = (Or)cost;

            foreach (var cc in orCost.costs)
            {
                var dic = GetItemCostDic(cc);
                foreach (var kv in dic)
                {
                    if (itemCountDic.TryGetValue(kv.Key, out long count))
                    {
                        itemCountDic[kv.Key] = count + kv.Value;
                    }
                    else
                    {
                        itemCountDic.Add(kv.Key, kv.Value);
                    }
                }
            }
        }

        return itemCountDic;
    }

    public Dictionary<ECurrencyType, long> GetCurrencyCostDic(Cost cost)
    {
        Dictionary<ECurrencyType, long> currencyCountDic = new Dictionary<ECurrencyType, long>();

        if (cost.GetTypeId() == CurrencyCost.TYPE_ID)
        {
            CurrencyCost currencyCost = (CurrencyCost)cost;
            if (currencyCountDic.TryGetValue(currencyCost.currency, out long count))
            {
                currencyCountDic[currencyCost.currency] = count + currencyCost.amount;
            }
            else
            {
                currencyCountDic.Add(currencyCost.currency, currencyCost.amount);
            }
        }
        else if (cost.GetTypeId() == And.TYPE_ID)
        {
            And andCost = (And)cost;

            foreach (var cc in andCost.costs)
            {
                var dic = GetCurrencyCostDic(cc);
                foreach (var kv in dic)
                {
                    if (currencyCountDic.TryGetValue(kv.Key, out long count))
                    {
                        currencyCountDic[kv.Key] = count + kv.Value;
                    }
                    else
                    {
                        currencyCountDic.Add(kv.Key, kv.Value);
                    }
                }
            }
        }
        else if (cost.GetTypeId() == Or.TYPE_ID)
        {
            Or orCost = (Or)cost;

            foreach (var cc in orCost.costs)
            {
                var dic = GetCurrencyCostDic(cc);
                foreach (var kv in dic)
                {
                    if (currencyCountDic.TryGetValue(kv.Key, out long count))
                    {
                        currencyCountDic[kv.Key] = count + kv.Value;
                    }
                    else
                    {
                        currencyCountDic.Add(kv.Key, kv.Value);
                    }
                }
            }
        }

        return currencyCountDic;
    }

    public bool IsEnough<T>(List<T> costList) where T : Cost
    {
        var enough = true;
        foreach (var cost in costList)
        {
            enough = enough && IsEnough(cost);
        }
        return enough;
    }

    public bool Cost(Cost cost, EReasonType costReason, Action onCostSuccess = null, bool _showTips = false)
    {
        return cost.GetTypeId() == None.TYPE_ID || CurrencyModule.Instance.Cost(cost, costReason, onCostSuccess, _showTips);
    }

}
