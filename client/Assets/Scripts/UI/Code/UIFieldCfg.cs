using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFieldCfg : MonoBehaviour
{
	public enum FieldType
	{
		GameObject = 0,
		RectTransform,
		Text,
		Image,
		RawImage,
		Button,
		UIFieldCfg,
		Slider,
		InputField,
		Toggle,
		GridLayoutGroup,
		Dropdown,
		CanvasGroup,
        Canvas,
		LineRender,
        ScrollRect,
        TextMeshProUGUI,
		HorizontalLayoutGroup,
		LineRenderer,
        ParticleSystem,
       	CanvasScaler, 
		ContentSizeFitter,
        MeshRenderer,
		VerticalLayoutGroup,
		SkeletonGraphic,
		UIItem,
		UIList,
		UISwitch,
        UITab,
        Animator,
        UIFrameAnimator,
        UIParticle,
        UIEffect,
		Component,
		TMP_InputField,
		TMP_Dropdown,
		UIButton,
        DynamicItem,
    }

	[System.Serializable]
	public class UICustomField
	{
		public FieldType type;
		public GameObject go;
		public string key;
		public string param;
		[HideInInspector] public int index;
	}

	[SerializeField]
	public UICustomField[] fields;

	public int Length
	{
		get
		{
			return fields != null ? fields.Length : 0;
		}
	}

	public GameObject GetObj(int i)
	{
		return i < fields.Length ? fields[i].go : null;
	}

	public string GetKey(int i)
	{
		return i < fields.Length && !string.IsNullOrEmpty(fields[i].key) ? fields[i].key : (GetObj(i) != null ? GetObj(i).name : null);
	}
}
