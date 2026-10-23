#if BYTE_GAME
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using StarkSDKSpace;

namespace FrankGame
{
    public class ClickableInputField : EventTrigger
    {
        public string confirmType = "done"; // 可选值有: "done", "next", "search", "go", "send"
        public int maxInputLength = 100; // 最大输入长度
        public bool multiple = false; // 是否多行输入
        private InputField _inputField;
        private void Start()
        {
            _inputField = GetComponent<InputField>();
        }
        public override void OnPointerClick(PointerEventData eventData)
        {
            if (_inputField != null)
            {
                if (_inputField.isFocused)
                {
                    StarkSDK.API.GetStarkKeyboard().ShowKeyboard(new StarkKeyboard.ShowKeyboardOptions()
                    {
                        maxLength = maxInputLength,
                        multiple = multiple,
                        defaultValue = _inputField.text,
                        confirmType = confirmType
                    });
                }
            }
        }
    }
}

#endif