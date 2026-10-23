using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using db.cfg;
using db.core;
using UnityEditor;
using UnityEngine;

public static class DatabaseEditorTools
{


    private static readonly string Dir = Application.dataPath.Replace("Assets", "")  + "/Assets/HotUpdate/DataBase/AutoGen";

    private static readonly HashSet<string> ParserSets = new(64);
    
    [MenuItem("FrankGame/DBParser生成")]
    public static void Generate() { 
        var types =  LoadAllModels();
        DeleteNoExistsFiles();
        var count = 0;
        foreach (var t in types) {
            if (!new DatabaseParserCreator(t).GenerateCSFile(Dir)) {
                continue;
            }
            Debug.Log($"生成 {t.Name}Parser.AutoGen.cs 成功");
            count++;
        }
        Debug.Log($"本次共生成[{count}]Parser文件");

        AssetDatabase.Refresh();
    }

    
    private static void DeleteNoExistsFiles() {
        if(!Directory.Exists(Dir)) {
            Directory.CreateDirectory(Dir);
        }

        var dir = new DirectoryInfo(Dir);
        //删除
        foreach (var f in dir.GetFiles()) {
            if (ParserSets.Contains(f.Name) || f.Extension.Equals(".meta")) {
                continue;
            }
            f.Delete();
        }
    }

    private static List<Type> LoadAllModels()
    {
        var assembly = Assembly.GetAssembly(typeof(ParserContext));
        var dataTypes = new List<Type>(64);
        var types = assembly.GetTypes();

        foreach (var type in types)
        {
            var database = type.IsDefined(typeof(DataBaseAttribute), false) || type.IsDefined(typeof(DatabaseRootAttribute), false);
            if (!database) {
                continue;
            }
            dataTypes.Add(type);
            ParserSets.Add($"{type.Name}Parser.cs");
        }
        return dataTypes;
    }
    
}