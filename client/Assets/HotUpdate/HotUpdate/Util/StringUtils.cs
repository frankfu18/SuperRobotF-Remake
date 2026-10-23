using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public static class StringUtils
{
    private static readonly string[] BigStrArray = new string[]
    {
        "", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" ,
        "AA","AB","AC","AD","AE","AF","AG","AH","AI","AJ","AK","AL","AM","AN","AO","AP","AQ","AR","AS","AT","AU","AV","AW","AX","AY","AZ",
        "BA","BB","BC","BD","BE","BF","BG","BH","BI","BJ","BK","BL","BM","BN","BO","BP","BQ","BR","BS","BT","BU","BV","BW","BX","BY","BZ",
        "CA","CB","CC","CD","CE","CF","CG","CH","CI","CJ","CK","CL","CM","CN","CO","CP","CQ","CR","CS","CT","CU","CV","CW","CX","CY","CZ"
    };

    private static StringBuilder stringBuilder = new StringBuilder();
    private const string Zero_Str = "0";
    private const string Point_Str = ".";

    public static string FormatWithType(double num, string formatType)
    {
        if (formatType == null)
        {
            return num.ToString();
        }
        switch (formatType)
        {
            case "p":
                return num.ToString("f1");
            case "pp":
                return num.ToString("f2");
            case "r":
                return num.ToString("P");
            case "k":
                return FormatFromNumber((long)num);
            case "s":
                return FormatFromNumberAbbreviation((long)num);
            case "tl":
                return FormatWithTimeDHHMMSS((int)num);
            case "ts":
                return FormatWithTimeHMMSS((int)num);
            default:
                return num.ToString();
        }
    }

    /// <summary>
    /// 数字格式化，三分符
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public static string FormatFromNumber(long num)
    {
        return num.ToString("N0");
    }

    public static string FormatFromNumber(int num)
    {
        return num.ToString("N0");
    }

    public static string[] NumberFormatSuffix = { "K", "M", "B", "T", "A", "AA" };
    /// <summary>
    /// 数字格式化，带后缀
    /// </summary>
    public static string FormatFromNumberAbbreviation(long num)
    {
        if (num < 1000)
        {
            return num.ToString();
        }

        string myStr = "";
        int length = num.ToString().Length;

        myStr += num / (long)Mathf.Pow(10, (length + 2) / 3 * 3 - 4) / (float)10;

        switch ((length - 1) / 3)
        {
            case 0:
                break;
            case 1:
                myStr += "K";
                break;
            case 2:
                myStr += "M";
                break;
            case 3:
                myStr += "B";
                break;
            case 4:
                myStr += "T";
                break;
            case 5:
                myStr += "aa";
                break;
            case 6:
                myStr += "ab";
                break;
            case 7:
                myStr += "ac";
                break;
        }

        return myStr;
    }

    /// <summary>
    /// K结尾显示
    /// </summary>
    public static string FormatNumberOnlyK(int number)
    {
        if (number <= 10000)
        {
            return number.ToString();
        }
        else
        {
            float kNumber = (float)number / 1000f;
            return kNumber.ToString("F0") + "k";
        }
    }

    public static string FormatWithTimeHMMSS(int num)
    {
        return TimeUtils.FormatSecondsToHhmmss(num);
    }

    public static string FormatWithTimeDHHMMSS(int num)
    {
        return TimeUtils.FormatSecondsToDhhmmss(num);
    }

    public static string SetRichText_Size(this string oriStr, int size)
    {
        return string.Format("<size={0}>{1}</size>", size, oriStr);
    }

    /// <summary>
    /// 富文本 设置指定颜色   要求 十六进制数值前加‘#’
    /// </summary>
    public static string SetRichText_Color(this string oriStr, string colorValue)
    {
        //string labelHead = "<color=";
        //if (!colorValue.StartsWith("#"))
        //    labelHead += "#";
        //return "<color=" + colorValue + ">" + oriStr + "</color>";
        return string.Format("<color={0}>{1}</color>", colorValue, oriStr);
    }

    static readonly string[] romaNums = {
            "", "I", "II", "III", "IV", "V",
            "VI", "VII", "VIII", "IX", "X",
            "XI", "XII", "XIII", "XIV", "XV",
            "XVI", "XVII", "XVIII", "XIX", "XX" };
    /// <summary>
    /// 整型转罗马数字
    /// </summary>
    public static string ToRomaNum(this int number)
    {
        if (number < romaNums.Length)
            return romaNums[number];
        return number.ToString();
    }

    /// <summary>
    /// String转int[]
    /// </summary>
    /// <param name="str"></param>
    /// <param name="splitChar"></param>
    /// <returns></returns>
    public static int[] StringToIntArray(string str, char splitChar)
    {
        string[] subStr = str.Split(splitChar);
        int[] intArray = new int[subStr.Length];
        for (int i = 0; i < subStr.Length; i++)
        {
            try
            {
                intArray[i] = int.Parse(subStr[i]);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                intArray[i] = 0;
            }
        }
        return intArray;
    }

    /// <summary>
    /// String[]转int[][]
    /// </summary>
    /// <param name="str"></param>
    /// <param name="splitChar"></param>
    /// <returns></returns>
    public static int[][] StringArrayToIntArrayArray(string[] strArray, char splitChar)
    {
        int[][] intArray = new int[strArray.Length][];
        for (int i = 0; i < strArray.Length; i++)
        {
            intArray[i] = StringToIntArray(strArray[i], splitChar);
        }
        return intArray;
    }

    /// <summary>
    /// 转Enum
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="str"></param>
    /// <returns></returns>
    public static T ToEnum<T>(this string str) where T : struct
    {
        if (Enum.TryParse(str, out T result))
            return result;
        else
            return default(T);
    }

    /// <summary>
    /// 获取 刚刚 | *天前 | *月前 .....
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    internal static string GetTimeAgo(long time, bool isMillisecond = false)
    {
        string timeAgo = string.Empty;

        if (time > TimeUtils.GetCurrentTimeSec())
        {
            return timeAgo;
        }

        if (isMillisecond)
            time /= 1000;

        DateTime date = new DateTime(1, 1, 1, 0, 0, 0).AddSeconds(TimeUtils.GetCurrentTimeSec() - time);
        if (date.Year - 1 > 0)
        {
            timeAgo = "年前";
        }
        else if (date.Month - 1 > 0)
        {
            timeAgo = "月前";
        }
        else if (date.Day - 1 > 0)
        {
            timeAgo = "天前";
        }
        else if (date.Hour > 0)
        {
            timeAgo = "小时前";
        }
        else if (date.Minute >= 5)
        {
            timeAgo = "分钟前";
        }
        else
        {
            timeAgo = "很久以前";
        }

        return timeAgo;
    }

    /// <summary>
    /// 123435454345400000000000 转 987.22E
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string BigDecimalNumberToFormatStr(string str)
    {
        string[] strArray = str.Split('.');
        string a = strArray[0];
        bool fu = false;
        if (a[0] == '-')
        {
            fu = true;
            a = a.Split('-')[1];
        }
        int length = a.Length;
        int bei = (int)((length - 1) / 3);
        if (bei > 0)
        {
            a = a.Insert(length - (bei * 3), ".");
            string[] aArray = a.Split('.');
            a = aArray[0] + "." + (aArray[1].Length >= 2 ? aArray[1].Substring(0, 2) : aArray[1]);

            a = a.TrimEnd('0');//去除多余0
            if (a.LastIndexOf('.') == a.Length - 1)
            {
                a = a.TrimEnd('.');
            }
        }
        else
        {
            a = a + "";
            if (strArray.Length == 2)
            {
                a = a + "." + (strArray[1].Length >= 2 ? strArray[1].Substring(0, 2) : strArray[1]);

                a = a.TrimEnd('0');//去除多余0
                if (a.LastIndexOf('.') == a.Length - 1)
                {
                    a = a.TrimEnd('.');
                }
            }
        }

        a = a + BigStrArray[bei];
        if (fu == true)
        {
            a = "-" + a;
        }

        return a;
    }

    /// <summary>
    /// 987.22E 转 123435454345400000000000
    /// </summary>
    /// <param name="_str"></param>
    /// <returns></returns>
    public static string BigDecimalFormatToNumberStr(string _str)
    {
        if (string.IsNullOrEmpty(_str))
        {
            return "0";
        }

        stringBuilder.Clear();

        int multiple = 0;
        int ratio = 0;
        int letterIndex = 0;
        int letterLen = 0;
        var chars = _str.ToCharArray();
        for (int i = chars.Length - 1; i >= 0; i--)
        {
            var c = chars[i];
            if (char.IsLetter(c))
            {
                EBigDecimalFlag flag;
                Enum.TryParse(c.ToString(), out flag);

                if (ratio == 0)
                {
                    multiple += (int)flag;
                }
                else
                {
                    multiple += (int)flag * ratio * 26;
                }

                ratio++;
                letterLen++;
            }
            else
            {
                letterIndex = i;
                break;
            }
        }

        multiple *= 3;

        if (letterLen > 0)//有字母
        {
            string newStr = _str.Substring(0, letterIndex + 1);
            int dianLen = 0;

            string[] dianSplits = _str.Split('.');
            if (dianSplits.Length == 2)//有小数点
            {
                dianLen = dianSplits[1].Length - letterLen;

                newStr = newStr.Replace(Point_Str, "");

                if (dianLen >= multiple)
                {
                    dianLen -= multiple;
                    multiple = 0;
                }
                else
                {
                    multiple -= dianLen;
                    dianLen = 0;
                }
            }

            if (dianLen > 0)
            {
                newStr = newStr.Insert(newStr.Length - dianLen, Point_Str);

                stringBuilder.Append(newStr);
            }
            else
            {
                stringBuilder.Append(newStr);

                for (int i = 0; i < multiple; i++)
                {
                    stringBuilder.Append(Zero_Str);
                }
            }

            return stringBuilder.ToString();
        }
        else
        {
            return _str;
        }
    }

    /// <summary>
    /// 替換文本中的顔色
    /// </summary>
    /// <param name="orgiStr"></param>
    /// <param name="newColor">#000000</param>
    /// <returns></returns>
    public static string ReplaceColor(string orgiStr, string newColor)
    {
        string filteredString = Regex.Replace(orgiStr, @"<color=#[0-9a-fA-F]{6}>(.*?)</color>", "<color=" + newColor + ">$1</color>");
        return filteredString;
    }

    /// <summary>
    /// 浮点型转百分比
    /// </summary>
    /// <param name="_number"></param>
    /// <returns></returns>
    public static string LongToRatio(long _number)
    {
        string str;

        long yushu = _number % 100;
        if (yushu > 0)
        {
            str = (_number / 100.0f).ToString("0.0") + "%";
        }
        else
        {
            str = (_number / 100).ToString() + "%";
        }

        return str;
    }
}
