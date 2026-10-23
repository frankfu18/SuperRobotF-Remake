using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using editor.cfg.common;

public class ItemDescTips : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI title;
    public TextMeshProUGUI desc;
    public GameObject top;
    public GameObject topArrow;
    public GameObject bottomArrow;

    public List<RectTransform> layoutGroups;

    private RectTransform targetRect;
    private float offsetX;
    private float offsetY;

    private void Awake()
    {
        Canvas canvas = GetComponent<Canvas>();
        canvas.sortingLayerName = "UI";
    }

    public void Show(ECurrencyType _currency, RectTransform _target, float _offsetX = 0, float _offsetY = 0)
    {
        targetRect = _target;
        offsetX = _offsetX;
        offsetY = _offsetY;

        var cfg = ConfigSystem.CurrentData.GetCurrency(_currency);

        if(this.icon != null)
        {
            AssetHandler.Instance.SetSpriteById(cfg.icon, icon, false);
        }

        title.text = LanguageSystem.Instance.GetLanguage(cfg.name);
        desc.text = LanguageSystem.Instance.GetLanguage(cfg.desc);

        if (layoutGroups != null)
        {
            foreach (var group in layoutGroups)
            {
                LayoutRebuilder.ForceRebuildLayoutImmediate(group);
            }
        }
        
        RefreshPos();
    }

    public void Show(int _itemId, RectTransform _target, float _offsetX = 0, float _offsetY = 0)
    {
        targetRect = _target;
        offsetX = _offsetX;
        offsetY = _offsetY;

        var cfg = ConfigSystem.CurrentData.GetItem(_itemId);

        if (this.icon != null)
        {
            AssetHandler.Instance.SetSpriteById(cfg.icon, icon, false);
        }

        title.text = LanguageSystem.Instance.GetLanguage(cfg.name);
        desc.text = LanguageSystem.Instance.GetLanguage(cfg.desc);

        if (layoutGroups != null)
        {
            foreach (var group in layoutGroups)
            {
                LayoutRebuilder.ForceRebuildLayoutImmediate(group);
            }
        }

        RefreshPos();
    }

    public void Show(string nameStr, string descStr, RectTransform _target,float _offsetX = 0, float _offsetY = 0)
    {
        targetRect = _target;
        offsetX = _offsetX;
        offsetY = _offsetY;

        if (!string.IsNullOrEmpty(nameStr) && nameStr.Length > 0)
        {
            title.text = nameStr;
        }
        else
        {
            title.gameObject.SetActive(false);
            icon?.gameObject.SetActive(false);
        }

        if (!string.IsNullOrEmpty(descStr) && descStr.Length > 0)
        {
            desc.text = descStr;
        }
        else
        {
            desc.gameObject.SetActive(false);
        }

        if (layoutGroups != null)
        {
            foreach (var group in layoutGroups)
            {
                LayoutRebuilder.ForceRebuildLayoutImmediate(group);
            }
        }

        RefreshPos();
    }


    public void Show(string iconStr, string nameStr, string descStr, RectTransform _target,float _offsetX = 0, float _offsetY = 0)
    {
        if (this.icon != null)
        {
            AssetHandler.Instance.SetSpriteById(iconStr, icon);
        }
        
        Show(nameStr, descStr, _target, _offsetX, _offsetY);
    }

    private void RefreshPos()
    {
        float screenHalfHeight = 0;
#if UNITY_EDITOR
        screenHalfHeight = Screen.height / 2f;
#else
        screenHalfHeight = Screen.currentResolution.height / 2f;
#endif
        RectTransform rectTransform = this.transform as RectTransform;

        float tipsHalfHeight = rectTransform.sizeDelta.y / 2;
        var localPos = UIUtils.WorldPointToUILocalPoint(this.transform.parent as RectTransform, this.targetRect.position);

        //localPos.x += (this.targetRect.pivot.x - 0.5f) * rectTransform.sizeDelta.x;
        localPos.y += (this.targetRect.pivot.y - 0.5f) * rectTransform.sizeDelta.y;

        localPos.x += offsetX;
        localPos.y += offsetY;

        if ((localPos.y + tipsHalfHeight) > screenHalfHeight)
        {
            localPos.y -= tipsHalfHeight;

            //this.top.SetActive(true);
            //this.topArrow.SetActive(true);
            //this.bottomArrow.SetActive(false);

            //this.topArrow.transform.position = this.top.transform.position;
        }
        else
        {
            localPos.y += tipsHalfHeight;

            //this.top.SetActive(false);
            //this.topArrow.SetActive(false);
            //this.bottomArrow.SetActive(true);
        }

        (this.transform as RectTransform).anchoredPosition = localPos;
    }
}
