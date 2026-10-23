using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using editor.cfg.common;
using DG.Tweening;

public class UIFlyCurrency : MonoBehaviour
{
    public Image Icon;

    public System.Action onFlyEnd;

    private RectTransform rectTransform;
    private Vector2 startPos;
    private Vector2 endPos;
    private Tween tweener;
    private Sequence tweenrQueue;
    private bool isStartFly = false;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void Fly(ECurrencyType _currencyType, Vector3 _from, Vector3 _to, float _duration = 0.5f)
    {
        var currencyCfg = ConfigSystem.CurrentData.GetCurrency(_currencyType);
        if (currencyCfg == null)
        {
            LogUtil.Error("货币类型未找到：" + _currencyType);
            return;
        }

        Fly(currencyCfg.icon, _from, _to, _duration);
    }

    public void Fly(string _icon, Vector3 _from, Vector3 _to, float _duration = 0.5f)
    {
        AssetHandler.Instance.SetSpriteById(_icon, Icon);

        if (rectTransform == null)
        {
            rectTransform = GetComponent<RectTransform>();
        }

        var localPos = UIUtils.WorldPointToUILocalPoint(rectTransform.parent as RectTransform, _from);

        startPos = new Vector2(localPos.x, localPos.y);
        rectTransform.anchoredPosition = startPos;

        localPos = UIUtils.WorldPointToUILocalPoint(rectTransform.parent as RectTransform, _to);
        endPos = new Vector2(localPos.x, localPos.y);

        this.gameObject.SetActive(true);

        if (tweener != null)
        {
            tweener.Kill();
            tweener = null;
        }
        tweener = this.rectTransform.DOAnchorPos(endPos, _duration);
        tweener.SetEase(Ease.InCubic);
        tweener.onComplete += () =>
        {
            tweener = null;
            onFlyEnd?.Invoke();
        };
    }

    private void Update()
    {
        if (!isStartFly)
        {
            return;
        }

        //Vector2 targetPos = UIUtils.WorldPointToUILocalPoint(FrankGame.Battle.BattleManager.Instance.BattleViewNode.BattleCamera, rectTransform.parent as RectTransform, endPos);

        //Vector2 curPos = this.rectTransform.anchoredPosition;

        //Vector2 dir = (targetPos - curPos).normalized;

        //curPos += dir * Time.deltaTime * 3000;

        //float dot = Vector2.Dot((curPos - targetPos).normalized, dir);
        //if (dot > 0)
        //{
        //    isStartFly = false;

        //    this.rectTransform.anchoredPosition = targetPos;

        //    onFlyEnd?.Invoke();
        //}
        //else
        //{
        //    this.rectTransform.anchoredPosition = curPos;
        //}
    }

    public void Dispose()
    {
        if (tweenrQueue != null)
        {
            tweenrQueue.Kill();
            tweenrQueue = null;
        }

        if (tweener != null)
        {
            tweener.Kill();
            tweener = null;
        }

        if (rectTransform != null)
        {
            rectTransform.DOKill();
            rectTransform.localEulerAngles = Vector3.zero;
        }

        transform.DOKill();

        isStartFly = false;
    }


}
