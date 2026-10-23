using db.parser;
using editor.cfg.attribute;
using System.Collections.Generic;
using UnityEngine;

public struct stGoBattleRoleData
{
    public int roleId;
    public Dictionary<EAttributeType, long> attrDic;
}

public class RoleModule : ModuleBase<RoleModule>
{
    private Dictionary<int, Dictionary<int, Dictionary<EAttributeType, stAttrInfo>>> roleLvAttrInfoDic = new();

    protected override void OnLogin()
    {
        base.OnLogin();

        //if (RoleDataParser.Ins.RoleInfoDic.Count == 0)//新号
        //{
        //    var roleUpgradeCfgs = ConfigSystem.CurrentData.GetRoleUpgradeList();
        //    foreach (var roleUpgradeCfg in roleUpgradeCfgs)
        //    {
        //        if(roleUpgradeCfg.unlockCost.GetTypeId() == editor.cfg.common.None.TYPE_ID)//默认解锁
        //        {
        //            RoleDataParser.Ins.CurRoleId = roleUpgradeCfg.id;
        //            RoleDataParser.Ins.SetRoleInfo(roleUpgradeCfg.id, 1);
        //        }
        //    }
        //}
    }

    public bool Send_RoleUnlock(int _roleId)
    {
        //if (IsRoleUnlock(_roleId))
        //{
        //    return ErrorHandler.SendError(EErrorCode.Role_HasUnlock);
        //}

        //var roleUpgradeConfig = ConfigSystem.CurrentData.GetRoleUpgrade(_roleId);

        //if (!CostModule.Instance.Cost(roleUpgradeConfig.unlockCost, EReasonType.Role_Unlock, null, true))
        //{
        //    return false;
        //}

        //RoleDataParser.Ins.SetRoleInfo(_roleId, 1);

        //SendEvent(EventConst.Role_UnlockSuccess, _roleId);

        return true;
    }

    public bool Send_RoleUpgrade(int _roleId)
    {
        //if (!IsRoleUnlock(_roleId))
        //{
        //    return ErrorHandler.SendError(EErrorCode.Role_NotUnlock);
        //}

        //if (IsRoleMaxLevel(_roleId))
        //{
        //    return ErrorHandler.SendError(EErrorCode.Role_Level_Max);
        //}

        //var roleUpgradeConfig = ConfigSystem.CurrentData.GetRoleUpgrade(_roleId);
        //int curLv = GetRoleLevel(_roleId);

        //if(!roleUpgradeConfig.levelAttrMap.TryGetValue(curLv, out var upgradeLvInfo))
        //{
        //    return ErrorHandler.SendError(EErrorCode.Exception_Error);
        //}

        //if (!CostModule.Instance.Cost(upgradeLvInfo.upgradeCost, EReasonType.Role_Upgrade, null, true))
        //{
        //    return false;
        //}

        //RoleDataParser.Ins.SetRoleInfo(_roleId, curLv + 1);

        //SendEvent(EventConst.Role_UpgradeSuccess, _roleId);

        return true;
    }

    public bool Send_RoleGoInBattle(int _roleId)
    {
        if (!IsRoleUnlock(_roleId))
        {
            return ErrorHandler.SendError(EErrorCode.Role_NotUnlock);
        }

        if(GetCurRoleId() == _roleId)
        {
            return ErrorHandler.SendError(EErrorCode.Role_HasGoInBattle);
        }

        RoleDataParser.Ins.CurRoleId = _roleId;

        SendEvent(EventConst.Role_GoInBattleSuccess);

        return true;
    }

    public bool IsRoleCanUpgrade(int _roleId)
    {
        //if (!IsRoleUnlock(_roleId))
        //{
        //    return false;
        //}

        //if (IsRoleMaxLevel(_roleId))
        //{
        //    return false;
        //}

        //var roleUpgradeConfig = ConfigSystem.CurrentData.GetRoleUpgrade(_roleId);
        //int curLv = GetRoleLevel(_roleId);

        //if (!roleUpgradeConfig.levelAttrMap.TryGetValue(curLv, out var upgradeLvInfo))
        //{
        //    return false;
        //}

        //if (!CostModule.Instance.IsEnough(upgradeLvInfo.upgradeCost))
        //{
        //    return false;
        //}

        return true;
    }

