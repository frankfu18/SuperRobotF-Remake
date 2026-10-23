using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectMask2D))]
[ExecuteAlways]
public class UIMaskFill : MonoBehaviour
{
    public enum EFillMethod
    {
        Horizontal,
        Vertical,
    }

    public enum EOriginDirection
    {
        Top,
        Bottom,
        Left,
        Right,
    }

    public EFillMethod fillMethod;
    public EOriginDirection fillOriginDirection;

    [Range(0,1)]
    public float fillAmount = 1.0f;

    private RectMask2D rectMask;
    private float width;
    private float height;
    private float lastFillAmount = -1;

    void Update()
    {
        if(this.rectMask == null)
        {
            this.rectMask = this.GetComponent<RectMask2D>();
            this.width = ((RectTransform)this.transform).rect.width;
            this.height = ((RectTransform)this.transform).rect.height;
        }

        if(fillMethod == EFillMethod.Horizontal)
        {
            if(fillOriginDirection != EOriginDirection.Left && fillOriginDirection != EOriginDirection.Right)
            {
                fillOriginDirection = EOriginDirection.Left;
            }
        }
        else if(fillMethod == EFillMethod.Vertical)
        {
            if (fillOriginDirection != EOriginDirection.Top && fillOriginDirection != EOriginDirection.Bottom)
            {
                fillOriginDirection = EOriginDirection.Top;
            }
        }

        if (lastFillAmount != fillAmount)
        {
            SetFillAmount(fillAmount);
            lastFillAmount = fillAmount;
        }
    }

    private void SetFillAmount(float amount)
    {
        this.fillAmount = Mathf.Clamp01(amount);

        Vector4 padding = this.rectMask.padding;

        switch (this.fillMethod)
        {
            case EFillMethod.Horizontal:
                {
                    float fillWidth = this.width * this.fillAmount;
                    switch (this.fillOriginDirection)
                    {
                        case EOriginDirection.Left:
                            padding.x = 0;
                            padding.z = this.width - fillWidth;
                            break;
                        case EOriginDirection.Right:
                            padding.x = this.width - fillWidth;
                            padding.z = 0;
                            break;
                    }
                }
                break;
            case EFillMethod.Vertical:
                {
                    float fillHeight = this.height * this.fillAmount;
                    switch (this.fillOriginDirection)
                    {
                        case EOriginDirection.Top:
                            padding.y = 0;
                            padding.w = this.height - fillHeight;
                            break;
                        case EOriginDirection.Bottom:
                            padding.y = this.height - fillHeight;
                            padding.w = 0;
                            break;
                    }
                }
                break;
        }

        this.rectMask.padding = padding;
    }
}
