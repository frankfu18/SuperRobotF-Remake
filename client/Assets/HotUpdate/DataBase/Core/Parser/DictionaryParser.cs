using System;
using System.Collections;
using System.Collections.Generic;

namespace db.core
{
    public class DictionaryParser<ID, T> : IParser , IEnumerable<KeyValuePair<ID, T>>{
        public ParserContext Context { get; private set; }
        
        public MonitorDictionary<ID, T> Datasource { get; private set; }

        public bool IsNull() => Datasource == null;
        
        public void Init(ParserContext _context) {
            Context = _context;
            RefreshDatasource();
            OnLoaded();
        }

        
        public void RefreshDatasource() {
            var source = Context.GetDataSource<IDictionary<ID, T>>();
            if (source != null) {
                Datasource = new MonitorDictionary<ID, T>(source);
                Datasource.OnChanged += Save;
            }else {
                Datasource = null;
            }
        }
        
        public P GetParser<P>(object _idx) where P : IParser {
            if (_idx is not ID || !DataBaseUtil.IsDicKeyType(_idx.GetType())) {
                throw new Exception($"[{Context.DataPath}] 字典 Key 类型错误[{nameof(ID)},{_idx.GetType().Name}].");
            }
            var _vType = typeof(T);
            if (_vType.IsPrimitive) {
                throw new Exception($"[{Context.DataPath}] 字典 Key 类型是基本类型[{nameof(_vType)}]不支持获取parser.");
            }
            
            // 处理字典类型
            if (DataBaseUtil.IsDic(_vType)) {
                var types = _vType.GetGenericArguments();
                return Context.RootParser.CreateDicParser<P>(this, _idx, types[0], types[1]);
            }
            // 列表
            if (DataBaseUtil.IsList(_vType)) {
                var types = _vType.GetGenericArguments();
                return Context.RootParser.CreateListParser<P>(this, _idx, types[0]);
            }
            
            // 处理 bean 类型
            if (DataBaseUtil.IsPlainClass(_vType)) {
                return Context.RootParser.CreateBeanParser<P>(this, _idx, _vType);
            }

            throw new Exception($"[{Context.DataPath}] 字典数据类型[{nameof(_vType)}]不支持Parser.");
            
        }

        public void ForEach(Func<ID, T, bool> _action) {
            if (Datasource == null || _action == null) {
                return;
            }

            foreach (var kp in Datasource) {
                if (!_action.Invoke(kp.Key, kp.Value)) {
                    return;
                }
            }
        }
        
        public void ForEach(Action<ID, T> _action) {
            if (Datasource == null || _action == null) {
                return;
            }

            foreach (var kp in Datasource) {
                _action.Invoke(kp.Key, kp.Value);
            }
        }

        public  void CreateDatasource() {
            var source = new Dictionary<ID, T>();
            Datasource = new MonitorDictionary<ID, T>(source);
            Datasource.OnChanged += Save;
            Context.Set(source);
            OnLoaded();
            Save();
        }

        public virtual void OnLoaded() {
            
        }

        public virtual void Save() {
            DataBaseUtil.SaveData(this);
        }

        /// <summary>
        /// 尝试获取
        /// </summary>
        /// <param name="_key"></param>
        /// <param name="_parser"></param>
        /// <typeparam name="P"></typeparam>
        /// <returns></returns>
        public bool TryGet<P>(ID _key, out P _parser) where P : IParser {
            _parser = default;
            if (!ContainsKey(_key)) {
                return false;
            }
            _parser = GetParser<P>(_key);
            return true;
        }

        public bool TryGetValue(ID _key, out T _data) => Datasource.TryGetValue(_key, out _data);

        public void Add(ID _key, T _data) {
            Datasource.Add(_key, _data);
        }

        public void Remove(ID _key) {
            Datasource.Remove(_key);
        }

        public void Clear() {
            Datasource?.Clear();
        }

        public bool ContainsKey(ID _key) => Datasource.ContainsKey(_key);

        public int Count => Datasource.Count;

        public void SetDataByKey(object _key, object _newData) {
            Datasource[(ID) _key] = (T) _newData;
        }

        public bool RemoveDataByKey(object _key) {
           return Datasource.Remove((ID)_key);
        }

        public D GetDataByKey<D>(object _key) {
            // 先获取转换成 object
            object datasource = Datasource.TryGetValue((ID)_key, out var data) ? data : default;
            
            return  (D) datasource;
        }

        public IEnumerator<KeyValuePair<ID, T>> GetEnumerator() => Datasource.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => Datasource.GetEnumerator();
    }
}