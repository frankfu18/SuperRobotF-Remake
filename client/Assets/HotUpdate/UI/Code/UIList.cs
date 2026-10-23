using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIList : MonoBehaviour
{
    public enum e_Direction
    {
        Horizontal,
        Vertical
    }

    //记录 物体的坐标 和 物体 
    protected class CellInfo
    {
        public Vector2 size;
        public Vector3 pos;
        public GameObject obj;
        public IUIItem item;
        public Button.ButtonClickedEvent buttonEvent;
    };

    public RectTransform ContentRectTrans
    {
        get
        {
            return m_ContentRectTrans;
        }
    }

    public int Count
    {
        get
        {
            if (m_CellInfos != null)
            {
                return m_CellInfos.Length;
            }

            return 0;
        }
    }

    public GameObject m_PointingFirstArrow;
    public GameObject m_PointingEndArrow;

    public e_Direction m_Direction = e_Direction.Horizontal;

    public int m_Row = 1;
    public bool m_IsSelfAdaptSize;

    public float m_HorizontalSpacing = 0f; //横向间距
    public float m_VerticalSpacing = 0f; //纵向间距
    public GameObject m_CellGameObject; //指定的cell
    public GameObject m_CellSelect;//选择框
    public bool isClickSelect = false;
    public bool isSelectTop = false;

    protected RectTransform rectTrans;

    protected float m_PlaneWidth;
    protected float m_PlaneHeight;

    protected float m_ContentWidth;
    protected float m_ContentHeight;

    protected float m_CellObjectWidth;
    protected float m_CellObjectHeight;

    protected GameObject m_Content;
    protected RectTransform m_ContentRectTrans;

    private bool m_isInited = false;


    protected CellInfo[] m_CellInfos;

    protected bool m_IsInited = false;

    protected ScrollRect m_ScrollRect;

    protected int m_MaxCount = -1; //列表数量

    protected int m_MinIndex = -1;
    protected int m_MaxIndex = -1;

    protected bool m_IsClearList = false; //是否清空列表

    private List<object> dataList;
    public object sender;
    private Func<IUIItem> onNewUIItem;

    private Vector2 m_OrgSize;
    private Vector3 m_CellObjScale = Vector3.one;
    private Vector2 m_CellSize;
    private Vector2 lastValue;
    private Vector2 contentPos;
    public UnityEngine.Events.UnityAction<Vector2> onDragChanged;

    private bool isRefresh = false;
    public bool m_IsLoop = true;
    public bool m_IgnoreContentSizeCaculate = false;//是否忽略content大小的计算

    [HideInInspector]
    public int selectIndex = -1;

    public virtual void Init<T>(object _sender) where T : IUIItem, new()
    {
        DisposeAll();

        sender = _sender;

        onNewUIItem = () =>
        { 
            return new T();
        };

        if (m_isInited)
            return;

        if (m_CellSelect != null)
        {
            m_CellSelect.SetActive(false);
        }

        m_Content = this.GetComponent<ScrollRect>().content.gameObject;

        if (m_CellGameObject == null)
        {
            m_CellGameObject = m_Content.transform.GetChild(0).gameObject;
        }

        m_CellObjScale = m_CellGameObject.transform.localScale;
        m_CellGameObject.SetActive(false);
        //SetPoolsObj(m_CellGameObject);

        RectTransform cellRectTrans = m_CellGameObject.GetComponent<RectTransform>();
        cellRectTrans.pivot = new Vector2(0f, 1f);
        CheckAnchor(cellRectTrans);
        cellRectTrans.anchoredPosition = Vector2.zero;

        //记录 Cell 信息

        m_CellObjectHeight = cellRectTrans.rect.height * m_CellObjScale.y;
        m_CellObjectWidth = cellRectTrans.rect.width * m_CellObjScale.x;
        m_CellSize = new Vector2(m_CellObjectWidth, m_CellObjectHeight);

        //记录 Plane 信息
        rectTrans = GetComponent<RectTransform>();
        Rect planeRect = rectTrans.rect;
        m_PlaneHeight = planeRect.height;
        m_PlaneWidth = planeRect.width;

        //记录 Content 信息
        m_ContentRectTrans = m_Content.GetComponent<RectTransform>();
        Rect contentRect = m_ContentRectTrans.rect;
        m_ContentHeight = contentRect.height;
        m_ContentWidth = contentRect.width;

        m_ContentRectTrans.pivot = new Vector2(0f, 1f);
        CheckAnchor(m_ContentRectTrans);

        m_ScrollRect = this.GetComponent<ScrollRect>();
        m_OrgSize = rectTrans.sizeDelta;

        m_ScrollRect.onValueChanged.RemoveAllListeners();
        //添加滑动事件
        m_ScrollRect.onValueChanged.AddListener(delegate (Vector2 value) { ScrollRectListener(value); });

        if (m_PointingFirstArrow != null || m_PointingEndArrow != null)
        {
            m_ScrollRect.onValueChanged.AddListener(delegate (Vector2 value) { OnDragListener(value); });
            OnDragListener(Vector2.zero);
        }

        //InitScrollBarGameObject(); // 废弃

        m_isInited = true;

    }
    //检查 Anchor 是否正确
    private void CheckAnchor(RectTransform rectTrans)
    {
        if (m_Direction == e_Direction.Vertical)
        {
            if (!((rectTrans.anchorMin == new Vector2(0, 1) && rectTrans.anchorMax == new Vector2(0, 1)) ||
                        (rectTrans.anchorMin == new Vector2(0, 1) && rectTrans.anchorMax == new Vector2(1, 1))))
            {
                rectTrans.anchorMin = new Vector2(0, 1);
                rectTrans.anchorMax = new Vector2(1, 1);
            }
        }
        else
        {
            if (!((rectTrans.anchorMin == new Vector2(0, 1) && rectTrans.anchorMax == new Vector2(0, 1)) ||
                        (rectTrans.anchorMin == new Vector2(0, 0) && rectTrans.anchorMax == new Vector2(0, 1))))
            {
                rectTrans.anchorMin = new Vector2(0, 0);
                rectTrans.anchorMax = new Vector2(0, 1);
            }
        }
    }

    public void Update()
    {
        if (isRefresh)
        {
            //UpdateCheck();
            //如果为空不刷新return
            if (m_CellInfos == null)
            {
                isRefresh = false;
                return;
            }
            //设置初始位置
            Vector3 pos = Vector3.zero;
            //设置content大小
            Vector3 conSize = Vector3.zero;
            //垂直情况下 content x值不变
            if (m_Direction == e_Direction.Vertical)
            {
                conSize.x = m_ContentRectTrans.sizeDelta.x;
            }//水平情况下 content y值不变
            else
            {
                conSize.y = m_ContentRectTrans.sizeDelta.y;
            }

            for (int i = 0; i < m_CellInfos.Length; i++)
            {
                CellInfo cellInfo = m_CellInfos[i];
                if (m_Direction == e_Direction.Vertical)
                {
                    //垂直 n倍 y值增加
                    if (i % m_Row == 0)
                    {
                        conSize.y += cellInfo.size.y;
                        conSize.y += (i / m_Row == 0 ? 0 : m_VerticalSpacing);
                    }
                }
                else
                {
                    //水平 n倍 x值增加
                    if (i % m_Row == 0)
                    {
                        conSize.x += cellInfo.size.x;
                        conSize.x += m_HorizontalSpacing;
                    }
                }
                //垂直x位置不变 
                if (m_Direction == e_Direction.Vertical)
                {

                    pos.x = cellInfo.pos.x;
                    //如果为1的时候
                    pos.y = -(conSize.y - cellInfo.size.y);
                }
                else
                {
                    //水平
                    pos.y = cellInfo.pos.y;
                    pos.x = (conSize.x - cellInfo.size.x);
                }
                //当前的位置需要跟随发生变化
                cellInfo.pos = pos;

                //判断是否超出显示范围
                if (IsOutRange(cellInfo))
                {
                    //把超出范围的cell 扔进 poolsObj里
                    if (cellInfo.obj != null)
                    {
                        UnusedCellObj(cellInfo);
                    }
                    continue;
                }
                //需要显示的数据 如果为空 poolsObj里面去取
                if (cellInfo.obj == null)
                {
                    SetCellObj(cellInfo, i, pos);
                }
                else
                {
                    //否则设置当前的位置
                    cellInfo.obj.transform.localPosition = pos;
                }
                //设置当前物体的大小
                cellInfo.obj.GetComponent<RectTransform>().sizeDelta = cellInfo.size;
            }
            //设置content的大小
            if (!m_IgnoreContentSizeCaculate)
            {
                m_ContentRectTrans.sizeDelta = conSize;
            }

            //刷新关闭
            isRefresh = false;
        }
    }

    private object GetShowItmeInfo(int _index)
    {
        return dataList[_index];
    }

    public void ShowList<T>(IEnumerable<T> _datas)
    {
        int num = 0;
        if (_datas != null)
        {
            num = _datas.Count();
            dataList = new List<object>(num);

            foreach (T data in _datas)
            {
                dataList.Add(data);
            }
        }
        else
        {
            dataList = new List<object>();
        }

        m_IsClearList = num == 0;

        m_MinIndex = -1;
        m_MaxIndex = -1;

        //-> 计算 Content 尺寸  
        if (m_Direction == e_Direction.Vertical)
        {
            //content=Item的(单个高度+高度间距)*(显示的个数) 垂直布局组件
            float contentSize = (m_VerticalSpacing + m_CellObjectHeight) * Mathf.CeilToInt((float)num / m_Row);
            m_ContentHeight = contentSize;
            //m_ContentWidth = m_ContentRectTrans.sizeDelta.x;
            m_ContentWidth = 0;
            contentSize = m_IsSelfAdaptSize ? contentSize : (contentSize < rectTrans.rect.height ? rectTrans.rect.height : contentSize);
            m_ContentRectTrans.sizeDelta = new Vector2(m_ContentWidth, contentSize);
            if (num != m_MaxCount)
            {
                m_ContentRectTrans.anchoredPosition = new Vector2(m_ContentRectTrans.anchoredPosition.x, 0);
            }
        }
        else
        {
            //content=Item的(单个横向间距+单个横向宽度)*(显示的个数) 水平布局组件
            float contentSize = (m_HorizontalSpacing + m_CellObjectWidth) * Mathf.CeilToInt((float)num / m_Row);
            m_ContentWidth = contentSize;
            //m_ContentHeight = m_ContentRectTrans.sizeDelta.x;
            m_ContentHeight = 0;
            //取Content的大小
            contentSize = m_IsSelfAdaptSize ? contentSize : (contentSize < rectTrans.rect.width ? rectTrans.rect.width : contentSize);
            m_ContentRectTrans.sizeDelta = new Vector2(contentSize, m_ContentHeight);
            if (num != m_MaxCount)
            {
                m_ContentRectTrans.anchoredPosition = new Vector2(0, m_ContentRectTrans.anchoredPosition.y);
            }
        }

        //-> 计算 开始索引
        int lastEndIndex = 0;

        //-> 过多的物体 扔到对象池 ( 首次调 ShowList函数时 则无效 )
        if (m_IsInited)
        {
            lastEndIndex = num - m_MaxCount > 0 ? m_MaxCount : num;
            lastEndIndex = m_IsClearList ? 0 : lastEndIndex;

            int count = m_IsClearList ? (m_CellInfos != null ? m_CellInfos.Length : 0) : m_MaxCount;
            if (count > 0)
            {
                for (int i = lastEndIndex; i < count; i++)
                {
                    if (m_CellInfos[i].obj != null)
                    {
                        UnusedCellObj(m_CellInfos[i]);
                    }
                }
            }
        }

        //-> 以下四行代码 在for循环所用
        CellInfo[] tempCellInfos = m_CellInfos;
        m_CellInfos = new CellInfo[num];

        //-> 1: 计算 每个Cell坐标并存储 2: 显示范围内的 Cell
        for (int i = 0; i < num; i++)
        {
            // * -> 存储 已有的数据 ( 首次调 ShowList函数时 则无效 )
            if (m_MaxCount > 0 && i < lastEndIndex)
            {
                CellInfo tempCellInfo = tempCellInfos[i];

                m_CellInfos[i] = tempCellInfo;

                //-> 计算是否超出范围
                if (!IsOutRange(tempCellInfo))
                {
                    //-> 记录显示范围中的 首位index 和 末尾index
                    m_MinIndex = m_MinIndex == -1 ? i : m_MinIndex; //首位index
                    m_MaxIndex = i; // 末尾index

                    SetCellObj(tempCellInfo, i, tempCellInfo.pos);
                }
                else
                {
                    if (tempCellInfo.obj != null)
                    {
                        UnusedCellObj(tempCellInfo);
                    }
                }
                continue;
            }

            CellInfo cellInfo = new CellInfo();
            cellInfo.size = m_CellSize;
            float pos = 0;  //坐标( isVertical ? 记录Y : 记录X )
            float rowPos = 0; //计算每排里面的cell 坐标

            // * -> 计算每个Cell坐标
            if (m_Direction == e_Direction.Vertical)
            {
                pos = m_CellObjectHeight * Mathf.FloorToInt(i / m_Row) + m_VerticalSpacing * Mathf.FloorToInt(i / m_Row);
                rowPos = m_CellObjectWidth * (i % m_Row) + m_HorizontalSpacing * (i % m_Row);
                cellInfo.pos = new Vector3(rowPos, -pos, 0);
            }
            else
            {
                pos = m_CellObjectWidth * Mathf.FloorToInt(i / m_Row) + m_HorizontalSpacing * Mathf.FloorToInt(i / m_Row);
                rowPos = m_CellObjectHeight * (i % m_Row) + m_VerticalSpacing * (i % m_Row);
                cellInfo.pos = new Vector3(pos, -rowPos, 0);
            }

            //-> 计算是否超出范围
            float cellPos = m_Direction == e_Direction.Vertical ? cellInfo.pos.y : cellInfo.pos.x;
            if (IsOutRange(cellInfo))
            {
                m_CellInfos[i] = cellInfo;

                UnusedCellObj(m_CellInfos[i]);

                continue;
            }

            //-> 记录显示范围中的 首位index 和 末尾index
            m_MinIndex = m_MinIndex == -1 ? i : m_MinIndex; //首位index
            m_MaxIndex = i; // 末尾index

            m_CellInfos[i] = cellInfo;

            SetCellObj(cellInfo, i, cellInfo.pos);
        }

        m_MaxCount = num;
        m_IsInited = true;

        Vector2 tempSize = rectTrans.sizeDelta;
        if (m_IsSelfAdaptSize)
        {
            if (m_Direction == e_Direction.Vertical)
            {
                tempSize.y = m_ContentRectTrans.sizeDelta.y;
            }
            else
            {
                tempSize.x = m_ContentRectTrans.sizeDelta.x;
            }
        }
        else
        {
            if (m_Direction == e_Direction.Vertical)
            {
                tempSize.y = m_OrgSize.y;
            }
            else
            {
                tempSize.x = m_OrgSize.x;
            }
        }

        rectTrans.sizeDelta = tempSize;

        OnDragListener(Vector2.zero);
    }

    // 更新滚动区域的大小
    public void UpdateSize()
    {
        Rect rect = GetComponent<RectTransform>().rect;
        m_PlaneHeight = rect.height;
        m_PlaneWidth = rect.width;
    }

    
    public virtual void ScrollRectListener(Vector2 value)
    {
        contentPos = m_ContentRectTrans.localPosition;
        if (Mathf.Abs(lastValue.x - contentPos.x) >= 0.5f || Mathf.Abs(lastValue.y - contentPos.y) >= 0.5f)
        {
            onDragChanged?.Invoke(value);
            lastValue = contentPos;
            isRefresh = true;
        }

    }

    //判断是否超出显示范围
    protected bool IsOutRange(CellInfo cell)
    {
        if (!m_IsLoop)
        {
            return false;
        }
        //获取了content的位置
        Vector3 listP = m_ContentRectTrans.anchoredPosition;
        if (m_Direction == e_Direction.Vertical)
        {
            if (cell.pos.y + listP.y > cell.size.y || cell.pos.y + listP.y < -rectTrans.rect.height)
            {
                return true;
            }
        }
        else
        {
            if (cell.pos.x + listP.x < -cell.size.x || cell.pos.x + listP.x > rectTrans.rect.width)
            {
                return true;
            }
        }
        return false;
    }

    //对象池 机制  (存入， 取出) cell
    protected Stack<GameObject> poolsObj = new Stack<GameObject>();
    //取出 cell
    private GameObject GetPoolsObj()
    {
        GameObject cell = null;
        if (poolsObj.Count > 0)
        {
            cell = poolsObj.Pop();
        }

        if (cell == null)
        {
            cell = Instantiate(m_CellGameObject) as GameObject;
        }
        cell.transform.SetParent(m_Content.transform);
        cell.transform.localScale = Vector3.one;
        cell.SetActive(true);

        return cell;
    }
    //存入 cell
    private void SetPoolsObj(GameObject cell)
    {
        if (cell != null)
        {
            poolsObj.Push(cell);
            cell.SetActive(false);
        }
    }

    protected virtual void SetCellObj(CellInfo _cell, int _index, Vector3 _pos)
    {
        if (_cell.obj != null)
        {
            UnusedCellObj(_cell);
        }

        _cell.obj = GetPoolsObj();
        _cell.obj.transform.localPosition = _pos;
        _cell.obj.gameObject.name = _index.ToString();

        _cell.item = onNewUIItem();

        _cell.item.Create(_cell.obj, _index, this);
        _cell.item.Show(GetShowItmeInfo(_index));

        EmptyButton button = _cell.obj.GetComponent<EmptyButton>();
        if (button == null)
        {
            button = _cell.obj.AddComponent<EmptyButton>();
        }

        button.onClick.AddListener(() =>
        {
            if (_cell.obj == null) {
                return;
            }
            UIItem uiItem = _cell.item as UIItem;
            uiItem.Click();

            if (isClickSelect)
            {
                SetSelect(uiItem.Index);
            }
        });
        _cell.buttonEvent = button.onClick;
    }

    protected virtual void UnusedCellObj(CellInfo _cell)
    {
        if (_cell == null || _cell.obj == null)
        {
            return;
        }

        SetPoolsObj(_cell.obj);
        
        _cell.item.Destroy();
        _cell.obj = null;
        _cell.item = null;
        _cell.buttonEvent.RemoveAllListeners();
    }

    public void DisposeAll()
    {
        if (m_CellInfos != null)
        {
            foreach (var cell in m_CellInfos)
            {
                UnusedCellObj(cell);
            }
            m_CellInfos = null;
        }

        m_MaxCount = 0;
    }

    protected void OnDestroy()
    {
        DisposeAll();
    }

    public virtual void OnClickCell(GameObject cell) { }

    //-> ExpandCircularScrollView 函数
    public virtual void OnClickExpand(int index) { }

    //-> FlipCircularScrollView 函数
    public virtual void SetToPageIndex(int index) { }

    /// <summary>
    /// 跳转至某位置 index：item所在位置
    /// </summary>
    public virtual void SetToScrollPosition(int index, bool anim = false)
    {
        CellInfo cellInfo = m_CellInfos[index];
        RectTransform viewPort = m_ScrollRect.viewport;
        if (!viewPort)
            viewPort = m_ScrollRect.transform as RectTransform;

        Vector2 scrollSize = viewPort.rect.size;

        if (m_Direction == e_Direction.Horizontal)
        {
            float tgtPosX = Mathf.Abs(cellInfo.pos.x) + cellInfo.size.x - scrollSize.x;

            float ratio = 0;

            float dp = m_ScrollRect.content.rect.width - scrollSize.x;
            if (dp != 0)
            {
                ratio = tgtPosX / dp;
            }

            if (anim)
            {
                float targetRatio = ratio;

                DOTween.To(() => m_ScrollRect.horizontalNormalizedPosition,
                    x => m_ScrollRect.horizontalNormalizedPosition = x,
                    targetRatio, 1);
            }
            else
            {
                m_ScrollRect.horizontalNormalizedPosition = ratio;
            }
        }
        else
        {
            float tgtPosY = Mathf.Abs(cellInfo.pos.y) + cellInfo.size.y - scrollSize.y;

            float ratio = 0;
            float dp = m_ScrollRect.content.rect.height - scrollSize.y;
            if (dp != 0)
            {
                ratio = 1 - tgtPosY / dp;
            }

            if (anim)
            {
                float targetRatio = ratio;

                DOTween.To(() => m_ScrollRect.verticalNormalizedPosition,
                    x => m_ScrollRect.verticalNormalizedPosition = x,
                    targetRatio, 1);
            }
            else
            {
                m_ScrollRect.verticalNormalizedPosition = ratio;
            }

            
        }

        m_ScrollRect.StopMovement();
        isRefresh = true;
    }

    protected void OnDragListener(Vector2 value)
    {
        float normalizedPos = m_Direction == e_Direction.Vertical ? m_ScrollRect.verticalNormalizedPosition : m_ScrollRect.horizontalNormalizedPosition;

        if (m_Direction == e_Direction.Vertical)
        {
            if (m_ContentHeight - rectTrans.rect.height < 10)
            {
                if (m_PointingFirstArrow != null)
                    m_PointingFirstArrow.SetActive(false);
                if (m_PointingEndArrow != null)
                    m_PointingEndArrow.SetActive(false);
                return;
            }
        }
        else
        {
            if (m_ContentWidth - rectTrans.rect.width < 10)
            {
                if (m_PointingFirstArrow != null)
                    m_PointingFirstArrow.SetActive(false);
                if (m_PointingEndArrow != null)
                    m_PointingEndArrow.SetActive(false);
                return;
            }
        }

        if (normalizedPos >= 0.9)
        {
            if (m_PointingFirstArrow != null)
                m_PointingFirstArrow.SetActive(false);
            if (m_PointingEndArrow != null)
                m_PointingEndArrow.SetActive(true);
        }
        else if (normalizedPos <= 0.1)
        {
            if (m_PointingFirstArrow != null)
                m_PointingFirstArrow.SetActive(true);
            if (m_PointingEndArrow != null)
                m_PointingEndArrow.SetActive(false);
        }
        else
        {
            if (m_PointingFirstArrow != null)
                m_PointingFirstArrow.SetActive(true);
            if (m_PointingEndArrow != null)
                m_PointingEndArrow.SetActive(true);
        }
    }

    public bool SelectCell(int index)
    {
        CellInfo cellInfo = m_CellInfos[index];
        bool isOutRange = IsOutRange(cellInfo);
        if (cellInfo.obj != null)
        {
            if (!isOutRange)
            {
                if (isClickSelect)
                {
                    (cellInfo.item as UIItem).Click();
                }
                SetSelect((cellInfo.item as UIItem).Index);
            }
            else
            {
                if (m_CellSelect != null)
                {
                    m_CellSelect.SetActive(false);
                }
            }
        }

        if (isOutRange)
        {
            selectIndex = index;
        }

        return !isOutRange;
    }

    public void SetSelect(int index, bool flag = true)
    {
        if (m_CellSelect == null || index < 0 || index > m_CellInfos.Length)
        {
            return;
        }

        selectIndex = index;

        CellInfo cellInfo = m_CellInfos[index];
        if (cellInfo.obj != null)
        {
            m_CellSelect.SetActive(flag);
            m_CellSelect.GetComponent<RectTransform>().anchoredPosition = cellInfo.obj.transform.GetComponent<RectTransform>().anchoredPosition;
            if (isSelectTop)
            {
                m_CellSelect.transform.SetAsLastSibling();
            }
            else
            {
                m_CellSelect.transform.SetAsFirstSibling();
            }
        }
    }

    public void CancelSelect()
    {
        if (m_CellSelect == null)
        {
            return;
        }

        m_CellSelect.SetActive(false);
    }

    public GameObject GetCell(int index)
    {
        if (m_CellInfos == null || index < 0 || index > m_CellInfos.Length)
        {
            return null;
        }

        return m_CellInfos[index].obj;
    }

    public T GetItem<T>(int index) where T : UIItem
    {
        if (m_CellInfos == null || index < 0 || index > m_CellInfos.Length || m_CellInfos[index] == null)
        {
            return default(T);
        }

        return m_CellInfos[index].item as T;
    }

    public void UpdateListRectTransformOrgSize()
    {
        m_OrgSize = rectTrans.sizeDelta;
    }
    
}
