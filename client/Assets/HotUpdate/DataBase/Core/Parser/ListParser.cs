using System;
using System.Collections.Generic;

namespace db.core
{
    public class ListParser<D> : IParser 
    {
        public ParserContext Context { get; private set; }


        public MonitorList<D> Datasource { get; private set; }
        
        public bool IsNull() => Datasource == null;

        public void Init(ParserContext _context) {
            Context = _context;
            RefreshDatasource();
            OnLoaded();
        }

        public P GetParser<P>(object _idx) where P : IParser {
            var dataType = typeof(D);

            if (DataBaseUtil.IsDic(dataType)) {
                var args = dataType.GetGenericArguments();
                return Context.RootParser.CreateDicParser<P>(this, _idx, args[0], args[1]);
            }

            if (DataBaseUtil.IsList(dataType)) {
                var args = dataType.GetGenericArguments();
                return Context.RootParser.CreateListParser<P>(this, _idx, args[0]);
            }
            
            if (DataBaseUtil.IsPlainClass(dataType)) {
                return Context.RootParser.CreateBeanParser<P>(this, _idx, dataType);
            }
            
            throw new Exception($"[{Context.DataPath}] 数据类型[{nameof(dataType)}]不支持 Parser.");
        }

        public void CreateDatasource() {
            var data = new List<D>();
            Context.Set(data);
            Datasource = new MonitorList<D>(data);
            OnLoaded();
            Save();
        }

        
        public void RefreshDatasource() {
            var source = Context.GetDataSource<IList<D>>();
            if (source != null) {
                Datasource = new MonitorList<D>(source);
                Datasource.OnChanged += Save;
            } else {
                Datasource = null;
            }
        }
        
        public virtual void OnLoaded() {
            
        }

        public virtual void Save() {
            DataBaseUtil.SaveData(this);
        }

        public void Add(D _data) {
            Datasource.Add(_data);
        }

        public void Clear()
        {
            Datasource.Clear();
        }

        public void ForEach(Func<D, bool> _action) {
            if (_action == null || Datasource == null) {
                return;
            }
            foreach (var d in Datasource) {
                if (!_action.Invoke(d)) {
                    return;
                }
            }
        }

        public void ForEach(Action<D> _action) {
            if (_action == null || Datasource == null) {
                return;
            }

            foreach (var d in Datasource) {
                _action.Invoke(d);
            }
        }

        public void SetDataByKey(object _key, object _newData) {
            if (_key is not int) {
                throw new ArgumentException($"[key] 必须是 int 类型，当前是[{_key.GetType().Name}]");
            }
            if (_newData is not D data) {
                throw new ArgumentException($"[_newData] 参数类型错误，当前是[{_newData.GetType().Name}]，需要[{nameof(D)}]");
            }
            Datasource.Add(data);
        }

        public D1 GetDataByKey<D1>(object _key) {
            if (_key is not int idx) {
                throw new ArgumentException($"[key] 必须是 int 类型，当前是[{_key.GetType().Name}]");
            }

            if (Datasource.Count < idx) {
                throw new IndexOutOfRangeException($"数据数组越界[{Datasource.Count},{idx}]");
            }

            object datasource = Datasource[idx];
            
            return  (D1) datasource;
        }
    }
}