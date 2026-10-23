using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FrankGame
{
    public class PromptPanel : MonoBehaviour
    {
        public Text TitleText;
        public Text ContentText;
        public Button ConfirmBtn;
        public Button CancelBtn;
        public Text ConfirmBtnText;
        public Text CancelBtnText;

        public void ShowTwoButton(string _title, string _content, string _confirm, string _cancel, System.Action _onConfirmFunc, System.Action _onCancelFunc)
        {
            this.ConfirmBtn.gameObject.SetActive(false);
            this.CancelBtn.gameObject.SetActive(true);

            this.TitleText.text = _title;
            this.ContentText.text = _content;
            this.CancelBtnText.text = _cancel;

            this.ConfirmBtn.onClick.RemoveAllListeners();
            this.ConfirmBtn.onClick.AddListener(() =>
            {
                _onConfirmFunc?.Invoke();
            });

            this.CancelBtn.onClick.RemoveAllListeners();
            this.CancelBtn.onClick.AddListener(() =>
            {
                _onCancelFunc?.Invoke();
            });
        }

        public void ShowOneButton(string _title, string _content, string _confirm, System.Action _onConfirmFunc)
        {
            this.ConfirmBtn.gameObject.SetActive(true);
            this.CancelBtn.gameObject.SetActive(false);

            this.TitleText.text = _title;
            this.ContentText.text = _content;
            this.ConfirmBtnText.text = _confirm;

            this.ConfirmBtn.onClick.RemoveAllListeners();

            this.ConfirmBtn.onClick.AddListener(() =>
            {
                _onConfirmFunc?.Invoke();
            });
        }
    }
}
