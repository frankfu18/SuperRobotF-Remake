using UnityEngine;
using UnityEditor;
using static UIFieldCfg;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System;
/*
说明：对要生成变量的物体按规则命名，自动生成的界面代码，凡是prefab中带用绑定脚本就要生成脚本，然后在初始化方法里对变量赋值，脚本以.变量的方式访问
规则：
1.命名，组件，Button,例如：m_Button_Close
Item，prefab挂脚本的，m_Item_SubInfoItem_SubInfo，中间加上Item对应的脚本类名字
2.使用，所有界面变量需要挂载到界面根节点，所以取名字不能重复
Item类型的需要在使用中赋值，暂时不支持初始赋值
生成的按钮回调方法需要在界面中实现，否则编译错误
*/
public class AutoGenPartialView
{
    public static string filePath = "Assets/HotUpdate/UI/AutoGen/";
    public static string fileNameFormat = "{0}.AutoGen.cs";
    public static string fileClassName = "{0}";
    private static string PanelBase = "UIPanel";
    private static string ItemBase = "UIItem";

    private static Regex numberRegex = new Regex(@"\d+$");

    /// <summary>
    /// 类型名 命名空间映射
    /// </summary>
    private static Dictionary<string, string> dic_TypeNamespace = new Dictionary<string, string>()
    {
        {"SkeletonGraphic", "using Spine.Unity;" },
        {"UIParticle", "using Coffee.UIExtensions;" },
        {"TextMeshProUGUI", "using TMPro;" },
        {"TMP_InputField", "using TMPro;" },
        {"TMP_Dropdown", "using TMPro;" },
    };

    private static List<string> namespaceList = new List<string>()
    {
        "UnityEngine",
        "UnityEngine.UI",
    };

    [MenuItem("GameObject/[UI] 生成脚本", priority = 11)]
    private static void GenericViewInfo()
    {
        //选中对象编辑
        GameObject select = Selection.gameObjects[0];

        bool success = false;

        if (select.name.EndsWith("Panel"))
        {
            success = AddCfgFields(select, true);
        }
        else if (select.name.EndsWith("Item"))
        {
            success = AddCfgFields(select, false);
        }
        else
        {
            Debug.LogError("命名必须以Panel或Item结尾！");
        }
    }

    public static System.Type GetTypeByName(string _namespcae, string _typeName)
    {
        var assembly = System.Reflection.Assembly.Load(_namespcae);
    
        foreach (System.Type type in assembly.GetTypes())
        {
            if (type.Namespace == _namespcae && type.Name == _typeName)
                return type;
        }

        return null;
    }

    private static bool AddCfgFields(GameObject select, bool isPanel, bool _createCode = true)
    {
        var cfg = select.transform.GetComponent<UIFieldCfg>();
        if (cfg == null)
        {
            cfg = select.AddComponent<UIFieldCfg>();
        }
        cfg.fields = null;

        List<UICustomField> customFields = new List<UICustomField>();

        GenericChild(select.transform, customFields);
        cfg.fields = customFields.ToArray();

        if (_createCode)
            GenericCode(select, cfg.fields, isPanel);

        EditorUtility.SetDirty(select);

        return true;
    }

