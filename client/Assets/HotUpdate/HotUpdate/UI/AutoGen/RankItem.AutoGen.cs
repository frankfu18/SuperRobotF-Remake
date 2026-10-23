//auto generic code don't edit.
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public partial class RankItem : UIItem
{
	public UIImageSwitch m_Com_UIImageSwitch_Bg;
	public GameObject m_GameObject_Rank1;
	public GameObject m_GameObject_Rank2;
	public GameObject m_GameObject_Rank3;
	public GameObject m_GameObject_Rank4;
	public TextMeshProUGUI m_TMP_Text_Rank;
	public Image m_Image_Head;
	public TextMeshProUGUI m_TMP_Text_UserName;
	public TextMeshProUGUI m_TMP_Text_Score;
	public List<GameObject> m_GameObject_RankList = new List<GameObject>();
	protected override void OnBind()
	{
		m_Com_UIImageSwitch_Bg = GetVaule<UIImageSwitch>("m_Com_UIImageSwitch_Bg");
		m_GameObject_Rank1 = GetVaule<GameObject>("m_GameObject_Rank1");
		m_GameObject_Rank2 = GetVaule<GameObject>("m_GameObject_Rank2");
		m_GameObject_Rank3 = GetVaule<GameObject>("m_GameObject_Rank3");
		m_GameObject_Rank4 = GetVaule<GameObject>("m_GameObject_Rank4");
		m_TMP_Text_Rank = GetVaule<TextMeshProUGUI>("m_TMP_Text_Rank");
		m_Image_Head = GetVaule<Image>("m_Image_Head");
		m_TMP_Text_UserName = GetVaule<TextMeshProUGUI>("m_TMP_Text_UserName");
		m_TMP_Text_Score = GetVaule<TextMeshProUGUI>("m_TMP_Text_Score");
		m_GameObject_RankList.Add(m_GameObject_Rank1);
		m_GameObject_RankList.Add(m_GameObject_Rank2);
		m_GameObject_RankList.Add(m_GameObject_Rank3);
		m_GameObject_RankList.Add(m_GameObject_Rank4);
	}
}
