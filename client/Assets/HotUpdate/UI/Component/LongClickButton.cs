using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[AddComponentMenu("UI/LongClickButton")]
public class LongClickButton : Button
{
    public Func<bool, bool> onLongClick;

    public float intervalSecond = 0.1f;
    public bool enableLongClick = true;

    private bool isTouchDown;
    private bool isTouchLong;
    private float lastDownTime;


    private void Update()
    {
        if (isTouchDown)
        {
            if (Time.time - lastDownTime > intervalSecond)
            {
                LongPress();
                lastDownTime = Time.time;
            }
        }
    }

    private void LongPress()
    {
        isTouchLong = true;

        if (onLongClick != null && !onLongClick.Invoke(false))
        {
            Stop();
        }
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);

        isTouchDown = true;
        isTouchLong = false;
        lastDownTime = Time.time;
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);

        ReleaseLongPress();
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (!isTouchLong)
        {
            base.OnPointerClick(eventData);
        }
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);

        // 手指拖出按钮后不一定还会收到本按钮的 PointerUp，必须在这里发送结束回调。
        ReleaseLongPress();
    }

    protected override void OnDisable()
    {
        // 面板隐藏或按钮失活也等价于松手，防止业务层保留长按状态。
        ReleaseLongPress();
        base.OnDisable();
    }

    private void ReleaseLongPress()
    {
        Stop();
        onLongClick?.Invoke(true);
    }

    public void Stop()
    {
        isTouchDown = false;
        isTouchLong = true;
    }
}
