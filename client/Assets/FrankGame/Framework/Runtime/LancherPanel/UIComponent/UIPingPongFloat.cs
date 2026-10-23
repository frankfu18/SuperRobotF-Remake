using UnityEngine;
using System.Collections;

// 确保在编辑器模式下也能看到效果，方便调整参数
[ExecuteAlways]
public class UIPingPongFloat : MonoBehaviour
{
    [Header("漂浮参数设置")]
    
    [Tooltip("漂浮的幅度：物体将上下移动的距离。例如 50 表示以当前位置为中心，向上 50，向下 50。")]
    public float floatAmplitude = 50f; 

    [Tooltip("漂浮的速度：数字越大，来回移动越快。这个值乘以 Time.time。")]
    public float floatSpeed = 1.0f;
    private float randomFloatSpeed = 1.0f; 

    // [Tooltip("初始时间偏移（延迟）：用于错开多个物体的漂浮动画，让它们看起来不同步。")]
    private float timeDelay = 0f;

    // 私有变量，用于存储 RectTransform 组件和初始位置
    private RectTransform rectTransform;
    private float startYPosition;

    void Awake()
    {
        timeDelay = Random.Range(-0.1f, 0.3f);
        timeDelay = Mathf.Clamp01(timeDelay);
        randomFloatSpeed = Random.Range(-0.1f, 0.3f);
        floatSpeed += randomFloatSpeed;
        
        // 尝试获取 RectTransform 组件
        rectTransform = GetComponent<RectTransform>();
        if (rectTransform == null)
        {
            Debug.LogError("UIPingPongFloat 脚本需要附加到一个具有 RectTransform 组件的 GameObject 上 (通常是 UI 元素)。");
            return;
        }

        // 记录物体开始时的本地 Y 轴位置
        startYPosition = rectTransform.anchoredPosition.y;

        // 如果在编辑器模式下运行，立即设置初始位置
        if (!Application.isPlaying)
        {
            // SetInitialPosition();
        }
    }

    // 设置初始位置，用于编辑器预览
    private void SetInitialPosition()
    {
        rectTransform.anchoredPosition = new Vector2(
            rectTransform.anchoredPosition.x,
            startYPosition
        );
    }

    void Update()
    {
        if (rectTransform == null) return;
        
        // 使用 Time.time 确保动画随着时间推进，并加上 timeDelay 偏移
        float time = Time.time * floatSpeed + timeDelay;

        // 使用 Mathf.Sin 函数创建平滑的 Yo-Yo 循环：
        // 1. Sin(time) 的值会在 -1 到 1 之间完美循环。
        // 2. 乘以 floatAmplitude 得到漂浮的最终位移距离。
        // 3. 加上 startYPosition 得到最终的本地 Y 坐标。
        
        float newY = startYPosition + Mathf.Sin(time) * floatAmplitude;

        // 更新 RectTransform 的 Y 轴位置
        rectTransform.anchoredPosition = new Vector2(
            rectTransform.anchoredPosition.x,
            newY
        );
        
        // 在编辑器模式下，确保每次调整参数后都能立即刷新
        if (!Application.isPlaying)
        {
            // 确保 startYPosition 在编辑器中调整位置后能更新
            // startYPosition = rectTransform.anchoredPosition.y - Mathf.Sin(timeDelay) * floatAmplitude;
        }
    }
}