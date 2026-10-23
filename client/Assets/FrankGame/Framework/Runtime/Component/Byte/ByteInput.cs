#if BYTE_GAME
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StarkSDKSpace;

namespace FrankGame
{
    [RequireComponent(typeof(InputField))]
    public class ByteInput : MonoBehaviour
    {
        public InputField input;

        void Awake()
        {
            input = this.GetComponent<InputField>();
        }

        private void Start()
        {
            SetInputTexts();
            RegisterKeyboardEvents();
        }

        private void SetInputTexts()
        {
            input.text = "done";
            var comp = input.GetComponent<ClickableInputField>();
            if (comp == null)
            {
                comp = input.gameObject.AddComponent<ClickableInputField>();
            }
            comp.multiple = false;
            comp.confirmType = input.text;
        }

        private void OnDestroy()
        {
            UnregisterKeyboardEvents();
        }

        private void RegisterKeyboardEvents()
        {
            StarkSDK.API.GetStarkKeyboard().onKeyboardInputEvent += OnKeyboardInput;
            StarkSDK.API.GetStarkKeyboard().onKeyboardConfirmEvent += OnKeyboardConfirm;
            StarkSDK.API.GetStarkKeyboard().onKeyboardCompleteEvent += OnKeyboardComplete;
        }

        private void UnregisterKeyboardEvents()
        {
            StarkSDK.API.GetStarkKeyboard().onKeyboardInputEvent -= OnKeyboardInput;
            StarkSDK.API.GetStarkKeyboard().onKeyboardConfirmEvent -= OnKeyboardConfirm;
            StarkSDK.API.GetStarkKeyboard().onKeyboardCompleteEvent -= OnKeyboardComplete;
        }

        private void OnKeyboardInput(string value)
        {
            //Debug.Log($"OnKeyboardInput: {value}");
            if (input.isFocused)
            {
                input.text = value;
            }
        }

        private void OnKeyboardConfirm(string value)
        {
            //Debug.Log($"OnKeyboardConfirm: {value}");
        }

        private void OnKeyboardComplete(string value)
        {
            //Debug.Log($"OnKeyboardComplete: {value}");
        }
    }
}
#endif