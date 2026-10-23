//auto generic code don't edit.
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public partial class UserInfoItem : UIItem
{
	public Image m_Image_Bg;
	public TextMeshProUGUI m_TMP_Text_UserName;
	public TextMeshProUGUI m_TMP_Text_ArenaName;
	public TextMeshProUGUI m_TMP_Text_Score;
	public Image m_Image_HeadBg;
	public Image m_Image_Head;
	public Image m_Image_ArenaIcon;
	public Image m_Image_ScoreIcon;
	protected override void OnBind()
	{
		m_Image_Bg = GetVaule<Image>("m_Image_Bg");
		m_TMP_Text_UserName = GetVaule<TextMeshProUGUI>("m_TMP_Text_UserName");
		m_TMP_Text_ArenaName = GetVaule<TextMeshProUGUI>("m_TMP_Text_ArenaName");
		m_TMP_Text_Score = GetVaule<TextMeshProUGUI>("m_TMP_Text_Score");
		m_Image_HeadBg = GetVaule<Image>("m_Image_HeadBg");
		m_Image_Head = GetVaule<Image>("m_Image_Head");
		m_Image_ArenaIcon = GetVaule<Image>("m_Image_ArenaIcon");
		m_Image_ScoreIcon = GetVaule<Image>("m_Image_ScoreIcon");
	}
}
