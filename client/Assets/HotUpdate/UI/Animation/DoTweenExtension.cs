using DG.Tweening;
using UnityEngine;

public static class DoTweenExtension
{
    /// <summary>
    /// anchored x 动画
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="endValue"></param>
    /// <param name="duration"></param>
    /// <param name="loops"></param>
    /// <param name="ease"></param>
    /// <param name="loopType"></param>
    /// <returns></returns>
    public static Tween DoAnchoredX(this RectTransform transform, float endValue, float duration, int loops = -1, Ease ease = Ease.Linear, LoopType loopType = LoopType.Yoyo) {
        var origin = transform.anchoredPosition;
        var tween = DOTween.To(() => origin.x, o => transform.anchoredPosition = new Vector2(o, origin.y), endValue, duration);
        tween.SetLoops(loops, loopType).SetEase(ease);
        return tween;
    }

    /// <summary>
    /// 扩展一个 swing up
    /// </summary>
    /// <param name="transform">动画目标</param>
    /// <param name="offset">偏移</param>
    /// <param name="duration">时长</param>
    /// <param name="dir">方向</param>
    /// <param name="ease">动画</param>
    /// <param name="loops">次数</param>
    /// <param name="loopType">切换方式</param>
    /// <returns>动画句柄</returns>
    public static Tween DoSwingUpDown(this Transform transform, float offset, float duration, SwingDirection dir = SwingDirection.X, Ease ease = Ease.Linear , int loops = -1, LoopType loopType = LoopType.Yoyo) {
        var offsetV3 = new Vector3(0, 0, 0);
        switch (dir) {
            case SwingDirection.X:
                offsetV3.x = offset;
                break;
            case SwingDirection.Y:
                offsetV3.y = - offset;
                break;
            case SwingDirection.Z:
                offsetV3.z = offset;
                break;
        }

        var currentPos = transform.localPosition;
        transform.localPosition = currentPos - offsetV3;
        return transform.DOLocalMove(currentPos + offsetV3, duration / 2).SetEase(ease).SetLoops(loops, loopType);
    }


    public enum SwingDirection { 
        X,
        Y,
        Z
    }
}