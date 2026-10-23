using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using UnityEngine;

namespace FrankGame
{
    public class ResCopyUtils
    {
        public static void Iterator<T>(T[] list, Action<T> callback, Func<T, bool> _filter = null) {
            if (list == null)
            {
                return;
            }
            list = _filter == null ? list : FilterArray(list, _filter);
            for (var i = 0; i < list.Length; i++)
            {
                callback?.Invoke(list[i]);
            }
        }
        public static T[] FilterArray<T>(T[] array, Func<T, bool> filter)
        {
            if (filter == null || array == null)
            {
                return array;
            }
            var result = new List<T>();
            for (var i = 0; i < array.Length; i++)
            {
                if (filter(array[i]))
                {
                    result.Add(array[i]);
                }
            }
            return result.ToArray();
        }
        public static string CreateMD5(string filePath)
        {
            try
            {
                using (var fileStream = new FileStream(filePath, FileMode.Open,FileAccess.Read))
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
    }
}