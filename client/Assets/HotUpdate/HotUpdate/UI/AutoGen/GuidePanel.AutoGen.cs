//auto generic code don't edit.
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public partial class GuidePanel : UIPanel
{
	public Button m_Button_Mask;
	public RectTransform m_RectTransform_Line;
	public GameObject m_GameObject_Dialog;
	public Image m_Image_DialogBg;
	public TextMeshProUGUI m_TMP_Text_Dialog;
	public GameObject m_GameObject_Hand;
	public Image m_Image_HandLight;
	public Button m_Button_Fake;
	protected override void OnBind()
	{
		m_Button_Mask = GetVaule<Button>("m_Button_Mask");
		m_RectTransform_Line = GetVaule<RectTransform>("m_RectTransform_Line");
		m_GameObject_Dialog = GetVaule<GameObject>("m_GameObject_Dialog");
		m_Image_DialogBg = GetVaule<Image>("m_Image_DialogBg");
		m_TMP_Text_Dialog = GetVaule<TextMeshProUGUI>("m_TMP_Text_Dialog");
		m_GameObject_Hand = GetVaule<GameObject>("m_GameObject_Hand");
		m_Image_HandLight = GetVaule<Image>("m_Image_HandLight");
		m_Button_Fake = GetVaule<Button>("m_Button_Fake");
	}
}
