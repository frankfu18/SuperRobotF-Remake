using editor.cfg.common;
using editor.cfg.item;
using UnityEngine;

public class stCommonRewardItemData
{
    ///  <summary>
    ///  是否是货币类型
    ///  </summary>
    public bool isCurrency; 
    ///  <summary>
    ///  货币类型
    ///  </summary>
    public ECurrencyType currencyType;
    ///  <summary>
    ///  数量
    ///  </summary>
    public long amount;
    ///  <summary>
    ///  物品ID
    ///  </summary>
    public int itemId; 
    ///  <summary>
    ///  自定义图标路径
    ///  </summary>
    public string icon; 
    ///  <summary>
    ///  是否显示提示信息
    ///  </summary>
    public bool showTips;
    /// <summary>
    /// 图标自适应
    /// </summary>
    public bool nativeSize;
    /// <summary>
    /// 点击事件
    /// </summary>
    public System.Action onClick;
}

///  <summary>
///  通用奖励物品UI组件
///  </summary>
public partial class CommonRewardItem : UIItem
{
    ///  <summary>
    ///  当前奖励物品数据
    ///  </summary>
    private stCommonRewardItemData _itemData;

    protected override void OnRegisterListener()
    {
        base.OnRegisterListener();
        
        AddButtonClick(m_Button_Click, ShowTips); 
    }

    protected override void OnShow()
    {
        base.OnShow();
        //  获取奖励物品数据
        _itemData = (stCommonRewardItemData)data;
        //  默认物品品质
        var itemQuality = EItemQuality.White; 
        
        //  优先使用自定义图标
        var imageIconSpriteId = _itemData.icon;

        if (_itemData.isCurrency)
        {
            var currencyCfg = ConfigSystem.CurrentData.GetCurrency(_itemData.currencyType);
            //  获取货币品质
            itemQuality = currencyCfg.quality;
             //  获取货币图标
            imageIconSpriteId = currencyCfg.icon;
        }
        //  处理普通物品
        else if(_itemData.itemId > 0)
        {
            var itemCfg = ConfigSystem.CurrentData.GetItem(_itemData.itemId);
            itemQuality = itemCfg.quality;
            imageIconSpriteId = itemCfg.icon;
        }

        //  设置图标和品质背景
        SetSpriteById(m_Image_Icon, imageIconSpriteId, _itemData.nativeSize);
        SetSpriteById(m_Image_Quality, ItemModule.Instance.GetItemQuality(itemQuality).itemBg);
        
        SetActive(m_TMP_Text_Count, _itemData.amount > 0);
        //  设置数量显示
        SetText(m_TMP_Text_Count, $"{_itemData.amount:N0}");
    }

    //  显示提示信息
    public void ShowTips()
    {
        _itemData.onClick?.Invoke();

        //  如果不显示提示则直接返回
        if (!_itemData.showTips) return;

        AddTips<ItemDescTips>("Common/ItemDescTips", this.rectTransform, (tips) =>
        {
            //去除缩放的影响
            tips.transform.localScale = new Vector3(1 / this.rectTransform.localScale.x, 1 / this.rectTransform.localScale.y, 1 / this.rectTransform.localScale.z);
            
            if (_itemData.isCurrency)
            {
                tips.Show(_itemData.currencyType, this.rectTransform);
            }
            else
            {
                tips.Show(_itemData.itemId, this.rectTransform, 0);
            }
        });
    }
}
