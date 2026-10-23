using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

/// <summary>
/// 双击
/// </summary>
[AddComponentMenu("UI/DoubleClickButton")]
public class DoubleClickButton : Button
{

    public Action onDoubleClick;
    public float interval = 0.4f;
    private float lastClickTime;

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (lastClickTime > 0 && lastClickTime + interval <= Time.time)
        {
            onDoubleClick?.Invoke();
            lastClickTime = 0;
        }
        else
        {
            lastClickTime = Time.time;
        }
    }

}
