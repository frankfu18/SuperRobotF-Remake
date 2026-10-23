using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(UIFieldCfg))]
public class UIFieldCfgInspector : Editor
{
	private ReorderableList m_List;

	private GameObject[] lastGos;


	void OnEnable()
	{
		m_List = new ReorderableList(serializedObject, serializedObject.FindProperty("fields"), true, true, true, true);
		m_List.drawHeaderCallback += OnDrawHeader;
		m_List.drawElementCallback += OnDrawElement;
	}

	public override void OnInspectorGUI()
	{
		EditorGUILayout.Space();
		serializedObject.Update();
		m_List.DoLayoutList();
		serializedObject.ApplyModifiedProperties();

		AutoSetKey();

		CheckDuplicated();
		//EditorUtility.SetDirty(target);
	}

	private void OnDrawHeader(Rect rect)
	{
		Rect posType = new Rect(rect.x + 15f, rect.y, rect.width, rect.height);
		EditorGUI.LabelField(posType, "Type");

		Rect posObj = new Rect(rect.x + rect.width * 0.35f - 10f, rect.y, rect.width, rect.height);
		EditorGUI.LabelField(posObj, "Object");

		Rect posKey = new Rect(rect.x + rect.width * 0.7f - 12f, rect.y, rect.width, rect.height);
		EditorGUI.LabelField(posKey, "Custom Name");

		// Rect posArr = new Rect(rect.x + rect.width - 35f, rect.y, 35f, rect.height);
		// EditorGUI.LabelField(posArr, "Array");
	}

	private void OnDrawElement(Rect rect, int index, bool isActive, bool isFocused)
	{
		SerializedProperty element = m_List.serializedProperty.GetArrayElementAtIndex(index);

		Rect posType = new Rect(rect.x, rect.y + 2f, rect.width * 0.35f - 25f, EditorGUIUtility.singleLineHeight);
		SerializedProperty fieldType = element.FindPropertyRelative("type");
		EditorGUI.PropertyField(posType, fieldType, GUIContent.none);

		Rect posObj = new Rect(rect.x + rect.width * 0.35f - 20f, rect.y + 2f, rect.width * 0.35f, EditorGUIUtility.singleLineHeight);
		System.Type type = GetFieldType(fieldType.intValue);
		SerializedProperty obj = element.FindPropertyRelative("go");
		EditorGUI.ObjectField(posObj, obj, type, GUIContent.none);

		Rect posKey = new Rect(rect.x + rect.width * 0.7f - 15f, rect.y + 2f, rect.width * 0.3f - 5f, EditorGUIUtility.singleLineHeight);
		SerializedProperty key = element.FindPropertyRelative("key");
		EditorGUI.PropertyField(posKey, key, GUIContent.none);

		// Rect posArr = new Rect(rect.x + rect.width - 15f, rect.y + 2f, 15f, EditorGUIUtility.singleLineHeight);
		// SerializedProperty arr = element.FindPropertyRelative("arr");
		// EditorGUI.PropertyField(posArr, arr, GUIContent.none);
	}

	private System.Type GetFieldType(int enumValue)
	{
		// string fullName = System.Enum.GetName(typeof(UIFieldCfg.FieldType), enumValue).Replace("_", ".");
		// string typeName = fullName.Substring(fullName.LastIndexOf(".") + 1);
		// string assembly = fullName.Substring(0, fullName.LastIndexOf("."));
		// return System.Reflection.Assembly.Load(assembly).GetType(fullName);
		string fullName = System.Enum.GetName(typeof(UIFieldCfg.FieldType), enumValue);
		return System.Type.GetType(fullName);
	}

	private void CheckDuplicated()
	{
		HashSet<string> keys = new HashSet<string>();
		// HashSet<string> arrs = new HashSet<string>();

		UIFieldCfg fields = target as UIFieldCfg;
		for (int i = 0; i < fields.Length; i++)
		{
			bool add = true;
			string key = fields.GetKey(i);
			// if (fields.IsArr(i))
			// {
			// 	add = arrs.AddAndGets(key);
			// }

			if (add && !keys.Add(key))
			{
				EditorGUILayout.HelpBox("Duplicated key: " + key, MessageType.Error);
			}
		}
	}
	
	private void AutoSetKey()
    {
		UIFieldCfg fields = target as UIFieldCfg;
		if (fields.fields == null)
			return;

		if (lastGos == null || lastGos.Length != fields.fields.Length)
        {
			lastGos = new GameObject[fields.fields.Length];
			for (int i = 0; i < lastGos.Length; i++)
            {
				lastGos[i] = fields.fields[i].go;
            }
        }
        else
        {
			for (int i = 0; i < lastGos.Length; i++)
			{
				if (lastGos[i] != fields.fields[i].go)
                {
					if(fields.fields[i].go && string.IsNullOrEmpty(fields.fields[i].key)) fields.fields[i].key = fields.fields[i].go.name;
					lastGos[i] = fields.fields[i].go;
				}
			}
		}
    }


}