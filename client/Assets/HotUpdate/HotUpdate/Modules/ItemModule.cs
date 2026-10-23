using db.parser;
using editor.cfg.bonus;
using editor.cfg.item;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Item = editor.cfg.item.Item;

public class ItemModule : ModuleBase<ItemModule>
{
    private Dictionary<EItemQuality, List<int>> equipFragmentIdByQualityDic = new();

    protected override void OnLoadConfigDone()
    {
        base.OnLoadConfigDone();

        var itemCfgs = ConfigSystem.CurrentData.GetItemList();

        foreach (var itemCfg in itemCfgs)
        {
            if(itemCfg.itemType == EItemType.EquipFragment)
            {
                if (!this.equipFragmentIdByQualityDic.TryGetValue(itemCfg.quality, out List<int> itemIds))
                {
                    itemIds = new List<int>();

                    this.equipFragmentIdByQualityDic.Add(itemCfg.quality, itemIds);
                }

                itemIds.Add(itemCfg.id);
            }
        }
    }

    public long GetItemCount(int _itemId)
    {
        if (!ItemDataParser.Ins.ItemInfoDic.TryGet<ItemInfoParser>(_itemId, out var info))
        {
            return 0;
        }
        return info.ItemCount;
    }

    private void SetItemCount(int _itemId, long _itemCount, bool sendItemRefreshEvent = true)
    {
        if (ItemDataParser.Ins.ItemInfoDic.TryGet<ItemInfoParser>(_itemId, out var info))
        {
            info.ItemCount = _itemCount;
        }
        else
        {
            var itemInfo = new db.cfg.ItemInfo();
            itemInfo.itemId = _itemId;
            itemInfo.itemCount = _itemCount;

            ItemDataParser.Ins.ItemInfoDic.Add(itemInfo.itemId, itemInfo);
        }

        if (sendItemRefreshEvent)
        {
            SendEvent(EventConst.Item_Refresh, _itemId);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="_itemId"></param>
    /// <param name="_itemCount"></param>
    /// <param name="_totalRewards"></param>
    /// <param name="isLastItem">批量添加 Item 时只有最后一次会发送 Item_Refresh 事件，不需要每次都发送，不然会很卡 </param>
    /// <returns></returns>
    public int AddItem(int _itemId, long _itemCount, EReasonType _reasonType, bool isLastItem = true)
    {
        if (_itemCount <= 0)
        {
            return EErrorCode.Item_Amount_Error;
        }

        var itemCfg = ConfigSystem.CurrentData.GetItem(_itemId);
        if (itemCfg == null)
        {
            return EErrorCode.Item_Not_Exist;
        }

        bool isHasGet = IsItemHasGet(_itemId);//之前是否已经获得过

        long curCount = GetItemCount(_itemId);
        curCount += _itemCount;

        SetItemCount(_itemId, curCount, isLastItem);

        if (!isHasGet)//第一次获得该物品
        {
            SendEvent(EventConst.Item_FirstGet, _itemId);
        }

        return EErrorCode.OK;
    }

    public int CostItem(int _itemId, long _costCount, EReasonType _reasonType)
    {
        if (_costCount <= 0)
        {
            return EErrorCode.Item_Amount_Error;
        }

        var itemCfg = ConfigSystem.CurrentData.GetItem(_itemId);
        if (itemCfg == null)
        {
            return EErrorCode.Item_Not_Exist;
        }

        long curCount = GetItemCount(_itemId);
        if (curCount >= _costCount)
        {
            SetItemCount(_itemId, curCount - _costCount);

            return EErrorCode.OK;
        }

        return EErrorCode.Item_Not_Enough;
    }

    public int UseItem(int _itemId, long _useCount, EReasonType _reasonType, bool _showPanel = false)
    {
        var itemConfig = GetItem(_itemId);
        if(itemConfig.itemType != EItemType.Gift && itemConfig.itemType != EItemType.ArenaBox)
        {
            return EErrorCode.Item_Not_Use;
        }

        if (_useCount <= 0)
        {
            return EErrorCode.Item_Amount_Error;
        }

        long curCount = GetItemCount(_itemId);
        if (curCount < _useCount)
        {
            return EErrorCode.Item_Not_Enough;
        }

        Bonus bonus = null;
        if (itemConfig.itemInfo.GetTypeId() == GiftInfo.TYPE_ID)
        {
            bonus = ConfigSystem.CurrentData.GetBonusData((itemConfig.itemInfo as GiftInfo).bonusId).bonus;
        }
        else if (itemConfig.itemInfo.GetTypeId() == ArenaBoxInfo.TYPE_ID)
        {
            bonus = ConfigSystem.CurrentData.GetBonusData((itemConfig.itemInfo as ArenaBoxInfo).bonusId).bonus;
        }

        SetItemCount(_itemId, curCount - _useCount);
        return EErrorCode.OK;
    }

    public int UseAllItem(int _itemId, EReasonType _reasonType, bool _showPanel = false)
    {
        var itemConfig = GetItem(_itemId);
        if(itemConfig.itemType != EItemType.Gift && itemConfig.itemType != EItemType.ArenaBox)
        {
            return EErrorCode.Item_Not_Use;
        }

        long curCount = GetItemCount(_itemId);
        if (curCount <= 0)
        {
            return EErrorCode.Item_Not_Enough;
        }

        Bonus bonus = null;
        if (itemConfig.itemInfo.GetTypeId() == GiftInfo.TYPE_ID)
        {
            bonus = ConfigSystem.CurrentData.GetBonusData((itemConfig.itemInfo as GiftInfo).bonusId).bonus;
        }
        else if (itemConfig.itemInfo.GetTypeId() == ArenaBoxInfo.TYPE_ID)
        {
            bonus = ConfigSystem.CurrentData.GetBonusData((itemConfig.itemInfo as ArenaBoxInfo).bonusId).bonus;
        }

        SetItemCount(_itemId, 0);
        return EErrorCode.OK;
    }



    public bool IsEnoughItemCount(int _itemId, long _count, bool _showTips = false)
    {
        long curCount = GetItemCount(_itemId);
        bool result = curCount >= _count;
        if (!result)
        {
            if (_showTips)
            {
                ErrorHandler.SendError(EErrorCode.Item_Not_Enough);
            }
        }

        return result;
    }

    /// <summary>
    /// 物品是否拥有过
    /// </summary>
    /// <returns></returns>
    public bool IsItemHasGet(int _itemId)
    {
        return ItemDataParser.Ins.ItemInfoDic.ContainsKey(_itemId);
    }

    // 获取品质信息
    public Quality GetItemQuality(EItemQuality itemQuality)
    {
        return ConfigSystem.CurrentData.GetQuality(itemQuality);
    }

    public string GetItemDesc(int itemId)
    {
        return ConfigSystem.CurrentData.GetItem(itemId).desc;
    }

    public Item GetItem(int itemId)
    {
        return ConfigSystem.CurrentData.GetItem(itemId);
    }

    public List<Item> FindItemList(EItemType itemType, EItemQuality itemQuality = default)
    {
        return ConfigSystem
                    .CurrentData
                    .GetItemList()
                    .FindAll(a =>
                    {
                        var con1 = a.itemType == itemType;
                        if (itemQuality != default)
                        {
                            con1 = con1 && a.quality == itemQuality;
                        }
                        return con1;
                    });
    }

    public List<int> GetEquipFragmentIdList(EItemQuality _quality)
    {
        List<int> resultList = new List<int>();

        if(this.equipFragmentIdByQualityDic.TryGetValue(_quality, out List<int> ids))
        {
            resultList.AddRange(ids);
        }

        return resultList;
    }
}
