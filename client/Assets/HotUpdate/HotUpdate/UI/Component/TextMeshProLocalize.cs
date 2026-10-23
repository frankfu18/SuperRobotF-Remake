using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextMeshProLocalize : MonoBehaviour
{
    public string key;

    public string[] formatParams;

    private TextMeshProUGUI text;

    private void Awake() 
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Start() 
    {
        if (!string.IsNullOrEmpty(key))
        {
            text.text = LanguageSystem.Instance.GetLanguage(key, formatParams);
        }
    }
}
