using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//打字机组件，只支持单一富文本，暂不支持嵌套富文本
[RequireComponent(typeof(TextMeshProUGUI))]
public class TMPTypeWriter : MonoBehaviour
{
    public struct RichTextData
	{
        public int index;
        public int length;

		public RichTextData(int index, int length)
		{
			this.index = index;
			this.length = length;
		}
	}

    public enum TextType
    {
        Text,
        TextMesh,
    }

    [SerializeField]
    public float WaitTime;
    private TextMeshProUGUI TextLabel;

    private Action _callback;

    private int _charIndex = -1;
    private string _dialog;
    //private string _tempText;

    private Dictionary<int, RichTextData> dicRichDatas;
    private List<int> richIndexList;
    private StringBuilder crtDialog;

    private const string REGEX_RICH_STR = @"<.*?>";
    private Regex regex_1 = new Regex(REGEX_RICH_STR);
    private bool isStart;
    private float startTime;

    public bool IsTyping
    {
        get
        {
            return _charIndex != -1;
        }
    }

    private void Awake()
    {
        TextLabel = this.GetComponent<TextMeshProUGUI>();
    }


    public void Show(string dialog, Action callback=null)
    {
        Stop();
        ClearText();

        _callback = callback;
        if (_charIndex == -1)
        {
            _charIndex = 0;
			_dialog = dialog;

            // 富文本处理
			MatchCollection matchs = regex_1.Matches(dialog);
            if (matchs.Count > 0)
			{
                dicRichDatas = new Dictionary<int, RichTextData>();
                richIndexList = new List<int>();
			}
			else
			{
                dicRichDatas = null;
                richIndexList = null;
			}

            for(int i = 0; i < matchs.Count; i++)
			{
                dicRichDatas.Add(matchs[i].Index, new RichTextData(matchs[i].Index, matchs[i].Length));
                richIndexList.Add(matchs[i].Index);
			}
            if (richIndexList != null)
                richIndexList.Sort();

            InitDialog();

			if (WaitTime == 0)
            {
                SetText();
            }
            else
            {
                this.isStart = true;
                this.startTime = Time.unscaledTime;
            }
        }
    }

    public void Stop()
    {
        _charIndex = -1;
        this.isStart = false;
    }

    public void Skip()
    {
        Stop();
        SetText();
    }

	private void InitDialog()
	{
        crtDialog = new StringBuilder();
        
        if (dicRichDatas != null)
		{
            foreach (int index in richIndexList)
			{
                crtDialog.Append(_dialog.Substring(dicRichDatas[index].index, dicRichDatas[index].length));
				if (dicRichDatas.ContainsKey(_charIndex))
				{
					_charIndex += dicRichDatas[_charIndex].length;
				}
			}
		}
	}

	public void SetText()
    {
        _charIndex = -1;

        TextLabel.text = _dialog;

        if (_callback!=null){
            _callback();
        }
    }

    public void ClearText()
    {
        TextLabel.text = string.Empty;
    }

    private void NextChar()
    {
        if (_charIndex != -1 && _charIndex < _dialog.Length)
        {
            crtDialog.Insert(_charIndex, _dialog[_charIndex]);

            TextLabel.text = crtDialog.ToString();

            _charIndex++;
            if (dicRichDatas != null && dicRichDatas.ContainsKey(_charIndex))
			{
                _charIndex += dicRichDatas[_charIndex].length;
			}

            this.isStart = true;
            this.startTime = Time.unscaledTime;
        }
        else
        {
            _charIndex = -1;
            
            if(_callback!=null){
                _callback();
            }

            this.isStart = false;
        }
    }

    private void Update()
    {
        if (!isStart)
        {
            return;
        }

        float delta = Time.unscaledTime - startTime;
        if (delta >= WaitTime)
        {
            NextChar();
        }
    }
}