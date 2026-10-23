using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FrankGame
{
    public class LancherPanel : MonoBehaviour
    {
        public const string ResPath = "FrankGame/LancherPanel";

        public Slider ProgressSlider;
        public Text ProgressText;
        public Text ProgressRatioText;
        public Text VersionText;
        public PromptPanel Prompt; 

        private System.Action onLoadStartFunc;
        private System.Action<float> onLoadFinishFunc;
        private float startTime;

        public static LancherPanel Load(GameObject _root, System.Action _onLoadStartFunc = null, System.Action<float> _onLoadFinishFunc = null)
        {
            GameObject panel = GameObject.Instantiate(Resources.Load<GameObject>(ResPath));

            LancherPanel lancherPanel = panel.GetComponent<LancherPanel>();
            lancherPanel.onLoadStartFunc = _onLoadStartFunc;
            lancherPanel.onLoadFinishFunc = _onLoadFinishFunc;

            panel.transform.SetParent(_root.transform);
            panel.transform.localPosition = Vector3.zero;
            panel.transform.localRotation = Quaternion.identity;
            panel.transform.localScale = Vector3.one;

            RectTransform rect = panel.transform as RectTransform;
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;

            // 偏移全部归0
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;

            // 中心居中
            rect.anchoredPosition = Vector2.zero;
            rect.sizeDelta = Vector2.zero;

            lancherPanel.onLoadStartFunc?.Invoke();
            
            return lancherPanel;
        }

        private void Awake()
        {
            this.VersionText.text = Application.version;
            this.Prompt?.gameObject.SetActive(false);
        }

        private void Start()
        {
            startTime = Time.unscaledTime;
        }

        public void SetProgress(float _progress, string _text)
        {
            this.ProgressSlider.value = _progress;
            if (this.ProgressText != null)
            {
                this.ProgressText.text = _text;
            }
            if(this.ProgressRatioText != null)
            {
                this.ProgressRatioText.text = $"{(int)(_progress * 100)}%";
            }
        }

        public void ShowTwoButton(string _content, System.Action _onConfirmFunc)
        {
            this.Prompt?.gameObject.SetActive(true);
            this.Prompt?.ShowTwoButton(LocalizationManager.Instance.GetPackageLanguageText("prompt_title"), _content, LocalizationManager.Instance.GetPackageLanguageText("prompt_confirm"), LocalizationManager.Instance.GetPackageLanguageText("prompt_cancel"), _onConfirmFunc, () => { Application.Quit(); });
        }

        public void ShowOneButton(string _content)
        {
            this.Prompt?.gameObject.SetActive(true);
            this.Prompt?.ShowOneButton(LocalizationManager.Instance.GetPackageLanguageText("prompt_title"), _content, LocalizationManager.Instance.GetPackageLanguageText("prompt_confirm"), () => { Application.Quit(); });
        }

        public void Close()
        {
            GameObject.Destroy(this.gameObject);
        }

        private void OnDestroy()
        {
            onLoadFinishFunc?.Invoke(Time.unscaledTime - startTime);
        }
    }
}