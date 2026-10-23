using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIPointerDrag : UIDrag, IPointerDownHandler, IPointerUpHandler
{
    private bool isHasClickDown = false;

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (isHasClickDown)
        {
            return;
        }

        isHasClickDown = true;
        onBeginDragFunc?.Invoke(eventData.position);
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        if (!isHasClickDown)
        {
            return;
        }
        isHasClickDown = false;

        onEndDragFunc?.Invoke(eventData.position);
    }
}