using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIDrag : UIBehaviour, IEndDragHandler, IBeginDragHandler, IDragHandler, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    public System.Action<Vector2, Vector2> onDragFunc;
    public System.Action<Vector2> onBeginDragFunc;
    public System.Action<Vector2> onEndDragFunc;
    public System.Action<Vector2> onClickFunc;
    public System.Action<Vector2> onClickDownFunc;
    public System.Action<Vector2> onClickUpFunc;
    public System.Action<PointerEventData> onPointerDownFunc;

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        onBeginDragFunc?.Invoke(eventData.position);
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        onDragFunc?.Invoke(eventData.position, eventData.delta);
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        onEndDragFunc?.Invoke(eventData.position);
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        onClickFunc?.Invoke(eventData.position);
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        onClickDownFunc?.Invoke(eventData.position);
        onPointerDownFunc?.Invoke(eventData);
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        onClickUpFunc?.Invoke(eventData.position);
    }
}