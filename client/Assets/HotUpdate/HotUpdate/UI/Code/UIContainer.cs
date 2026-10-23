using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using FrankGame;
using editor.cfg.common;
using Spine.Unity;

public abstract class UIContainer : IUIBind
{
    public delegate void OnButtonClickCallback();
    public delegate bool OnLongButtonClickCallback(bool touchUp);

    public delegate void OnButtonDoubleClickCallback();

    private static int Instance_Item_Id = 1;

    protected int itemUid;//用于池子的唯一id

    public GameObject GameObject { get { return go; } }
    public RectTransform RectTransform { get { return rectTransform; } }

    protected GameObject go;
    protected RectTransform rectTransform;
    protected object container;

    protected bool isClickButton;//是否可以按钮点击
    protected bool isDestory;//是否已经销毁

    private Dictionary<string, object> viewInfo;
    private Dictionary<Button, List<OnButtonClickCallback>> buttonClickDic = new Dictionary<Button, List<OnButtonClickCallback>>();
    private Dictionary<LongClickButton, List<OnLongButtonClickCallback>> longButtonClickDic = new Dictionary<LongClickButton, List<OnLongButtonClickCallback>>();
    private Dictionary<DoubleClickButton, List<OnButtonDoubleClickCallback>> doubleClickButtonDic = new Dictionary<DoubleClickButton, List<OnButtonDoubleClickCallback>>();
    private Dictionary<EventConst, List<CallBackParams>> eventDic = new Dictionary<EventConst, List<CallBackParams>>();
    private Dictionary<string, EffectObject> effectDic = new Dictionary<string, EffectObject>();
    private List<IUIItem> uiItemList = new List<IUIItem>();
    private List<int> timerIdList = new List<int>();
    private List<MonoBehaviour> dynamicMonoList = new List<MonoBehaviour>();//动态添加的脚本
    private List<DynamicItem> dynamicItemList = new List<DynamicItem>();//动态Item

    private Dictionary<Type, Queue<UIItem>> itemPoolDic = new Dictionary<Type, Queue<UIItem>>();
    private Dictionary<int, UIItem> useItemDic = new Dictionary<int, UIItem>();//key uid
    private Dictionary<string, Queue<GameObject>> gameObjectPoolDic = new Dictionary<string, Queue<GameObject>>();//对象实例化池
    private Dictionary<int, GameObject> useGameObjectDic = new Dictionary<int, GameObject>();//key instanceId

    public void Bind(GameObject _go, object _container)
    {
        this.go = _go;
        this.rectTransform = (RectTransform)this.go.transform;
        this.isClickButton = true;
        this.isDestory = false;

        this.viewInfo = UIFieldDicMgr.InitUIFieldCfg(this.go);
        this.container = _container;

        OnBind();
        OnBindAfter();
        OnRegisterListener();
    }

    protected virtual void OnBind()
    {

    }

    protected virtual void OnBindAfter()
    {

    }

    protected virtual void OnRegisterListener()
    {

    }

    protected T GetVaule<T>(string key) where T : class
    {
        T t = viewInfo.ValueBP<T>(key);

#if WX_GAME
        if (t is InputField)
        {
            GameObject inputGo = (t as InputField).gameObject;
            FrankGame.WXInput wxinput = inputGo.GetComponent<FrankGame.WXInput>();
            if (wxinput == null)
            {
                inputGo.AddComponent<FrankGame.WXInput>();
            }
        }
#elif BYTE_GAME
        if (t is InputField)
        {
            GameObject inputGo = (t as InputField).gameObject;
            FrankGame.ByteInput wxinput = inputGo.GetComponent<FrankGame.ByteInput>();
            if (wxinput == null)
            {
                inputGo.AddComponent<FrankGame.ByteInput>();
            }
        }
#endif

        return t;
    }

    protected bool TryGetVaule<T>(string key, out T _t) where T : class
    {
        return viewInfo.TryValueBT<T>(key, out _t);
    }

