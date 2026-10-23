using System;
using System.Reflection;

namespace db.core
{
    public class DatabaseRootParser {

        public object Datasource { get; private set; }
        public Type DataType { get; private set; }

        public IParser RootParser { get; private set; }

        private static MethodInfo CreateRootParserMethodInfo;
        
        public virtual void Init<T>(T rootData, Type _beanType) where T : new() {
            Datasource = rootData;
            DataType = _beanType;
            if (!DataBaseManager.Instance.TryGetParserType(_beanType, out var type)) {
                throw new Exception($"[Root.{_beanType.Name}, {nameof(_beanType)}] 类型错误。");
            }

            InitMethodInfo();
            var methodInfo = CreateRootParserMethodInfo.MakeGenericMethod(type);
            RootParser = (IParser) methodInfo.Invoke(this, new object[] {_beanType});
            if (Datasource == null) {
                CreateDatabase();
            }
            RootParser.Context.SetSetter((_, data) => Datasource = data);
            RootParser.Context.SetGetter((_, _) => Datasource);
            RootParser.RefreshDatasource();
        }

        private static void InitMethodInfo() {
            if (CreateRootParserMethodInfo != null) {
                return;
            }
            CreateRootParserMethodInfo = typeof(DatabaseRootParser).GetMethod("CreateRootParser", BindingFlags.NonPublic | BindingFlags.Instance);
        }

        private void CreateDatabase() {
            Datasource = Activator.CreateInstance(DataType);
            Save();
        }

        public void Save() {
            
        }

        private P CreateRootParser<P>(Type _beanType) where P : IParser {
            return CreateBeanParser<P>(null, default, _beanType);
        }

        /// <summary>
        /// 创建一个 bean parser
        /// </summary>
        /// <param name="_parentParser"></param>
        /// <param name="_fieldInfo"></param>
        /// <typeparam name="P"></typeparam>
        /// <returns></returns>
        public P CreateBeanParser<P>(IParser _parentParser, FieldInfo _fieldInfo) where P : IParser  {
            var _parserContext = _parentParser.Context;
            if (DataBaseUtil.IsPlainClass(_fieldInfo.FieldType)) {
                throw new Exception($"[{_parserContext.ParentPath},{_fieldInfo.Name}, {nameof(_fieldInfo.FieldType)}] 类型错误。");
            }
            return CreateBeanParser<P>(_parentParser, _fieldInfo, _fieldInfo.FieldType);
        }

        /// <summary>
        /// 创建一个 Bean Parser
        /// </summary>
        /// <param name="_parentParser"></param>
        /// <param name="_key"></param>
        /// <param name="_beanType"></param>
        /// <typeparam name="P"></typeparam>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public P CreateBeanParser<P>(IParser _parentParser, object _key, Type _beanType) where P : IParser {
            var _parserContext = _parentParser == null ? new ParserContext() : _parentParser.Context;
            if (!DataBaseManager.Instance.TryGetParserType(_beanType, out var type)) {
                throw new Exception($"[{_parserContext.ParentPath},{_beanType.Name}, {nameof(_beanType)}] 类型错误。");
            }
            //检查映射
            if (type.IsSubclassOf(typeof(P))) {
                throw new Exception($"[{_parserContext.ParentPath},{_beanType}] 类型转换 [{typeof(P)},{nameof(type)}]错误。");
            }
            var parserObject = Activator.CreateInstance(type);
            if (parserObject is not P parser) {
                throw new Exception($"[{_parserContext.ParentPath},{_beanType}] 类型转换 [{typeof(P)},{parserObject.GetType()}]错误。");
            }
            // 初始化
            parser.Init(new ParserContext()
                    .SetParentParser(_parentParser)
                    .SetRootParser(this)
                    .SetParentField(_key)
                    .SetParentPath(_parentParser == null ? "" : _parentParser.Context.DataPath)
                    
            );
            if (_parentParser != null) {
                parser.Context.SetSetter(_parentParser.SetDataByKey).SetGetter((_, key) => _parentParser.GetDataByKey<object>(key));
                parser.RefreshDatasource();
            }


            return parser;
        }
        
        public P CreateDicParser<P>(IParser _parentParser, FieldInfo _fieldInfo) where P : IParser  {
            var _parserContext = _parentParser.Context;
            if (!DataBaseUtil.IsDic(_fieldInfo.FieldType)) {
                throw new Exception($"[{_parserContext.ParentPath},{_fieldInfo.Name}, {nameof(_fieldInfo.FieldType)}] 类型错误。");
            }
            var args = _fieldInfo.FieldType.GetGenericArguments();
            return CreateDicParser<P>(_parentParser, _fieldInfo, args[0], args[1]);
        }
        
        public P CreateDicParser<P>(IParser _parentParser, object _key, Type _keyType, Type _dataType) where P : IParser {
            var constructed = typeof(DictionaryParser<,>).MakeGenericType(_keyType, _dataType);
            var dicParser = (P) Activator.CreateInstance(constructed);
            dicParser.Init(new ParserContext()
                    .SetParentParser(_parentParser)
                    .SetParentField(_key)
                    .SetRootParser(this)
                    .SetParentPath(_parentParser.Context.DataPath)
                    .SetSetter(_parentParser.SetDataByKey)
                    .SetGetter((_, key) => _parentParser.GetDataByKey<object>(key))
            );
            if (dicParser.IsNull()) {
                dicParser.CreateDatasource();
            }
            return dicParser;
        }
        
        public P CreateListParser<P>(IParser _parentParser, FieldInfo _fieldInfo) where P : IParser {
            var _parserContext = _parentParser.Context;
            if (!DataBaseUtil.IsList(_fieldInfo.FieldType)) {
                throw new Exception($"[{_parserContext.ParentPath},{_fieldInfo.Name}, {nameof(_fieldInfo.FieldType)}] 类型错误。");
            }
            var args = _fieldInfo.FieldType.GetGenericArguments();
            return CreateListParser<P>(_parentParser, _fieldInfo, args[0]);
        }
        
        public P CreateListParser<P>(IParser _parentParser, object _key, Type _dataType) where P : IParser {
            var constructed = typeof(ListParser<>).MakeGenericType(_dataType);
            var listParser = (P) Activator.CreateInstance(constructed);
            listParser.Init(new ParserContext()
                    .SetParentParser(_parentParser)
                    .SetParentField(_key)
                    .SetRootParser(this)
                    .SetParentPath(_parentParser.Context.DataPath)
                    .SetSetter(listParser.SetDataByKey)
                    .SetGetter((p, key) => p.GetDataByKey<object>(key))
            );
            if (listParser.IsNull()) {
                listParser.CreateDatasource();
            }
            return listParser;
        }
        

    }
}