//auto generic code don't edit.
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public partial class UserChangeHeadPanel : UIPanel
{
	public GameObject m_GameObject_Container;
	public TextMeshProUGUI m_TMP_Text_Title;
	public Button m_Button_Close;
	public Button m_Button_ChangeHead;
	public UIList m_List_HeadItem;
	protected override void OnBind()
	{
		m_GameObject_Container = GetVaule<GameObject>("m_GameObject_Container");
		m_TMP_Text_Title = GetVaule<TextMeshProUGUI>("m_TMP_Text_Title");
		m_Button_Close = GetVaule<Button>("m_Button_Close");
		m_Button_ChangeHead = GetVaule<Button>("m_Button_ChangeHead");
		//m_List_HeadItem = InitUIList("m_List_HeadItem", typeof(HeadItem));
	}
}
