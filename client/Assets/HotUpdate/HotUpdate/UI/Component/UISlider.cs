using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectMask2D), typeof(RectTransform))]
public class UISlider : MonoBehaviour
{
    public Slider.Direction direction = Slider.Direction.LeftToRight;

    [Range(0f, 1f)]
    public float value = 1;

    private RectMask2D rectMask2D;
    private RectTransform rectTransform;
    private Vector4 orgPadding;
    private float lastValue = -1;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        rectMask2D = GetComponent<RectMask2D>();
        orgPadding = rectMask2D.padding;
    }

    private void Update()
    {
        if (value != lastValue)
        {
            lastValue = value;

            var padding = orgPadding;

            switch (direction)
            {
                case Slider.Direction.LeftToRight:
                    padding.z = (1 - value) * rectTransform.rect.width;
                    break;
                case Slider.Direction.RightToLeft:
                    padding.x = (1 - value) * rectTransform.rect.width;
                    break;
                case Slider.Direction.BottomToTop:
                    padding.w = (1 - value) * rectTransform.rect.height;
                    break;
                case Slider.Direction.TopToBottom:
                    padding.y = (1 - value) * rectTransform.rect.height;
                    break;
                default:
                    break;
            }

            rectMask2D.padding = padding;
        }
    }
}
