using System;
using UnityEngine;
using UnityEngine.EventSystems;

public enum EJoystickType
{
    All = 0,        // 全方向输入：上下左右都会生效。
    Horizontal = 1, // 只允许左右输入：上下方向会被过滤。
    Vertical = 2,   // 只允许上下输入：左右方向会被过滤。
}

public class UIJoystick : UIBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public RectTransform handle;
    public float radius = 120f;

    [SerializeField]
    private EJoystickType joystickType = EJoystickType.All;

    // 摇杆方向变化回调，参数为按方向类型过滤后的归一化方向。
    public Action<Vector2> onJoystickInput;

    // 摇杆松手回调。
    public Action onJoystickEnd;

    private RectTransform rectTransform;

    protected override void Awake()
    {
        base.Awake();
        this.rectTransform = this.transform as RectTransform;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        UpdateJoystick(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        UpdateJoystick(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ResetJoystick();
        this.onJoystickEnd?.Invoke();
    }

    public void ResetJoystick()
    {
        if (this.handle != null)
        {
            this.handle.anchoredPosition = Vector2.zero;
        }
    }

    // 根据固定摇杆自身坐标刷新 handle 位置，并按配置的方向类型输出输入方向。
    public void UpdateJoystick(PointerEventData _eventData)
    {
        if (this.rectTransform == null)
        {
            this.rectTransform = this.transform as RectTransform;
        }

        if (this.rectTransform == null)
        {
            return;
        }

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            this.rectTransform,
            _eventData.position,
            _eventData.pressEventCamera,
            out Vector2 localPos);

        // ScreenPointToLocalPointInRectangle 得到的是相对 pivot 的坐标；
        // handle 的 anchoredPosition 以摇杆中心为零点，所以这里统一转成“相对矩形中心”的偏移。
        Vector2 centerLocalPos = localPos - (Vector2)this.rectTransform.rect.center;
        float safeRadius = Mathf.Max(1f, this.radius);
        Vector2 clampedPos = Vector2.ClampMagnitude(centerLocalPos, safeRadius);
        if (this.handle != null)
        {
            // handle 跟随手指方向移动，但限制在摇杆半径内，避免视觉超出底盘。
            this.handle.anchoredPosition = clampedPos;
        }

        Vector2 inputDir = clampedPos / safeRadius;
        inputDir = FilterInputDirByJoystickType(inputDir);
        this.onJoystickInput?.Invoke(inputDir);
    }

    private Vector2 FilterInputDirByJoystickType(Vector2 _inputDir)
    {
        switch (this.joystickType)
        {
            case EJoystickType.Horizontal:
                return new Vector2(_inputDir.x, 0);
            case EJoystickType.Vertical:
                return new Vector2(0, _inputDir.y);
            case EJoystickType.All:
            default:
                return _inputDir;
        }
    }
}
