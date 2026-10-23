using editor.cfg.condition;
using System.Collections.Generic;

public class ConditionModule : ModuleBase<ConditionModule>
{
    public bool IsEnough(Condition _con)
    {
        bool result = false;
        switch (_con.GetTypeId())
        {
            case None.TYPE_ID:
                result = true;
                break;
            case Currency.TYPE_ID:
                {
                    Currency condition = _con as Currency;

                    result = CurrencyModule.Instance.IsCurrencyEnough(condition.currency, condition.amount);
                }
                break;
            case Item.TYPE_ID:
                {
                    var itemCondition = (Item)_con;
                    var itemCount = ItemModule.Instance.GetItemCount(itemCondition.itemId);
                    result = itemCount >= itemCondition.amount;
                    break;
                }
            case OwnItem.TYPE_ID:
                {
                    var itemCondition = (OwnItem)_con;
                    result = ItemModule.Instance.IsItemHasGet(itemCondition.itemId);
                    break;
                }
            case And.TYPE_ID:
                {
                    And condition = _con as And;

                    foreach (var con in condition.conditions)
                    {
                        result = IsEnough(con);
                        if (!result)
                        {
                            break;
                        }
                    }
                }
                break;
            case Or.TYPE_ID:
                {
                    Or condition = _con as Or;

                    foreach (var con in condition.conditions)
                    {
                        result = IsEnough(con);
                        if (result)
                        {
                            break;
                        }
                    }
                }
                break;
        }

        return result;
    }

    /// <summary>
    /// 数字比较
    /// </summary>
    /// <param name="numCompareType"></param>
    /// <param name="excelConfigNum">表格配置的数值</param>
    /// <param name="currNum">当前数值</param>
    /// <returns></returns>
    public static bool NumCompare(ENumCompareType numCompareType, int excelConfigNum, int currNum)
    {
        switch (numCompareType)
        {
            //  等于
            case ENumCompareType.Eq:
                return excelConfigNum == currNum;
            //  大于
            case ENumCompareType.Gt:
                return currNum > excelConfigNum;
            //  大于等于
            case ENumCompareType.Gte:
                return currNum >= excelConfigNum;
            //  小于
            case ENumCompareType.Lt:
                return currNum < excelConfigNum;
            //  小于等于
            case ENumCompareType.Lte:
                return currNum <= excelConfigNum;
            //  是奇数
            case ENumCompareType.OddNumber:
                return currNum % 2 == 1;
            //  是偶数
            case ENumCompareType.EvenNumber:
                return currNum % 2 == 0;
        }
        return false;
    }

    public string GetConditionDesc(Condition _con)
    {
        string result = string.Empty;
        switch (_con.GetTypeId())
        {
            case None.TYPE_ID:
                result = "无条件";
                break;
            case Currency.TYPE_ID:
                {
                    Currency condition = _con as Currency;
                    result = $"需要{condition.amount}{ConfigSystem.CurrentData.GetCurrency(condition.currency).name}";
                }
                break;
            case Item.TYPE_ID:
                {
                    var itemCondition = (Item)_con;
                    result = $"需要{itemCondition.amount}{ConfigSystem.CurrentData.GetItem(itemCondition.itemId).name}";
                    break;
                }
            case OwnItem.TYPE_ID:
                {
                    var itemCondition = (OwnItem)_con;
                    result = $"需要拥有{ConfigSystem.CurrentData.GetItem(itemCondition.itemId).name}";
                    break;
                }
            
            case And.TYPE_ID:
                {
                    And condition = _con as And;
                    List<string> descList = new List<string>();
                    foreach (var con in condition.conditions)
                    {
                        descList.Add(GetConditionDesc(con));
                    }
                    result = string.Join("，", descList);
                }
                break;
            case Or.TYPE_ID:
                {
                    Or condition = _con as Or;
                    List<string> descList = new List<string>();
                    foreach (var con in condition.conditions)
                    {
                        descList.Add(GetConditionDesc(con));
                    }
                    result = string.Join("或", descList);
                }
                break;
        }
        return result;
    }
}
