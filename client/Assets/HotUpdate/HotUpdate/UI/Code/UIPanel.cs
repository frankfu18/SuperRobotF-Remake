using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public abstract class UIPanel : UIContainer, IUIInterface
{
    private class PanelRefData
    {
        public object data;
        public int layer;
        public Action<UIPanel> onOpenDone;//打开完成事件回调
    } 

    private const string Panel_Container = "m_GameObject_Container";
    private const string Button_Bg = "m_Button_Bg";
    private const string Button_Close = "m_Button_Close";

    public UIConfig UIConfigId { get { return uiConfigId; } }

    public UIInfo UIInfo { get { return uiInfo; } }

    public int SortLayer
    {
        get { return this.refDataDic[this.refCount].layer; }
        private set
        {
            int sortLayer = value;

            this.refDataDic[this.refCount].layer = sortLayer;
            if (this.canvas != null)
            {
                this.canvas.overrideSorting = true;
                this.canvas.sortingLayerName = "UI";
                this.canvas.sortingOrder = sortLayer;
            }

            foreach (var dic in childCanvasLayerDic)
            {
                Canvas childCanvas = dic.Key;
                childCanvas.overrideSorting = true;
                childCanvas.sortingLayerName = "UI";
                childCanvas.sortingOrder = sortLayer + Mathf.Min(dic.Value, 1);
            }
        }
    }

    public int RefCount
    {
        get { return refCount; }
    }

    public bool IsHasClose
    {
        get { return isHasClose; }
    }

    protected UIConfig uiConfigId;
    protected UIInfo uiInfo;
    protected object data;
    private Dictionary<int, PanelRefData> refDataDic = new Dictionary<int, PanelRefData>();

    private int refCount;
    private Canvas canvas;
    private Dictionary<Canvas, int> childCanvasLayerDic = new Dictionary<Canvas, int>();
    private UIMono uiMono;
    private GameObject panelContainer;//界面容器，用于动画
    protected Animator animator;//动画控制器
    private bool isHasClose;//是否已经关闭

    public void Init(UIInfo _uiInfo)
    {
        this.uiInfo = _uiInfo;
        this.uiConfigId = this.uiInfo.uiConfigId;

        OnInit();
    }

    public void Open(object _data, Action<UIPanel> _onOpenDone)
    {
        this.refDataDic[this.refCount].data = _data;
        this.refDataDic[this.refCount].onOpenDone = _onOpenDone;

        this.isHasClose = false;

        Show(true);

        UISystem.Instance.ExecutePanelOpenCallback(this.uiConfigId);

        if (this.uiInfo.showPlayAnimation)
        {
            ShowAnimTween(OnShowAnimComplete);
        }
        else
        {
            this.isClickButton = true;
            OnShowAnimComplete();
        }
    }

    public void Show(bool _first)
    {
        this.data = this.refDataDic[this.refCount].data;
        if (_first)
        {
            Action<UIPanel> onFunc = this.refDataDic[this.refCount].onOpenDone;
            if (onFunc != null)
            {
                onFunc(this);

                this.refDataDic[this.refCount].onOpenDone = null;
            }
        }
        this.isClickButton = !_first;

        OnShow();
    }

    public void SetLayer(int _layer)
    {
        SortLayer = _layer;
    }

    public Canvas GetChildCanvas(string _childName)
    {
        Canvas childCanvas = GetVaule<Canvas>(_childName);
        if (childCanvas != null)
        {
            childCanvasLayerDic.Add(childCanvas, childCanvas.sortingOrder);
        }

        return childCanvas;
    }

    public void Hide()
    {
        OnHide();
    }

    public void Create(GameObject _go, int _layer, object _container = null)
    {
        this.go = _go;
        this.rectTransform = (RectTransform)this.go.transform;
        this.rectTransform.pivot = new Vector2(0.5f, 0.5f);

        this.rectTransform.anchoredPosition = Vector2.zero;

        this.rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, 0);
        this.rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, 0);

        this.rectTransform.anchorMax = Vector2.one;
        this.rectTransform.anchorMin = Vector2.zero;

        this.canvas = this.go.GetComponent<Canvas>();
        if (this.canvas == null)
        {
            this.canvas = this.go.AddComponent<Canvas>();
        }
        this.uiMono = this.go.GetComponent<UIMono>();
        if (this.uiMono == null)
        {
            this.uiMono = this.go.AddComponent<UIMono>();
        }
        this.uiMono.onUpdate = Update;
        this.animator = this.go.GetComponent<Animator>();

        SortLayer = _layer;

        Bind(_go, _container);

        foreach (var dic in childCanvasLayerDic)
        {
            Canvas childCanvas = dic.Key;
            childCanvas.overrideSorting = true;
            childCanvas.sortingLayerName = "UI";
            childCanvas.sortingOrder = SortLayer + Mathf.Max(dic.Value, 1);
        }

        OnCreate();
    }

    protected override void OnBindAfter()
    {
        InitCommonFieldComponent();
    }

    private void InitCommonFieldComponent()
    {
        TryGetVaule(Panel_Container, out this.panelContainer);

        if (TryGetVaule(Button_Bg, out Button bgBtn))
        {
            SetButtonClick(bgBtn, Close);
        }

        if (TryGetVaule(Button_Close, out Button closeBtn))
        {
            SetButtonClick(closeBtn, Close);
        }
    }

    protected virtual void StartCoroutine(IEnumerator task)
    {
        uiMono.StartCoroutine(task);
    }

    public override void Update(float _deltaTime)
    {
        OnUpdate(_deltaTime);
    }

    protected void Close()
    {
        Close(null);
    }

    protected void Close(System.Action _onComplete)
    {
        if (this.isHasClose)
        {
            return;
        }

        this.isHasClose = true;
        this.isClickButton = false;

        EventCenterSystem.Instance.SendEvent(EventConst.Panel_Close, this.uiConfigId);

        _onComplete?.Invoke();
        UISystem.Instance.Close(this);
    }

    public void Shut()
    {
        OnClose();
    }

    protected virtual void OnInit()
    {

    }

    protected virtual void OnCreate()
    {

    }

    protected virtual void OnShow()
    {

    }

    protected virtual void OnUpdate(float _deltaTime)
    {

    }

    protected virtual void OnShowAnimComplete()
    {

    }

    protected virtual void OnHide()
    {

    }

    protected virtual void OnClose()
    {

    }

    protected override void OnUnbind()
    {
        base.OnUnbind();
    }

    public void Destroy()
    {
        ReduceRef();//引用减一
        Hide();
        StopAnimTween();

        if (this.refCount <= 0)//没有引用了，可以销毁了
        {
            if (this.uiMono != null)
            {
                this.uiMono.onUpdate = null;
            }

            OnDestroy();
            Unbind();

            GameObject.Destroy(this.GameObject);

            //todo暂时不释放，图片计数有问题
            //AssetSystem.Instance.UnloadUIPrefab(uiInfo.prefabName, true);
        }
    }

    protected virtual void OnDestroy()
    {

    }

    private void ShowAnimTween(System.Action _onComplete)
    {
        GameObject animTarget = this.panelContainer != null ? this.panelContainer : this.go;

        PlayAnimTween(animTarget, _onComplete);
    }

    protected void PlayAnimTween(GameObject _animTarget, System.Action _onComplete = null)
    {
        this.isClickButton = false;
        _animTarget.transform.DOKill();
        _animTarget.transform.localScale = Vector3.zero;
        _animTarget.transform.DOScale(Vector3.one, UIAnimateConfig.Open_UIPanel_Seconds).SetUpdate(true).SetLoops(1).SetEase(Ease.InOutExpo).Play().onComplete += () => 
        {
            this.isClickButton = true; 
            _onComplete?.Invoke();
        };
    }

    private void StopAnimTween()
    {
        if (!this.uiInfo.showPlayAnimation)
        {
            return;
        }

        GameObject animTarget = this.panelContainer != null ? this.panelContainer : this.go;

        animTarget.transform.DOKill();
    }

    public void AddRef()
    {
        refCount++;

        this.refDataDic.Add(refCount, new PanelRefData());
    }

    public void ReduceRef()
    {
        this.refDataDic.Remove(refCount);

        refCount--;
    }

    protected void AddClosePanelClick(Button _btn)
    {
        AddButtonClick(_btn, Close);
    }
    
}
