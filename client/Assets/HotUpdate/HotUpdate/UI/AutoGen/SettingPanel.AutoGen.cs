//auto generic code don't edit.
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public partial class SettingPanel : UIPanel
{
	public GameObject m_GameObject_Container;
	public TextMeshProUGUI m_TMP_Text_Title;
	public Button m_Button_Close;
	public Slider m_Slider_Sound;
	public Slider m_Slider_Music;
	public Button m_Button_Privacy;
	protected override void OnBind()
	{
		m_GameObject_Container = GetVaule<GameObject>("m_GameObject_Container");
		m_TMP_Text_Title = GetVaule<TextMeshProUGUI>("m_TMP_Text_Title");
		m_Button_Close = GetVaule<Button>("m_Button_Close");
		m_Slider_Sound = GetVaule<Slider>("m_Slider_Sound");
		m_Slider_Music = GetVaule<Slider>("m_Slider_Music");
		m_Button_Privacy = GetVaule<Button>("m_Button_Privacy");
	}
}
