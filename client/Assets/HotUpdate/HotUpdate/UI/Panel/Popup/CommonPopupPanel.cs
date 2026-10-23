using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class stCommonPopupPanelData
{
    public int btnCount;
    public string title;//标题名
    public string content;//内容
    public string confirmBtnName;//确认按钮名
    public string cancelBtnName;//取消按钮名
    public System.Action onConfirmFunc;//确认按钮事件
    public System.Action onCancelFunc;//取消按钮事件
    public bool isNotClosePopup;//是否不要关闭弹窗
    public bool oneBtnHasClose;//一个按钮时x是否只是关闭
}

public partial class CommonPopupPanel : UIPanel
{
    private int btnCount;
    private string title;
    private string content;
    private string confirmBtnName;
    private string cancelBtnName;
    private System.Action onConfirmFunc;
    private System.Action onCancelFunc;
    private bool isNotClosePopup;
    private bool oneBtnHasClose;

    protected override void OnRegisterListener()
    {
        base.OnRegisterListener();

        AddButtonClick(this.m_Button_Confirm, OnClickConfirm);
        AddButtonClick(this.m_Button_Cancel, OnClickCancel);
        SetButtonClick(this.m_Button_Close, OnClickCancel);
    }

    protected override void OnShow()
    {
        base.OnShow();

        stCommonPopupPanelData panelData = this.data as stCommonPopupPanelData;

        this.btnCount = panelData.btnCount;
        this.title = panelData.title;
        this.content = panelData.content;
        this.confirmBtnName = panelData.confirmBtnName;
        this.cancelBtnName = panelData.cancelBtnName;
        this.onConfirmFunc = panelData.onConfirmFunc;
        this.onCancelFunc = panelData.onCancelFunc;
        this.isNotClosePopup = panelData.isNotClosePopup;
        this.oneBtnHasClose = panelData.oneBtnHasClose;

        if (string.IsNullOrEmpty(this.title))
        {
            this.title = GetLocalizeText("title_prompt");
        }

        if (string.IsNullOrEmpty(this.confirmBtnName))
        {
            this.confirmBtnName = GetLocalizeText("btn_sure");
        }

        if (string.IsNullOrEmpty(this.cancelBtnName))
        {
            this.cancelBtnName = GetLocalizeText("btn_cancel");
        }

        SetText(this.m_TMP_Text_Title, this.title);
        SetText(this.m_TMP_Text_Content, this.content);

        SetText(this.m_TMP_Text_BtnConfirm, this.confirmBtnName);
        SetText(this.m_TMP_Text_BtnCancel, this.cancelBtnName);

        if (btnCount >= 2)
        {
            SetActive(this.m_Button_Confirm, true);
            SetActive(this.m_Button_Cancel, true);
        }
        else
        {
            SetActive(this.m_Button_Confirm, true);
            SetActive(this.m_Button_Cancel, false);
        }
    }

    private void OnClickConfirm()
    {
        this.onConfirmFunc?.Invoke();

        if (!this.isNotClosePopup)
        {
            this.Close();
        }
    }

    private void OnClickCancel()
    {
        if(this.btnCount > 1)
        {
            this.onCancelFunc?.Invoke();
        }
        else
        {
            if (!oneBtnHasClose)
            {
                this.onConfirmFunc?.Invoke();
            }
        }

        if (!this.isNotClosePopup)
        {
            this.Close();
        }
    }
}
