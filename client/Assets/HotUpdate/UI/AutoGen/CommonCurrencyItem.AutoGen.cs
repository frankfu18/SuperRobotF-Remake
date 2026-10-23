//auto generic code don't edit.
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public partial class CommonCurrencyItem : UIItem
{
	public Image m_Image_Icon;
	public Button m_Button_Add;
	public TextMeshProUGUI m_TMP_Text_Count;
	public TextMeshProUGUI m_TMP_Text_Time;
	protected override void OnBind()
	{
		m_Image_Icon = GetVaule<Image>("m_Image_Icon");
		m_Button_Add = GetVaule<Button>("m_Button_Add");
		m_TMP_Text_Count = GetVaule<TextMeshProUGUI>("m_TMP_Text_Count");
		m_TMP_Text_Time = GetVaule<TextMeshProUGUI>("m_TMP_Text_Time");
	}
}
