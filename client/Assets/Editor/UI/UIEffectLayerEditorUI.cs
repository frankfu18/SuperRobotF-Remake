using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(UIEffectLayer))]
public class UIEffectLayerEditorUI : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        //layer = EditorGUILayout.IntField(layer);
        if (GUILayout.Button("重置层级"))
        {
            (target as UIEffectLayer).SetLayerEditor();
        }
    }
}
