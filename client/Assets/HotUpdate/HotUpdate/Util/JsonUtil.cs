using UnityEngine;

public static class JsonUtil
{
    
    public static bool IsJson(string str)
    {
        return IsJsonObject(str) || IsJsonArray(str);
    }
    
    /// <summary>
    /// 检查是否为有效JSON（对象/数组）
    /// </summary>
    private static bool IsValidJson(string jsonString)
    {
        if (string.IsNullOrWhiteSpace(jsonString)) return false;
        try 
        {
            // 尝试解析为泛型对象
            JsonUtility.FromJson<object>(jsonString);
            return jsonString.StartsWith("{") || jsonString.StartsWith("[");
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// 检查是否为JSON对象
    /// </summary>
    private static bool IsJsonObject(string jsonString)
    {
        if (!IsValidJson(jsonString)) return false;
        try 
        {
            // 尝试解析为字典类型
            JsonUtility.FromJson<System.Collections.Generic.Dictionary<string, object>>(jsonString);
            return jsonString.Trim().StartsWith("{");
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// 检查是否为JSON数组
    /// </summary>
    private static bool IsJsonArray(string jsonString)
    {
        if (!IsValidJson(jsonString)) return false;
        try 
        {
            // 尝试解析为列表类型
            JsonUtility.FromJson<System.Collections.Generic.List<object>>(jsonString);
            return jsonString.Trim().StartsWith("[");
        }
        catch
        {
            return false;
        }
    }
}
