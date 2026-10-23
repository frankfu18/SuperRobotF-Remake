using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

public class UIConfigCreateEditor : EditorWindow
{
    private const string UIConfig_Json_Path = "Assets/Editor/UI/UIEditorConfig.json";
    private const string UIConfig_Class_Path = "Assets/HotUpdate/UI/Config/UIConfig.Auto.cs";
    private const string New_UIConfig_Name = "New";
    private const float Space = 150;

    private static UIConfigCreateEditor instance;
    private bool openedOnce = false;
    private UIConfigList uiConfigList;
    private Vector2 listScrollPos;
    private bool showEnum;//显示枚举

    [MenuItem("FrankGame/UI/UI配置", false, 1)]
    public static void CreateWin()
    {
        if (instance == null)
        {
            instance = CreateWindow<UIConfigCreateEditor>("UI配置编辑");
        }

        instance.Show();
    }

    private void OnEnable()
    {
        if (!openedOnce)
        {
            openedOnce = true;
            OnOpen();
        }
    }

    private void OnDisable()
    {
        instance = null;
        openedOnce = false;
    }

    private void OnOpen()
    {
        var jsonText = AssetDatabase.LoadAssetAtPath<TextAsset>(UIConfig_Json_Path);
        if (jsonText == null) 
        {
            uiConfigList = new UIConfigList();
        }
        else
        {
            uiConfigList = JsonUtility.FromJson<UIConfigList>(jsonText.text);
        }
    }

