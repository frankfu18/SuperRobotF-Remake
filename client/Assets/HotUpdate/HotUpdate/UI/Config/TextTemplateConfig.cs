using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TextTemplateConfig
{
    private static Dictionary<ETextFontColor, FontColorElement> FontColorDic;
    private static Dictionary<ETextFontOutline, FontOutlineElement> FontOutlineDic;
    private static Dictionary<ETextGradient, FontGradientElement> FontGradientDic;

    public static void InitTemplate(TextTemplateScriptableObject _template)
    {
        FontColorDic = new Dictionary<ETextFontColor, FontColorElement>();
        FontOutlineDic = new Dictionary<ETextFontOutline, FontOutlineElement>();
        FontGradientDic = new Dictionary<ETextGradient, FontGradientElement>();

        foreach (var item in _template.FontColors)
        {
            FontColorDic[item.fontColor] = item;
        }

        foreach (var item in _template.FontOutlines)
        {
            FontOutlineDic[item.outline] = item;
        }

        foreach (var item in _template.FontGradients)
        {
            FontGradientDic[item.gradient] = item;
        }
    }

    public static Color GetFontColor(ETextFontColor _fontColor)
    {
        Color col = Color.clear;
        bool find = false;
        if (FontColorDic == null)
        {
#if UNITY_EDITOR
            var template = UnityEditor.AssetDatabase.LoadAssetAtPath<TextTemplateScriptableObject>("Assets/Res/UI/Font/TextTemplate.asset");

            foreach (var item in template.FontColors)
            {
                if (item.fontColor == _fontColor)
                {
                    col = item.color;
                    find = true;
                    break;
                }
            }
#endif
        }
        else
        {
            if (FontColorDic.TryGetValue(_fontColor, out FontColorElement ele))
            {
                col = ele.color;
                find = true;
            }
        }

        if (!find)
        {
            Debug.LogError("字体颜色未找到：" + _fontColor);
        }

        return col;
    }

    public static Material GetOutlineMat(ETextFontOutline _outline)
    {
        Material mat = null;
        if (FontColorDic == null)
        {
#if UNITY_EDITOR
            var template = UnityEditor.AssetDatabase.LoadAssetAtPath<TextTemplateScriptableObject>("Assets/Res/UI/Font/TextTemplate.asset");

            foreach (var item in template.FontOutlines)
            {
                if (item.outline == _outline)
                {
                    mat = item.material;
                    break;
                }
            }
#endif
        }
        else
        {
            if (FontOutlineDic.TryGetValue(_outline, out FontOutlineElement ele))
            {
                mat = ele.material;
            }
        }

        if (mat == null)
        {
            Debug.LogError("字体描边颜色未找到：" + _outline);
        }

        return mat;
    }

    public static (Color, Color) GetGradientColor(ETextGradient _gradient)
    {
        (Color, Color) col = (Color.clear, Color.clear);
        bool find = false;
        if (FontColorDic == null)
        {
#if UNITY_EDITOR
            var template = UnityEditor.AssetDatabase.LoadAssetAtPath<TextTemplateScriptableObject>("Assets/Res/UI/Font/TextTemplate.asset");

            foreach (var item in template.FontGradients)
            {
                if (item.gradient == _gradient)
                {
                    col = (item.topColor, item.bottomColor);
                    find = true;
                    break;
                }
            }
#endif
        }
        else
        {
            if (FontGradientDic.TryGetValue(_gradient, out FontGradientElement ele))
            {
                col = (ele.topColor, ele.bottomColor);
                find = true;
            }
        }

        if (!find)
        {
            Debug.LogError("字体渐变颜色未找到：" + _gradient);
        }

        return col;
    }
}

/// <summary>
/// 文本样式
/// </summary>
public enum ETextStyle
{
    Custom = 1,
    Common = 10,//通用样式
    Common_Yellow = 11,//通用黄色
    Common_Yellow_Light = 12,//通用浅黄色
    Tag_Default = 20,//标签默认
    Tag_Select = 21,//标签选中
    Green = 30,//通用绿
    Red = 40,//通用红
    Button_Default = 50,//按钮默认文本样式
    Button_Disable = 51,//按钮不可点文本样式
    Button_Red = 52,//按钮货币不足文本样式
    PopUp_Title = 61,//弹板的标题
    Power_Green = 71,//通用绿
    Power_Red = 72,//通用红
    
    Guild_Dungeon_Name = 81,    //  公会副本名称
    
    Battle_Normal = 90,//战斗普通
    Battle_Critical = 91,//战斗暴击
}

/// <summary>
/// 字体大小
/// </summary>
public enum ETextFontSize
{
    Custom = 1,
    Size_24 = 24,
    Size_28 = 28,
    Size_36 = 36,
    Size_44 = 44,
    Size_48 = 48,
    Size_62 = 62,
}

/// <summary>
/// 字体颜色
/// </summary>
public enum ETextFontColor
{
    Custom = 1,
    White = 10,//白色
    Gray = 20,//灰色
    Yellow = 30,//黄色
    YellowLight = 31,//浅黄色
    YellowLight2 = 32,//浅黄色
    Red = 40,//红色
    Green = 50,//绿色
    BlueLight = 61,//浅蓝
    BlueLight2 = 62,//浅蓝
    Quality_White = 101,//品质白
    Quality_Green = 102,//品质绿
    Quality_Blue = 103,//品质蓝
    Quality_Purple = 104,//品质紫
    Quality_Red = 105,//品质红
    Quality_Orange = 106,//品质橙
    
}

/// <summary>
/// 字体描边
/// </summary>
public enum ETextFontOutline
{
    None = 0,
    Custom = 1,
    Black = 10,//黑色
    Red = 30,//红色
    RedDeep = 31,//深红色
    Blue = 40,//蓝色
    Brown = 50,//棕色
    Brown2 = 51,//棕色
    Quality_White = 101,//品质白
    Quality_Green = 102,//品质绿
    Quality_Blue = 103,//品质蓝
    Quality_Purple = 104,//品质紫
    Quality_Red = 105,//品质红
    Quality_Orange = 106,//品质橙
    
    Black_Normal = 110,
    Black_Critical = 111,
    Guild_Dungeon_Name = 201, //  公会副本名称
}

/// <summary>
/// 字体渐变
/// </summary>
public enum ETextGradient
{
    None = 0,
    Custom = 1,
    YellowRed = 10,//黄红
    YellowWhite = 11,//黄白
    PowerGreen = 21,//战力绿
    PowerRed = 22,//战力红
    
    Guild_Dungeon_Name = 31,  //  公会副本名称
    Battle_Normal = 40,
    Battle_Critical = 41,
}