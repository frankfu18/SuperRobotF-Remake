//auto generic code don't edit.
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public partial class BattleRankItem : UIItem
{
	public TextMeshProUGUI m_TMP_Text_Rank;
	public TextMeshProUGUI m_TMP_Text_Name;
	public TextMeshProUGUI m_TMP_Text_Score;
	protected override void OnBind()
	{
		m_TMP_Text_Rank = GetVaule<TextMeshProUGUI>("m_TMP_Text_Rank");
		m_TMP_Text_Name = GetVaule<TextMeshProUGUI>("m_TMP_Text_Name");
		m_TMP_Text_Score = GetVaule<TextMeshProUGUI>("m_TMP_Text_Score");
	}
}
