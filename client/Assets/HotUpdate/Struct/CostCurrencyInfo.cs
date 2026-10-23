using editor.cfg.common;

/// <summary>
/// 货币消耗记录
/// </summary>
public struct CostCurrencyInfo
{

    /// <summary>
    /// 货币类型
    /// </summary>
    public ECurrencyType currencyType;
    
    /// <summary>
    /// 用在哪了
    /// </summary>
    public ECostCurrencyType costCurrencyType;
    
    /// <summary>
    /// 参数
    /// </summary>
    public object data;

    /// <summary>
    /// 数量
    /// </summary>
    public long amount;

    /// <summary>
    /// 如果购买的是其他，这个记录物品的英文名字
    /// </summary>
    public string itemEngName;

}

public enum ECostCurrencyType
{
    
    /// <summary>
    /// 购买体力
    /// </summary>
    BuyStrength,
    
    /// <summary>
    /// 商店购买宝箱
    /// </summary>
    ShopBuyBox,
    
    /// <summary>
    /// 商店购买金币
    /// </summary>
    ShopBuyGold,
    
    /// <summary>
    /// 商店购买装备
    /// </summary>
    ShopBuyEquip,
    
    /// <summary>
    /// 商店仙侣礼物
    /// </summary>
    ShopBuyCompanionGift,
    
    /// <summary>
    /// 其他
    /// </summary>
    Other,

}