    protected void SetButtonClick(Button _btn, OnButtonClickCallback _func, string _soundId = "common_click")
    {
        List<OnButtonClickCallback> btnCallbackList;
        if (!buttonClickDic.TryGetValue(_btn, out btnCallbackList))
        {
            btnCallbackList = new List<OnButtonClickCallback>();
            buttonClickDic.Add(_btn, btnCallbackList);

            _btn.onClick.RemoveAllListeners();
            _btn.onClick.AddListener(() => { OnButtonClick(_btn, _soundId); });
        }

        if (_func != null)
        {
            btnCallbackList.Clear();
            btnCallbackList.Add(_func);
        }
    }

    protected void AddButtonClick(Button _btn, OnButtonClickCallback _func, string _soundId = "common_click")
    {
        List<OnButtonClickCallback> btnCallbackList;
        if (!buttonClickDic.TryGetValue(_btn, out btnCallbackList))
        {
            btnCallbackList = new List<OnButtonClickCallback>();
            buttonClickDic.Add(_btn, btnCallbackList);

            _btn.onClick.AddListener(() => { OnButtonClick(_btn, _soundId); });
        }

        if (_func != null)
        {
            btnCallbackList.Add(_func);
        }
    }

    protected void AddLongButtonClick(LongClickButton _btn, OnLongButtonClickCallback _func)
    {
        if (!longButtonClickDic.ContainsKey(_btn))
        {
            longButtonClickDic.Add(_btn, new List<OnLongButtonClickCallback>());

            _btn.onLongClick = (touchUp) =>
            {
                return OnLongButtonClick(_btn, touchUp);
            };
        }

        if (_func != null)
        {
            longButtonClickDic[_btn].Add(_func);
        }
    }

    protected void AddButtonDoubleClick(DoubleClickButton _btn, OnButtonDoubleClickCallback callback)
    {
        if (!doubleClickButtonDic.ContainsKey(_btn))
        {
            doubleClickButtonDic.Add(_btn, new List<OnButtonDoubleClickCallback>());
            _btn.onDoubleClick = () =>
            {
                OnButtonDoubleClick(_btn);
            };
        }
        if (callback != null)
        {
            doubleClickButtonDic[_btn].Add(callback);
        }
    }

    protected void AddButtonClick(GameObject _btnGo, OnButtonClickCallback _func)
    {
        Button btn = _btnGo.GetComponent<Button>();
        if (btn != null)
        {
            AddButtonClick(btn, _func);
        }
    }

    protected void AddHideGameObjectButton(Button _btn)
    {
        AddButtonClick(_btn, HideGameObject);
    }

    private void HideGameObject()
    {
        GameObject.SetActive(false);
    }

    private void OnButtonClick(Button _btn, string _soundId = "")
    {
        if (!this.isClickButton)
        {
            return;
        }

        foreach (var func in buttonClickDic[_btn])
        {
            func?.Invoke();
        }

        if (!string.IsNullOrEmpty(_soundId))
        {
            AudioHandler.Instance.PlayAudioById(_soundId);
        }
    }

    private bool OnLongButtonClick(LongClickButton _btn, bool _touchUp)
    {
        if (!this.isClickButton)
        {
            return false;
        }

        foreach (var func in longButtonClickDic[_btn])
        {
            if (func == null || !func.Invoke(_touchUp))
            {
                return false;
            }
        }

        return true;
    }

    private void OnButtonDoubleClick(DoubleClickButton _btn)
    {
        if (!isClickButton)
        {
            return;
        }
        foreach (var func in doubleClickButtonDic[_btn])
        {
            func?.Invoke();
        }
    }

