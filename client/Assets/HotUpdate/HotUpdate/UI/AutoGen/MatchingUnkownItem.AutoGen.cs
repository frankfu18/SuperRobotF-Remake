//auto generic code don't edit.
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public partial class MatchingUnkownItem : UIItem
{
	public RectTransform m_RectTransform_Find;
	public RectTransform m_RectTransform_FindIcon;
	protected override void OnBind()
	{
		m_RectTransform_Find = GetVaule<RectTransform>("m_RectTransform_Find");
		m_RectTransform_FindIcon = GetVaule<RectTransform>("m_RectTransform_FindIcon");
	}
}
