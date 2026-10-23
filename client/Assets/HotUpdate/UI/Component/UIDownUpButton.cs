using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIDownUpButton : UIBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public System.Action<bool> downUpAction;

    public void OnPointerDown(PointerEventData eventData)
    {
        downUpAction?.Invoke(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        downUpAction?.Invoke(false);
    }
}