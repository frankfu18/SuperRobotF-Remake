using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static UnityEngine.UI.Button;

[RequireComponent(typeof(CanvasRenderer))]
public class EmptyButton : MaskableGraphic, IPointerClickHandler
{
    private ButtonClickedEvent m_OnClick = new ButtonClickedEvent();

    public ButtonClickedEvent onClick
    {
        get { return m_OnClick; }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        m_OnClick.Invoke();
    }

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();
    }
}