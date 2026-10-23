using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Button3D : Button, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public System.Action<bool> onExecuteClickCallback;

    private bool executeClick;

    protected override void Awake()
    {
        base.Awake();
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (executeClick)
        {
            onExecuteClickCallback?.Invoke(true);
            onExecuteClickCallback = null;

            base.OnPointerClick(eventData);
        }
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);

        executeClick = true;
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        executeClick = false;

        onExecuteClickCallback?.Invoke(false);
        onExecuteClickCallback = null;
    }

    public void OnDrag(PointerEventData eventData)
    {

    }

    public void OnEndDrag(PointerEventData eventData)
    {

    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        onExecuteClickCallback = null;
    }
}
