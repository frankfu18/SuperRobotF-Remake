using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public static class UIUtils
{
    public static void SetText(Text _txt, string _str)
    {
        _txt.text = _str;
    }

    public static void OpenLoading()
    {
        EventCenterSystem.Instance.SendEvent(SysEvent.Loading_Open);
    }

    public static void CloseLoading()
    {
        EventCenterSystem.Instance.SendEvent(SysEvent.Loading_Close);
    }

    public static void SetLayerAllChildren(Transform root, int layer)
    {
        var children = root.GetComponentsInChildren<Transform>(includeInactive: true);
        foreach (var child in children)
        {
            child.gameObject.layer = layer;
        }
    }

    public static Vector3 WorldPointToUILocalPoint(Camera _cam, RectTransform _rectTf, Vector3 _point)
    {
        Camera uiCam = UISystem.Instance.UICamera;
        if (uiCam == null)
        {
            uiCam = Camera.main;
        }

        Vector3 screenPoint = RectTransformUtility.WorldToScreenPoint(_cam, _point);

        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_rectTf, screenPoint, uiCam, out localPoint);

        return localPoint;
    }

    public static Vector3 WorldPointToUILocalPoint(Camera _cam, Camera _targetCam, RectTransform _rectTf, Vector3 _point)
    {
        Vector3 screenPoint = RectTransformUtility.WorldToScreenPoint(_cam, _point);

        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_rectTf, screenPoint, _targetCam, out localPoint);

        return localPoint;
    }

    public static Vector3 WorldPointToUILocalPoint(RectTransform _rectTf, Vector3 _point)
    {
        Camera uiCam = UISystem.Instance.UICamera;
        if (uiCam == null)
        {
            uiCam = Camera.main;
        }

        return WorldPointToUILocalPoint(uiCam, _rectTf, _point);
    }

    public static Vector3 WorldPointToWorldPoint(Camera _cam, Camera _targetCam, Vector3 _point)
    {
        Vector3 screenPoint = _cam.WorldToScreenPoint(_point);
        Vector3 worldPoint = _targetCam.ScreenToWorldPoint(screenPoint);

        return worldPoint;
    }

    public static float SetAlpha(SpriteRenderer _image, float _alpha)
    {
        Color color = _image.color;
        float oa = color.a;
        color.a = _alpha;
        _image.color = color;
        return oa;
    }
    public static float SetAlpha(Tilemap _image, float _alpha)
    {
        Color color = _image.color;
        float oa = color.a;
        color.a = _alpha;
        _image.color = color;
        return oa;
    }

    /// <summary>
    /// 格式化大金额，用K，M，KM来处理
    /// </summary>
    /// <param name="_amount">金额</param>
    /// <param name="_comma">是否需要,分割</param>
    /// <returns>结果</returns>
    public static string FormatBigAmount(long _amount, bool _comma = true)
    {
        return _comma ? $"{_amount:###,##0}" : $"{_amount}";
    }
}
