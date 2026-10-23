using System.Collections.Generic;
using UnityEngine;
using FrankGame;
using editor.cfg.common;

public class FlyTextManager : Singlet<FlyTextManager>
{
    public Transform FlyTextRoot
    {
        get
        {
            return flyTextRoot;
        }
    }

    private const string FlyTextPrefabName = "Common/UIFlyText";
    private const string FlyCurrencyPrefabName = "Common/UIFlyCurrency";
    private const string FlyAddCurrencyPrefabName = "Common/UIFlyAddCurrency";
    private const string FlyAddCurrencyTextPrefabName = "Common/UIFlyAddCurrencyText";

    private static Color White_Color = Color.white;
    private static Color Gray_Color = new Color(187 / 255.0f, 197 / 255.0f, 193 / 255.0f);
    private static Color Yellow_Color = new Color(222f / 255.0f, 180/255.0f, 98f / 255.0f);
    private static Color Purple_Color = new Color(218 / 255.0f, 137 / 255.0f, 245 / 255.0f);

    private Transform flyTextRoot;
    private Queue<UIFlyText> flyTextQueue = new Queue<UIFlyText>();
    private List<MonoBehaviour> flyingTextList = new List<MonoBehaviour>();
    private List<MonoBehaviour> flyingText2List = new List<MonoBehaviour>();
    private GameObjectPool flyTextPool;

    public void Init(Transform _parent)
    {
        flyTextRoot = GameObject.Find("UIRoot/FlyTextRoot").transform;

        flyTextRoot.SetParent(_parent);
        flyTextRoot.localPosition = Vector3.zero;
        flyTextRoot.localRotation = Quaternion.identity;
        flyTextRoot.localScale = Vector3.one;

        GameObject go = new GameObject("FlyTextPool");
        flyTextPool = new GameObjectPool(go.transform);
    }
    
    public void AddLocalizeText(string _key, params object[] _params) {
        AddText(LanguageSystem.Instance.GetLanguage(_key, _params) ?? _key);
    }

    public void AddText(string _content)
    {
        flyTextPool.GetGameObject(EResType.UIPrefab, FlyTextPrefabName, (go) => 
        {
            go.transform.SetParent(flyTextRoot);

            RectTransform rt = go.GetComponent<RectTransform>();
            rt.pivot = new Vector2(0.5f, 0.5f);
            rt.anchorMax = new Vector2(0.5f, 0.5f);
            rt.anchorMin = new Vector2(0.5f, 0.5f);
            rt.anchoredPosition = Vector2.zero;
            rt.localScale = Vector3.one;

            go.SetActive(false);

            UIFlyText flyText = go.GetComponent<UIFlyText>();
            flyText.content = _content;
            flyText.flyHeight = 250;
            flyText.onFlyTop = () =>
            {
                flyingTextList.Remove(flyText);
                ExecuteFly();
            };
            flyText.onFlyEnd = () =>
            {
                flyText.Dispose();
                flyTextPool.Unused(EResType.UIPrefab, FlyTextPrefabName, flyText.gameObject);
            };

            flyTextQueue.Enqueue(flyText);

            if (flyingTextList.Count == 0)
            {
                ExecuteFly();
            }
        });
    }

