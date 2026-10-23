using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;

[RequireComponent(typeof(Image))]
public class SpriteFrameAnim : MonoBehaviour
{
    public Image Image { get { return image; } }

    public string atlas;
    public string spritePrefix = "";
    public int startIndex = 1;
    public int endIndex = 1;
    public int frameCount = 10;
    public float intervalDuration;

    private System.Action onPlayStart;
    private System.Action onPlayEnd;
    private Image image;
    private int frame;
    private int totalCount;
    private float interval;
    private float curTime;
    private bool isStart;

    void Awake()
    {
        image = GetComponent<Image>();
    }

    void Start()
    {
        totalCount = endIndex - startIndex + 1;
        interval = 1.0f / frameCount;
        curTime = 0;
        frame = 1;
        isStart = true;

        onPlayStart?.Invoke();

        SetImage();
    }

    
    void Update()
    {
        if (isStart)
        {
            curTime += Time.deltaTime;
            if (curTime >= interval)
            {
                curTime = 0;

                frame++;
                if (frame == totalCount)
                {
                    if (intervalDuration > 0)
                    {
                        isStart = false;
                    }

                    onPlayEnd?.Invoke();
                }
                else if(frame > totalCount)
                {
                    frame = 1;
                }

                if(frame == 1)
                {
                    onPlayStart?.Invoke();
                }

                SetImage();
            }
        }
        else
        {
            curTime += Time.deltaTime;
            if (curTime >= intervalDuration)
            {
                curTime = interval;
                isStart = true;
                frame = 0;
            }
        }
       
        
    }

    void SetImage()
    {
        AssetHandler.Instance.SetSprite(atlas, spritePrefix + (startIndex + frame - 1), image);
    }

    public void SetPlayEnd(System.Action _func)
    {
        onPlayEnd = _func;
    }

    public void SetPlayStart(System.Action _func)
    {
        onPlayStart = _func;
    }
}
