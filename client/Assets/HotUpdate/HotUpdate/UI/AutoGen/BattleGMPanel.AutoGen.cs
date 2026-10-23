//auto generic code don't edit.
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public partial class BattleGMPanel : UIPanel
{
	public Button m_Button_Bg;
	public GameObject m_GameObject_Container;
	public Button m_Button_AddRes;
	public TMP_InputField m_TMP_InputField_AddRes;
	public Button m_Button_AddHp;
	public Button m_Button_SubHp;
	public TMP_InputField m_TMP_InputField_HpValue;
	public Button m_Button_RefreshBuff;
	protected override void OnBind()
	{
		m_Button_Bg = GetVaule<Button>("m_Button_Bg");
		m_GameObject_Container = GetVaule<GameObject>("m_GameObject_Container");
		m_Button_AddRes = GetVaule<Button>("m_Button_AddRes");
		m_TMP_InputField_AddRes = GetVaule<TMP_InputField>("m_TMP_InputField_AddRes");
		m_Button_AddHp = GetVaule<Button>("m_Button_AddHp");
		m_Button_SubHp = GetVaule<Button>("m_Button_SubHp");
		m_TMP_InputField_HpValue = GetVaule<TMP_InputField>("m_TMP_InputField_HpValue");
		m_Button_RefreshBuff = GetVaule<Button>("m_Button_RefreshBuff");
	}
}
