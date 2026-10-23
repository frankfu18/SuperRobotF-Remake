using System.Collections;
using System.Collections.Generic;
using Coffee.UIExtensions;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Spine.Unity;

public class UIFieldDicMgr
{
	public static Dictionary<string, object> InitUIFieldCfg(GameObject go)
	{
		Dictionary<string, object> newObj = new Dictionary<string, object>();

		UIFieldCfg fieldCfg = go.GetComponent<UIFieldCfg>();
		newObj.Add("go", go);
		RectTransform t = go.GetComponent<RectTransform>();
		if (t == null)
		{
			newObj.Add("transform", go.transform);
		}
		else
		{
			newObj.Add("transform", t);
		}
		if (fieldCfg != null)
		{
			for (int i = 0; i < fieldCfg.fields.Length; i++)
			{
				UIFieldCfg.UICustomField field = fieldCfg.fields[i];
				if (field.go == null)
				{
					// Debugger.LogError(string.Format("No bound game object in the filed with index {0} under game object {1}", i, go.name));
					continue;
				}
				string fieldName = field.key;
				if (string.IsNullOrEmpty(fieldName))
				{
					fieldName = field.go.name;
				}
				switch (field.type)
				{
					case UIFieldCfg.FieldType.GameObject:
						newObj.Add(fieldName, field.go);
						break;
					case UIFieldCfg.FieldType.RectTransform:
						newObj.Add(fieldName, field.go.GetComponent<RectTransform>());
						break;
					case UIFieldCfg.FieldType.Text:
						newObj.Add(fieldName, field.go.GetComponent<Text>());
						break;
					case UIFieldCfg.FieldType.Button:
						newObj.Add(fieldName, field.go.GetComponent<Button>());
						break;
					case UIFieldCfg.FieldType.GridLayoutGroup:
						newObj.Add(fieldName, field.go.GetComponent<GridLayoutGroup>());
						break;
					case UIFieldCfg.FieldType.Image:
						newObj.Add(fieldName, field.go.GetComponent<Image>());
						break;
					case UIFieldCfg.FieldType.RawImage:
						newObj.Add(fieldName, field.go.GetComponent<RawImage>());
						break;
					case UIFieldCfg.FieldType.Dropdown:
						newObj.Add(fieldName, field.go.GetComponent<Dropdown>());
						break;
					case UIFieldCfg.FieldType.InputField:
						newObj.Add(fieldName, field.go.GetComponent<InputField>());
						break;
					case UIFieldCfg.FieldType.CanvasGroup:
						newObj.Add(fieldName, field.go.GetComponent<CanvasGroup>());
						break;
					case UIFieldCfg.FieldType.Slider:
						newObj.Add(fieldName, field.go.GetComponent<Slider>());
						break;
					case UIFieldCfg.FieldType.Toggle:
						newObj.Add(fieldName, field.go.GetComponent<Toggle>());
						break;
					case UIFieldCfg.FieldType.Canvas:
						newObj.Add(fieldName, field.go.GetComponent<Canvas>());
						break;
					case UIFieldCfg.FieldType.LineRender:
						newObj.Add(fieldName, field.go.GetComponent<LineRenderer>());
						break;
					case UIFieldCfg.FieldType.HorizontalLayoutGroup:
						newObj.Add(fieldName, field.go.GetComponent<HorizontalLayoutGroup>());
						break;
					case UIFieldCfg.FieldType.ScrollRect:
						newObj.Add(fieldName, field.go.GetComponent<ScrollRect>());
						break;
					case UIFieldCfg.FieldType.TextMeshProUGUI:
						newObj.Add(fieldName, field.go.GetComponent<TextMeshProUGUI>());
						break;
					case UIFieldCfg.FieldType.TMP_InputField:
						newObj.Add(fieldName, field.go.GetComponent<TMP_InputField>());
						break;
					case UIFieldCfg.FieldType.TMP_Dropdown:
						newObj.Add(fieldName, field.go.GetComponent<TMP_Dropdown>());
						break;
					case UIFieldCfg.FieldType.VerticalLayoutGroup:
						newObj.Add(fieldName, field.go.GetComponent<VerticalLayoutGroup>());
						break;
					case UIFieldCfg.FieldType.SkeletonGraphic:
						newObj.Add(fieldName, field.go.GetComponent<SkeletonGraphic>());
						break;
					case UIFieldCfg.FieldType.UIFieldCfg:
						Dictionary<string, object> viewInfo = InitUIFieldCfg(field.go);
						newObj.Add(fieldName, viewInfo);
						break;
					case UIFieldCfg.FieldType.LineRenderer:
						newObj.Add(fieldName, field.go.GetComponent<LineRenderer>());
						break;
					case UIFieldCfg.FieldType.CanvasScaler:
						newObj.Add(fieldName, field.go.GetComponent<CanvasScaler>());
						break;
					case UIFieldCfg.FieldType.UIItem:
						newObj.Add(fieldName, field.go.GetComponent<UIFieldCfg>());
						break;
					case UIFieldCfg.FieldType.UIList:
						newObj.Add(fieldName, field.go.GetComponent<UIList>());
						break;
					case UIFieldCfg.FieldType.UITab:
						//newObj.AddAndGets(fieldName, field.go.GetComponent<UITab>());
						break;
					case UIFieldCfg.FieldType.UISwitch:
						newObj.Add(fieldName, field.go.GetComponent<UISwitch>());
						break;
					case UIFieldCfg.FieldType.Animator:
						newObj.Add(fieldName, field.go.GetComponent<Animator>());
						break;
					case UIFieldCfg.FieldType.UIFrameAnimator:
						//newObj.AddAndGets(fieldName, field.go.GetComponent<UIFrameAnimator>());
						break;
					case UIFieldCfg.FieldType.UIParticle:
						newObj.Add(fieldName, field.go.GetComponent<UIParticle>());
						break;
					case UIFieldCfg.FieldType.UIEffect:
						//newObj.AddAndGets(fieldName, field.go.GetComponent<UIEffect>());
						break;
					case UIFieldCfg.FieldType.Component:
						newObj.Add(fieldName, field.go.GetComponent(System.Type.GetType(field.param)));
						break;
					case UIFieldCfg.FieldType.UIButton:
						//newObj.Add(fieldName, field.go.GetComponent<UIButton>());
						break;
                    case UIFieldCfg.FieldType.DynamicItem:
                        newObj.Add(fieldName, field.go.GetComponent<DynamicItem>());
                        break;
                    default:
						break;
				}
			}
		}

		return newObj;
	}

}
