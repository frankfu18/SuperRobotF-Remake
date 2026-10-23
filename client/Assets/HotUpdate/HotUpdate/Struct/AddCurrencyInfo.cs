using editor.cfg.common;

/// <summary>
/// 货币增加记录
/// </summary>
public struct AddCurrencyInfo
{

    /// <summary>
    /// 货币类型
    /// </summary>
    public ECurrencyType currencyType;
    
    /// <summary>
    /// 来源
    /// </summary>
    public EAddCurrencyType addCurrencyType;
    
    /// <summary>
    /// 参数
    /// </summary>
    public object data;

    /// <summary>
    /// 数量
    /// </summary>
    public long amount;
}

public enum EAddCurrencyType
{
    /// <summary>
    /// 商店内购买
    /// </summary>
    IAP,
    /// <summary>
    /// 看广告获得
    /// </summary>
    Ad,
    /// <summary>
    /// 商店免费领取
    /// </summary>
    ShopFree,
    /// <summary>
    /// 战斗章节奖励，第几波xxx
    /// </summary>
    ChapterBox,
    /// <summary>
    /// 任务
    /// </summary>
    Task,
    /// <summary>
    /// 其他
    /// </summary>
    Other
}