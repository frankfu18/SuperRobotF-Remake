using editor.cfg.attribute;
using editor.cfg.common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stAttrInfo
{
    public EAttributeType AttrType
    {
        get
        {
            return attrType;
        }
    }

    private EAttributeType attrType;
    private long amountLong;
    private Attribute attrConfig;
    private EAttrNumberType attrNumberType;

    public stAttrInfo(EAttributeType _attrType)
    {
        this.attrType = _attrType;
        this.attrConfig = ConfigSystem.CurrentData.GetAttribute(_attrType);
        this.attrNumberType = this.attrConfig.attrNumberType;
    }

    public stAttrInfo(EAttributeType _attrType, long _amount)
    {
        this.attrType = _attrType;
        this.attrConfig = ConfigSystem.CurrentData.GetAttribute(_attrType);
        this.attrNumberType = this.attrConfig.attrNumberType;
        this.amountLong = _amount;
    }

    public void SetLong(long _amount)
    {
        this.amountLong = _amount;
    }

    public void AddLong(long _amount)
    {
        this.amountLong += _amount;
    }
    
    public void SubLong(long _amount)
    {
        this.amountLong -= _amount;
    }

    public void Multiply(int _num)
    {
        this.amountLong *= _num;
    }

    public long GetLong()
    {
        return this.amountLong;
    }

    public int GetInt()
    {
        return (int)this.amountLong;
    }

    public float GetFloat()
    {
        return this.amountLong / 100.0f;
    }

    public float GetRatio()
    {
        return GetLong() / 10000.0f;
    }

    public float GetSecond()
    {
        return GetLong() / 1000.0f;
    }

    // 每秒类小数属性，配置中 100 表示 1.0/s。
    public float GetRoundPreSecond()
    {
        return GetLong() / 100.0f;
    }

    public string GetAttrName()
    {
        if (this.attrConfig != null)
        {
            return this.attrConfig.name;
        }

        return this.attrType.ToString();
    }

    public string GetValueString()
    {
        if (this.attrNumberType == EAttrNumberType.Fixed)
        {
            return this.GetLong().ToString();
        }
        else if (this.attrNumberType == EAttrNumberType.Ratio)
        {
            return this.GetRatio() * 100 + "%";
        }
        else if (this.attrNumberType == EAttrNumberType.Millsecond)
        {
            return this.GetSecond().ToString("0.0") + "s";
        }
        else if (this.attrNumberType == EAttrNumberType.RoundPreSecond)
        {
            return this.GetRoundPreSecond().ToString("0.00") + "/s";
        }
        else if (this.attrNumberType == EAttrNumberType.Round)
        {
            return this.GetFloat().ToString("0.00");
        }

        return null;
    }

    public static string GetValueString(EAttributeType _attrType, long _attrValue)
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
            return (_attrValue / 1000) + "s";
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
}
