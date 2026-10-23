//auto generic code don't edit.
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public partial class HeadItem : UIItem
{
	public Image m_Image_Head;
	public GameObject m_GameObject_Currenct;
	public Button m_Button_Click;
	protected override void OnBind()
	{
		m_Image_Head = GetVaule<Image>("m_Image_Head");
		m_GameObject_Currenct = GetVaule<GameObject>("m_GameObject_Currenct");
		m_Button_Click = GetVaule<Button>("m_Button_Click");
	}
}
