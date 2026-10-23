using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace FrankGame
{
    public class JsonEncryption
    {
        private static byte[] Key = Encoding.UTF8.GetBytes("1234567856781234"); // 16字节密钥
        private static byte[] IV = Encoding.UTF8.GetBytes("5678123412345678"); // 16字节初始化向量

        public static void ApplyKeyAndValue(string key, string value)
        {
            Key = Encoding.UTF8.GetBytes(key);
            IV = Encoding.UTF8.GetBytes(value);
        }

        public static string Encrypt(string json)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = IV;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                        {
                            sw.Write(json);
                        }
                    }

                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        public static string Decrypt(string base64Encrypted)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = IV;

                byte[] bytes = Convert.FromBase64String(base64Encrypted);

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader sr = new StreamReader(cs))
                        {
                            return sr.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}