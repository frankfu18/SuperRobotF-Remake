using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using editor.cfg.attribute;
using editor.cfg.common;
using Attribute = editor.cfg.attribute.Attribute;

public class AttributeModule : ModuleBase<AttributeModule>
{
    protected override void OnLoadConfigDone()
    {
        base.OnLoadConfigDone();

    }


    public string GetAttrName(EAttributeType _attrType)
    {
        var attrConfig = ConfigSystem.CurrentData.GetAttribute(_attrType);
        if (attrConfig != null)
        {
            return attrConfig.name;
        }
        return "";
    }

    public string GetAttrValueString(EAttributeType _attrType, long _attrValue)
    {
        var attrConfig = ConfigSystem.CurrentData.GetAttribute(_attrType);

        if (attrConfig.attrNumberType == EAttrNumberType.Fixed)
        {
            return _attrValue.ToString();
        }
        else if (attrConfig.attrNumberType == EAttrNumberType.Ratio)
        {
            return (_attrValue / 100.0f) + "%";
        }
        else if (attrConfig.attrNumberType == EAttrNumberType.Millsecond)
        {
            return (_attrValue / 1000.0f) + "s";
        }
        else if (attrConfig.attrNumberType == EAttrNumberType.RoundPreSecond)
        {
            return (_attrValue / 100.0f).ToString("0.00") + "/s";
        }
        else if (attrConfig.attrNumberType == EAttrNumberType.Round)
        {
            return (_attrValue / 100.0f).ToString("0.00");
        }

        return null;
    }

    //public string GetAttrIcon(EAttributeType _attrType)
    //{
    //    var attrConfig = ConfigSystem.CurrentData.GetAttribute(_attrType);
    //    if (attrConfig != null)
    //    {
    //        return attrConfig.icon;
    //    }
    //    return "";
    //}

    public void CalculateAttrValue(Dictionary<EAttributeType, long> _attrDic, EAttributeType _base, EAttributeType _add, EAttributeType _sub)
    {
        if (_attrDic.TryGetValue(_base, out long baseValue))
        {
            float addRatio = 0;
            if (_attrDic.TryGetValue(_add, out long addValue))
            {
                addRatio += addValue / 10000.0f;

                _attrDic.Remove(_add);
            }
            if (_attrDic.TryGetValue(_sub, out long subValue))
            {
                addRatio -= subValue / 10000.0f;

                _attrDic.Remove(_sub);
            }

            if (addRatio != 0)
            {
                baseValue = (long)(baseValue * addRatio);

                _attrDic[_base] = baseValue;
            }
        }
    }

    protected override void OnLogout()
    {
        base.OnLogout();
    }
}


