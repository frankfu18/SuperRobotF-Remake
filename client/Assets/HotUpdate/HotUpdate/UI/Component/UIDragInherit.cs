using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIDragInherit : MonoBehaviour, IEndDragHandler, IBeginDragHandler, IDragHandler
{
    public enum EDragDirection
    {
        All,
        Horizontal,
        Vertical,
    }

    public GameObject InheritGameObject;
    public EDragDirection Direction;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (Direction == EDragDirection.All
            || (Direction == EDragDirection.Horizontal && eventData.delta.x != 0)
            || (Direction == EDragDirection.Vertical && eventData.delta.y != 0))
        {
            ExecuteEvents.Execute(InheritGameObject, eventData, ExecuteEvents.beginDragHandler);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Direction == EDragDirection.All
            || (Direction == EDragDirection.Horizontal && eventData.delta.x != 0)
            || (Direction == EDragDirection.Vertical && eventData.delta.y != 0))
        {
            ExecuteEvents.Execute(InheritGameObject, eventData, ExecuteEvents.dragHandler);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (Direction == EDragDirection.All
            || (Direction == EDragDirection.Horizontal && eventData.delta.x != 0)
            || (Direction == EDragDirection.Vertical && eventData.delta.y != 0))
        {
            ExecuteEvents.Execute(InheritGameObject, eventData, ExecuteEvents.endDragHandler);
        }
    }
}
