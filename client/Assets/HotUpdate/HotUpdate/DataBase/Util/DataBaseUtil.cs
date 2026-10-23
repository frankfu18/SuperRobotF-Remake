using System;
using System.Collections.Generic;
using db.core;

namespace db
{
    public static class DataBaseUtil
    {
        public static bool IsDic(Type t) => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Dictionary<,>);

        public static bool IsList(Type t) => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(List<>);

        public static bool IsDicKeyType(Type _type) => _type.IsPrimitive;


        public static bool IsPlainClass(Type _type) => !IsList(_type) && !IsDic(_type) && !_type.IsPrimitive ;

        public static string GetTableName(Type _type) => _type.Name;

        public static Dictionary<T, D> GetMonitorDicSource<T, D>(IDictionary<T, D> _source) {
            if (_source == null) {
                return null;
            }

            if (_source.GetType().IsGenericType && _source.GetType().GetGenericTypeDefinition() == typeof(MonitorDictionary<,>)) {
                return (Dictionary<T, D>) ((MonitorDictionary<T, D>)_source).GetDataSource();
            }
            return (Dictionary<T, D>)_source;
        }

        public static List<D> GetMonitorListSource<D>(IList<D> _source) {
            if (_source == null) {
                return null;
            }
            if (_source.GetType().IsGenericType && _source.GetType().GetGenericTypeDefinition() == typeof(MonitorList<>)) {
                return (List<D>) ((MonitorList<D>)_source).GetDataSource();
            }

            return (List<D>)_source;
        }


        public static T FindOneByArray<T>(T[] _array, Func<T, bool> _filter) {
            if (_array == null || _filter == null) {
                return default;
            }

            foreach (var a in _array) {
                if (_filter.Invoke(a)) {
                    return a;
                }
            }

            return default;
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="_dataParser">当前数据</param>
        public static void SaveData(IParser _dataParser) {
            DataBaseManager.Instance.SaveDatabase(_dataParser.Context.RootParser);
        }
    }
}