//auto generic code don't edit.
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public partial class UserPanel : UIPanel
{
	public GameObject m_GameObject_Container;
	public TextMeshProUGUI m_TMP_Text_Title;
	public Button m_Button_Close;
	public Button m_Button_Head;
	public Image m_Image_Head;
	public TextMeshProUGUI m_TMP_Text_UserName;
	public Button m_Button_ChangeName;
	public TextMeshProUGUI m_TMP_Text_Score;
	public Image m_Image_ArenaIcon;
	public TextMeshProUGUI m_TMP_Text_ArenaName;
	public TextMeshProUGUI m_TMP_Text_ArenaTotalCount;
	public TextMeshProUGUI m_TMP_Text_ArenaWinCount;
	public TextMeshProUGUI m_TMP_Text_ArenaLoseCount;
	public TextMeshProUGUI m_TMP_Text_ArenaWinRate;
	protected override void OnBind()
	{
		m_GameObject_Container = GetVaule<GameObject>("m_GameObject_Container");
		m_TMP_Text_Title = GetVaule<TextMeshProUGUI>("m_TMP_Text_Title");
		m_Button_Close = GetVaule<Button>("m_Button_Close");
		m_Button_Head = GetVaule<Button>("m_Button_Head");
		m_Image_Head = GetVaule<Image>("m_Image_Head");
		m_TMP_Text_UserName = GetVaule<TextMeshProUGUI>("m_TMP_Text_UserName");
		m_Button_ChangeName = GetVaule<Button>("m_Button_ChangeName");
		m_TMP_Text_Score = GetVaule<TextMeshProUGUI>("m_TMP_Text_Score");
		m_Image_ArenaIcon = GetVaule<Image>("m_Image_ArenaIcon");
		m_TMP_Text_ArenaName = GetVaule<TextMeshProUGUI>("m_TMP_Text_ArenaName");
		m_TMP_Text_ArenaTotalCount = GetVaule<TextMeshProUGUI>("m_TMP_Text_ArenaTotalCount");
		m_TMP_Text_ArenaWinCount = GetVaule<TextMeshProUGUI>("m_TMP_Text_ArenaWinCount");
		m_TMP_Text_ArenaLoseCount = GetVaule<TextMeshProUGUI>("m_TMP_Text_ArenaLoseCount");
		m_TMP_Text_ArenaWinRate = GetVaule<TextMeshProUGUI>("m_TMP_Text_ArenaWinRate");
	}
}
