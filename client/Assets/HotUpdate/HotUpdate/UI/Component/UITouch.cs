using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UITouch : UIBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public bool dragActive = true;
    public bool scaleActive = true;

    public System.Action<Vector2, Vector2, Vector2> onDragFunc;
    public System.Action<Vector2> onClickFunc;
    public System.Action<float> onScaleFunc;

    private List<PointerEventData> pointerEventDatas = new List<PointerEventData>(2);// 触摸事件参数集合
    private Vector2 touch00Postion;// Touch触摸点1的位置
    private Vector2 touch01Postion;// Touch触摸点2的位置
    private Vector2 lastTouch00Position;
    private Vector2 pressOffSet;// 偏移

    //protected override void Awake()
    //{
    //    onDragFunc = (delta, ChairIndex) =>
    //    {
    //        Debug.Log(delta + "  " + ChairIndex);
    //    };
    //}

    public void OnPointerDown(PointerEventData eventData)
    {
        if (this.pointerEventDatas.Count >= 2)
        {
            return;
        }

        //鼠标左键：-1；鼠标右键：-2；鼠标中键：-3
        this.pointerEventDatas.Add(eventData);
        //偏移
        if (this.pressOffSet == Vector2.zero)
            this.pressOffSet = this.pointerEventDatas[0].delta;

        if (this.pointerEventDatas.Count == 1)
        {
            this.touch00Postion = this.pointerEventDatas[0].position;
            this.lastTouch00Position = this.pointerEventDatas[0].position;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (this.pointerEventDatas.Count <= 0)
            return;

        if (this.pointerEventDatas.Count == 1)
        {
            this.pressOffSet = this.pointerEventDatas[0].delta;
            this.touch00Postion = this.pointerEventDatas[0].position;

            if (dragActive && this.pressOffSet.magnitude >= 1)
            {
                onDragFunc?.Invoke(this.pressOffSet, this.touch00Postion, this.lastTouch00Position);
            }

            this.lastTouch00Position = this.touch00Postion;
        }
        else if (this.pointerEventDatas.Count == 2)
        {
            if (this.touch00Postion == Vector2.zero && this.touch01Postion == Vector2.zero)
            {
                this.touch00Postion = this.pointerEventDatas[0].position;
                this.touch01Postion = this.pointerEventDatas[1].position;
                return;
            }

            //上一帧距离
            float lastDistance = Vector2.Distance(this.touch00Postion, this.touch01Postion);
            //当前帧距离
            float currentDistance = Vector2.Distance(this.pointerEventDatas[0].position, this.pointerEventDatas[1].position);
            //差值
            float difference = lastDistance - currentDistance;

            if (scaleActive)
            {
                onScaleFunc?.Invoke(difference);
            }

            this.touch00Postion = this.pointerEventDatas[0].position;
            this.touch01Postion = this.pointerEventDatas[1].position;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        for (int i = 0; i < this.pointerEventDatas.Count; i++)
        {
            if (this.pointerEventDatas[i].pointerId == eventData.pointerId)
            {
                if (this.pointerEventDatas.Count == 1)
                {
                    if (this.pressOffSet.magnitude < 1)
                    {
                        onClickFunc?.Invoke(this.pointerEventDatas[i].position);
                    }
                }

                this.pointerEventDatas.RemoveAt(i);
                break;
            }
        }

        //偏移归零
        if (this.pointerEventDatas.Count <= 0)
        {
            this.pressOffSet = Vector2.zero;
            this.touch00Postion = Vector2.zero;
            this.touch01Postion = Vector2.zero;
        }

    }

#if UNITY_EDITOR

    void Update()
    {
        if (!scaleActive)
        {
            return;
        }

        float delta = Input.GetAxis("Mouse ScrollWheel");

        if (delta != 0)
        {
            onScaleFunc?.Invoke(delta);
        }
    }
#endif
}