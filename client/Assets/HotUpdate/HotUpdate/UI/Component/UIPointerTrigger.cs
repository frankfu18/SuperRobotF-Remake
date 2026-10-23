using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 监测UI触摸时的行为，
/// 是否点击在当前UI及其子物体上，
/// 用以实现如触摸空白时关闭面板等需求
/// </summary>
public class UIPointerTrigger : MonoBehaviour
{
	private Action<bool> m_OnPointerDown;
	private Action<bool> m_OnPointerUp;

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Check(GetFristObject(), m_OnPointerDown);
		}
		else if (Input.GetMouseButtonUp(0))
		{
			Check(GetFristObject(), m_OnPointerUp);
		}
	}

	private GameObject GetFristObject()
	{
		PointerEventData eventData = new PointerEventData(UnityEngine.EventSystems.EventSystem.current);
		eventData.position = Input.mousePosition;
		eventData.pressPosition = Input.mousePosition;
		List<RaycastResult> results = new List<RaycastResult>();
		UnityEngine.EventSystems.EventSystem.current.RaycastAll(eventData, results);
		return results.Count > 0 ? results[0].gameObject : null;
	}

	private void Check(GameObject eventData, Action<bool> action)
	{
		Transform trans = null;
		if (eventData != null)
		{
			trans = eventData.transform;
			while (trans != null)
			{
				if (trans.gameObject == gameObject)
				{
					break;
				}
				else
				{
					trans = trans.parent;
				}
			}
		}
		action?.Invoke(trans != null);
	}

	public void SetPointerDownEvent(Action<bool> action)
	{
		m_OnPointerDown = action;
	}

	public void SetPointerUpEvent(Action<bool> action)
	{
		m_OnPointerDown = action;
	}
}