    private static void GenericChild(Transform target, List<UICustomField> customFields)
    {
        for (int i = 0; i < target.childCount; i++)
        {
            bool findChild = true;//是否继续向下查找
            var child = target.GetChild(i);
            if (child.name.StartsWith("m_"))
            {
                string[] nameInfo = child.name.Split(new char[] { '_' }, 4);
                string varType = nameInfo[1];
                string varName = child.name;

                string[] typeInfo = varType.Split('&');
                for (int j = 0; j < typeInfo.Length; j++)
                {
                    varType = typeInfo[j];

                    if (typeInfo.Length > 1)
                    {
                        varName = child.name.Replace(child.name.Substring(child.name.IndexOf('_') + 1, child.name.IndexOf('_', 2) - child.name.IndexOf('_') - 1), varType);
                    }
                    
                    bool suport = true;
                    bool hasParam = false;
                    FieldType ftype = FieldType.GameObject;
                    switch (varType)
                    {
                        case "Item":
                            ftype = FieldType.UIItem;
                            hasParam = true;
                            findChild = false;
                            break;
                        case "List":
                            ftype = FieldType.UIList;
                            hasParam = true;
                            findChild = false;
                            break;
                        case "Switch":
                            ftype = FieldType.UISwitch;
                            findChild = false;
                            break;
                        case "Tab":
                            ftype = FieldType.UITab;
                            findChild = false;
                            break;
                        case "FrameAnimator":
                            ftype = FieldType.UIFrameAnimator;
                            findChild = false;
                            break;
                        case "UIParticle":
                            ftype = FieldType.UIParticle;
                            findChild = false;
                            break;
                        case "ScrollView":
                            ftype = FieldType.ScrollRect;
                            findChild = false;
                            break;
                        case "UIEffect":
                            ftype = FieldType.UIEffect;
                            findChild = false;
                            break;
                        case "Com":
                            ftype = FieldType.Component;
                            hasParam = true;
                            break;
                        case "TMP":
                            hasParam = true;
                            string tmpType = nameInfo[2];
                            if (tmpType == "Text")
                            {
                                ftype = FieldType.TextMeshProUGUI;
                            }
                            else if (tmpType == "InputField")
                            {
                                ftype = FieldType.TMP_InputField;
                            }
                            else if (tmpType == "Dropdown")
                            {
                                ftype = FieldType.TMP_Dropdown;
                            }
                            break;
                        case "DynamicItem":
                            ftype = FieldType.DynamicItem;
                            hasParam = true;
                            break;
                        default:
                            try
                            {
                                ftype = (FieldType)Enum.Parse(typeof(FieldType), varType);
                            }
                            catch (Exception)
                            {
                                Debug.LogError("不支持的类型：" + varType);
                                suport = false;
                                break;
                            }
                            break;
                    }
                    if (suport)
                    {
                        UICustomField field = new UICustomField();
                        field.go = child.gameObject;
                        field.key = varName;
                        field.type = ftype;
                        if (hasParam)
                        {
                            field.param = nameInfo[2];
                        }
                        customFields.Add(field);

                        if (ftype == FieldType.UIList)
                        {
                            AddUIList(child.gameObject);
                        }
                    }
                }
            }

            if (findChild)
            {
                GenericChild(child, customFields);
            }
        }
    }

    private static void AddUIList(GameObject target)
    {
        var uiList = target.GetComponent<UIList>();
        if (uiList == null)
        {
            uiList = target.AddComponent<UIList>();
        }
        var content = target.transform.GetChild(0);
        if (content == null)
        {
            Debug.LogError(target.name + "没有添加Content!");
            return;
        }

        var cell = content.GetChild(0);
        if (cell == null)
        {
            Debug.LogError(target.name + "没有找到Cell");
            return;
        }

        if (uiList.m_CellGameObject == null)
        {
            uiList.m_CellGameObject = cell.gameObject;
        }
    }

