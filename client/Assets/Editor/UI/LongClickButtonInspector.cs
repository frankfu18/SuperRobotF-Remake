using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UI;

[CustomEditor(typeof(LongClickButton))]
public class LongClickButtonInspector : ButtonEditor
{
    SerializedProperty intervalMillisecondProperty;
    SerializedProperty enableLongClickProperty;

    protected override void OnEnable()
    {
        base.OnEnable();
        intervalMillisecondProperty = serializedObject.FindProperty("intervalSecond");
        enableLongClickProperty = serializedObject.FindProperty("enableLongClick");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.PropertyField(intervalMillisecondProperty, new GUIContent("触发间隔"));
        EditorGUILayout.PropertyField(enableLongClickProperty, new GUIContent("长按"));

        serializedObject.ApplyModifiedProperties();
    }
}
