//auto generic code don't edit.
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public partial class BattleResultPanel : UIPanel
{
	public GameObject m_GameObject_Container;
	public TextMeshProUGUI m_TMP_Text_WinCount;
	public TextMeshProUGUI m_TMP_Text_AddScore;
	public CommonRewardItem m_Item_CommonRewardItem_Temp;
	public Button m_Button_Continue;
	public Button m_Button_Home;
	protected override void OnBind()
	{
		m_GameObject_Container = GetVaule<GameObject>("m_GameObject_Container");
		m_TMP_Text_WinCount = GetVaule<TextMeshProUGUI>("m_TMP_Text_WinCount");
		m_TMP_Text_AddScore = GetVaule<TextMeshProUGUI>("m_TMP_Text_AddScore");
		m_Item_CommonRewardItem_Temp = InitUIItem<CommonRewardItem>("m_Item_CommonRewardItem_Temp");
		m_Button_Continue = GetVaule<Button>("m_Button_Continue");
		m_Button_Home = GetVaule<Button>("m_Button_Home");
	}
}