    protected void SendEvent(EventConst _evtName, params object[] _args)
    {
        EventCenterSystem.Instance.SendEvent(_evtName, _args);
    }
    protected void AddEventListeners(CallBackParams _callback, params EventConst[] events)
    {
        if (events == null || events.Length == 0)
        {
            return;
        }
        Utils.Iterator(events, (o, i) => AddEventListener(o, _callback));
    }
    protected void AddEventListener(EventConst _evtName, CallBackParams _callback)
    {
        if (!eventDic.ContainsKey(_evtName))
        {
            eventDic.Add(_evtName, new List<CallBackParams>());
        }

        eventDic[_evtName].Add(_callback);

        EventCenterSystem.Instance.AddEvent(_evtName, this, _callback);
    }

    protected void RemoveEventListener(EventConst _evtName, CallBackParams _callback)
    {
        eventDic[_evtName].Remove(_callback);

        EventCenterSystem.Instance.RemoveEvent(_evtName, this, _callback);
    }

    //protected EffectObject AddEffect(string _effKey, string _effectName, Transform _parent, Vector3 _pos, float _delay = 0)
    //{
    //    if (effectDic.ContainsKey(_effKey))
    //    {
    //        return effectDic[_effKey];
    //    }

    //    EffectObject eff = EffectSystem.Instance.LoadEffect(_effectName, _parent, _pos);
    //    eff.Show(_delay);

    //    effectDic.Add(_effKey, eff);

    //    return eff;
    //}

    protected void RemoveEffect(string _effKey)
    {
        if (effectDic.ContainsKey(_effKey))
        {
            effectDic.Remove(_effKey);
        }
    }

    protected T InitUIItem<T>(string _key) where T : UIItem, new()
    {
        var itemGo = GetVaule<GameObject>(_key);

        return AddItem<T>(itemGo);
    }

    protected void InitDynamicItem<K>(DynamicItem _dynamicItem) where K : UIItem, new()
    {
        _dynamicItem.InitContainer<K>(this);
    }

    protected void LoadItem<K>(string _assetPath, RectTransform _parent, System.Action<K> _onLoadDone) where K : IUIItem, new()
    {
        AssetHandler.Instance.LoadPrefab(_assetPath, (go) => {
            if (IsPanelHasClose())
            {
                GameObject.Destroy(go);
                return;
            }

            var k = AddItem<K>(go);
            go.transform.SetParent(_parent, false);
            go.transform.localScale = Vector3.one;
            go.transform.localPosition = Vector3.zero;

            _onLoadDone?.Invoke(k);
        });
    }

    protected K AddItem<K>(GameObject _go) where K : IUIItem, new()
    {
        K k = new K();

        uiItemList.Add(k);

        k.Create(_go, 0, this);

        return k;
    }

    protected void RemoveItem(IUIItem _item)
    {
        var item = uiItemList.Find((view) => {
            return view == _item;
        });
        if (item != null)
        {
            item.Destroy();

            uiItemList.Remove(item);

            GameObject.Destroy(item.GameObject);
        }
    }

    public UIItem AddDynamicItem<K>(DynamicItem _dynamicItem, GameObject _go, object _container) where K : UIItem, new()
    {
        UIItem uiItem = new K();

        dynamicItemList.Add(_dynamicItem);

        uiItem.Create(_go, 0, _container);

        return uiItem;
    }

    protected void AddMono<T>(string _assetPath, Transform _parent, System.Action<T> _onLoadDone) where T : MonoBehaviour
    {
        AssetHandler.Instance.LoadPrefab(_assetPath, (go) =>
        {
            go.transform.SetParent(_parent);
            go.transform.localPosition = Vector3.zero;
            go.transform.localScale = Vector3.one;

            var mono = go.GetComponent<T>();
            if (mono == null)
            {
                mono = go.AddComponent<T>();
            }

            dynamicMonoList.Add(mono);

            _onLoadDone?.Invoke(mono);
        });
    }

    protected void RemoveMono<T>(T _mono) where T : MonoBehaviour
    {
        for (int i = 0; i < dynamicMonoList.Count; i++)
        {
            var mono = dynamicMonoList[i];
            if (mono == _mono)
            {
                dynamicMonoList.RemoveAt(i);

                GameObject.Destroy(_mono.gameObject);
                break;
            }
        }
    }

