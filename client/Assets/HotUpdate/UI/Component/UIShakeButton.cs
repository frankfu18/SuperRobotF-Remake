using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class UIShakeButton : MonoBehaviour {

    private Sequence ani;

    public float ShakeOffset = 10;

    public float ShakeDuration = 0.5f;

    public bool OffsetX = true;

    private void OnEnable() {
        Invoke(nameof(StartAnimation), 2f);
    }

    private void StartAnimation() {
        if (!gameObject.activeSelf) {
            return;
        }
        ani = DOTween.Sequence();

        if (OffsetX) {
            ani.Append(transform.DOLocalMoveX(-ShakeOffset, ShakeDuration));
            ani.Append(transform.DOLocalMoveX(ShakeOffset * 2, ShakeDuration * 2));
            ani.Append(transform.DOLocalMoveX(-ShakeOffset, ShakeDuration));
        } else {
            ani.Append(transform.DOLocalMoveY(-ShakeOffset, ShakeDuration));
            ani.Append(transform.DOLocalMoveY(ShakeOffset * 2, ShakeDuration * 2));
            ani.Append(transform.DOLocalMoveY(-ShakeOffset, ShakeDuration));
        }
        ani.SetEase(Ease.Linear).SetRelative(true).SetLoops(-1, LoopType.Restart);
    }

    private void OnDisable() {
        ani?.Complete();
        ani?.Kill();
        ani = null;
    }
}
