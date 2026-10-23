using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class GuideMask : UIBehaviour, ICanvasRaycastFilter
{
    private const string Params = "_Params";
    private const string IsRect = "_IsRect";
    private const string Gradient = "_Gradient";
    private const float AnimDuration = 0.2f;

    public Image hole;

    private Canvas canvas;
    private Camera uiCamera;
    private Material holeMat;
    private Bounds bounds;
    private bool isPlayAnim;
    private Vector4 fromInfo;
    private Vector4 toInfo;
    private float startTime;
    private Color defaultColor;
    private bool isInit;
    private System.Action onAnimDone;

    protected override void Awake()
    {
        Init();
    }

    public void SetCanvas(Canvas _canvas, Camera _uiCamera)
    {
        this.canvas = _canvas;
        this.uiCamera = _uiCamera;
    }

    private void Init()
    {
        if (isInit)
        {
            return;
        }

        if (hole == null)
        {
            hole = GetComponent<Image>();
        }
        hole.material = Material.Instantiate(hole.material);
        holeMat = hole.material;
        defaultColor = hole.color;

        isInit = true;
    }

    public Vector4 SetGuideTarget(RectTransform rect, bool isRect, int gradient, float offsetWidth, float offsetHeight, float _offsetX, float _offsetY, bool _alpha = false, System.Action _onAnimDone = null)
    {
        //  如果父组件有 HorizontalLayoutGroup，直接获取的 corners 不正确
        var parent = rect.transform.parent.gameObject;
        if (parent.TryGetComponent(out HorizontalLayoutGroup hlg))
        {
            hlg.CalculateLayoutInputHorizontal();
            hlg.CalculateLayoutInputVertical();
            hlg.SetLayoutHorizontal();
            hlg.SetLayoutVertical();
        }

        var corners = new Vector3[4];
        rect.GetWorldCorners(corners);
        var crect = this.canvas.transform as RectTransform;

        Vector2 p1 = WorldPointToUILocalPoint(this.uiCamera, crect, corners[0]);
        Vector2 p2 = WorldPointToUILocalPoint(this.uiCamera, crect, corners[2]);

        return SetGuideTarget(p1, p2, isRect, gradient, offsetWidth, offsetHeight, _offsetX, _offsetY, _alpha, _onAnimDone);
    }

    public Vector4 SetGuideTargetByMainCamera(RectTransform rect, bool isRect, int gradient, float offsetWidth, float offsetHeight, float _offsetX, float _offsetY, bool _alpha = false, System.Action _onAnimDone = null)
    {
        //  如果父组件有 HorizontalLayoutGroup，直接获取的 corners 不正确
        var parent = rect.transform.parent.gameObject;
        if (parent.TryGetComponent(out HorizontalLayoutGroup hlg))
        {
            hlg.CalculateLayoutInputHorizontal();
            hlg.CalculateLayoutInputVertical();
            hlg.SetLayoutHorizontal();
            hlg.SetLayoutVertical();
        }

        var corners = new Vector3[4];
        rect.GetWorldCorners(corners);
        var crect = this.canvas.transform as RectTransform;

        Vector2 p1 = WorldPointToUILocalPoint(Camera.main, crect, corners[0]);
        Vector2 p2 = WorldPointToUILocalPoint(Camera.main, crect, corners[2]);

        return SetGuideTarget(p1, p2, isRect, gradient, offsetWidth, offsetHeight, _offsetX, _offsetY, _alpha, _onAnimDone);
    }

    public Vector4 SetGuideSceneTarget(Transform target, bool isRect, int gradient, float offsetWidth, float offsetHeight, float _offsetX, float _offsetY, bool _alpha = false, System.Action _onAnimDone = null)
    {
        var crect = this.canvas.transform as RectTransform;

        Vector2 uiPos = WorldPointToUILocalPoint(Camera.main, crect, target.position);

        return SetGuideTarget(uiPos, uiPos, isRect, gradient, offsetWidth, offsetHeight, _offsetX, _offsetY, _alpha, _onAnimDone);
    }

    private Vector4 SetGuideTarget(Vector2 p1, Vector2 p2, bool isRect, int gradient, float offsetWidth, float offsetHeight, float _offsetX, float _offsetY, bool _alpha = false, System.Action _onAnimDone = null)
    {
        bounds.SetMinMax(p1, p2);

        var info = new Vector4();
        if (isRect)
        {
            info.Set(p1.x - offsetWidth / 2 + _offsetX, p1.y - offsetHeight / 2 + _offsetY, p2.x + offsetWidth / 2, p2.y + offsetHeight / 2);
        }
        else
        {
            var offset = (p2 - p1 + new Vector2(offsetWidth, offsetHeight)) * 0.5f;
            var center = p1 + offset;
            info.Set(center.x - offsetWidth / 2 + _offsetX, center.y - offsetHeight / 2 + _offsetY, offset.magnitude, 0);
        }

        SetAlpha(_alpha);

        SetGuideTarget(info, isRect, gradient, _onAnimDone);

        return info;
    }

    public void SetGuideTarget(Vector4 info, bool isRect, int gradient, System.Action _onAnimDone = null)
    {
        if (holeMat == null)
        {
            holeMat = hole.material;
        }

        toInfo = info;
        fromInfo = toInfo;
        fromInfo.x -= 1000;
        fromInfo.y -= 1000;
        fromInfo.z += 1000;
        fromInfo.w += 1000;

        holeMat.SetFloat(IsRect, isRect ? 1 : 0);
        holeMat.SetVector(Params, fromInfo);
        holeMat.SetInt(Gradient, gradient);

        this.onAnimDone = _onAnimDone;
        isPlayAnim = true;
        startTime = Time.unscaledTime;
    }

    private void SetAlpha(bool _alpha)
    {
        if (!isInit)
        {
            Init();
        }

        if (_alpha)
        {
            Color col = defaultColor;
            col.a = 0;

            hole.color = col;
        }
        else
        {
            hole.color = defaultColor;
        }
    }

    public Vector4 GetGuideVector()
    {
        if (holeMat == null)
        {
            holeMat = hole.material;
        }
        return holeMat.GetVector(Params);
    }

    public void SetGuideVector(Vector4 info)
    {
        if (holeMat == null)
        {
            holeMat = hole.material;
        }
        holeMat.SetVector(Params, info);
    }

    public void ResetGuideTarget()
    {
        SetGuideTarget(Vector4.zero, false, 0);
        bounds.SetMinMax(Vector3.zero, Vector3.zero);
    }

    public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
    {
        var rect = this.canvas.transform as RectTransform;
        Vector2 local;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, sp, eventCamera, out local);
        return !bounds.Contains(local);
    }

    private void Update()
    {
        if (!isPlayAnim)
        {
            return;
        }

        float delta = Time.unscaledTime - startTime;
        if (delta >= AnimDuration)
        {
            isPlayAnim = false;
            holeMat.SetVector(Params, toInfo);

            this.onAnimDone?.Invoke();
            this.onAnimDone = null;
        }
        else
        {
            float ratio = delta / AnimDuration;
            var lerp = Vector4.Lerp(fromInfo, toInfo, ratio);

            holeMat.SetVector(Params, lerp);
        }
    }

    private Vector3 WorldPointToUILocalPoint(Camera _cam, RectTransform _rectTf, Vector3 _point)
    {
        Camera uiCam = this.uiCamera;
        if (uiCam == null)
        {
            uiCam = Camera.main;
        }

        Vector3 screenPoint = RectTransformUtility.WorldToScreenPoint(_cam, _point);

        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_rectTf, screenPoint, uiCam, out localPoint);

        return localPoint;
    }
}
