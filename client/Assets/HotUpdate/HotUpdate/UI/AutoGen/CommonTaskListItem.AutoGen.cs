//auto generic code don't edit.
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public partial class CommonTaskListItem : UIItem
{
	public CommonRewardItem m_Item_CommonRewardItem;
	public TextMeshProUGUI m_TMP_Text_Name;
	public RectTransform m_RectTransform_Progress_BG;
	public RectTransform m_RectTransform_Progress_Front;
	public TextMeshProUGUI m_TMP_Text_Progress;
	public Button m_Button_InProgress;
	public Button m_Button_Claim;
	public GameObject m_GameObject_Black;
	protected override void OnBind()
	{
		m_Item_CommonRewardItem = InitUIItem<CommonRewardItem>("m_Item_CommonRewardItem");
		m_TMP_Text_Name = GetVaule<TextMeshProUGUI>("m_TMP_Text_Name");
		m_RectTransform_Progress_BG = GetVaule<RectTransform>("m_RectTransform_Progress_BG");
		m_RectTransform_Progress_Front = GetVaule<RectTransform>("m_RectTransform_Progress_Front");
		m_TMP_Text_Progress = GetVaule<TextMeshProUGUI>("m_TMP_Text_Progress");
		m_Button_InProgress = GetVaule<Button>("m_Button_InProgress");
		m_Button_Claim = GetVaule<Button>("m_Button_Claim");
		m_GameObject_Black = GetVaule<GameObject>("m_GameObject_Black");
	}
}