    protected UIList InitUIList<K>(string _listKey) where K : IUIItem, new()
    {
        UIList uiList = GetVaule<UIList>(_listKey);
        uiList.Init<K>(this);

        return uiList;
    }

    protected int AddDelay(float _delay, OnTimerCallback _func)
    {
        int timerId = 0;
        timerId = TimeSystem.Instance.AddDelay(_delay, () =>
        {
            timerIdList.Remove(timerId);
            _func?.Invoke();
        });

        if(timerId > 0)
        {
            timerIdList.Add(timerId);
        }

        return timerId;
    }

    protected int AddUpdate(float _interval, OnTimerCallback _func, bool _immediately = false, bool _ignoreScale = false)
    {
        int timerId = TimeSystem.Instance.AddIntervalUpdate(_interval, _func, _immediately, _ignoreScale);

        if(timerId > 0)
        {
            timerIdList.Add(timerId);
        }

        return timerId;
    }

    protected void RemoveTimer(int _timeId)
    {
        timerIdList.Remove(_timeId);

        TimeSystem.Instance.RemoveTimer(_timeId);
    }

    protected T BorrowItem<T>(T _templateItem, object _data, Transform _parent, Action<GameObject, int> _onCreare = null) where T : UIItem, new()
    {
        T item = GetPoolItem(_templateItem, 0);

        item.RectTransform.SetParent(_parent, false);
        _onCreare?.Invoke(item.GameObject, 0);

        item.SetIndex(0);
        item.Show(_data);
        if (!item.GameObject.activeSelf)
        {
            item.GameObject.SetActive(true);
        }
        return item;
    }

    protected List<T> BorrowItems<T>(T _templateItem, IEnumerable _datas, Transform _parent, Action<GameObject, int> _onCreare = null) where T : UIItem, new()
    {
        List<T> itemList = new List<T>();
        int idx = 0;
        foreach (var data in _datas)
        {
            T item = GetPoolItem(_templateItem, idx);
            item.GameObject.SetActive(true);
            item.RectTransform.SetParent(_parent, false);
            _onCreare?.Invoke(item.GameObject, idx);
            item.SetIndex(idx);
            item.Show(data);

            itemList.Add(item);

            idx++;
        }

        return itemList;
    }

    private T GetPoolItem<T>(T _templateItem, int _index) where T : UIItem, new()
    {
        T item = null;
        Queue<UIItem> poolQueue;
        Type itemType = typeof(T);
        if (itemPoolDic.TryGetValue(itemType, out poolQueue))
        {
            if (poolQueue.Count > 0)
            {
                item = poolQueue.Dequeue() as T;
                item.itemUid = Instance_Item_Id++;

                useItemDic.Add(item.itemUid, item);

                return item;
            }
        }

        item = new T();
        item.itemUid = Instance_Item_Id++;

        item.Create(GameObject.Instantiate(_templateItem.GameObject), _index, this);

        useItemDic.Add(item.itemUid, item);

        return item;
    }

    protected void RepayItem<T>(T _item) where T : UIItem
    {
        UIItem item = null;
        if (!useItemDic.TryGetValue(_item.itemUid, out item) || item != _item)
        {
            LogUtil.Error("未找到池子中的Item:" + _item.GameObject.name);
            return;
        }

        item.Recover();
        item.Hide();
        useItemDic.Remove(_item.itemUid);

        Type itemType = typeof(T);

        Queue<UIItem> poolQueue;
        if (!itemPoolDic.TryGetValue(itemType, out poolQueue))
        {
            poolQueue = new Queue<UIItem>();

            itemPoolDic.Add(itemType, poolQueue);
        }

        item.SetActive(false);

        poolQueue.Enqueue(item);
    }

    protected void RepayItems<T>(List<T> _items) where T : UIItem
    {
        foreach (var item in _items)
        {
            RepayItem(item);
        }
    }

