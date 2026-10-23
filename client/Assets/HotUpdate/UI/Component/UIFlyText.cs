using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class UIFlyText : MonoBehaviour 
{
    public Image contentIcon;
    public TextMeshProUGUI contentText;
    public TextMeshProUGUI amountText;
    public CanvasGroup canvasGroup;

    public System.Action onFlyEnd;
    public System.Action onFlyTop;

    [HideInInspector]
    public string content;
    [HideInInspector]
    public string amount;
    [HideInInspector]
    public string icon;
    [HideInInspector]
    public float flyHeight = 250;
    [HideInInspector]
    public Color textColor = Color.white;


    public void Fly()
    {
        this.contentText.text = content;
        this.contentText.color = textColor;
        if (this.amountText != null)
        {
            this.amountText.text = amount;
            this.amountText.color = textColor;
        }
        
        if(contentIcon != null)
        {
            if (!string.IsNullOrEmpty(icon))
            {
                AssetHandler.Instance.SetSpriteById(icon, contentIcon);
            }
        }

        //this.transform.localPosition = new Vector3(0, 0, 0);
        this.transform.localRotation = Quaternion.identity;
        this.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

        this.gameObject.SetActive(true);

        canvasGroup.alpha = 0;
        canvasGroup.DOFade(1, 0.5f).SetUpdate(true);

        this.transform.DOScale(Vector3.one, 0.2f).SetUpdate(true);

        var posTween = this.transform.DOLocalMoveY(this.transform.localPosition.y + flyHeight, 0.5f).SetUpdate(true);
        posTween.onComplete = () =>
        {
            onFlyTop?.Invoke();

            var alphaTween = canvasGroup.DOFade(0, 0.5f).SetUpdate(true);
            alphaTween.SetDelay(0.2f);
            alphaTween.onComplete = () =>
            {
                onFlyEnd?.Invoke();
            };
        };
    }

    public void Dispose()
    {
        this.transform.DOKill();
        this.canvasGroup.DOKill();
        onFlyEnd = null;
    }
}
