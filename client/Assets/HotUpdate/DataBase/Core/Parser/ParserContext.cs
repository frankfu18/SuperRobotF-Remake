using System;
using System.Reflection;

namespace db.core
{
    public class ParserContext
    {
        public string ParentPath { get; private set; }
        public object ParentField { get; private set; }

        private Action<object, object> Setter;

        private Func<IParser, object, object> Getter;

        public DatabaseRootParser RootParser { get; private set; }

        public IParser ParentParser { get; private set; }


        private string m_DataPath;

        public string DataPath => m_DataPath ??= GetDataPath();

        public ParserContext() {
            ParentPath = "";
        }


        private string GetDataPath() {
            string path;
            if (ParentField is FieldInfo info) {
                path = info.Name;
            } else {
                path = $"[{ParentField}]";
            }
            return  ParentPath == null ? path : $"{ParentPath}.{path}";
        }

        public ParserContext SetParentPath(string _parentPath) {
            ParentPath = _parentPath;
            return this;
        }

        public ParserContext SetParentField(object _parentField) {
            ParentField = _parentField;
            return this;
        }

        public ParserContext SetSetter(Action<object, object> _setter) {
            Setter = _setter;
            return this;
        }

        public ParserContext SetGetter(Func<IParser, object, object> _getter) {
            Getter = _getter;
            return this;
        }


        public ParserContext SetParentParser(IParser _parentParser) {
            ParentParser = _parentParser;
            return this;
        }

        public ParserContext SetRootParser(DatabaseRootParser _rootParser) {
            RootParser = _rootParser;
            return this;
        }

        public void Set(object newData) {
            Setter?.Invoke(ParentField, newData);
        }

        public T GetDataSource<T>() => (T) Getter?.Invoke(ParentParser, ParentField);
    }
}