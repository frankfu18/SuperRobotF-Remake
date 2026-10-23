using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISafeArea : MonoBehaviour
{
    RectTransform Panel;
    Rect LastSafeArea = new Rect(0, 0, 0, 0);

    void Awake()
    {
        Panel = GetComponent<RectTransform>();
        Refresh();
    }

    void Update()
    {
        Refresh();
    }

    void Refresh()
    {
        Rect safeArea = GetSafeArea();

        if (safeArea != LastSafeArea)
            ApplySafeArea(safeArea);
    }

    Rect GetSafeArea()
    {
        return Screen.safeArea;
    }

    void ApplySafeArea(Rect r)
    {
        LastSafeArea = r;

        float widthRatio = r.width / Screen.width;
        float heightRatio = r.height / Screen.height;

        Vector2 anchorMax = new Vector2(widthRatio, heightRatio);

        Panel.anchorMax = anchorMax;
    }
}