    private static void GenericCode(GameObject select, UICustomField[] fields, bool isPanel)
    {
        Dictionary<string, List<UICustomField>> componentDic = new Dictionary<string, List<UICustomField>>();

        //生成界面代码
        string fileFullPath = filePath + string.Format(fileNameFormat, select.name);
        if (File.Exists(fileFullPath))
        {
            File.Delete(fileFullPath);
        }

        List<string> usingList = new List<string>();
        AddUsingNamespace(usingList, "#COMM#");

        StringBuilder sb1 = new StringBuilder();
        sb1.AppendLine("");
        sb1.AppendLine($"public partial class {string.Format(fileClassName, select.name)} : {(isPanel ? PanelBase : ItemBase)}");
        sb1.AppendLine("{");

        StringBuilder sb2 = new StringBuilder();
        sb2.AppendLine("\tprotected override void OnBind()");
        sb2.AppendLine("\t{");

        //变量和方法
        foreach (var item in fields)
        {
            string type = "";
            string append2 = "";
            //string append3 = "";
            type = Enum.GetName(typeof(FieldType), item.type);
            string varName = item.key;
            bool init = true;
            switch (item.type)
            {
                case FieldType.UIItem:
                    type = item.param;
                    init = false;
                    append2 = $"\t\t{varName} = InitUIItem<{type}>(\"{varName}\");";
                    break;
                case FieldType.UIList:
                    init = false;
                    append2 = $"\t\t{varName} = InitUIList<{item.param}>(\"{varName}\");";
                    break;
                case FieldType.UIEffect:
                    append2 = $"\t\tInitEffect({varName});";
                    break;
                case FieldType.Component:
                    type = item.param;
                    break;
                case FieldType.Canvas:
                    init = false;
                    append2 = $"\t\t{varName} = GetChildCanvas(\"{varName}\");";
                    break;
                case FieldType.DynamicItem:
                    append2 = $"\t\tInitDynamicItem<{item.param}>({varName});";
                    break;
                default:
                    break;
            }

            if (numberRegex.IsMatch(varName))
            {
                Match match =  numberRegex.Match(varName);

                int numIdx = varName.LastIndexOf(match.Value);
                if (numIdx > -1)
                {
                    string keyName = varName.Substring(0, numIdx);
                    item.index = Int32.Parse(match.Value);

                    List<UICustomField> comList;
                    if (!componentDic.TryGetValue(keyName, out comList))
                    {
                        comList = new List<UICustomField>();

                        componentDic.Add(keyName, comList);
                    }

                    comList.Add(item);

                    
                }
            }

            if (init)
            {
                sb2.AppendLine($"\t\t{varName} = GetVaule<{type}>(\"{varName}\");");
            }
            sb1.AppendLine($"\tpublic {type} {varName};");
            AddUsingNamespace(usingList, type);

            if (append2.Length > 0)
            {
                sb2.AppendLine(append2);
            }
        }

        foreach (var dic in componentDic)
        {
            string comName = dic.Key;
            List<UICustomField> comList = dic.Value;

            if (comList.Count > 1)
            {
                sb1.AppendLine($"\tpublic List<{comList[0].type}> {comName}List = new List<{comList[0].type}>();");

                List<string> keys = new List<string>();

                foreach (var com in comList)
                {
                    keys.Add(com.key);
                }

                keys.Sort((a, b) => 
                {
                    Match matchA = numberRegex.Match(a);
                    Match matchB = numberRegex.Match(b);

                    int aNumIdx = a.LastIndexOf(matchA.Value);
                    int bNumIdx = b.LastIndexOf(matchB.Value);

                    string keyName = a.Substring(0, aNumIdx);
                    int aNum = Int32.Parse(matchA.Value);

                    keyName = b.Substring(0, bNumIdx);
                    int bNum = Int32.Parse(matchB.Value);

                    return aNum.CompareTo(bNum);
                });

                foreach (var key in keys)
                {
                    sb2.AppendLine($"\t\t{comName}List.Add({key});");
                }
            }
        }

        sb2.AppendLine("\t}");
        sb1.Append(sb2.ToString());
        sb1.AppendLine("}");

        StringBuilder sb = new StringBuilder();
        AddHeadInfo(sb);
        foreach (string ns in usingList)
        {
            sb.AppendLine(ns);
        }
        sb.Append(sb1.ToString());

        using (var fileStream = new FileStream(fileFullPath, FileMode.Create))
        {
            using (var stream = new StreamWriter(fileStream, Encoding.UTF8))
            {
                stream.Write(sb.ToString());
            }
        }

        AssetDatabase.Refresh();
    }

    private static void AddHeadInfo(StringBuilder stringBuilder)
    {
        //head
        stringBuilder.AppendLine($"//auto generic code don't edit.");
    }

    /// <summary>
    /// 添加类型的命名空间引用
    /// </summary>
    /// <param name="sBuilder"></param>
    /// <param name="type">当传入"#COMM#"时为通用</param>
    private static void AddUsingNamespace(List<string> usingList, string type)
    {
        if (type.Equals("#COMM#"))
        {
            usingList.Add("using UnityEngine;");
            usingList.Add("using UnityEngine.UI;");
            usingList.Add("using System.Collections.Generic;");
        }
        else if (dic_TypeNamespace.ContainsKey(type))
        {
            if (!usingList.Contains(dic_TypeNamespace[type]))
                usingList.Add(dic_TypeNamespace[type]);
        }
    }
}