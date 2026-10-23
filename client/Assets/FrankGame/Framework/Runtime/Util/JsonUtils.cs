using System.Collections.Generic;
using Newtonsoft.Json;

namespace FrankGame
{
    public static class JsonUtils
    {
        /// <summary>
        /// 加载数据
        /// </summary>
        /// <param name="_jsonPath"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T LoadObject<T>(string _jsonPath) => LoadObject<T>(_jsonPath, typeof(T));
        
        /// <summary>
        /// </summary>
        /// <param name="_jsonPath"></param>
        /// <param name="_dataType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T LoadObject<T>(string _jsonPath, System.Type _dataType) {
            if (string.IsNullOrEmpty(_jsonPath)) {
                return default;
            }
            return (T) JsonConvert.DeserializeObject(_jsonPath, _dataType);
        }

        /// <summary>
        /// 加载 Json List
        /// </summary>
        /// <param name="_jsonPath"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<T> LoadObjects<T>(string _jsonPath){
            return string.IsNullOrEmpty(_jsonPath) ? default : JsonConvert.DeserializeObject<List<T>>(_jsonPath);
        }

        /// <summary>
        /// 数据对象转换成字符串
        /// </summary>
        /// <param name="_data"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string ToJson<T>(T _data) {
            return JsonConvert.SerializeObject(_data);
        }
    }
}

