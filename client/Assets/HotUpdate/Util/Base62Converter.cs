using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Assertions;
using Random = System.Random;

public static class Base62Converter
{
    // Base62 字符集: 0-9, A-Z, a-z
    private const string Base62Chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
    private const int Base = 62;

    /// <summary>
    /// 将字符串编码为Base62格式
    /// </summary>
    /// <param name="input">要编码的字符串</param>
    /// <param name="defaultValue">编码失败时返回的默认值（默认为空字符串）</param>
    /// <returns>Base62编码的字符串，如果编码失败则返回defaultValue</returns>
    public static string Encode(string input, string defaultValue = "")
    {
        try
        {
            if (string.IsNullOrEmpty(input))
                return defaultValue;
                
            // 使用UTF-8编码将字符串转换为字节数组
            byte[] bytes = Encoding.UTF8.GetBytes(input);
            
            // 将字节数组转换为Base62字符串
            var result = new StringBuilder();
            
            // 处理空输入
            if (bytes.Length == 0)
                return defaultValue;
                
            // 处理每个字节
            foreach (byte b in bytes)
            {
                // 将每个字节转换为两个Base62字符（0-3843）
                int value = b;
                if (value >= Base62Chars.Length * Base62Chars.Length)
                    value = Base62Chars.Length * Base62Chars.Length - 1;
                    
                result.Append(Base62Chars[value / Base62Chars.Length]);
                result.Append(Base62Chars[value % Base62Chars.Length]);
            }
            
            return result.ToString();
        }
        catch
        {
            return defaultValue;
        }
    }

    /// <summary>
    /// 将Base62字符串解码为原始字符串
    /// </summary>
    /// <param name="base62String">要解码的Base62字符串</param>
    /// <param name="defaultValue">解码失败时返回的默认值（默认为空字符串）</param>
    /// <returns>解码后的原始字符串，如果解码失败则返回defaultValue</returns>
    public static string Decode(string base62String, string defaultValue = "")
    {
        try
        {
            if (string.IsNullOrEmpty(base62String))
                return defaultValue;
                
            // 检查长度是否为偶数（因为每个字节被编码为两个字符）
            if (base62String.Length % 2 != 0)
                throw new ArgumentException("Base62字符串长度必须是偶数");
            
            var byteList = new List<byte>();
            
            // 每两个字符解码为一个字节
            for (int i = 0; i < base62String.Length; i += 2)
            {
                if (i + 1 >= base62String.Length)
                    throw new ArgumentException("Base62字符串格式无效");
                    
                char c1 = base62String[i];
                char c2 = base62String[i + 1];
                
                int value1 = Base62Chars.IndexOf(c1);
                int value2 = Base62Chars.IndexOf(c2);
                
                if (value1 == -1 || value2 == -1)
                    throw new ArgumentException($"Base62字符串中包含无效字符: {(value1 == -1 ? c1 : c2)}");
                
                // 将两个Base62字符转换回字节值
                byte b = (byte)((value1 * Base62Chars.Length) + value2);
                byteList.Add(b);
            }
            
            // 将字节数组转换回字符串
            return Encoding.UTF8.GetString(byteList.ToArray());
        }
        catch
        {
            return defaultValue;
        }
    }
    
    #region 测试

    public static void Test()
    {
        EncodeDecode_RandomStrings_ShouldMatch();
    }

    private static void EncodeDecode_RandomStrings_ShouldMatch()
    {
        Debug.Log(Encode("ABCD"));
        var random = new Random();
        var urlSafeRegex = new Regex(@"^[a-zA-Z0-9\-_\.~]+$");
        for (int i = 0; i < 10000; i++)
        {
            int length = random.Next(1, 11);
            string original = GenerateRandomString(length, random);
            string encoded = Encode(original);
            Assert.IsTrue(encoded.Trim().Length > 0 && urlSafeRegex.IsMatch(encoded), $"检测到非URL安全字符: '{encoded}', 原始输入: {original}\n编码结果: {encoded}");
            string decoded = Decode(encoded);
            Assert.AreEqual(original, decoded);
            Debug.Log($"original: {original}, encoded: {encoded}, decoded: {decoded}");
        }
    }

    private static string GenerateRandomString(int length, Random random)
    {
        var sb = new StringBuilder(length);
        // 扩展随机范围到16种可能性
        var next = random.Next(0, 12);
        for (int i = 0; i < length; i++)
        {
            switch (next)
            {
                case 0: // 数字
                    sb.Append((char)random.Next(48, 58));
                    break;
                case 1: // 大写英文
                case 2: // 大写英文（增加概率）
                    sb.Append((char)random.Next(65, 91));
                    break;
                case 3: // 小写英文
                case 4: // 小写英文（增加概率）
                    sb.Append((char)random.Next(97, 123));
                    break;
                case 5: // 中文
                    sb.Append((char)random.Next(0x4E00, 0x9FFF));
                    break;
                case 6: // 日文
                    sb.Append((char)random.Next(0x3040, 0x309F));
                    break;
                case 7: // 韩文
                    sb.Append((char)random.Next(0xAC00, 0xD7AF));
                    break;
                case 8: // Emoji
                    sb.Append(char.ConvertFromUtf32(random.Next(0x1F600, 0x1F64F)));
                    break;
                case 9: // 特殊符号
                    sb.Append((char)random.Next(33, 48));
                    break;
                case 10: // 越南语
                    sb.Append((char)random.Next(0x1EA0, 0x1EFF));
                    break;
                case 11: // 拉丁扩展
                    sb.Append((char)random.Next(0x00C0, 0x017F));
                    break;
                default: // 混合字符（12-15概率生成组合字符）
                    // 50%概率生成英文字母+其他字符组合
                    if (random.Next(0, 2) == 0)
                    {
                        sb.Append((char)random.Next(65, 91)); // 大写英文
                    }
                    else
                    {
                        // 从非英文字符中随机选择
                        switch (random.Next(0, 7))
                        {
                            case 0: sb.Append((char)random.Next(48, 58)); break;
                            case 1: sb.Append((char)random.Next(0x4E00, 0x9FFF)); break;
                            case 2: sb.Append((char)random.Next(0x3040, 0x309F)); break;
                            case 3: sb.Append((char)random.Next(0xAC00, 0xD7AF)); break;
                            case 4: sb.Append(char.ConvertFromUtf32(random.Next(0x1F600, 0x1F64F))); break;
                            case 5: sb.Append((char)random.Next(0x1EA0, 0x1EFF)); break;
                            case 6: sb.Append((char)random.Next(0x00C0, 0x017F)); break;
                        }
                    }
                    break;
            }
        }
        return sb.ToString();
    }

    private static string EscapeNonAscii(string input)
    {
        var sb = new StringBuilder();
        foreach (char c in input)
        {
            if (c > 127)
                sb.AppendFormat("\\u{0:X4}", (int)c);
            else
                sb.Append(c);
        }
        return sb.ToString();
    }
    #endregion

}