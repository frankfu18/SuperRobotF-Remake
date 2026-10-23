//auto generic code don't edit.
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public partial class CardSlotItem : UIItem
{
	public GameObject m_GameObject_Null;
	public GameObject m_GameObject_Lock;
	public TextMeshProUGUI m_TMP_Text_Lock;
	protected override void OnBind()
	{
		m_GameObject_Null = GetVaule<GameObject>("m_GameObject_Null");
		m_GameObject_Lock = GetVaule<GameObject>("m_GameObject_Lock");
		m_TMP_Text_Lock = GetVaule<TextMeshProUGUI>("m_TMP_Text_Lock");
	}
}
