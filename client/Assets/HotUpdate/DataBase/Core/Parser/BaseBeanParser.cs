using System;
using System.Collections.Generic;
using System.Reflection;

namespace db.core
{
    public abstract class BaseBeanParser<T> : IParser where T : new()
    {

        protected T Datasource { get; private set; }

        public ParserContext Context { get; private set; }

        /// <summary>
        /// 自己的字段和反射
        /// </summary>
        private readonly Dictionary<string, FieldInfo> FieldDic = new();

        public FieldInfo ParentField { get; private set; }

        public bool IsNull() => Datasource == null;

        public void Init(ParserContext _context) {
            Context = _context;
            Init();
            OnLoaded();
        }

        private void Init() {
            var fields = typeof(T).GetFields();
            foreach (var field in fields) {
                if (field.Attributes != FieldAttributes.Public) {
                    continue;
                }
                FieldDic.Add(field.Name, field);
            }
            ParentField = Context.ParentField as FieldInfo;
            // 初始化数据
            Datasource = (T) ParentField?.GetValue(Context.GetDataSource<object>());
            OnLoaded();
        }

        public void RefreshDatasource() {
            Datasource = Context.GetDataSource<T>();
        }

        public void CreateDatasource() {
            // 初始化
            var datasource = (T) Activator.CreateInstance(typeof(T));
            // 设置
            Context.Set(datasource);
            Datasource = datasource;
            OnLoaded();
            Save();
        }

        public virtual void OnLoaded() {
            
        }

        public P GetParser<P>(object _fieldName) where P : IParser {
            // 检查 DataSource 是否存在，如果不存在，则忽略
            if (Datasource == null) {
                throw new Exception($"[{Context.ParentPath}.{ParentField.Name}][type={nameof(ParentField.FieldType)}] is null.");
            }
            if (_fieldName is not string name) {
                throw new ArgumentException($"[{Context.ParentPath}.{ParentField.Name}][type={nameof(ParentField.FieldType)}] 参数 _fieldName 不是字符串.");
            }
            if (!FieldDic.TryGetValue(name, out var fieldInfo)) {
                throw new Exception($"[{Context.ParentPath}.{ParentField.Name}.{_fieldName}] 属性不存在.");
            }
            // 如果是 dic
            if (DataBaseUtil.IsDic(fieldInfo.FieldType)) {
                return Context.RootParser.CreateDicParser<P>(this, fieldInfo);
            }
            if (DataBaseUtil.IsList(fieldInfo.FieldType)) {
                return Context.RootParser.CreateListParser<P>(this, fieldInfo);
            }
            // 如果是 bean
            if (DataBaseUtil.IsPlainClass(fieldInfo.FieldType)) {
                return Context.RootParser.CreateBeanParser<P>(this, fieldInfo);
            }
            
            throw new Exception($"[{Context.ParentPath}.{ParentField.Name}.{_fieldName}] 属性类型[{fieldInfo.FieldType}]不支持Parser.");
        }

        

        public void SetDataByKey(object _key, object _newData) {
            if (_key is not FieldInfo field) {
                throw new Exception($"[{Context.ParentPath}.{ParentField.Name}.{_key}] 属性错误."); 
            }
            if (Datasource == null) {
                CreateDatasource();
            }
            field.SetValue(Datasource, _newData);
            Save();
        }

        public D GetDataByKey<D>(object _key) {
            if (_key is not FieldInfo field || typeof(D).IsSubclassOf(field.FieldType)) {
                throw new Exception($"[{Context.ParentPath}.{ParentField.Name}.{_key}] 属性错误."); 
            }
            if (Datasource == null) {
                return default;
            }
            return (D) field.GetValue(Datasource);
        }
        
        public virtual void Save() {
            DataBaseUtil.SaveData(this);
        }
    }
}