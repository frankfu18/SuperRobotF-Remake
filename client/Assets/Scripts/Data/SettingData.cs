using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingData : IPlayerPrefsData
{
    private const string Key_AudioMute = "SettingData_AudioMute";
    private const string Key_AudioSoundMute = "SettingData_AudioSoundMute";
    private const string Key_PhoneVibrate = "SettingData_PhoneVibrate";
    private const string Key_AudioVolume = "SettingData_AudioVolume";
    private const string Key_BgSoundVolume = "SettingData_BgSoundVolume";

    public bool audioMute = false;
    public bool phoneVibrate = true;
    public bool bgSoundMute = false;
    public int audioVolume = 100;//音效音量
    public int bgSoundVolume = 100;//背景音乐音量

    private PlayerPrefsSystem dataSystem;

    public SettingData(PlayerPrefsSystem _sys)
    {
        dataSystem = _sys;
    }

    public void Load()
    {
        audioMute = dataSystem.GetBool(Key_AudioMute, audioMute);
        phoneVibrate = dataSystem.GetBool(Key_PhoneVibrate, phoneVibrate);
        bgSoundMute = dataSystem.GetBool(Key_AudioSoundMute, bgSoundMute);
        audioVolume = dataSystem.GetInt(Key_AudioVolume, audioVolume);
        bgSoundVolume = dataSystem.GetInt(Key_BgSoundVolume, bgSoundVolume);
    }

    public void Save()
    {
        dataSystem.SetBool(Key_AudioMute, audioMute);
        dataSystem.SetBool(Key_PhoneVibrate, phoneVibrate);
        dataSystem.SetBool(Key_AudioSoundMute, bgSoundMute);
        dataSystem.SetInt(Key_AudioVolume, audioVolume);
        dataSystem.SetInt(Key_BgSoundVolume, bgSoundVolume);
    }
}
