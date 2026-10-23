//auto generic code don't edit.
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public partial class GetRewardPanel : UIPanel
{
	public GameObject m_GameObject_Container;
	public GameObject m_GameObject_Card;
	public CommonRewardItem m_Item_CommonRewardItem_Temp;
	public Button m_Button_Close;
	protected override void OnBind()
	{
		m_GameObject_Container = GetVaule<GameObject>("m_GameObject_Container");
		m_GameObject_Card = GetVaule<GameObject>("m_GameObject_Card");
		m_Item_CommonRewardItem_Temp = InitUIItem<CommonRewardItem>("m_Item_CommonRewardItem_Temp");
		m_Button_Close = GetVaule<Button>("m_Button_Close");
	}
}
