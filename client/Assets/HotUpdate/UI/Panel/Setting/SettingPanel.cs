using UnityEngine;
using System.Collections.Generic;

public partial class SettingPanel : UIPanel
{

    protected override void OnCreate()
    {
        base.OnCreate();

        this.m_Slider_Sound.onValueChanged.AddListener(OnSoundVolumeChange);
        this.m_Slider_Music.onValueChanged.AddListener(OnMusicVolumeChange);
    }

    protected override void OnRegisterListener()
    {
        base.OnRegisterListener();

        AddButtonClick(this.m_Button_Privacy, OnClickPrivacy);
    }

    protected override void OnShow()
    {
        base.OnShow();

        this.m_Slider_Sound.value = AudioSystem.Instance.AudioVolume / 100.0f;
        this.m_Slider_Music.value = AudioSystem.Instance.BgSoundVolume / 100.0f;
    }

    private void OnSoundVolumeChange(float _value)
    {
        AudioSystem.Instance.AudioVolume = (int)(_value * 100.0f);
    }

    private void OnMusicVolumeChange(float _value)
    {
        AudioSystem.Instance.BgSoundVolume = (int)(_value * 100.0f);
    }

    private void OnClickPrivacy()
    {
        SdkManager.Instance.OpenPrivacy();
    }

    /// <summary>
    /// 账户云储存
    /// </summary>
    private void OnClickCloudStorage()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            CheckNetWorkPanelUtil.Open();
            return;
        }
        //if (AccountModule.Instance.IsLogin == false)
        //{
        //    FlyTextManager.Instance.AddLocalizeText("setting_tips_not_login");
        //    return;
        //}
        ////  是否可以备份
        //if (!BackupManager.Instance.CanBackup())
        //{
        //    return;
        //}
        //LoadingMaskPanel.ShowMask(LanguageSystem.Instance.GetLanguage("settings.achive-panel.load-remote-data"));
        //// 获取数据
        //BackupManager.Instance.GetStoreDatas(
        //    datas =>
        //    {
        //        LoadingMaskPanel.CloseMask();
        //        UISystem.Instance.Open(UIConfig.SettingArchivedPanel, datas);
        //        Close();
        //    },
        //    e => {
        //        LoadingMaskPanel.CloseMask();
        //        FlyTextManager.Instance.AddLocalizeText("Load Fail");
        //    }
        //);
    }

}
