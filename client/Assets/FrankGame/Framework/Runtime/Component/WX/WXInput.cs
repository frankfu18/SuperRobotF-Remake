#if WX_GAME
using UnityEngine;
using System.Collections;
using WeChatWASM;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace FrankGame
{
    [RequireComponent(typeof(InputField))]
    public class WXInput : MonoBehaviour, IPointerClickHandler, IPointerExitHandler
    {
        public InputField input;
        private bool isShowKeyboard = false;

        void Awake()
        {
            input = this.GetComponent<InputField>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            ShowKeyboard();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!input.isFocused)
            {
                HideKeyboard();
            }
        }

        public void OnInput(OnKeyboardInputListenerResult v)
        {
            if (input.isFocused)
            {
                input.text = v.value;
            }
        }

        public void OnConfirm(OnKeyboardInputListenerResult v)
        {
            // 输入法confirm回调
            HideKeyboard();
        }

        public void OnComplete(OnKeyboardInputListenerResult v)
        {
            // 输入法complete回调
            HideKeyboard();
        }

        private void ShowKeyboard()
        {
            if (!isShowKeyboard)
            {
                WX.ShowKeyboard(new ShowKeyboardOption()
                {
                    defaultValue = "",
                    maxLength = 20,
                    confirmType = "go"
                });

                //绑定回调
                WX.OnKeyboardConfirm(OnConfirm);
                WX.OnKeyboardComplete(OnComplete);
                WX.OnKeyboardInput(OnInput);
                isShowKeyboard = true;
            }
        }

        private void HideKeyboard()
        {
            if (isShowKeyboard)
            {
                WX.HideKeyboard(new HideKeyboardOption());
                //删除掉相关事件监听
                WX.OffKeyboardInput(OnInput);
                WX.OffKeyboardConfirm(OnConfirm);
                WX.OffKeyboardComplete(OnComplete);
                isShowKeyboard = false;
            }
        }
    }
}
#endif