    protected GameObject BorrowGameObject(GameObject _templateGo, object _data, Transform _parent, Action<GameObject, object> _onCreare = null)
    {
        GameObject go = GetPoolGameObject(_templateGo);

        go.transform.SetParent(_parent, false);
        _onCreare?.Invoke(go, _data);

        if (!go.activeSelf)
        {
            go.SetActive(true);
        }

        return go;
    }

    protected List<GameObject> BorrowGameObjects(GameObject _templateGo, IEnumerable _datas, Transform _parent, Action<GameObject, int, object> _onCreare = null)
    {
        List<GameObject> goList = new List<GameObject>();
        int idx = 0;
        foreach (var data in _datas)
        {
            GameObject go = GetPoolGameObject(_templateGo);
            go.SetActive(true);
            go.transform.SetParent(_parent, false);
            _onCreare?.Invoke(go, idx, data);

            goList.Add(go);

            idx++;
        }

        return goList;
    }

    private GameObject GetPoolGameObject(GameObject _templateGo)
    {
        GameObject go = null;
        Queue<GameObject> poolQueue;
        if (gameObjectPoolDic.TryGetValue(_templateGo.name, out poolQueue))
        {
            if (poolQueue.Count > 0)
            {
                go = poolQueue.Dequeue();

                useGameObjectDic.Add(go.GetInstanceID(), go);

                return go;
            }
        }

        go = GameObject.Instantiate(_templateGo);
        go.name = _templateGo.name;

        useGameObjectDic.Add(go.GetInstanceID(), go);

        return go;
    }

    protected void RepayGameObject(GameObject _go)
    {
        GameObject go;
        if (!useGameObjectDic.TryGetValue(_go.GetInstanceID(), out go) || go != _go)
        {
            LogUtil.Error("未找到池子中的GameObject:" + _go.name);
            return;
        }

        useGameObjectDic.Remove(go.GetInstanceID());

        Queue<GameObject> poolQueue;
        if (!gameObjectPoolDic.TryGetValue(go.name, out poolQueue))
        {
            poolQueue = new Queue<GameObject>();

            gameObjectPoolDic.Add(go.name, poolQueue);
        }

        go.SetActive(false);

        poolQueue.Enqueue(go);
    }

    protected void RepayGameObjects(List<GameObject> _gos)
    {
        foreach (var go in _gos)
        {
            RepayGameObject(go);
        }
    }

    /// <summary>
    /// 动态添加Tips，点击任意地方关闭
    /// </summary>
    /// <param name="_assetPath"></param>
    protected void AddTips<T>(string _assetPath, Transform _parent, Action<T> _onLoadDone) where T : MonoBehaviour
    {
        AddMono<UIPointerTrigger>(_assetPath, _parent, (pointer) =>
        {
            T t = pointer.gameObject.GetComponent<T>();

            _onLoadDone?.Invoke(t);

            pointer.SetPointerUpEvent((mine) =>
            {
                if (!mine)
                {
                    RemoveMono(pointer);
                }
            });
        });
    }

    /// <summary>
    /// 点击非物体的任意地方隐藏物体
    /// </summary>
    /// <param name="_go"></param>
    protected void AddClickOtherHide(GameObject _go)
    {
        UIPointerTrigger pointer = _go.GetComponent<UIPointerTrigger>();
        if (pointer == null)
        {
            pointer = _go.AddComponent<UIPointerTrigger>();
        }
        pointer.SetPointerUpEvent((mine) =>
        {
            if (!mine)
            {
                _go.SetActive(false);
            }
        });
    }

    protected void ClearItemPool()
    {
        foreach (var itemQueue in itemPoolDic.Values)
        {
            while (itemQueue.Count > 0)
            {
                var item = itemQueue.Dequeue();
                item.Destroy();

                GameObject.Destroy(item.GameObject);
            }
        }

        itemPoolDic.Clear();

        foreach (var item in useItemDic.Values)
        {
            item.Destroy();

            GameObject.Destroy(item.GameObject);
        }

        useItemDic.Clear();
    }

