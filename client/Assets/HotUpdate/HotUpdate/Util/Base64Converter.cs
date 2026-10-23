using System;
using System.Text;

public class Base64Converter
{

    public static string Encode(string input, string defaultVal = null)
    {
        try
        {
            var bytes = Encoding.UTF8.GetBytes(input);
            var base64 = Convert.ToBase64String(bytes);
            return base64;
        }
        catch
        {
            return defaultVal;
        }
    }

    public static string Decode(string encoded)
    {
        try
        {
            var originalBytes = Convert.FromBase64String(encoded);
            return Encoding.UTF8.GetString(originalBytes);
        }
        catch
        {
            return null; // 解码失败时返回
        }
    }
    
}