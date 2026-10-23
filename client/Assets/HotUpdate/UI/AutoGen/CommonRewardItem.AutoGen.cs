//auto generic code don't edit.
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public partial class CommonRewardItem : UIItem
{
	public Image m_Image_Quality;
	public GameObject m_GameObject_IconParent;
	public Image m_Image_Icon;
	public TextMeshProUGUI m_TMP_Text_Count;
	public Button m_Button_Click;
	protected override void OnBind()
	{
		m_Image_Quality = GetVaule<Image>("m_Image_Quality");
		m_GameObject_IconParent = GetVaule<GameObject>("m_GameObject_IconParent");
		m_Image_Icon = GetVaule<Image>("m_Image_Icon");
		m_TMP_Text_Count = GetVaule<TextMeshProUGUI>("m_TMP_Text_Count");
		m_Button_Click = GetVaule<Button>("m_Button_Click");
	}
}
