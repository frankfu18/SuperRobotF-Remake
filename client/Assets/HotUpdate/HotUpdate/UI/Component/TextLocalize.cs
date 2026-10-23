using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TextLocalize : MonoBehaviour {
    public string key;

    public bool ToUppercase = false;

    public string[] formatParams;

    private Text text;

    private void Awake() {
        text = GetComponent<Text>();
    }

    private void Start() {
        var txt = LanguageSystem.Instance.GetLanguage(key, formatParams) ?? key;
        text.text = ToUppercase ? txt.ToUpper() : txt;
    }
}
