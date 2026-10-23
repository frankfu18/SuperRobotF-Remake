//auto generic code don't edit.
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public partial class UserChangeNamePanel : UIPanel
{
	public GameObject m_GameObject_Container;
	public TextMeshProUGUI m_TMP_Text_Title;
	public TextMeshProUGUI m_TMP_Text_Tips;
	public TMP_InputField m_TMP_InputField_UserName;
	public Button m_Button_Close;
	public Button m_Button_Sure;
	protected override void OnBind()
	{
		m_GameObject_Container = GetVaule<GameObject>("m_GameObject_Container");
		m_TMP_Text_Title = GetVaule<TextMeshProUGUI>("m_TMP_Text_Title");
		m_TMP_Text_Tips = GetVaule<TextMeshProUGUI>("m_TMP_Text_Tips");
		m_TMP_InputField_UserName = GetVaule<TMP_InputField>("m_TMP_InputField_UserName");
		m_Button_Close = GetVaule<Button>("m_Button_Close");
		m_Button_Sure = GetVaule<Button>("m_Button_Sure");
	}
}