    public void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button(showEnum ? "显示列表" : "显示枚举", GUILayout.Width(100)))
        {
            showEnum = !showEnum;
        }

        if (GUILayout.Button("保存配置", GUILayout.Width(100)))
        {
            bool success = true;
            HashSet<string> keys = new HashSet<string>();
            foreach (var uiName in uiConfigList.uiConfigNameList)
            {
                if (keys.Contains(uiName))
                {
                    success = false;
                    Debug.LogError("UI界面名称重复: " + uiName);
                }
                else
                {
                    keys.Add(uiName);
                }
            }

            if (success)
            {
                keys.Clear();

                foreach (var uiInfo in uiConfigList.uiInfoList)
                {
                    string panelName = uiConfigList.uiConfigNameList[uiInfo.uiConfigIndex];

                    if (keys.Contains(panelName))
                    {
                        success = false;
                        Debug.LogError("UI界面重复配置: " + panelName);
                    }
                    else
                    {
                        keys.Add(panelName);
                    }
                }

                foreach (var uiName in uiConfigList.uiConfigNameList)
                {
                    if (!keys.Contains(uiName))
                    {
                        Debug.LogError("UI界面未配置: " + uiName);
                    }
                }
            }

            if (success)
            {
                string json = JsonUtility.ToJson(uiConfigList);

                File.WriteAllText(UIConfig_Json_Path, json);

                AssetDatabase.Refresh();

                Debug.Log("保存配置成功! 数量:" + keys.Count);
            }
        }
        if (GUILayout.Button("生成文件", GUILayout.Width(100)))
        {
            GenericCode();
        }
        EditorGUILayout.EndHorizontal();

        if (showEnum)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("UI名称", GUILayout.Width(200));
            if (GUILayout.Button("增加", GUILayout.Width(50)))
            {
                uiConfigList.uiConfigNameList.Add(New_UIConfig_Name);
            }
            EditorGUILayout.EndHorizontal();

            for (int i = 0;i < uiConfigList.uiConfigNameList.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                uiConfigList.uiConfigNameList[i] = EditorGUILayout.TextField(uiConfigList.uiConfigNameList[i], GUILayout.Width(200));
                if (GUILayout.Button("删除", GUILayout.Width(50)))
                {
                    uiConfigList.uiConfigNameList.RemoveAt(i);
                }
                EditorGUILayout.EndHorizontal();
            }
        }
        else
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("名称", GUILayout.Width(Space));
            EditorGUILayout.LabelField("层级", GUILayout.Width(Space));
            EditorGUILayout.LabelField("脚本路径", GUILayout.Width(Space));
            EditorGUILayout.LabelField("单例", GUILayout.Width(Space));
            EditorGUILayout.LabelField("播放动画", GUILayout.Width(Space));
            EditorGUILayout.LabelField("Resources加载", GUILayout.Width(Space));
            if (GUILayout.Button("增加", GUILayout.Width(Space)))
            {
                uiConfigList.uiInfoList.Add(OnAddUIInfo());
            }
            EditorGUILayout.EndHorizontal();

            listScrollPos = EditorGUILayout.BeginScrollView(listScrollPos);
            for (int i = 0; i < uiConfigList.uiInfoList.Count; i++)
            {
                OnUIInfoView(uiConfigList.uiInfoList[i], i);
            }
            EditorGUILayout.EndScrollView();
        }

    }

    private void OnUIInfoView(UIInfoEditor _uiInfo, int _index)
    {
        EditorGUILayout.BeginHorizontal();
        _uiInfo.uiConfigIndex = EditorGUILayout.Popup(_uiInfo.uiConfigIndex, uiConfigList.uiConfigNameList.ToArray(), GUILayout.Width(Space));
        _uiInfo.layer = (EUILayer)EditorGUILayout.EnumPopup(_uiInfo.layer, GUILayout.Width(Space));
        _uiInfo.prefabName = EditorGUILayout.TextField(_uiInfo.prefabName, GUILayout.Width(Space));
        _uiInfo.instance = EditorGUILayout.Toggle(_uiInfo.instance, GUILayout.Width(Space));
        _uiInfo.showPlayAnimation = EditorGUILayout.Toggle(_uiInfo.showPlayAnimation, GUILayout.Width(Space));
        _uiInfo.loadResources = EditorGUILayout.Toggle(_uiInfo.loadResources, GUILayout.Width(Space));

        if (GUILayout.Button("删除", GUILayout.Width(50)))
        {
            uiConfigList.uiInfoList.RemoveAt(_index);
        }
        EditorGUILayout.EndHorizontal();
    }

    private UIInfoEditor OnAddUIInfo()
    {
        UIInfoEditor uIInfo = new UIInfoEditor();
        uIInfo.layer = EUILayer.Default;
        uIInfo.uiConfigIndex = 0;
        uIInfo.instance = true;
        uIInfo.showPlayAnimation = false;
        uIInfo.loadResources = false;

        return uIInfo;
    }

    private static void GenericCode()
    {
        var jsonText = AssetDatabase.LoadAssetAtPath<TextAsset>(UIConfig_Json_Path);
        if (jsonText == null)
        {
            Debug.LogError("UI配置文件不存在，无法生成代码");
            return;
        }

        var uiconfig = JsonUtility.FromJson<UIConfigList>(jsonText.text);

        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendLine($"//auto generic code don't edit.");
        stringBuilder.AppendLine("using System.Collections.Generic;");
        stringBuilder.AppendLine("");
        stringBuilder.AppendLine("public enum UIConfig");
        stringBuilder.AppendLine("{");
        foreach (var uiName in uiconfig.uiConfigNameList)
        {
            stringBuilder.AppendLine($"\t{uiName},");
        }
        stringBuilder.AppendLine("}");
        stringBuilder.AppendLine("");

        stringBuilder.AppendLine("public class UIInfoTable");
        stringBuilder.AppendLine("{");
        stringBuilder.AppendLine("\tprivate static Dictionary<UIConfig, UIInfo> UIInfoDic = new Dictionary<UIConfig, UIInfo>()");
        stringBuilder.AppendLine("\t{");
        foreach (var uiInfo in uiconfig.uiInfoList)
        {
            string panelName = uiconfig.uiConfigNameList[uiInfo.uiConfigIndex];
            stringBuilder.AppendLine($"\t\t{{ UIConfig.{panelName}, new UIInfo() {{ layer = EUILayer.{uiInfo.layer}, uiConfigId = UIConfig.{panelName}, prefabName = \"{uiInfo.prefabName}\", instance = {uiInfo.instance.ToString().ToLower()}, showPlayAnimation = {uiInfo.showPlayAnimation.ToString().ToLower()}, loadResources = {uiInfo.loadResources.ToString().ToLower()}}}}},");
        }
        stringBuilder.AppendLine("\t};");

        stringBuilder.AppendLine("\tpublic static UIInfo GetUIInfo(UIConfig _uiConfig)");
        stringBuilder.AppendLine("\t{");
        stringBuilder.AppendLine("\t\tif (UIInfoDic.TryGetValue(_uiConfig, out UIInfo uiInfo))");
        stringBuilder.AppendLine("\t\t{");
        stringBuilder.AppendLine("\t\t\treturn uiInfo;");
        stringBuilder.AppendLine("\t\t}");

        stringBuilder.AppendLine("\t\treturn null;");
        stringBuilder.AppendLine("\t}");
       
        stringBuilder.AppendLine("\tpublic static IUIInterface CreateUIPanel(UIConfig _uiConfig)");
        stringBuilder.AppendLine("\t{");
        stringBuilder.AppendLine("\t\tswitch (_uiConfig)");
        stringBuilder.AppendLine("\t\t{");
        foreach (var uiInfo in uiconfig.uiInfoList)
        {
            string panelName = uiconfig.uiConfigNameList[uiInfo.uiConfigIndex];
            stringBuilder.AppendLine($"\t\t\tcase UIConfig.{panelName}:");
            stringBuilder.AppendLine($"\t\t\t\treturn new {panelName}();");
        }
        stringBuilder.AppendLine("\t\t}");
        stringBuilder.AppendLine($"\t\tthrow new System.Exception($\"Class {{_uiConfig}} not exist!\");");
        stringBuilder.AppendLine("\t}");

        stringBuilder.AppendLine("}");

        File.WriteAllText(UIConfig_Class_Path, stringBuilder.ToString());

        AssetDatabase.Refresh();

        Debug.Log("UI配置代码生成成功! 路径:" + UIConfig_Class_Path);
    }
}
