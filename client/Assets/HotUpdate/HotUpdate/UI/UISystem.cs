using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public class UISystem : Singlet<UISystem>
{
    public Camera UICamera
    {
        get { return uiCamera; }
    }

    public Canvas RootCanvas
    {
        get { return rootCanvas; }
    }

    public GuideMask GuideMask
    {
        get { return guideMask; }
    }

    public int CameraHideLayer => _CameraHidLayer;

    private const string UI_Root = "UIRoot";
    private const string UI_Camera = "UICamera";
    private const string Guide_Mask = "GuideMask";
    private const string Wait_Mask = "WaitMask";
    private const int UI_Delta_Depth = 10;

    private Dictionary<EUILayer, Stack<IUIInterface>> uiStack = new Dictionary<EUILayer, Stack<IUIInterface>>();
    private Dictionary<UIConfig, List<Action<UIConfig>>> panelOpenCallbackDic = new Dictionary<UIConfig, List<Action<UIConfig>>>();
    private Transform UIRoot;
    private Canvas rootCanvas;
    private Camera uiCamera;
    private GuideMask guideMask;
    private WaitMask waitMask;

    private int _CameraHidLayer = LayerMask.NameToLayer("CamereHide");

    public void Init()
    {
        UIRoot = GameObject.Find(UI_Root).transform;
        rootCanvas = UIRoot.GetComponent<Canvas>();
        uiCamera = GameObject.Find(UI_Camera).GetComponent<Camera>();
        guideMask = UIRoot.Find(Guide_Mask)?.GetComponent<GuideMask>();
        waitMask = UIRoot.Find(Wait_Mask)?.GetComponent<WaitMask>();
        if (guideMask != null)
        {
            guideMask.SetCanvas(rootCanvas, uiCamera);
            guideMask.gameObject.SetActive(false);
        }
        if (waitMask != null)
        {
            waitMask.gameObject.SetActive(false);
        }

        FlyTextManager.Instance.Init(UIRoot);

        foreach (var uiLayer in System.Enum.GetValues(typeof(EUILayer)))
        {
            uiStack.Add((EUILayer)uiLayer, new Stack<IUIInterface>());
        }

        TimeSystem.Instance.onRequestServerTimeSuccess = () =>
        {
            CloseWaitMask();
        };

        TimeSystem.Instance.onRequestServerTime = () =>
        {
            OpenWaitMask(false);
        };

        TimeSystem.Instance.onRequestServerTimeFail = () =>
        {
            Open(UIConfig.CommonPopupPanel, new stCommonPopupPanelData
            {
                btnCount = 1,
                content = LanguageSystem.Instance.GetLanguage("settings.tips.netError"),
                onConfirmFunc = () => Application.Quit(),
                confirmBtnName = LanguageSystem.Instance.GetLanguage("btn_exit"),
                isNotClosePopup = true,
            });
        };
    }

    

    public void AddPanelOpenCallback(UIConfig _panelName, Action<UIConfig> _onCallback)
    {
        if (_onCallback == null)
        {
            return;
        }

        List<Action<UIConfig>> callbackList;
        if (!panelOpenCallbackDic.TryGetValue(_panelName, out callbackList))
        {
            callbackList = new List<Action<UIConfig>>();

            panelOpenCallbackDic.Add(_panelName, callbackList);
        }

        callbackList.Add(_onCallback);
    }

    public void ExecutePanelOpenCallback(UIConfig _panelName)
    {
        List<Action<UIConfig>> callbackList;
        if (panelOpenCallbackDic.TryGetValue(_panelName, out callbackList))
        {
            foreach (var callback in callbackList)
            {
                callback?.Invoke(_panelName);
            }

            RemovePanelOpenCallback(_panelName);
        }
    }

    public void RemovePanelOpenCallback(UIConfig _panelName)
    {
        panelOpenCallbackDic.Remove(_panelName);
    }

    public void OpenWaitMask(bool _blackTween)
    {
        waitMask.Show(_blackTween);
    }

    public void CloseWaitMask()
    {
        waitMask.Hide();
    }

    public void Open(string _panelName, object _data = null, System.Action<UIPanel> _onOpenDone = null)
    {
        if (Enum.TryParse(_panelName, out UIConfig panel))
        {
            Open(panel, _data, _onOpenDone);
        }
    }

    public void Open(UIConfig _uiConfig, object _data = null, System.Action<UIPanel> _onOpenDone = null)
    {
        UIInfo uiInfo = UIInfoTable.GetUIInfo(_uiConfig);
        if (uiInfo != null)
        {
            Open(uiInfo, _data, _onOpenDone);
        }
    }

    public void Open(UIInfo _uiInfo, object _data = null, System.Action<UIPanel> _onOpenDone = null)
    {
        if (_uiInfo.instance)
        {
            if (IsOpen(_uiInfo))
            {
                return;
            }
        }

        var stack = uiStack[_uiInfo.layer];
        int layer = (int)_uiInfo.layer;

        if (stack.Count > 0)
        {
            var peek = stack.Peek();
            int lastLayer = peek.SortLayer;

            layer = lastLayer + UI_Delta_Depth + 1;
        }

        IUIInterface uiPanel = null;
        bool createPrefab = true;//是否需要创建Prefab

        if (!_uiInfo.instance)
        {
            if (IsOpen(_uiInfo, out uiPanel))//已经开了一个同样的界面
            {
                createPrefab = false;
            }
        }

        if (uiPanel == null)
        {
            uiPanel = UIInfoTable.CreateUIPanel(_uiInfo.uiConfigId);
            uiPanel.Init(_uiInfo);
        }

        stack.Push(uiPanel);
        uiPanel.AddRef();//引用计数加一
        uiPanel.SetLayer(layer);
        if (createPrefab)
        {
            if (_uiInfo.loadResources)//如果是从Resources里加载
            {
                var prefab = AssetSystem.Instance.LoadResources<GameObject>(_uiInfo.prefabName);
                if (prefab != null)
                {
                    SetPanelAsset(uiPanel, GameObject.Instantiate(prefab), layer, _data, _onOpenDone);
                }
            }
            else
            {
                OpenWaitMask(false);
                AssetHandler.Instance.LoadPrefab(_uiInfo.prefabName, (go) =>
                {
                    CloseWaitMask();

                    SetPanelAsset(uiPanel, go, layer, _data, _onOpenDone);
                });
            }
        }
        else
        {
            uiPanel.Open(_data, _onOpenDone);
        }
        //  事件通知
        EventCenterSystem.Instance.SendEvent(EventConst.Panel_Open, _uiInfo.uiConfigId);
    }

    private void SetPanelAsset(IUIInterface _uiPanel, GameObject _go, int _layer, object _data = null, System.Action<UIPanel> _onOpenDone = null)
    {
        _go.transform.SetParent(UIRoot);
        _go.transform.localPosition = Vector3.zero;
        _go.transform.localRotation = Quaternion.identity;
        _go.transform.localScale = Vector3.one;

        _uiPanel.Create(_go, _layer, null);
        _uiPanel.Open(_data, _onOpenDone);
    }

    public void Close(UIConfig _uiConfig)
    {
        UIInfo uiInfo = UIInfoTable.GetUIInfo(_uiConfig);
        if (uiInfo != null)
        {
            Close(uiInfo);
        }
    }

    public void Close(IUIInterface _uiPanel)
    {
        if (!IsOpen(_uiPanel))
        {
            return;
        }

        var stack = uiStack[_uiPanel.UIInfo.layer];

        Queue<IUIInterface> samePanelQueue = new Queue<IUIInterface>();
        Stack<IUIInterface> tempStack = new Stack<IUIInterface>();

        IUIInterface curPanel = null;

        while (stack.Count > 0)
        {
            var panel = stack.Pop();

            if (curPanel == null && panel == _uiPanel)
            {
                curPanel = panel;
            }
            else
            {
                tempStack.Push(panel);

                if (panel.UIConfigId == _uiPanel.UIConfigId)
                {
                    samePanelQueue.Enqueue(panel);
                }
            }
        }

        while (tempStack.Count > 0)
        {
            stack.Push(tempStack.Pop());
        }

        ClosePanel(_uiPanel);

        if (samePanelQueue.Count > 0)
        {
            IUIInterface panel = samePanelQueue.Dequeue();

            panel.SetLayer(panel.SortLayer);
            panel.Show(false);
        }
    }

    public void Close(UIInfo _uiInfo)
    {
        var stack = uiStack[_uiInfo.layer];

        Queue<IUIInterface> samePanelQueue = new Queue<IUIInterface>();
        Stack<IUIInterface> tempStack = new Stack<IUIInterface>();

        IUIInterface curPanel = null;

        while (stack.Count > 0)
        {
            var panel = stack.Pop();

            if (curPanel == null && panel.UIInfo == _uiInfo)
            {
                curPanel = panel;
            }
            else
            {
                tempStack.Push(panel);

                if (panel.UIConfigId == _uiInfo.uiConfigId)
                {
                    samePanelQueue.Enqueue(panel);
                }
            }
        }

        while (tempStack.Count > 0)
        {
            stack.Push(tempStack.Pop());
        }

        if (curPanel != null)
        {
            ClosePanel(curPanel);

            if (samePanelQueue.Count > 0)
            {
                IUIInterface panel = samePanelQueue.Dequeue();

                panel.SetLayer(panel.SortLayer);
                panel.Show(false);
            }
        }
        //  事件通知
        EventCenterSystem.Instance.SendEvent(EventConst.Panel_Close, _uiInfo.uiConfigId);
    }

    private void ClosePanel(IUIInterface _uiPanel)
    {
        _uiPanel.Shut();
        _uiPanel.Destroy();
    }

    /// <summary>
    /// 根据Layer关闭界面
    /// </summary>
    /// <param name="_uiLayer"></param>
    public void CloseAllPanelByUILayer(EUILayer _uiLayer)
    {
        var stack = uiStack[_uiLayer];

        while (stack.Count > 0)
        {
            var panel = stack.Pop();

            panel.Shut();
            panel.Destroy();
        }
    }

    /// <summary>
    /// 关闭除了主界面以外的所有界面
    /// </summary>
    public void CloseAllOtherPanel()
    {
        foreach (var layer in uiStack.Keys)
        {
            if (layer != EUILayer.Main)
            {
                CloseAllPanelByUILayer(layer);
            }
        }
    }

    public UIPanel GetUIPanel(UIConfig _panelName)
    {
        foreach (var stack in uiStack.Values)
        {
            foreach (var uiPanel in stack)
            {
                if (uiPanel.UIConfigId == _panelName)
                {
                    return uiPanel as UIPanel;
                }
            }
        }

        return null;
    }

    public bool IsOpen(UIInfo _uiInfo)
    {
        var stack = uiStack[_uiInfo.layer];

        foreach (var uiPanel in stack)
        {
            if (uiPanel.UIConfigId == _uiInfo.uiConfigId)
            {
                return true;
            }
        }

        return false;
    }

    public bool IsOpen(UIInfo _uiInfo, out IUIInterface _panel)
    {
        var stack = uiStack[_uiInfo.layer];

        foreach (var uiPanel in stack)
        {
            if (uiPanel.UIConfigId == _uiInfo.uiConfigId)
            {
                _panel = uiPanel;
                return true;
            }
        }

        _panel = null;

        return false;
    }

    public bool IsOpen(UIConfig _panelName)
    {
        foreach (var stack in uiStack.Values)
        {
            foreach (var uiPanel in stack)
            {
                if (uiPanel.UIConfigId == _panelName)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public bool IsOpen(IUIInterface _uiPanel)
    {
        var stack = uiStack[_uiPanel.UIInfo.layer];

        foreach (var uiPanel in stack)
        {
            if (uiPanel == _uiPanel)
            {
                return true;
            }
        }

        return false;
    }

    public bool IsCloseCheckHomePopUp(UIInfo _uiInfo)
    {
        return _uiInfo.layer == EUILayer.Default || _uiInfo.layer == EUILayer.High;
    }

    /// <summary>
    /// 主界面是否在顶部
    /// </summary>
    /// <returns></returns>
    public bool IsHomePanelInTop()
    {
        foreach (var dic in uiStack)
        {
            if (dic.Key == EUILayer.Default || dic.Key == EUILayer.High)
            {
                if (dic.Value.Count > 0)
                {
                    return false;
                }
            }
        }

        return true;
    }
}
