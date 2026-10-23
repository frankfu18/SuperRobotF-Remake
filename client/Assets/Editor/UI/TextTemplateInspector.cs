using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TextTemplateScriptableObject))]
public class TextTemplateInspector : Editor
{
    private enum OperateAction
    {
        None,
        Add,
        Remove
    }

    private TextTemplateScriptableObject textTemplate;
    private SerializedProperty fontColors;
    private SerializedProperty fontOutlines;
    private SerializedProperty fontGradients;

    private void OnEnable()
    {
        textTemplate = this.target as TextTemplateScriptableObject;
        fontColors = serializedObject.FindProperty("FontColors");
        fontOutlines = serializedObject.FindProperty("FontOutlines");
        fontGradients = serializedObject.FindProperty("FontGradients");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUI.BeginChangeCheck();

        if (EditorGUILayout.Foldout(true, "字体颜色"))
        {
            OperateAction buttonAction = OperateAction.None;
            int buttonActionIdx = 0;

            if (GUILayout.Button("增加", GUILayout.Width(50)))
            {
                buttonAction = OperateAction.Add;
                buttonActionIdx = fontColors.arraySize;
            }

            for (int i = 0; i < fontColors.arraySize; i++)
            {
                FontColorElement ele = textTemplate.FontColors[i];

                EditorGUILayout.BeginHorizontal();
                ele.fontColor = (ETextFontColor)EditorGUILayout.EnumPopup(ele.fontColor, GUILayout.Width(100));
                ele.color = EditorGUILayout.ColorField(new GUIContent(""), ele.color, true, true, false);
                if (GUILayout.Button("删除"))
                {
                    buttonAction = OperateAction.Remove;
                    buttonActionIdx = i;
                }
                EditorGUILayout.EndHorizontal();
            }

            if (buttonAction == OperateAction.Add)
            {
                fontColors.InsertArrayElementAtIndex(buttonActionIdx);
            }
            else if (buttonAction == OperateAction.Remove)
            {
                fontColors.DeleteArrayElementAtIndex(buttonActionIdx);
            }
        }

        if (EditorGUILayout.Foldout(true, "字体描边"))
        {
            OperateAction buttonAction = OperateAction.None;
            int buttonActionIdx = 0;

            if (GUILayout.Button("增加", GUILayout.Width(50)))
            {
                buttonAction = OperateAction.Add;
                buttonActionIdx = fontOutlines.arraySize;
            }

            for (int i = 0; i < fontOutlines.arraySize; i++)
            {
                FontOutlineElement ele = textTemplate.FontOutlines[i];

                EditorGUILayout.BeginHorizontal();
                ele.outline = (ETextFontOutline)EditorGUILayout.EnumPopup(ele.outline, GUILayout.Width(100));
                ele.material = (Material)EditorGUILayout.ObjectField(ele.material, typeof(Material), false);
                if (GUILayout.Button("删除"))
                {
                    buttonAction = OperateAction.Remove;
                    buttonActionIdx = i;
                }
                EditorGUILayout.EndHorizontal();
            }

            if (buttonAction == OperateAction.Add)
            {
                fontOutlines.InsertArrayElementAtIndex(buttonActionIdx);
            }
            else if (buttonAction == OperateAction.Remove)
            {
                fontOutlines.DeleteArrayElementAtIndex(buttonActionIdx);
            }
        }

        if (EditorGUILayout.Foldout(true, "字体渐变"))
        {
            OperateAction buttonAction = OperateAction.None;
            int buttonActionIdx = 0;

            if (GUILayout.Button("增加", GUILayout.Width(50)))
            {
                buttonAction = OperateAction.Add;
                buttonActionIdx = fontGradients.arraySize;
            }

            for (int i = 0; i < fontGradients.arraySize; i++)
            {
                FontGradientElement ele = textTemplate.FontGradients[i];

                EditorGUILayout.BeginHorizontal();
                ele.gradient = (ETextGradient)EditorGUILayout.EnumPopup(ele.gradient, GUILayout.Width(100));
                EditorGUILayout.BeginVertical();
                ele.topColor = EditorGUILayout.ColorField(new GUIContent("Top"), ele.topColor, true, true, false);
                ele.bottomColor = EditorGUILayout.ColorField(new GUIContent("Bottom"), ele.bottomColor, true, true, false);
                EditorGUILayout.EndVertical();
                if (GUILayout.Button("删除"))
                {
                    buttonAction = OperateAction.Remove;
                    buttonActionIdx = i;
                }
                EditorGUILayout.EndHorizontal();
            }

            if (buttonAction == OperateAction.Add)
            {
                fontGradients.InsertArrayElementAtIndex(buttonActionIdx);
            }
            else if (buttonAction == OperateAction.Remove)
            {
                fontGradients.DeleteArrayElementAtIndex(buttonActionIdx);
            }
        }

        if (EditorGUI.EndChangeCheck())
        {
            serializedObject.ApplyModifiedProperties();
        }
    }
}
