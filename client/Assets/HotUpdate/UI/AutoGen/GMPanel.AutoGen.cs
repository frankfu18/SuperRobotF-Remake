//auto generic code don't edit.
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public partial class GMPanel : UIPanel
{
	public Button m_Button_Bg;
	public GameObject m_GameObject_Container;
	public Button m_Button_Currency;
	public TMP_InputField m_TMP_InputField_CurrencyType;
	public TMP_InputField m_TMP_InputField_CurrencyAmount;
	public Button m_Button_Item;
	public Button m_Button_AllItem;
	public TMP_InputField m_TMP_InputField_ItemId;
	public TMP_InputField m_TMP_InputField_ItemAmount;
	public Button m_Button_AddGold;
	public Button m_Button_AddDiamond;
	public Button m_Button_AddScore1;
	public Button m_Button_AddScore10;
	public Button m_Button_SetScore;
	public TMP_InputField m_TMP_InputField_Score;
	public TMP_InputField m_TMP_InputField_Bonus;
	public Button m_Button_BonusTest;
	public Button m_Button_BonusAdd;
	public Button m_Button_DBClear;
	public List<Button> m_Button_AddScoreList = new List<Button>();
	protected override void OnBind()
	{
		m_Button_Bg = GetVaule<Button>("m_Button_Bg");
		m_GameObject_Container = GetVaule<GameObject>("m_GameObject_Container");
		m_Button_Currency = GetVaule<Button>("m_Button_Currency");
		m_TMP_InputField_CurrencyType = GetVaule<TMP_InputField>("m_TMP_InputField_CurrencyType");
		m_TMP_InputField_CurrencyAmount = GetVaule<TMP_InputField>("m_TMP_InputField_CurrencyAmount");
		m_Button_Item = GetVaule<Button>("m_Button_Item");
		m_Button_AllItem = GetVaule<Button>("m_Button_AllItem");
		m_TMP_InputField_ItemId = GetVaule<TMP_InputField>("m_TMP_InputField_ItemId");
		m_TMP_InputField_ItemAmount = GetVaule<TMP_InputField>("m_TMP_InputField_ItemAmount");
		m_Button_AddGold = GetVaule<Button>("m_Button_AddGold");
		m_Button_AddDiamond = GetVaule<Button>("m_Button_AddDiamond");
		m_Button_AddScore1 = GetVaule<Button>("m_Button_AddScore1");
		m_Button_AddScore10 = GetVaule<Button>("m_Button_AddScore10");
		m_Button_SetScore = GetVaule<Button>("m_Button_SetScore");
		m_TMP_InputField_Score = GetVaule<TMP_InputField>("m_TMP_InputField_Score");
		m_TMP_InputField_Bonus = GetVaule<TMP_InputField>("m_TMP_InputField_Bonus");
		m_Button_BonusTest = GetVaule<Button>("m_Button_BonusTest");
		m_Button_BonusAdd = GetVaule<Button>("m_Button_BonusAdd");
		m_Button_DBClear = GetVaule<Button>("m_Button_DBClear");
		m_Button_AddScoreList.Add(m_Button_AddScore1);
		m_Button_AddScoreList.Add(m_Button_AddScore10);
	}
}
