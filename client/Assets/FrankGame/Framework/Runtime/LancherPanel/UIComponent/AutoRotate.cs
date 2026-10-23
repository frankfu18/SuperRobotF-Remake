using UnityEngine;

// 确保在编辑器模式下也能看到旋转效果（可选，但方便调试）
[ExecuteAlways]
public class AutoRotate : MonoBehaviour
{
    
    [Header("旋转设置")]
    [Tooltip("围绕哪个轴旋转 (X, Y, Z)。例如，Vector3.up 是绕 Y 轴。")]
    public Vector3 rotationAxis = Vector3.up;
    
    [Tooltip("每秒旋转的角度数 (Degrees Per Second)。")]
    public float rotationSpeed = 50.0f; 

    // 枚举定义旋转方向
    public enum RotateDirection { Clockwise = -1, CounterClockwise = 1 }
    
    [Tooltip("设置旋转方向：顺时针（Clockwise）或逆时针（CounterClockwise）。")]
    public RotateDirection direction = RotateDirection.CounterClockwise;

    // 私有变量，用于存储旋转方向的数值（-1 或 1）
    private float directionMultiplier => (int)direction;

    // Update 在每一帧都会被调用
    void Update()
    {
        // 核心旋转逻辑：
        // 1. rotationAxis: 定义旋转轴 (X, Y, Z)
        // 2. rotationSpeed: 每秒转过的度数
        // 3. directionMultiplier: 控制正向（逆时针）或负向（顺时针）
        // 4. Time.deltaTime: 乘以时间间隔，实现匀速且与帧率无关的旋转
        
        // 计算旋转量：速度 * 方向 * 时间
        float rotationAmount = rotationSpeed * directionMultiplier * Time.deltaTime;
        // 应用旋转。Space.Self 表示绕物体的自身轴旋转
        transform.Rotate(rotationAxis, rotationAmount, Space.Self);
    }

    /* * 如果您希望绕世界坐标轴旋转（不随物体自身朝向改变）：
     * 可以将 Update 中的 Space.Self 改为 Space.World。
     * transform.Rotate(rotationAxis, rotationAmount, Space.World);
     */
    
}