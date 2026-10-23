using System;
using UnityEngine;

public class RayCastTarget : MonoBehaviour
{
    public string clickParam;
    public System.Action<string> OnClickFunc;

    private BoxCollider2D boxCollider;

    private void Awake()
    {
        boxCollider = this.GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;

        if (boxCollider == null) return;
        
        // // 如果点在UI上，直接返回，不处理
        // if (UnityEngine.EventSystems.EventSystem.current != null && UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        //     return;
        
        Vector2 p = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (boxCollider.OverlapPoint(p))
        {
            OnClick();
        }
    }
    
    public void OnClick()
    {
        OnClickFunc?.Invoke(clickParam);
    }
}