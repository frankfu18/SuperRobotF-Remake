using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using db;
using db.core;
using FrankGame;
using UnityEngine;

public class DatabaseParserCreator {

    private readonly Type DataType;
    
    private FieldInfo[] Fields;
    
    private readonly string ParserName;
    
    private readonly CreatorBuilder CreatorBuilder;
    
    public DatabaseParserCreator(Type _dataType) {
        DataType = _dataType;
        ParserName = $"{DataType.Name}Parser";
        InitContext();
        CreatorBuilder = new CreatorBuilder();
    }
    

    private void InitContext() {
        Init();
        // 获取所有 public field
        Fields = DataType.GetFields(BindingFlags.Public | BindingFlags.Instance);
    }
    
    public bool GenerateCSFile(string dir) {
        AppendPackages();
        AppendPrefix();
        
        CreatorBuilder.Enter();
        
        AppendPropertyNames();
        AppendPropertyGetSets();

        CreatorBuilder.Quit();

        AppendSuffix();
        var filePath = Path.Combine(dir, $"{ParserName}.AutoGen.cs");
        var newContent = ListToString();
        if (!CanOverride(newContent, filePath)) {
            return false;
        }
        // 先删除旧文件
        if (File.Exists(filePath)) {
            File.Delete(filePath);
        }
        // root 生成
        File.WriteAllText(filePath, newContent, Encoding.UTF8);
        return true;
    }


    private bool CanOverride(string _newContent, string filePath) {
        if (!File.Exists(filePath)) {
            return true;
        }
        var content = File.ReadAllText(filePath, Encoding.UTF8);
        var v1 = Utility.GetMd5(content);
        var v2 = Utility.GetMd5(_newContent);
        return !string.Equals(v1, v2);
    }

    

    private string ListToString(string _split = Enter) => CreatorBuilder.ToString(_split);

    private void AppendPackages() {
        CreatorBuilder.Append("using System.Collections.Generic;").EnterLine().Append("using db.cfg;").Append("using db.core;").EnterLine();
    }

    private void AppendPrefix() {
        CreatorBuilder.
                Append("namespace db.parser {")
                .Enter()
                .Append($"public partial class {ParserName} : BaseBeanParser<{DataType.Name}>{{");
    }

    
    private void AppendPropertyNames() {
        CreatorBuilder.Append($"public static {ParserName} Ins => DataBaseManager.Instance.GetRootParser<{ParserName}, {DataType.Name}>();");
        CreatorBuilder.EnterLine();
    }
    
    private string ToUpCase(string _str) => _str.Length == 0 ? "" : (_str.Substring(0, 1).ToUpper() + _str.Substring(1));

    private void AppendPropertyGetSets() {
        foreach (var f in Fields) {
            AppendPropertyGetSet(f);
        }
    }

    private void AppendPropertyGetSet(FieldInfo _f) {
        
        // 处理枚举
        if (TryAppendEnumProperty(_f)) {
            return;
        }
        
        // 处理 dic
        if (TryAppendCollectionProperty(_f)) {
            return;
        }
        
        if (!TypeMapping.TryGetValue(_f.FieldType.Name, out var typeName)) {
            throw new Exception($"{DataType.FullName}.{_f.Name} 类型{_f.FieldType.Name}无法识别");
        }

        CreatorBuilder
                .Append($"public {typeName} {ToUpCase(_f.Name)} {{")
                .Enter()
                .Append($"get => Datasource.{_f.Name};")
                .Append("set {").Enter()
                .Append($"Datasource.{_f.Name} = value;").Append("Save();")
                .Quit(false).Append("}")
                .Quit(false)
                .Append("}")
                .EnterLine();

    }

    private bool TryAppendEnumProperty(FieldInfo _f) {
        if (!_f.FieldType.IsEnum) {
            return false;
        }
        CreatorBuilder
                .Append($"public {_f.FieldType.FullName} {ToUpCase(_f.Name)} {{")
                .Enter()
                .Append($"get => Datasource.{_f.Name};")
                .Append("set {").Enter()
                .Append($"Datasource.{_f.Name} = value;").Append("Save();")
                .Quit(false).Append("}")
                .Quit(false)
                .Append("}")
                .EnterLine();

        return true;
    }


    private bool IsParserModelType(System.Type _type) => _type.IsDefined(typeof(DataBaseAttribute), false);

