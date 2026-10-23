//auto generic code don't edit.
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public partial class MatchingUserItem : UIItem
{
	public UserInfoItem m_Item_UserInfoItem;
	public GameObject m_GameObject_Fighting;
	public GameObject m_GameObject_Victory;
	public TextMeshProUGUI m_TMP_Text_Victory;
	public GameObject m_GameObject_Lose;
	public TextMeshProUGUI m_TMP_Text_Lose;
	public GameObject m_GameObject_EffMask;
	protected override void OnBind()
	{
		m_Item_UserInfoItem = InitUIItem<UserInfoItem>("m_Item_UserInfoItem");
		m_GameObject_Fighting = GetVaule<GameObject>("m_GameObject_Fighting");
		m_GameObject_Victory = GetVaule<GameObject>("m_GameObject_Victory");
		m_TMP_Text_Victory = GetVaule<TextMeshProUGUI>("m_TMP_Text_Victory");
		m_GameObject_Lose = GetVaule<GameObject>("m_GameObject_Lose");
		m_TMP_Text_Lose = GetVaule<TextMeshProUGUI>("m_TMP_Text_Lose");
		m_GameObject_EffMask = GetVaule<GameObject>("m_GameObject_EffMask");
	}
}
