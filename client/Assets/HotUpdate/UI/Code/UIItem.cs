using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIItem : UIContainer, IUIItem
{
    public int Index
    {
        get { return this.index; }
    }

    protected int index;
    protected object data;

    public void Create(GameObject _go, int _index, object _container = null)
    {
        this.go = _go;
        this.index = _index;
        this.go.layer = LayerMask.NameToLayer("UI");

        //this.rectTransform = (RectTransform)this.go.transform;
        //this.rectTransform.pivot = new Vector2(0.5f, 0.5f);

        //this.rectTransform.anchoredPosition = Vector2.zero;

        //this.rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, 0);
        //this.rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, 0);

        //this.rectTransform.anchorMax = Vector2.one;
        //this.rectTransform.anchorMin = Vector2.zero;

        Bind(_go, _container);
        OnCreate();
    }

    protected virtual void OnCreate()
    {

    }

    public void SetIndex(int _index)
    {
        this.index = _index;
    }
    
    public void Show(object _data = null)
    {
        this.data = _data;

        OnShow();
    }

    protected virtual void OnShow()
    {

    }

    protected override void OnSetActive(bool _active)
    {
        base.OnSetActive(_active);

        if (_active)
        {
            OnShow();
        }
        else
        {
            Hide();
        }
    }

    public void Hide()
    {
        OnHide();
    }

    protected virtual void OnHide()
    {

    }

    protected override void OnUnbind()
    {
        base.OnUnbind();
    }

    public void SetItemClick(bool _active)
    {
        EmptyButton button = this.GameObject.GetComponent<EmptyButton>();
        if (_active)
        {
            if (button == null)
            {
                button = this.GameObject.AddComponent<EmptyButton>();
                button.onClick.AddListener(Click);
            }
        }
        else
        {
            if(button != null)
            {
                Object.Destroy(button);
            }
        }
        
    }

    public void Click()
    {
        OnClick();
    }

    protected virtual void OnClick()
    {
        
    }

    public void Recover()
    {
        OnRecover();
    }

    protected virtual void OnRecover()
    {
        
    }

    public void Destroy()
    {
        Recover();
        Hide();
        OnDestory();
        Unbind();
    }

    protected virtual void OnDestory()
    {

    }
    
    public bool activeSelf => GameObject.activeSelf;
    
}