    protected void ClearGameObjectPool()
    {
        foreach (var gameObjectQueue in gameObjectPoolDic.Values)
        {
            while (gameObjectQueue.Count > 0)
            {
                var go = gameObjectQueue.Dequeue();

                GameObject.Destroy(go);
            }
        }

        gameObjectPoolDic.Clear();

        foreach (var go in useGameObjectDic.Values)
        {
            GameObject.Destroy(go);
        }

        useItemDic.Clear();
    }

    private void ClearDynamicMono()
    {
        foreach (var mono in dynamicMonoList)
        {
            if (mono != null)
            {
                GameObject.DestroyImmediate(mono.gameObject);
            }
        }
        dynamicMonoList.Clear();
    }

    public virtual void Update(float _deltaTime)
    {

    }

    public void Unbind()
    {
        this.isDestory = true;

        OnUnbind();
    }

    protected virtual void OnUnbind()
    {
        foreach (var dic in buttonClickDic)
        {
            dic.Key.onClick.RemoveAllListeners();
        }
        buttonClickDic.Clear();

        foreach (var dic in longButtonClickDic)
        {
            dic.Key.onLongClick = null;
        }
        longButtonClickDic.Clear();

        foreach (var key in eventDic.Keys)
        {
            EventCenterSystem.Instance.RemoveEvent(key, this);
        }
        eventDic.Clear();

        foreach (var dic in effectDic)
        {
            //EffectSystem.Instance.UnusedEffect(dic.Value);
        }
        effectDic.Clear();

        foreach (var uiItem in uiItemList)
        {
            uiItem.Destroy();
        }
        uiItemList.Clear();

        foreach (var dynamicItem in dynamicItemList)
        {
            dynamicItem.Dispose();
        }
        dynamicItemList.Clear();

        ClearTimers();
        ClearItemPool();
        ClearGameObjectPool();
        ClearDynamicMono();
    }

    protected void ClearTimers()
    {
        foreach (var timerId in timerIdList)
        {
            TimeSystem.Instance.RemoveTimer(timerId);
        }
        timerIdList.Clear();
    }

    public void SetActive(bool _active)
    {
        if (this.go != null)
        {
            this.go.SetActive(_active);
        }

        OnSetActive(_active);
    }

    protected virtual void OnSetActive(bool _active)
    {

    }

    public bool IsPanelHasClose()
    {
        UIContainer rootContainer = GetRootUIContainer(this.container);

        if (rootContainer != null && rootContainer is UIPanel)
        {
            return (rootContainer as UIPanel).IsHasClose;
        }

        return false;
    }

    private UIContainer GetRootUIContainer(object _container)
    {
        if (_container is UIList)
        {
            return GetRootUIContainer(((UIList)_container).sender);
        }
        else
        {
            UIContainer uiContainer = _container as UIContainer;
            if (uiContainer != null)
            {
                if (uiContainer.container != null)
                {
                    return GetRootUIContainer(uiContainer.container);
                }
                else
                {
                    return uiContainer;
                }
            }
        }

        return null;
    }

    #region Tools

    protected void SetActive(Component _com, bool _active)
    {
        if (_com.gameObject.activeSelf == _active)
        {
            return;
        }
        _com.gameObject.SetActive(_active);
    }

    protected void SetActive(GameObject _go, bool _active)
    {
        _go.SetActive(_active);
    }

    protected void SetActive(UIItem _item, bool _active)
    {
        if (_item.GameObject.activeSelf == _active)
        {
            return;
        }
        _item.GameObject.SetActive(_active);
    }

    protected void SetSpriteById(Image _image, string _assetId, bool _nativeSize = false)
    {
        AssetHandler.Instance.SetSpriteById(_assetId, _image, _nativeSize);
    }

