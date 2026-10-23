using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace FrankGame
{
    public class TMPFontCheckTool : MonoBehaviour
    {
        public SystemLanguage language = SystemLanguage.Chinese;

        private TextMeshProUGUI text;

        private void Awake()
        {
            text = GetComponent<TextMeshProUGUI>();
            language = LanguageSystem.Instance.CurLanguage;
        }


        private void OnGUI()
        {
            if (GUILayout.Button("检测"))
            {
                CheckLanguageConfig(language);
            }
        }

        private void CheckLanguageConfig(SystemLanguage _language)
        {
            if (text == null)
            {
                Debug.LogError("Text为空");
                return;
            }

            List<string> keyList = new List<string>();
            List<string> valueList = new List<string>();

            var list = ConfigSystem.CurrentData.GetLanguageList();

            foreach (var item in list)
            {
                keyList.Add(item.key);
                valueList.Add(item.english);
            }

            if (valueList.Count > 0)
            {
                StartCoroutine(IECheckText(keyList, valueList));
            }
        }
        private IEnumerator IECheckText(List<string> _keyList, List<string> _valueList)
        {
            Debug.Log("开始检测...");
            int count = _keyList.Count;
            for (int i = 0; i < count; i++)
            {
                string key = _keyList[i];
                string value = _valueList[i];

                Debug.Log("key:" + key + "\nvalue:" + value);

                text.text = value;
                yield return 0;
            }

            Debug.Log("检测结束。");
        }
    }
}
