using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(menuName = "ScriptableObject/TextTemplate", fileName = "New Text Template")]
public class TextTemplateScriptableObject : ScriptableObject
{
    public List<FontColorElement> FontColors = new List<FontColorElement>();
    public List<FontOutlineElement> FontOutlines = new List<FontOutlineElement>();
    public List<FontGradientElement> FontGradients = new List<FontGradientElement>();

    public Color GetFontColor(ETextFontColor _fontColor)
    {
        foreach (var col in FontColors)
        {
            if (col.fontColor == _fontColor)
            {
                return col.color;
            }
        }

        return Color.clear;
    }

    public Material GetOutlineMat(ETextFontOutline _outline)
    {
        foreach (var col in FontOutlines)
        {
            if (col.outline == _outline)
            {
                return col.material;
            }
        }

        return null;
    }

    public (Color,Color) GetGradientColor(ETextGradient _gradient)
    {
        foreach (var col in FontGradients)
        {
            if (col.gradient == _gradient)
            {
                return (col.topColor, col.bottomColor);
            }
        }

        return (Color.clear, Color.clear);
    }
}

[System.Serializable]
public class FontColorElement
{
    public ETextFontColor fontColor;

    [ColorUsage(true)]
    public Color color = Color.black;
}

[System.Serializable]
public class FontOutlineElement
{
    public ETextFontOutline outline;

    public Material material;
}

[System.Serializable]
public class FontGradientElement
{
    public ETextGradient gradient;

    [ColorUsage(true, true)]
    public Color topColor = Color.black;

    [ColorUsage(true, true)]
    public Color bottomColor = Color.black;
}


