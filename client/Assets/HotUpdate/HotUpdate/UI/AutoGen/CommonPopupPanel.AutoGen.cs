//auto generic code don't edit.
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public partial class CommonPopupPanel : UIPanel
{
	public GameObject m_GameObject_Container;
	public TextMeshProUGUI m_TMP_Text_Title;
	public Button m_Button_Close;
	public TextMeshProUGUI m_TMP_Text_Content;
	public Button m_Button_Confirm;
	public TextMeshProUGUI m_TMP_Text_BtnConfirm;
	public Button m_Button_Cancel;
	public TextMeshProUGUI m_TMP_Text_BtnCancel;
	protected override void OnBind()
	{
		m_GameObject_Container = GetVaule<GameObject>("m_GameObject_Container");
		m_TMP_Text_Title = GetVaule<TextMeshProUGUI>("m_TMP_Text_Title");
		m_Button_Close = GetVaule<Button>("m_Button_Close");
		m_TMP_Text_Content = GetVaule<TextMeshProUGUI>("m_TMP_Text_Content");
		m_Button_Confirm = GetVaule<Button>("m_Button_Confirm");
		m_TMP_Text_BtnConfirm = GetVaule<TextMeshProUGUI>("m_TMP_Text_BtnConfirm");
		m_Button_Cancel = GetVaule<Button>("m_Button_Cancel");
		m_TMP_Text_BtnCancel = GetVaule<TextMeshProUGUI>("m_TMP_Text_BtnCancel");
	}
}
