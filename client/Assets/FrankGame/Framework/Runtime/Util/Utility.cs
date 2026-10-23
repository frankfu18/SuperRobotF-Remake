using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using UnityEngine;

namespace FrankGame
{
    public static class Utility
    {
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
        
        public static void Error(this string str)
        {
            Debug.LogError($"<color=red> {str}</color>");
        }
    }
}