    public bool IsRoleCanUnlock(int _roleId)
    {
        //if (IsRoleUnlock(_roleId))
        //{
        //    return false;
        //}

        //var roleUpgradeConfig = ConfigSystem.CurrentData.GetRoleUpgrade(_roleId);

        //if (!CostModule.Instance.IsEnough(roleUpgradeConfig.unlockCost))
        //{
        //    return false;
        //}

        return true;
    }

    public bool IsRoleHasRedDot()
    {
        //var roleConfigs = ConfigSystem.CurrentData.GetRoleList();
        //foreach (var roleConfig in roleConfigs)
        //{
        //    if (IsRoleCanUnlock(roleConfig.id))
        //    {
        //        return true;
        //    }

        //    if (IsRoleCanUpgrade(roleConfig.id))
        //    {
        //        return true;
        //    }
        //}

        return false;
    }

    public Dictionary<EAttributeType, stAttrInfo> GetRoleAttrInfoDic(int _roleId, int _level)
    {
        Dictionary<int, Dictionary<EAttributeType, stAttrInfo>> lvAttrInfoDic = null;
        Dictionary<EAttributeType, stAttrInfo> attrInfoDic = null;
        if (!roleLvAttrInfoDic.TryGetValue(_roleId, out lvAttrInfoDic))
        {
            lvAttrInfoDic = new Dictionary<int, Dictionary<EAttributeType, stAttrInfo>>();

            roleLvAttrInfoDic.Add(_roleId, lvAttrInfoDic);
        }

        if (lvAttrInfoDic.TryGetValue(_level, out attrInfoDic))
        {
            return attrInfoDic;
        }
        else
        {
            attrInfoDic = new Dictionary<EAttributeType, stAttrInfo>();

            lvAttrInfoDic.Add(_level, attrInfoDic);
        }

        //var roleConfig = ConfigSystem.CurrentData.GetRole(_roleId);
        //var roleUpgradeConfig = ConfigSystem.CurrentData.GetRoleUpgrade(_roleId);

        //foreach (var attr in roleConfig.baseAttrDic)
        //{
        //    if (!attrInfoDic.TryGetValue(attr.type, out stAttrInfo attrInfo))
        //    {
        //        attrInfo = new stAttrInfo(attr.type);

        //        attrInfoDic.Add(attr.type, attrInfo);
        //    }

        //    attrInfo.AddLong(attr.number.amount);
        //}

        //if (!roleUpgradeConfig.levelAttrMap.TryGetValue(_level, out var roleUpgradeInfo))
        //{
        //    return null;
        //}

        //foreach (var attr in roleUpgradeInfo.attrDic)
        //{
        //    if (!attrInfoDic.TryGetValue(attr.type, out stAttrInfo attrInfo))
        //    {
        //        attrInfo = new stAttrInfo(attr.type);

        //        attrInfoDic.Add(attr.type, attrInfo);
        //    }

        //    attrInfo.AddLong(attr.number.amount);
        //}

        return attrInfoDic;
    }

    public int GetRoleLevel(int _roleId)
    {
        return RoleDataParser.Ins.GetRoleLevel(_roleId);
    }

    public bool IsRoleUnlock(int _roleId)
    {
        return GetRoleLevel(_roleId) > 0;
    }

    public bool IsRoleMaxLevel(int _roleId)
    {
        return false;
        //int curLevel = GetRoleLevel(_roleId);

        //var roleUpgradeCfg = ConfigSystem.CurrentData.GetRoleUpgrade(_roleId);

        //return curLevel >= roleUpgradeCfg.maxLv;
    }

    public int GetCurRoleId()
    {
        return RoleDataParser.Ins.CurRoleId;
    }

    public stGoBattleRoleData GetGoBattleRoleData(int _roleId, int _level)
    {
        var attrDic = GetRoleAttrInfoDic(_roleId, _level);

        stGoBattleRoleData roleData = new stGoBattleRoleData();
        roleData.roleId = _roleId;
        roleData.attrDic = new Dictionary<EAttributeType, long>();

        foreach (var attr in attrDic)
        {
            roleData.attrDic.Add(attr.Key, attr.Value.GetLong());
        }

        return roleData;
    }

    public stGoBattleRoleData GetUserGoBattleRoleData()
    {
        int roleId = GetCurRoleId();
        int level = GetRoleLevel(roleId);

        return GetGoBattleRoleData(roleId, level);
    }

    
}
