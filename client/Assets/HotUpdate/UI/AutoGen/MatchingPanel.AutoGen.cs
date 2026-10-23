//auto generic code don't edit.
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public partial class MatchingPanel : UIPanel
{
	public GameObject m_GameObject_Container;
	public GameObject m_GameObject_Round;
	public TextMeshProUGUI m_TMP_Text_Session;
	public GameObject m_GameObject_Result;
	public TextMeshProUGUI m_TMP_Text_Session2;
	public TextMeshProUGUI m_TMP_Text_Result;
	public GameObject m_GameObject_VS;
	public UserInfoItem m_Item_UserInfoItem_Self;
	public UserInfoItem m_Item_UserInfoItem_Enemy;
	public RectTransform m_RectTransform_UserList;
	public MatchingUserItem m_Item_MatchingUserItem_Temp;
	public MatchingUnkownItem m_Item_MatchingUnkownItem_Temp;
	public Button m_Button_Cancel;
	public Button m_Button_Continue;
	protected override void OnBind()
	{
		m_GameObject_Container = GetVaule<GameObject>("m_GameObject_Container");
		m_GameObject_Round = GetVaule<GameObject>("m_GameObject_Round");
		m_TMP_Text_Session = GetVaule<TextMeshProUGUI>("m_TMP_Text_Session");
		m_GameObject_Result = GetVaule<GameObject>("m_GameObject_Result");
		m_TMP_Text_Session2 = GetVaule<TextMeshProUGUI>("m_TMP_Text_Session2");
		m_TMP_Text_Result = GetVaule<TextMeshProUGUI>("m_TMP_Text_Result");
		m_GameObject_VS = GetVaule<GameObject>("m_GameObject_VS");
		m_Item_UserInfoItem_Self = InitUIItem<UserInfoItem>("m_Item_UserInfoItem_Self");
		m_Item_UserInfoItem_Enemy = InitUIItem<UserInfoItem>("m_Item_UserInfoItem_Enemy");
		m_RectTransform_UserList = GetVaule<RectTransform>("m_RectTransform_UserList");
		m_Item_MatchingUserItem_Temp = InitUIItem<MatchingUserItem>("m_Item_MatchingUserItem_Temp");
		m_Item_MatchingUnkownItem_Temp = InitUIItem<MatchingUnkownItem>("m_Item_MatchingUnkownItem_Temp");
		m_Button_Cancel = GetVaule<Button>("m_Button_Cancel");
		m_Button_Continue = GetVaule<Button>("m_Button_Continue");
	}
}
