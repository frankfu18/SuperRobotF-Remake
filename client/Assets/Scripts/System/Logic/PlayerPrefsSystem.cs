using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsSystem : SystemBase<PlayerPrefsSystem>
{
    public SettingData settingData;

    protected override void OnInit()
    {
        settingData = new SettingData(this);
        settingData.Load();
    }

    public bool HasKey(string _key)
    {
        return PlayerPrefs.HasKey(_key);
    }

    public void SetInt(string _key, int _value)
    {
        PlayerPrefs.SetInt(_key, _value);
        PlayerPrefs.Save();
    }

    public void SetString(string _key, string _value)
    {
        PlayerPrefs.SetString(_key, _value);
        PlayerPrefs.Save();
    }

    public void SetFloat(string _key, float _value)
    {
        PlayerPrefs.SetFloat(_key, _value);
        PlayerPrefs.Save();
    }

    public void SetBool(string _key, bool _value)
    {
        PlayerPrefs.SetInt(_key, _value ? 1 : 0);
        PlayerPrefs.Save();
    }

    public int GetInt(string _key, int _defaultValue)
    {
        return PlayerPrefs.GetInt(_key, _defaultValue);
    }

    public string GetString(string _key, string _defaultValue)
    {
        return PlayerPrefs.GetString(_key, _defaultValue);
    }

    public float GetFloat(string _key, float _defaultValue)
    {
        return PlayerPrefs.GetFloat(_key, _defaultValue);
    }

    public bool GetBool(string _key, bool _defaultValue)
    {
        int result = PlayerPrefs.GetInt(_key, _defaultValue ? 1 : 0);

        return result > 0;
    }
}
