using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIFullScreenJoystick : UIBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public RectTransform joystickRoot;
    public UIJoystick joystick;

    // 全屏摇杆方向变化回调，外部自行决定如何使用方向。
    public Action<Vector2> onJoystickInput;

    // 全屏摇杆松手回调，外部通常在这里清空输入。
    public Action onJoystickEnd;

    private RectTransform rectTransform;
    private bool isPressing;

    protected override void Awake()
    {
        base.Awake();
        this.rectTransform = this.transform as RectTransform;
        EnsureRaycastImage();
        SetJoystickVisible(false);
    }

    public void Bind(UIJoystick _joystick, RectTransform _joystickRoot)
    {
        this.joystick = _joystick;
        this.joystickRoot = _joystickRoot;
        BindJoystickCallbacks();
        SetJoystickVisible(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (this.joystick == null || this.joystickRoot == null)
        {
            return;
        }

        this.isPressing = true;
        SetJoystickRootPosition(eventData);
        SetJoystickVisible(true);
        this.joystick.ResetJoystick();
        this.joystick.UpdateJoystick(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!this.isPressing || this.joystick == null)
        {
            return;
        }

        this.joystick.UpdateJoystick(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        EndJoystick();
    }

    public void EndJoystick()
    {
        this.isPressing = false;

        if (this.joystick != null)
        {
            this.joystick.ResetJoystick();
        }

        SetJoystickVisible(false);
        this.onJoystickEnd?.Invoke();
    }

    public void SetJoystickVisible(bool _visible)
    {
        if (this.joystickRoot != null)
        {
            this.joystickRoot.gameObject.SetActive(_visible);
        }
    }

    private void BindJoystickCallbacks()
    {
        if (this.joystick == null)
        {
            return;
        }

        this.joystick.onJoystickInput = (dir) =>
        {
            this.onJoystickInput?.Invoke(dir);
        };

        this.joystick.onJoystickEnd = () =>
        {
            EndJoystick();
        };
    }

    private void SetJoystickRootPosition(PointerEventData _eventData)
    {
        if (this.rectTransform == null)
        {
            this.rectTransform = this.transform as RectTransform;
        }

        if (this.rectTransform == null || this.joystickRoot == null)
        {
            return;
        }

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            this.rectTransform,
            _eventData.position,
            _eventData.pressEventCamera,
            out Vector2 localPos);

        // 按下屏幕的位置就是摇杆中心点，后续拖动偏移都相对这个中心计算。
        this.joystickRoot.anchoredPosition = localPos;
    }

    private void EnsureRaycastImage()
    {
        Image image = this.GetComponent<Image>();
        if (image == null)
        {
            image = this.gameObject.AddComponent<Image>();
            image.color = new Color(1f, 1f, 1f, 0f);
        }

        image.raycastTarget = true;
    }
}
