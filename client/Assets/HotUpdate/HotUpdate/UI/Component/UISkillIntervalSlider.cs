using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class UISkillIntervalSlider : MonoBehaviour,IBeginDragHandler,IEndDragHandler
{
    public Action beginDrag;
    public Action endDrag;

    public void OnBeginDrag(PointerEventData eventData)
    {
        beginDrag?.Invoke();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        endDrag?.Invoke();
    }
}