    protected void SetSprite(Image _image, string _atlasName, string _spriteName, bool _nativeSize = false)
    {
        AssetHandler.Instance.SetSprite(_atlasName, _spriteName, _image, _nativeSize);
    }

    protected void SetTextureById(RawImage _rawImage, string _assetId, bool _nativeSize = false)
    {
        AssetHandler.Instance.SetTextureById(_assetId, _rawImage, _nativeSize);
    }

    protected void SetTextureById(Image _image, string _assetId, bool _nativeSize = false)
    {
        AssetHandler.Instance.SetTextureById(_assetId, _image, _nativeSize);
    }

    protected void SetTexture(RawImage _rawImage, string _texName, bool _nativeSize = false)
    {
        AssetHandler.Instance.SetTexture(_texName, _rawImage, _nativeSize);
    }

    protected void SetTexture(Image _image, string _texName, bool _nativeSize = false)
    {
        AssetHandler.Instance.SetTexture(_texName, _image, _nativeSize);
    }

    protected void SetSpineById(SkeletonGraphic _skeletonGraphic, string _assetId, bool _scale = false)
    {
        AssetHandler.Instance.SetSpineById(_assetId, _skeletonGraphic, _scale);
    }

    protected void SetText(Text _text, string _str)
    {
        _text.text = _str;
    }

    protected void SetText(TextMeshProUGUI _text, string _str)
    {
        _text.text = _str;
    }

    protected void SetText(Text _text, object _obj)
    {
        _text.text = _obj.ToString();
    }

    protected void SetText(TextMeshProUGUI _text, object _obj)
    {
        _text.text = _obj.ToString();
    }

    protected void SetFormatText(Text _text, string _format, params object[] _params)
    {
        _text.text = string.Format(_format, _params);
    }

    protected void SetFormatText(TextMeshProUGUI _text, string _format, params object[] _params)
    {
        _text.text = string.Format(_format, _params);
    }

    protected void SetLocalizeText(Text _text, string _key, params object[] _params)
    {
        string result = LanguageSystem.Instance.GetLanguage(_key, _params);
        if (result == null)
        {
            result = _key;
        }

        _text.text = result;
    }

    protected void SetLocalizeText(TextMeshProUGUI _text, string _key, params object[] _params)
    {
        string result = LanguageSystem.Instance.GetLanguage(_key, _params);
        if (result == null)
        {
            result = _key;
        }

        _text.text = result;
    }

    protected void SetFormatLocalizeText(TextMeshProUGUI _text, string _key, params object[] _params)
    {
        string result = LanguageSystem.Instance.GetLanguage(_key, _params);
        if (result == null)
        {
            result = _key;
        }

        SetFormatText(_text, result, _params);
    }

    protected string GetLocalizeText(string _key, params object[] _params)
    {
        if (_params != null && _params.Length > 0)
        {
            return LanguageSystem.Instance.GetLanguage(_key, _params);
        }
        return LanguageSystem.Instance.GetLanguage(_key);
    }

    protected void SetTextureLocalize(Image _image, string _key)
    {
        SetTextureLanguage(_image.gameObject, _key);
    }

    protected void SetTextureLocalize(RawImage _rawImage, string _key)
    {
        SetTextureLanguage(_rawImage.gameObject, _key);
    }

    private void SetTextureLanguage(GameObject _go, string _key)
    {
        var texLanguageCfg = ConfigSystem.CurrentData.GetTextureLanguage(_key);
        if (texLanguageCfg == null)
        {
            return;
        }

        TextureLocalize textureLocalize = _go.GetComponent<TextureLocalize>();
        if (textureLocalize == null)
        {
            textureLocalize = _go.AddComponent<TextureLocalize>();
        }

        textureLocalize.key = _key;
        textureLocalize.SetLocalizeTexture(LanguageSystem.Instance.CurLanguage);
    }

    protected void SetItemIcon(Image _image, int _itemId, bool _nativeSize = false)
    {
        var itemConfig = ConfigSystem.CurrentData.GetItem(_itemId);
        if(itemConfig != null)
        {
            SetSpriteById(_image, itemConfig.icon, _nativeSize);
        }
    }

