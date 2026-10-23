using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public static class DictionaryExtension
{
    #region Dictionary 扩展方法
    public static char[] separator = { '.' };
    public static string SeparatorStr = ".";
    public static object ValueByPath(this Dictionary<string, object> dic, string keyPath)
    {
        if (keyPath.Contains(SeparatorStr))
        {
            string[] keyList = keyPath.Split(separator, 2);
            Dictionary<string, object> tempDic = (Dictionary<string, object>)dic[keyList[0]];
            if (tempDic == null) return null;
            return tempDic.ValueByPath(keyList[1]);
        }
        else if (dic != null && dic.ContainsKey(keyPath))
        {
            return dic[keyPath];
        }
        else
        {
            return null;
        }
    }
    //value by path a.b.c
    public static T ValueBP<T>(this Dictionary<string, object> dic, string keyPath)
    {
        while (keyPath.Contains(SeparatorStr))
        {
            string[] keyList = keyPath.Split(separator, 2);
            Dictionary<string, object> tempDic = dic.ValueBT<Dictionary<string, object>>(keyList[0]);
            if (tempDic == null)
            {
                throw new Exception($"数据'{keyPath}'丢失！！");
                //return default(T);
            }
            dic = tempDic;
            keyPath = keyList[1];
            if (!keyPath.Contains(SeparatorStr))
            {
                return dic.ValueBT<T>(keyPath);
            }
        }
        return dic.ValueBT<T>(keyPath);
    }

    public static bool TryValueBT<T>(this Dictionary<string, object> dic, string keyPath, out T t)
    {
        t = default(T);
        if (dic != null && dic.ContainsKey(keyPath))
        {
            var item = dic[keyPath];
            if (item is T)
            {
                t = (T)item;

                return true;
            }
            else
            {
                if (!(item is Component))
                {
                    return false;
                }

                if (typeof(T) == typeof(GameObject))
                {
                    t = (T)(((Component)item).gameObject as object);

                    return true;
                }
                else if (typeof(T) == typeof(Transform))
                {
                    t = (T)(((Component)item).transform as object);

                    return true;
                }
                else if (typeof(T) == typeof(RectTransform))
                {
                    t = (T)(((Component)item).gameObject.GetComponent<RectTransform>() as object);

                    return true;
                }
                else if (typeof(T) == typeof(Component))
                {
                    t = (T)item;

                    return true;
                }
            }
        }

        return false;
    }

    //value by type
    public static T ValueBT<T>(this Dictionary<string, object> dic, string keyPath)
    {
        if (dic != null && dic.ContainsKey(keyPath))
        {
            var item = dic[keyPath];
            if (item is T)
            {
                return (T)item;
            }
            else
            {
                if(item == null)
                {
                    Debug.Log($"数据'{keyPath}'获取失败！！");
                    return default(T);
                }
                else if (!(item is Component))
                {
                    //当是cfg类型的时候获取gameobject加.go能取到，这里就不处理了   
                    Debug.Log($"数据'{keyPath}'获取失败，{item.GetType().ToString()}不是组件类型！！");
                    return default(T);
                }
                if (typeof(T) == typeof(GameObject))
                {
                    return (T)(((Component)item).gameObject as object);
                }
                else if (typeof(T) == typeof(Transform))
                {
                    return (T)(((Component)item).transform as object);
                }
                else if (typeof(T) == typeof(RectTransform))
                {
                    return (T)(((Component)item).gameObject.GetComponent<RectTransform>() as object);
                }
                else if (typeof(T) == typeof(Component))
                {
                    return (T)item;
                }
                else
                {
                    Debug.Log($"数据'{keyPath}'获取失败，{item.GetType().ToString()}不是组件类型！！");
                    return default(T);
                }
            }
        }
        else
        {
            Debug.Log($"数据'{keyPath}'丢失！！");
            return default(T);
            //throw new Exception($"数据'{keyPath}'丢失！！");
        }
    }

    /// <summary>
    /// 字典扩展 默认返回字典的第一个值
    /// </summary>
    /// <typeparam name="K"></typeparam>
    /// <typeparam name="V"></typeparam>
    /// <param name="dic"></param>
    /// <returns></returns>
    public static KeyValuePair<K, V> First<K, V>(this Dictionary<K, V> dic)
    {
        KeyValuePair<K, V> dicTemp = new KeyValuePair<K, V>();
        if (dic.Count > 0)
        {
            foreach (var item in dic)
            {
                dicTemp = item;
                return dicTemp;
            }
            return dicTemp;
        }
        else
        {
            return dicTemp;
        }
    }
    #endregion

}