    public void AddCurrencyText(string _content, string _icon, int _amount)
    {
        flyTextPool.GetGameObject(EResType.UIPrefab, FlyAddCurrencyTextPrefabName, (go) =>
        {
            go.transform.SetParent(flyTextRoot);

            RectTransform rt = go.GetComponent<RectTransform>();
            rt.pivot = new Vector2(0.5f, 0.5f);
            rt.anchorMax = new Vector2(0.5f, 0.5f);
            rt.anchorMin = new Vector2(0.5f, 0.5f);
            rt.anchoredPosition = Vector2.zero;
            rt.localScale = Vector3.one;

            go.SetActive(false);

            UIFlyText flyText = go.GetComponent<UIFlyText>();
            flyText.icon = _icon;
            flyText.amount = _amount > 0 ? $"+{_amount}" : _amount.ToString();
            flyText.content = _content;
            flyText.flyHeight = 250;
            flyText.onFlyTop = () =>
            {
                flyingTextList.Remove(flyText);
                ExecuteFly();
            };
            flyText.onFlyEnd = () =>
            {
                flyText.Dispose();
                flyTextPool.Unused(EResType.UIPrefab, FlyAddCurrencyTextPrefabName, flyText.gameObject);
            };

            flyTextQueue.Enqueue(flyText);

            if (flyingTextList.Count == 0)
            {
                ExecuteFly();
            }
        });
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="_icon"></param>
    /// <param name="_amount"></param>
    /// <param name="_parent"></param>
    /// <param name="_fromWorldPosition"></param>
    /// <param name="_offsetX"></param>
    /// <param name="_offsetY"></param>
    /// <param name="_colorType">1 灰色 2 黄色 3 紫色 0 白色</param>
    public void AddCurrencyText(string _icon, int _amount, Transform _parent, Vector3 _fromWorldPosition, float _offsetX = 0, float _offsetY = 0, int _colorType = 0)
    {
        if(_amount == 0)
        {
            return;
        }

        flyTextPool.GetGameObject(EResType.UIPrefab, FlyAddCurrencyPrefabName, (go) =>
        {
            go.transform.SetParent(_parent);

            RectTransform rt = go.GetComponent<RectTransform>();
            rt.pivot = new Vector2(0.5f, 0.5f);
            rt.anchorMax = new Vector2(0.5f, 0.5f);
            rt.anchorMin = new Vector2(0.5f, 0.5f);
            rt.localScale = Vector3.one;

            var localPos = UIUtils.WorldPointToUILocalPoint(rt.parent as RectTransform, _fromWorldPosition);

            rt.anchoredPosition = new Vector2(localPos.x + _offsetX, localPos.y + _offsetY);

            go.SetActive(false);

            UIFlyText flyText = go.GetComponent<UIFlyText>();
            flyText.content = _amount > 0 ? ("+" + _amount) : ("" + _amount);
            flyText.icon = _icon;
            flyText.flyHeight = 40;
            if(_colorType == 1)
            {
                flyText.textColor = Gray_Color;
            }
            else if(_colorType == 2)
            {
                flyText.textColor = Yellow_Color;
            }
            else if(_colorType == 3)
            {
                flyText.textColor = Purple_Color;
            }
            else
            {
                flyText.textColor = White_Color;
            }

            flyText.onFlyTop = () =>
            {
                flyingTextList.Remove(flyText);
                ExecuteFly();
            };
            flyText.onFlyEnd = () =>
            {
                flyingTextList.Remove(flyText);
                flyText.Dispose();
                flyTextPool.Unused(EResType.UIPrefab, FlyAddCurrencyPrefabName, flyText.gameObject);
            };

            flyTextQueue.Enqueue(flyText);

            //if (flyingTextList.Count == 0)
            {
                ExecuteFly();
            }
        });
    }

    public void AddCurrencyText2(string _icon, int _amount, Transform _parent, Vector3 _fromWorldPosition, float _offsetX = 0, float _offsetY = 0, int _colorType = 0)
    {
        if (_amount == 0)
        {
            return;
        }

        flyTextPool.GetGameObject(EResType.UIPrefab, FlyAddCurrencyPrefabName, (go) =>
        {
            go.transform.SetParent(_parent);

            RectTransform rt = go.GetComponent<RectTransform>();
            rt.pivot = new Vector2(0.5f, 0.5f);
            rt.anchorMax = new Vector2(0.5f, 0.5f);
            rt.anchorMin = new Vector2(0.5f, 0.5f);
            rt.localScale = Vector3.one;

            var localPos = UIUtils.WorldPointToUILocalPoint(rt.parent as RectTransform, _fromWorldPosition);

            rt.anchoredPosition = new Vector2(localPos.x + _offsetX, localPos.y + _offsetY);

            go.SetActive(false);

            UIFlyText flyText = go.GetComponent<UIFlyText>();
            flyText.content = _amount > 0 ? ("+" + _amount) : ("" + _amount);
            flyText.icon = _icon;
            flyText.flyHeight = 40;
            if (_colorType == 1)
            {
                flyText.textColor = Gray_Color;
            }
            else if (_colorType == 2)
            {
                flyText.textColor = Yellow_Color;
            }
            else if (_colorType == 3)
            {
                flyText.textColor = Purple_Color;
            }
            else
            {
                flyText.textColor = White_Color;
            }
            flyText.onFlyTop = () =>
            {
                flyingText2List.Remove(flyText);
                ExecuteFly2();
            };
            flyText.onFlyEnd = () =>
            {
                flyingText2List.Remove(flyText);
                flyText.Dispose();
                flyTextPool.Unused(EResType.UIPrefab, FlyAddCurrencyPrefabName, flyText.gameObject);
            };

            flyTextQueue.Enqueue(flyText);

            //if (flyingText2List.Count == 0)
            {
                ExecuteFly2();
            }
        });
    }

    public void AddCurrency(Transform _parent, ECurrencyType _currencyType, Vector3 _fromWorldPosition, Vector3 _toWorldPosition, float _duration = 0.5f)
    {
        var currencyCfg = ConfigSystem.CurrentData.GetCurrency(_currencyType);
        if (currencyCfg == null)
        {
            LogUtil.Error("货币类型未找到：" + _currencyType);
            return;
        }

        if (_parent == null)
        {
            _parent = flyTextRoot;
        }

        flyTextPool.GetGameObject(EResType.UIPrefab, FlyCurrencyPrefabName, (go) =>
        {
            go.transform.SetParent(_parent);

            RectTransform rt = go.GetComponent<RectTransform>();
            rt.pivot = new Vector2(0.5f, 0.5f);
            rt.anchorMax = new Vector2(0.5f, 0.5f);
            rt.anchorMin = new Vector2(0.5f, 0.5f);
            rt.anchoredPosition = new Vector2(0, 0);
            rt.localScale = Vector3.one;

            go.SetActive(false);

            UIFlyCurrency currencyText = go.GetComponent<UIFlyCurrency>();
            currencyText.onFlyEnd = () =>
            {
                currencyText.Dispose();
                flyTextPool.Unused(EResType.UIPrefab, FlyCurrencyPrefabName, currencyText.gameObject);
            };

            currencyText.Fly(_currencyType, _fromWorldPosition, _toWorldPosition, _duration);
        });
    }

    public void AddCurrency(Transform _parent, string _icon, Vector3 _fromWorldPosition, Vector3 _toWorldPosition, float _duration = 0.5f)
    {
        if (_parent == null)
        {
            _parent = flyTextRoot;
        }

        flyTextPool.GetGameObject(EResType.UIPrefab, FlyCurrencyPrefabName, (go) =>
        {
            go.transform.SetParent(_parent);

            RectTransform rt = go.GetComponent<RectTransform>();
            rt.pivot = new Vector2(0.5f, 0.5f);
            rt.anchorMax = new Vector2(0.5f, 0.5f);
            rt.anchorMin = new Vector2(0.5f, 0.5f);
            rt.anchoredPosition = new Vector2(0, 0);
            rt.localScale = Vector3.one;

            go.SetActive(false);

            UIFlyCurrency currencyText = go.GetComponent<UIFlyCurrency>();
            currencyText.onFlyEnd = () =>
            {
                currencyText.Dispose();
                flyTextPool.Unused(EResType.UIPrefab, FlyCurrencyPrefabName, currencyText.gameObject);
            };

            currencyText.Fly(_icon, _fromWorldPosition, _toWorldPosition, _duration);
        });
    }

    private void ExecuteFly()
    {
        if (flyTextQueue.Count == 0)
        {
            return;
        }

        UIFlyText flyText = flyTextQueue.Dequeue();
        flyingTextList.Add(flyText);
        flyText.Fly();
    }

    private void ExecuteFly2()
    {
        if (flyTextQueue.Count == 0)
        {
            return;
        }

        UIFlyText flyText = flyTextQueue.Dequeue();
        flyingText2List.Add(flyText);
        flyText.Fly();
    }
}
