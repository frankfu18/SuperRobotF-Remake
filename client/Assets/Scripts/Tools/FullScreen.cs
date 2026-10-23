using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullScreen : MonoBehaviour
{
    public Image Image;

    private float width;
    private float height;

    void Awake()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        float radio = Screen.height / rectTransform.sizeDelta.y;

        rectTransform.sizeDelta = new Vector2(Screen.width * radio, Screen.height);
    }
}
