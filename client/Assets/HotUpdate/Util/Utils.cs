using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public static class Utils
{
    public static Color ToColor(string _htmlColor)
    {
        Color color;

        ColorUtility.TryParseHtmlString("#" + _htmlColor, out color);

        return color;
    }

    public static string ToJson<T>(T _obj)
    {
        return JsonUtility.ToJson(_obj, true);
    }

    public static T FromJson<T>(string _json)
    {
        return JsonUtility.FromJson<T>(_json);
    }

    public static string CreateMD5(string filePath)
    {
        try
        {
            using (var fileStream = new FileStream(filePath, FileMode.Open))
            {
                var len = (int)fileStream.Length;
                var data = new byte[len];

                fileStream.Read(data, 0, len);


                var md5 = new MD5CryptoServiceProvider();
                var hash = md5.ComputeHash(data);
                var result = "";

                foreach (var bt in hash)
                    result += Convert.ToString(bt, 16);

                return result;
            }
        }
        catch (FileNotFoundException e)
        {
            Debug.LogError(e.StackTrace);
            return "";
        }
    }

    public static string CreateMD5(byte[] bytes)
    {
        try
        {
            var md5 = new MD5CryptoServiceProvider();
            var hash = md5.ComputeHash(bytes);
            var result = "";

            foreach (var bt in hash)
                result += Convert.ToString(bt, 16);

            return result;
        }
        catch (FileNotFoundException e)
        {
            Debug.LogError(e.StackTrace);
            return "";
        }
    }

    public static string GetMd5(string data)
    {
        var md5 = new MD5CryptoServiceProvider();
        byte[] output = md5.ComputeHash(Encoding.ASCII.GetBytes(data));

        return BitConverter.ToString(output).Replace("-", "").ToLower();
    }

    public static Vector3 StringToVector3(string _str)
    {
        Vector3 result = new Vector3();

        _str = _str.Trim();
        _str = _str.Replace("(", "");
        _str = _str.Replace(")", "");

        string[] array = _str.Split(',');
        if (array.Length == 3)
        {
            result.x = Convert.ToSingle(array[0]) / 1000;
            result.y = Convert.ToSingle(array[1]) / 1000;
            result.z = Convert.ToSingle(array[2]) / 1000;
        }

        return result;
    }

    public static void FindChildren(Transform _root, Action<Transform> _filter)
    {
        _filter?.Invoke(_root);

        for (int i = 0; i < _root.childCount; i++)
        {
            var child = _root.GetChild(i);

            FindChildren(child, _filter);
        }
    }

    /// <summary>
    /// 从动态数组中查询对象是否存在
    /// </summary>
    /// <param name="findObject">要查询的目标对象</param>
    /// <param name="array">数组</param>
    /// <returns>true 包含，false 不包含</returns>
    public static bool Contains<T>(T findObject, params T[] array)  {
        if (array == null || array.Length == 0) {
            return false;
        }
        foreach (var o in array) {
            if (o.Equals(findObject)) {
                return true;
            }
        }
        return false;
    }


    public static void Iterator<T>(List<T> list, Action<T, int> callback, Func<T, int, bool> filter = null) {
        Iterator(list, (o, i) => {
            callback(o, i);
            return true;
        }, filter);
    }

    public static void Iterator<T>(List<T> list, Func<T, int, bool> callback, Func<T, int, bool> filter = null) {
        if (list == null) {
            return;
        }
        list = filter == null ? list : FilterList(list, filter);
        for (var i = 0;i < list.Count;i ++) {
            if (!callback.Invoke(list[i], i)) {
                break;
            }
        }
    }

    public static void Iterator(int count, Func<int, bool> callback) {
        if (count <= 0) {
            return;
        }
        for (var i = 0; i < count; i++) {
            if (!callback.Invoke(i)) {
                break;
            }
        }
    }

    public static void Iterator(int count, Action<int> callback) {
        if (count <= 0) {
            return;
        }
        for (var i = 0; i < count; i++) {
            callback.Invoke(i);
        }
    }

    /// <summary>
    /// 过滤数据
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    /// <param name="list">要过滤的数据列表</param>
    /// <param name="filter">过滤器</param>
    /// <returns></returns>
    public static List<T> FilterList<T>(List<T> list, Func<T, int, bool> filter) {
        if (filter == null || list == null) {
            return list;
        }
        var result = new List<T>();
        for (var i = 0; i < list.Count; i++) {
            if (filter(list[i], i)) {
                result.Add(list[i]);
            }
        }
        return result;
    }

    /// <summary>
    /// 过滤数组
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    /// <param name="array">数据数组</param>
    /// <param name="filter">过滤器</param>
    /// <returns>结果</returns>
    public static T[] FilterArray<T>(T[] array, Func<T, int, bool> filter) {
        if (filter == null || array == null) {
            return array;
        }
        var result = new List<T>();
        for (var i = 0; i < array.Length; i++) {
            if (filter(array[i], i)) {
                result.Add(array[i]);
            }
        }
        return result.ToArray();
    }
    public static void Iterator<T>(T[] list, Action<T, int> callback, Func<T, int, bool> filter = null) {
        Iterator(list, (o, i) => { 
            callback(o, i);
            return true;
        }, filter);
    }


   public static void Iterator<T>(T[] list, Func<T, int, bool> callback, Func<T, int, bool> filter = null) {
        if (list == null) {
            return;
        }
        list = filter == null ? list : FilterArray(list, filter);
        for (var i = 0; i < list.Length; i++) {
            if (!callback.Invoke(list[i], i)) {
                break;
            }
        }
    }

    public static void Iterator<T,K>(Dictionary<T,K> dic, Action<T, K> callback, Func<T, K, bool> filter = null) {
        if (dic == null || callback == null) {
            return;
        }
        foreach (var d in dic) {
            if (filter != null && !filter.Invoke(d.Key, d.Value)) {
                continue;
            }
            callback.Invoke(d.Key, d.Value);
        }
    }

    public static T EnumValueOf<T>(string name) {
        Type enumType = typeof(T);
        foreach (var e in System.Enum.GetValues(enumType)) {
            if (System.Enum.GetName(enumType, e).Equals(name)) {
                return (T)e;
            }
        }
        return default;
    }

    /// <summary>
    /// 移除字典中符合条件的数据
    /// </summary>
    /// <typeparam name="T1">key 类型</typeparam>
    /// <typeparam name="T2">value 类型</typeparam>
    /// <param name="dic">字典</param>
    /// <param name="filter">过滤函数</param>
    /// <param name="_removeCallback">移除元素回调</param>
    /// <returns>移除的数量</returns>
    public static int RemoveDic<T1,T2>(Dictionary<T1, T2> dic, Func<T1, T2, bool> filter, System.Action<T1, T2> _removeCallback = null) {
        int counter = 0;
        if (dic == null || filter == null) {
            return counter;
        }
        var keys = new List<T1>(dic.Keys);
        foreach (var key in keys) {
            if (filter(key, dic[key])) {
                counter++;
                _removeCallback?.Invoke(key, dic[key]);
                dic.Remove(key);
            }
        }
        return counter;
    }

    /// <summary>
    /// 移除特定元素
    /// </summary>
    /// <typeparam name="T">元素类型</typeparam>
    /// <param name="array">数组</param>
    /// <param name="filter">过滤函数</param>
    /// <returns>新的数组</returns>
    public static T[] Remove<T>(T[] array, Func<T, bool> filter) {
        if (array == null || array.Length == 0 || filter == null) {
            return array;
        }
        int index = 0;
        var list = array.ToList();
        while (index < list.Count) {
            if (filter.Invoke(list[index])) {
                list.RemoveAt(index);
            } else {
                index++;
            }
        }
        return list.ToArray();
    }

    public static Vector3[] GetPath(int len, Vector3 start, Vector3 end, Vector3 dir, float offset)
    {
        Vector3[] _path = new Vector3[len];
        var bezierControlPoint = (start + end) * 0.5f + (dir * offset);
        for (int i = 0; i < 20; i++)
        {
            var t = (i + 1) / (float)len;//归化到0~1范围
            _path[i] = GetBezierPoint(t, start, bezierControlPoint, end);//使用贝塞尔曲线的公式取得t时的路径点
        }
        return _path;
    }

    /// <param name="t">0到1的值，0获取曲线的起点，1获得曲线的终点</param>
    /// <param name="start">曲线的起始位置</param>
    /// <param name="center">决定曲线形状的控制点</param>
    /// <param name="end">曲线的终点</param>
    public static Vector3 GetBezierPoint(float t, Vector3 start, Vector3 center, Vector3 end)
    {
        return (1 - t) * (1 - t) * start + 2 * t * (1 - t) * center + t * t * end;
    }


    public static string Or(bool _condition,string _ok, string _no) {
        return _condition ? _ok : _no;
    }

    /// <summary>
    /// 打乱List列表
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    public static void ShuffleList<T>(List<T> list)
    {
        System.Random random = new System.Random();
        int n = list.Count;
        for (int i = n - 1; i > 0; i--)
        {
            int j = random.Next(i + 1);
            T temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }

    public static bool IsEmpty<T>(T[] array) {
        return array == null || array.Length == 0;
    }

    public static bool IsEmpty<T>(List<T> list) {
        return list == null || list.Count == 0;
    }
    
    /// <summary>
    /// 退出/重启
    /// </summary>
    public static void Quit(bool _restart = true)
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    
}
