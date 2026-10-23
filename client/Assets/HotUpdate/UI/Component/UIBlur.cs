using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasRenderer))]
public class UIBlur : MonoBehaviour
{
    public Graphic graphic;
    public Material blurMat;
    public bool blur;

    private Material orgiMat;

    private void Awake()
    {
        graphic = GetComponent<Graphic>();
    }

    private void Start()
    {
        SetBlur(blur);
    }

    public void SetBlur(bool _blur)
    {
        if (graphic == null || blurMat == null)
        {
            return;
        }

        blur = _blur;

        if (_blur)
        {
            if (graphic.material != blurMat)
            {
                orgiMat = graphic.material;
                graphic.material = blurMat;
            }
        }
        else
        {
            if (orgiMat != null)
            {
                graphic.material = orgiMat;
            }
        }
    }
}
