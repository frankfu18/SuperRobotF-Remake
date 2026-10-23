//auto generic code don't edit.
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using Spine.Unity;

public partial class BattlePanel : UIPanel
{
	public GameObject m_GameObject_Container;
	public TextMeshProUGUI m_TMP_Text_Time;
	public TextMeshProUGUI m_TMP_Text_LiveCount;
	public RectTransform m_RectTransform_RankRoot;
	public BattleRankItem m_Item_BattleRankItem_Temp;
	public Image m_Image_Res;
	public TextMeshProUGUI m_TMP_Text_Res;
	public RectTransform m_RectTransform_Map;
	public RectTransform m_RectTransform_OtherRoot;
	public GameObject m_GameObject_Other;
	public Button m_Button_Start;
	public SkeletonGraphic m_SkeletonGraphic_FightStart;
	public Button m_Button_GM;
	public UIJoystick m_Com_UIJoystick_Touch;
	public LongClickButton m_Com_LongClickButton_Accelerate;
	public TextMeshProUGUI m_TMP_Text_Power;
	public RectTransform m_RectTransform_SelectBuff;
	public RectTransform m_RectTransform_BuffRoot;
	public BattleSelectBuffItem m_Item_BattleSelectBuffItem_Temp;
	protected override void OnBind()
	{
		m_GameObject_Container = GetVaule<GameObject>("m_GameObject_Container");
		m_TMP_Text_Time = GetVaule<TextMeshProUGUI>("m_TMP_Text_Time");
		m_TMP_Text_LiveCount = GetVaule<TextMeshProUGUI>("m_TMP_Text_LiveCount");
		m_RectTransform_RankRoot = GetVaule<RectTransform>("m_RectTransform_RankRoot");
		m_Item_BattleRankItem_Temp = InitUIItem<BattleRankItem>("m_Item_BattleRankItem_Temp");
		m_Image_Res = GetVaule<Image>("m_Image_Res");
		m_TMP_Text_Res = GetVaule<TextMeshProUGUI>("m_TMP_Text_Res");
		m_RectTransform_Map = GetVaule<RectTransform>("m_RectTransform_Map");
		m_RectTransform_OtherRoot = GetVaule<RectTransform>("m_RectTransform_OtherRoot");
		m_GameObject_Other = GetVaule<GameObject>("m_GameObject_Other");
		m_Button_Start = GetVaule<Button>("m_Button_Start");
		m_SkeletonGraphic_FightStart = GetVaule<SkeletonGraphic>("m_SkeletonGraphic_FightStart");
		m_Button_GM = GetVaule<Button>("m_Button_GM");
		m_Com_UIJoystick_Touch = GetVaule<UIJoystick>("m_Com_UIJoystick_Touch");
		m_Com_LongClickButton_Accelerate = GetVaule<LongClickButton>("m_Com_LongClickButton_Accelerate");
		m_TMP_Text_Power = GetVaule<TextMeshProUGUI>("m_TMP_Text_Power");
		m_RectTransform_SelectBuff = GetVaule<RectTransform>("m_RectTransform_SelectBuff");
		m_RectTransform_BuffRoot = GetVaule<RectTransform>("m_RectTransform_BuffRoot");
		m_Item_BattleSelectBuffItem_Temp = InitUIItem<BattleSelectBuffItem>("m_Item_BattleSelectBuffItem_Temp");
	}
}