    private bool TryAppendCollectionProperty(FieldInfo _f) {
        
        if (!DataBaseUtil.IsDic(_f.FieldType) && !DataBaseUtil.IsList(_f.FieldType)) {
            return false;
        }
        
        var types = _f.FieldType.GenericTypeArguments;
        if (types.Length is 0 or > 2) {
            return false;
        }
        
        // 检查是否是 dic 或者 list
        if (_f.Name.Equals("RoleTitles")) {
            Debug.Log($"type is {GetGenericFieldFullName(_f.FieldType)}");
        }


        // 检查是否是 bean parser
        var beanType = DataBaseUtil.FindOneByArray(types, IsParserModelType);
        if (beanType == null && IsBasicGeneric(_f.FieldType)) {
            AppendBasicCollectionProperty(_f);
            return true;
        }
        var propertyName = $"m_{_f.Name}";
        if (DataBaseUtil.IsDic(_f.FieldType)) {
            if (!TypeMapping.TryGetValue(types[0].Name, out var a)) {
                a = types[0].FullName;
            }
            CreatorBuilder.Append($"private DictionaryParser<{a}, {GetGenericFieldFullName(types[1])}> {propertyName};");
            CreatorBuilder.Append($"public DictionaryParser<{a}, {GetGenericFieldFullName(types[1])}> {ToUpCase(_f.Name)} => {propertyName} ??= GetParser<DictionaryParser<{a}, {GetGenericFieldFullName(types[1])}>>(\"{_f.Name}\");");
        } else { // list
            CreatorBuilder.Append($"private ListParser<{GetGenericFieldFullName(types[0])}> {propertyName};");
            CreatorBuilder.Append($"public  ListParser<{GetGenericFieldFullName(types[0])}> {ToUpCase(_f.Name)} => {propertyName} ??= GetParser<ListParser<{GetGenericFieldFullName(types[0])}>>(\"{_f.Name}\");");
        }
        
        return true;
    }


    private bool IsBasicGeneric(Type _type) {
        var types = _type.GenericTypeArguments;
        foreach (var t in types) {
            if (t.IsGenericType) {
                return false;
            }
        }
        return true;
    }

    private string GetGenericFieldFullName(Type _type) {
        if (!_type.IsGenericType) {
            return GetTypeName(_type);
        }
        var str = new StringBuilder();
        if (IsBasicGeneric(_type)) {
            str.Append(_type.Name.Remove(_type.Name.IndexOf("`", StringComparison.Ordinal)));
        } else {
            if (DataBaseUtil.IsDic(_type)) {
                str.Append("DictionaryParser");
            } else {
                str.Append("ListParser");
            }
        }
        str.Append("<");
        var argLength = _type.GenericTypeArguments.Length;
        for (var i = 0; i < argLength ; i++) {
            if (i != 0) {
                str.Append(", ");
            }
            str.Append(GetGenericFieldFullName(_type.GenericTypeArguments[i]));
        }
        str.Append(">");
        return str.ToString();
    }

    private string GetTypeName(Type _type) {
        if (TypeMapping.TryGetValue(_type.Name, out var tn)) {
            return tn;
        }

        return _type.FullName;
    }


    private void AppendBasicCollectionProperty(FieldInfo _f) {
        var types = _f.FieldType.GenericTypeArguments;
        if (DataBaseUtil.IsDic(_f.FieldType)) {
            var param = new Dictionary<string, string>(8);
            if (!TypeMapping.TryGetValue(types[0].Name, out var a)) {
                a = types[0].FullName;
            }
            param.Add("T1", a);
            if (!TypeMapping.TryGetValue(types[1].Name, out var b)) {
                b = types[1].FullName;
            }
            param.Add("T2", b);
            param.Add("P1", _f.Name);
            param.Add("P2", ToUpCase(_f.Name));
            AppendReplaceTemplate(param, "Dic.txt");
        } else {
            var param = new Dictionary<string, string>(8);
            if (!TypeMapping.TryGetValue(types[0].Name, out var a)) {
                a = types[0].FullName;
            }
            param.Add("T1", a);
            param.Add("P1", _f.Name);
            param.Add("P2", ToUpCase(_f.Name));
            AppendReplaceTemplate(param, "List.txt");
        }
    }

    private void AppendSuffix() {
        CreatorBuilder.Append("}");
        CreatorBuilder.Quit();
        CreatorBuilder.Append("}");
    }

    private void AppendReplaceTemplate(Dictionary<string, string> _params, string _template) {
        var file = File.ReadAllText($"{TemplateDir}/{_template}");
        foreach (var p in _params) {
            file = file.Replace(p.Key, p.Value);
        }

        var enters = file.Split(Enter);
        foreach (var e in enters) {
            CreatorBuilder.Append(e);
        }
    }

    private static void Init() {
        if (TypeMapping.Count > 0) {
            return;
        }
        TypeMapping.Add("Int32", "int");
        TypeMapping.Add("Int64", "long");
        TypeMapping.Add("String", "string");
        TypeMapping.Add("Boolean", "bool");
        TypeMapping.Add(nameof(Single), "float");
        TypeMapping.Add(DictTypeName, "MonitorDictionary");
        TypeMapping.Add(ListTypeName, "MonitorList");
    }
    
    static readonly Dictionary<string, string> TypeMapping = new(16);
    private static string TemplateDir = Application.dataPath.Replace("Assets", "") + "/Assets/Editor/Database/Template";
    
    public const string Tab = "   ";
    
    public const string Tab2 = Tab + Tab;
    
    public const string Tab3 = Tab2 + Tab;
    
    public const string Tab4 = Tab3 + Tab;

    public const string Enter = "\n";
    
    const string DictTypeName = "Dictionary";
    
    const string ListTypeName = "List";
    
    
}