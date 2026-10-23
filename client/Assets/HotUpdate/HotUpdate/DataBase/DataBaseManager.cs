using db.core;
using System;
using System.Collections;
using System.Collections.Generic;
using db;
using UnityEngine;
using FrankGame;

public class DataBaseManager : Singlet<DataBaseManager>
{
    
    private int exceptionFrame;

    public DataBaseManager()
    {
        Application.logMessageReceived += OnLogPrint;
    }

    public void LoadDB(Action _onLoadDone) {
        LoadAllTypes();
        ReadAllDB(_onLoadDone);
    }

    private void ReadAllDB(Action _onLoadDone)
    {
        int dataTotalCount = RootDatabaseModels.Count;
        int dataCurCount = 0;
        foreach (var dbType in RootDatabaseModels)
        {
            ReadLocalDB(dbType, GameConfig.DB.IsEncrypt, datasource => {
                CreateRootParser(datasource, dbType);
                dataCurCount++;
                if (dataCurCount != dataTotalCount) {
                    return;
                }
                _onLoadDone?.Invoke();
            });
        }

        GameRoot.Instance.StopCoroutine(IESaveDB());
        GameRoot.Instance.StartCoroutine(IESaveDB());
    }


    private void ReadLocalDB(Type dataType, bool _isEncrypt, Action<object> _onLoadDone)
    {
        DataSaveManager.LoadData(DataBaseUtil.GetTableName(dataType), dataType, _isEncrypt, _onLoadDone);
    }
    
    private IEnumerator IESaveDB()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            if (exceptionFrame == Time.frameCount && saveQueue.Count > 0)
            {
                LogUtil.Error("抛出异常，存储数据失败！");

                saveQueue.Clear();
                needSaveSet.Clear();
            }

            while (saveQueue.Count > 0) {
                var data = saveQueue.Dequeue();
                needSaveSet.Remove(data);
                DataSaveManager.SaveData(DataBaseUtil.GetTableName(data.DataType), data.Datasource, GameConfig.DB.IsEncrypt);
            }
        }
    }

    private void OnLogPrint(string condition, string stackTrace, LogType type)
    {
        if (type == LogType.Exception)
        {
            exceptionFrame = Time.frameCount; //异常帧率
        }
    }

    /// <summary>
    /// 加载数据
    /// </summary>
    /// <param name="_saveName"></param>
    /// <param name="_onDone"></param>
    public void LoadTable(string _saveName, Action<string> _onDone) {
        DataSaveManager.LoadData(_saveName, GameConfig.DB.IsEncrypt, _onDone);
    }

    public bool IsExists(string _saveName) => DataSaveManager.IsExists(_saveName);

    private readonly Dictionary<Type, Type> DatabaseParsersTypes = new();

    private readonly List<Type> RootDatabaseModels = new();

    private readonly Dictionary<Type, DatabaseRootParser> RootParsers = new();
    
    
    private readonly HashSet<DatabaseRootParser> needSaveSet = new();
    private readonly Queue<DatabaseRootParser> saveQueue = new();
    /// <summary>
    /// 创建一个 parser
    /// </summary>
    /// <param name="data"></param>
    /// <param name="_dataType"></param>
    /// <typeparam name="T"></typeparam>
    private void CreateRootParser<T>(T data, Type _dataType) where T : new()  {
        if (!TryGetParserType(_dataType, out _)) {
            throw new ArgumentException($"数据库类型[{_dataType.Name}]");
        }
        // 创建一个 root parser
        var rootParser = new DatabaseRootParser();
        rootParser.Init(data, _dataType);
        RootParsers.Add(_dataType, rootParser);
    }

    public bool TryGetParserType(Type _modelType, out Type _parserType) => DatabaseParsersTypes.TryGetValue(_modelType, out _parserType);

    /// <summary>
    /// 获取 Root 节点
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="D"></typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public T GetRootParser<T, D>() where T : IParser where D : new() {
        var dataType = typeof(D);
        // 检查是否是 root type
        if (!RootDatabaseModels.Contains(dataType)) {
            throw new ArgumentException($"数据库数据类型[{typeof(D)}] 不是Root节点.");
        }
        // 检查 Parser 是否匹配
        if (!RootParsers.TryGetValue(dataType, out var parser) || parser.RootParser is not T) {
            throw new ArgumentException($"数据库数据类型[{typeof(D)}]的Parser类型错误[{typeof(T)}].");
        }
        return (T) parser.RootParser;
    }

    public void SaveDatabase(DatabaseRootParser _rootParser) {
        if (!needSaveSet.Add(_rootParser)) return;
        saveQueue.Enqueue(_rootParser);
    }

    private void LoadAllTypes() {
        System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
        var types = assembly.GetTypes();
        foreach (var type in types) {
            LoadType(type);
        }
    }
    
    private void LoadType(Type _type) {
        var isRoot = _type.IsDefined(typeof(DatabaseRootAttribute), false);
        var isModel = isRoot || _type.IsDefined(typeof(DataBaseAttribute), false);
        if (!isRoot && !isModel) {
            return;
        }
        var parserName = $"db.parser.{_type.Name}Parser";
        // 加载对应的 parser
        var parserType = Type.GetType(parserName);
        if (parserType == null) {
            throw new Exception($"{_type} 没有加载到[{parserName}].");
        }
        DatabaseParsersTypes.Add(_type, parserType);
        if (isRoot) {
            RootDatabaseModels.Add(_type);
        }
    }
}
