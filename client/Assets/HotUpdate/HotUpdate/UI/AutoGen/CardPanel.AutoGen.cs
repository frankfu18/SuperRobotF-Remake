//auto generic code don't edit.
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using Spine.Unity;

public partial class CardPanel : UIPanel
{
	public GameObject m_GameObject_Container;
	public CardPropItem m_Item_CardPropItem_Temp;
	public CardSlotItem m_Item_CardSlotItem_Temp;
	public RectTransform m_RectTransform_Content;
	public RectTransform m_RectTransform_CardSlotRoot;
	public GameObject m_GameObject_TitleBox;
	public RectTransform m_RectTransform_CardBagRoot;
	public RectTransform m_RectTransform_SelectFrame;
	public TextMeshProUGUI m_TMP_Text_CardName;
	public TextMeshProUGUI m_TMP_Text_CardLevel;
	public RectTransform m_RectTransform_CardIcon;
	public SkeletonGraphic m_SkeletonGraphic_Card;
	public Button m_Button_Upgrade;
	public TextMeshProUGUI m_TMP_Text_UpgradeCost;
	public Button m_Button_Use;
	public Button m_Button_Remove;
	public Image m_Image_FragmentBar;
	public Image m_Image_FragmentFull;
	public TextMeshProUGUI m_TMP_Text_Fragment;
	public TextMeshProUGUI m_TMP_Text_Attack;
	public TextMeshProUGUI m_TMP_Text_AttackSpeed;
	public TextMeshProUGUI m_TMP_Text_HP;
	public TextMeshProUGUI m_TMP_Text_SkillDesc;
	public Button m_Button_Close;
	protected override void OnBind()
	{
		m_GameObject_Container = GetVaule<GameObject>("m_GameObject_Container");
		m_Item_CardPropItem_Temp = InitUIItem<CardPropItem>("m_Item_CardPropItem_Temp");
		m_Item_CardSlotItem_Temp = InitUIItem<CardSlotItem>("m_Item_CardSlotItem_Temp");
		m_RectTransform_Content = GetVaule<RectTransform>("m_RectTransform_Content");
		m_RectTransform_CardSlotRoot = GetVaule<RectTransform>("m_RectTransform_CardSlotRoot");
		m_GameObject_TitleBox = GetVaule<GameObject>("m_GameObject_TitleBox");
		m_RectTransform_CardBagRoot = GetVaule<RectTransform>("m_RectTransform_CardBagRoot");
		m_RectTransform_SelectFrame = GetVaule<RectTransform>("m_RectTransform_SelectFrame");
		m_TMP_Text_CardName = GetVaule<TextMeshProUGUI>("m_TMP_Text_CardName");
		m_TMP_Text_CardLevel = GetVaule<TextMeshProUGUI>("m_TMP_Text_CardLevel");
		m_RectTransform_CardIcon = GetVaule<RectTransform>("m_RectTransform_CardIcon");
		m_SkeletonGraphic_Card = GetVaule<SkeletonGraphic>("m_SkeletonGraphic_Card");
		m_Button_Upgrade = GetVaule<Button>("m_Button_Upgrade");
		m_TMP_Text_UpgradeCost = GetVaule<TextMeshProUGUI>("m_TMP_Text_UpgradeCost");
		m_Button_Use = GetVaule<Button>("m_Button_Use");
		m_Button_Remove = GetVaule<Button>("m_Button_Remove");
		m_Image_FragmentBar = GetVaule<Image>("m_Image_FragmentBar");
		m_Image_FragmentFull = GetVaule<Image>("m_Image_FragmentFull");
		m_TMP_Text_Fragment = GetVaule<TextMeshProUGUI>("m_TMP_Text_Fragment");
		m_TMP_Text_Attack = GetVaule<TextMeshProUGUI>("m_TMP_Text_Attack");
		m_TMP_Text_AttackSpeed = GetVaule<TextMeshProUGUI>("m_TMP_Text_AttackSpeed");
		m_TMP_Text_HP = GetVaule<TextMeshProUGUI>("m_TMP_Text_HP");
		m_TMP_Text_SkillDesc = GetVaule<TextMeshProUGUI>("m_TMP_Text_SkillDesc");
		m_Button_Close = GetVaule<Button>("m_Button_Close");
	}
}
