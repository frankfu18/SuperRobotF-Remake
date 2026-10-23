//auto generic code don't edit.
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Spine.Unity;
using TMPro;

public partial class CardPropItem : UIItem
{
	public GameObject m_GameObject_Container;
	public GameObject m_GameObject_Lock;
	public Image m_Image_Icon;
	public Image m_Image_LockQuality;
	public GameObject m_GameObject_Info;
	public Image m_Image_Bg;
	public Image m_Image_Quality;
	public SkeletonGraphic m_SkeletonGraphic_Card;
	public TextMeshProUGUI m_TMP_Text_Level;
	public Image m_Image_FragmentBar;
	public Image m_Image_FragmentFull;
	public GameObject m_GameObject_RedPoint;
	public TextMeshProUGUI m_TMP_Text_Fragment;
	public Button m_Button_Click;
	protected override void OnBind()
	{
		m_GameObject_Container = GetVaule<GameObject>("m_GameObject_Container");
		m_GameObject_Lock = GetVaule<GameObject>("m_GameObject_Lock");
		m_Image_Icon = GetVaule<Image>("m_Image_Icon");
		m_Image_LockQuality = GetVaule<Image>("m_Image_LockQuality");
		m_GameObject_Info = GetVaule<GameObject>("m_GameObject_Info");
		m_Image_Bg = GetVaule<Image>("m_Image_Bg");
		m_Image_Quality = GetVaule<Image>("m_Image_Quality");
		m_SkeletonGraphic_Card = GetVaule<SkeletonGraphic>("m_SkeletonGraphic_Card");
		m_TMP_Text_Level = GetVaule<TextMeshProUGUI>("m_TMP_Text_Level");
		m_Image_FragmentBar = GetVaule<Image>("m_Image_FragmentBar");
		m_Image_FragmentFull = GetVaule<Image>("m_Image_FragmentFull");
		m_GameObject_RedPoint = GetVaule<GameObject>("m_GameObject_RedPoint");
		m_TMP_Text_Fragment = GetVaule<TextMeshProUGUI>("m_TMP_Text_Fragment");
		m_Button_Click = GetVaule<Button>("m_Button_Click");
	}
}
