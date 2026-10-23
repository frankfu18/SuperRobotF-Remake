using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasRenderer))]
public class UIGray : MonoBehaviour
{
    public Graphic graphic;
    public Material grayMat;
    public bool gray;

    private Material orgiMat;

    private void Awake()
    {
        graphic = GetComponent<Graphic>();
    }

    private void Start()
    {
        SetGray(gray);
    }

    public void SetGray(bool _gray)
    {
        if(graphic == null)
        {
            graphic = GetComponent<Graphic>();
        }

        if (graphic == null || grayMat == null)
        {
            return;
        }

        gray = _gray;

        if (_gray)
        {
            if (graphic.material != grayMat)
            {
                orgiMat = graphic.material;
                graphic.material = grayMat;
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
