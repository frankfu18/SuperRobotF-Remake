using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class WaitMask : MonoBehaviour
{
    private Color whiteColor = new Color(0, 0, 0, 0);
    private Color blackColor = new Color(0, 0, 0, 0.4f);

    public Image WaitBg;
    public GameObject WaitIcon;
    public float duration = 2;

    private bool isBlackTween;

    public void Show(bool _blackTween)
    {
        this.gameObject.SetActive(true);
        this.WaitBg.DOKill();
        StopAllCoroutines();

        isBlackTween = _blackTween;
        if (isBlackTween)
        {
            WaitBg.color = Color.black;
            WaitIcon.SetActive(false);
        }
        else
        {
            if (duration > 0)
            {
                StartCoroutine(DelayShowIcon());
            }
            else
            {
                WaitIcon.SetActive(true);
                WaitBg.color = blackColor;
            }
        }
    }

    public void Hide()
    {
        this.WaitBg.DOKill();
        StopAllCoroutines();

        if (isBlackTween)
        {
            this.WaitBg.DOFade(0, 0.5f).SetUpdate(true).onComplete = () =>
            {
                this.gameObject.SetActive(false);
            };
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }

    private IEnumerator DelayShowIcon()
    {
        WaitBg.color = whiteColor;
        WaitIcon.SetActive(false);
        yield return new WaitForSecondsRealtime(2);
        WaitIcon.SetActive(true);
        WaitBg.color = blackColor;
    }
}