    protected void SetCurrencyIcon(Image _image, ECurrencyType _currencyType, bool _nativeSize = false)
    {
        var currencyConfig = ConfigSystem.CurrentData.GetCurrency(_currencyType);
        if (currencyConfig != null)
        {
            SetSpriteById(_image, currencyConfig.icon, _nativeSize);
        }
    }

    public static string Color_Red = "<color=#FD3532>{0}</color>";

    protected void SetCostText(TextMeshProUGUI _text, ECurrencyType _currencyType, long _needAmount, bool _full = false)
    {
        long curCount = CurrencyModule.Instance.GetCurrencyCount(_currencyType);
        bool enough = curCount >= _needAmount;

        if (enough)
        {
            if (_full)
            {
                SetText(_text, $"{curCount}/{_needAmount}");
            }
            else
            {
                SetText(_text, _needAmount.ToString());
            }
        }
        else
        {
            if (_full)
            {
                SetText(_text, $"{string.Format(Color_Red, curCount)}/{_needAmount}");
            }
            else
            {
                SetText(_text, string.Format(Color_Red, _needAmount));
            }
        }
    }

    protected void SetCostText(TextMeshProUGUI _text, int _itemId, long _needAmount, bool _full = false)
    {
        long curCount = ItemModule.Instance.GetItemCount(_itemId);
        bool enough = curCount >= _needAmount;

        if (enough)
        {
            if (_full)
            {
                SetText(_text, $"{curCount}/{_needAmount}");
            }
            else
            {
                SetText(_text, _needAmount.ToString());
            }
        }
        else
        {
            if (_full)
            {
                SetText(_text, $"{string.Format(Color_Red, curCount)}/{_needAmount}");
            }
            else
            {
                SetText(_text, string.Format(Color_Red, _needAmount));
            }
        }
    }

    protected void SetCostText(TextMeshProUGUI _text, bool _enough, string _str)
    {
        if(_enough)
        {
            SetText(_text, _str);
        }
        else
        {
            SetText(_text, string.Format(Color_Red, _str));
        }
    }

    protected void SetCostFormatText(TextMeshProUGUI _text, ECurrencyType _currencyType, long _needAmount, bool _full = false)
    {
        long curCount = CurrencyModule.Instance.GetCurrencyCount(_currencyType);
        bool enough = curCount >= _needAmount;

        if (enough)
        {
            if (_full)
            {
                SetFormatText(_text, $"{curCount}/{_needAmount}");
            }
            else
            {
                SetFormatText(_text, _needAmount.ToString());
            }
        }
        else
        {
            if (_full)
            {
                SetFormatText(_text, $"{string.Format(Color_Red, curCount)}/{_needAmount}");
            }
            else
            {
                SetFormatText(_text, string.Format(Color_Red, _needAmount));
            }
        }
    }

    protected void SetCostFormatText(TextMeshProUGUI _text, int _itemId, long _needAmount, bool _full = false)
    {
        long curCount = ItemModule.Instance.GetItemCount(_itemId);
        bool enough = curCount >= _needAmount;

        if (enough)
        {
            if (_full)
            {
                SetFormatText(_text, $"{curCount}/{_needAmount}");
            }
            else
            {
                SetFormatText(_text, _needAmount.ToString());
            }
        }
        else
        {
            if (_full)
            {
                SetFormatText(_text, $"{string.Format(Color_Red, curCount)}/{_needAmount}");
            }
            else
            {
                SetFormatText(_text, string.Format(Color_Red, _needAmount));
            }
        }
    }

    protected void SetCostFormatText(TextMeshProUGUI _text, bool _enough, string _str)
    {
        if (_enough)
        {
            SetFormatText(_text, _str);
        }
        else
        {
            SetFormatText(_text, string.Format(Color_Red, _str));
        }
    }

    #endregion


}
