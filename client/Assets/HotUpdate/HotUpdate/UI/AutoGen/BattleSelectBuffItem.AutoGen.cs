//auto generic code don't edit.
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public partial class BattleSelectBuffItem : UIItem
{
	public TextMeshProUGUI m_TMP_Text_Name;
	public TextMeshProUGUI m_TMP_Text_Desc;
	public Button m_Button_Sure;
	protected override void OnBind()
	{
		m_TMP_Text_Name = GetVaule<TextMeshProUGUI>("m_TMP_Text_Name");
		m_TMP_Text_Desc = GetVaule<TextMeshProUGUI>("m_TMP_Text_Desc");
		m_Button_Sure = GetVaule<Button>("m_Button_Sure");
	}
}
