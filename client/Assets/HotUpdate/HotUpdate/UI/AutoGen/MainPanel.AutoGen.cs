//auto generic code don't edit.
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public partial class MainPanel : UIPanel
{
	public GameObject m_GameObject_Container;
	public CommonCurrencyItem m_Item_CommonCurrencyItem_Gold;
	public Button m_Button_Setting;
	public UserInfoItem m_Item_UserInfoItem_Self;
	public Button m_Button_Head;
	public TextMeshProUGUI m_TMP_Text_ArenaName;
	public Image m_Image_Arena;
	public Button m_Button_Left;
	public Button m_Button_Right;
	public GameObject m_GameObject_ScoreProgress;
	public Image m_Image_ScoreProgress;
	public TextMeshProUGUI m_TMP_Text_ScoreProgress;
	public Button m_Button_GM;
	public Button m_Button_Card;
	public GameObject m_GameObject_CardRedPoint;
	public Button m_Button_Rank;
	public Button m_Button_Match;
	protected override void OnBind()
	{
		m_GameObject_Container = GetVaule<GameObject>("m_GameObject_Container");
		m_Item_CommonCurrencyItem_Gold = InitUIItem<CommonCurrencyItem>("m_Item_CommonCurrencyItem_Gold");
		m_Button_Setting = GetVaule<Button>("m_Button_Setting");
		m_Item_UserInfoItem_Self = InitUIItem<UserInfoItem>("m_Item_UserInfoItem_Self");
		m_Button_Head = GetVaule<Button>("m_Button_Head");
		m_TMP_Text_ArenaName = GetVaule<TextMeshProUGUI>("m_TMP_Text_ArenaName");
		m_Image_Arena = GetVaule<Image>("m_Image_Arena");
		m_Button_Left = GetVaule<Button>("m_Button_Left");
		m_Button_Right = GetVaule<Button>("m_Button_Right");
		m_GameObject_ScoreProgress = GetVaule<GameObject>("m_GameObject_ScoreProgress");
		m_Image_ScoreProgress = GetVaule<Image>("m_Image_ScoreProgress");
		m_TMP_Text_ScoreProgress = GetVaule<TextMeshProUGUI>("m_TMP_Text_ScoreProgress");
		m_Button_GM = GetVaule<Button>("m_Button_GM");
		m_Button_Card = GetVaule<Button>("m_Button_Card");
		m_GameObject_CardRedPoint = GetVaule<GameObject>("m_GameObject_CardRedPoint");
		m_Button_Rank = GetVaule<Button>("m_Button_Rank");
		m_Button_Match = GetVaule<Button>("m_Button_Match");
	}
}
