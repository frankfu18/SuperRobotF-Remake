using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class UIDropdown : TMP_Dropdown
{
    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);

        var dropdownList = this.transform.Find("Dropdown List");
        if (dropdownList != null)
        {
            Canvas canvas = dropdownList.GetComponent<Canvas>();
            if (canvas != null)
            {
                canvas.sortingLayerName = "UI";
            }
        }
    }
}
