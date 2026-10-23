//auto generic code don't edit.
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public partial class GetBoxPanel : UIPanel
{
	public GameObject m_GameObject_Container;
	public GameObject m_GameObject_Box;
	public Animator m_Animator_Box;
	public Image m_Image_Box;
	public Image m_Image_Box_1;
	public Image m_Image_Box_2;
	public GameObject m_GameObject_Card;
	public TextMeshProUGUI m_TMP_Text_CloseTips;
	public CardPropItem m_Item_CardPropItem_Temp;
	public Button m_Button_Close;
	public List<Image> m_Image_Box_List = new List<Image>();
	protected override void OnBind()
	{
		m_GameObject_Container = GetVaule<GameObject>("m_GameObject_Container");
		m_GameObject_Box = GetVaule<GameObject>("m_GameObject_Box");
		m_Animator_Box = GetVaule<Animator>("m_Animator_Box");
		m_Image_Box = GetVaule<Image>("m_Image_Box");
		m_Image_Box_1 = GetVaule<Image>("m_Image_Box_1");
		m_Image_Box_2 = GetVaule<Image>("m_Image_Box_2");
		m_GameObject_Card = GetVaule<GameObject>("m_GameObject_Card");
		m_TMP_Text_CloseTips = GetVaule<TextMeshProUGUI>("m_TMP_Text_CloseTips");
		m_Item_CardPropItem_Temp = InitUIItem<CardPropItem>("m_Item_CardPropItem_Temp");
		m_Button_Close = GetVaule<Button>("m_Button_Close");
		m_Image_Box_List.Add(m_Image_Box_1);
		m_Image_Box_List.Add(m_Image_Box_2);
	}
}
