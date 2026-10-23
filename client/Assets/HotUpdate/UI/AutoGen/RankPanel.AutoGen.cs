//auto generic code don't edit.
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public partial class RankPanel : UIPanel
{
	public GameObject m_GameObject_Container;
	public Image m_Image_AreanIcon;
	public TextMeshProUGUI m_TMP_Text_ArenaName;
	public Button m_Button_Close;
	public Button m_Button_Setting;
	public UIList m_List_RankItem;
	public GameObject m_GameObject_MyRank;
	public RankItem m_Item_RankItem_MySelf;
	protected override void OnBind()
	{
		m_GameObject_Container = GetVaule<GameObject>("m_GameObject_Container");
		m_Image_AreanIcon = GetVaule<Image>("m_Image_AreanIcon");
		m_TMP_Text_ArenaName = GetVaule<TextMeshProUGUI>("m_TMP_Text_ArenaName");
		m_Button_Close = GetVaule<Button>("m_Button_Close");
		m_Button_Setting = GetVaule<Button>("m_Button_Setting");
		//m_List_RankItem = InitUIList("m_List_RankItem", typeof(RankItem));
		m_GameObject_MyRank = GetVaule<GameObject>("m_GameObject_MyRank");
		m_Item_RankItem_MySelf = InitUIItem<RankItem>("m_Item_RankItem_MySelf");
	}
}
