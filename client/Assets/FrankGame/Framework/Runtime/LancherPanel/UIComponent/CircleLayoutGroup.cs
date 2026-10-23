using System.Collections.Generic;
using UnityEngine;

// 确保脚本在编辑器模式下也能执行，方便实时预览效果
[ExecuteAlways]
public class CircleLayoutGroup : MonoBehaviour
{
    // 公开参数，可以在 Inspector 中调整
    public float radius = 50f; // 圆形布局的半径（您希望是 5）
    [Range(0, 360)]
    public float startAngle = 90f; // 起始角度 (0度在右边，90度在上方)

    // 每一帧是否自动刷新布局
    public bool autoRefresh = true;

    // 核心布局逻辑
    public void UpdateLayout()
    {
        // 确保半径设置合理
        if (radius <= 0) return;
        // 只统计并获取活跃的子对象
        var activeChildren = new List<RectTransform>();
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            if (child.gameObject.activeSelf)
            {
                activeChildren.Add(child as RectTransform);
            }
        }
        int activeChildCount = activeChildren.Count;
        if (activeChildCount == 0) return;
        // 计算每张图片之间需要间隔的角度
        // 360 度除以子对象数量，得到平均角度间隔
        float angleStep = 360f / activeChildCount;

        // 遍历所有子对象
        for (int i = 0; i < activeChildCount; i++)
        {
            RectTransform child = activeChildren[i];
            if (child == null) continue;
            // 计算当前角度（将起始角度转换为弧度）
            float angle = (startAngle + i * angleStep) * Mathf.Deg2Rad;
            
            // 计算位置
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;
            
            // 设置位置
            child.anchoredPosition = new Vector2(x, y);

            //  可选：使子对象朝向圆心
            // Vector2 directionToCenter = -child.anchoredPosition.normalized;
            // float angleToRotate = Mathf.Atan2(directionToCenter.y, directionToCenter.x) * Mathf.Rad2Deg;
            // child.localRotation = Quaternion.Euler(0, 0, -(angleToRotate - 90));
            Vector3 direction = transform.position - child.position;
            child.rotation = Quaternion.LookRotation(Vector3.forward, -direction);

            

            // RectTransform child = transform.GetChild(i).GetComponent<RectTransform>();
            // if (child == null) continue;

            // // 计算当前图片的角度（起始角度 + 序号 * 角度间隔）
            // float currentAngle = startAngle - (i * angleStep);

            // // 将角度从度（Degrees）转换为弧度（Radians），因为 Mathf.Cos/Sin 使用弧度
            // float angleInRadians = currentAngle * Mathf.Deg2Rad;

            // // 使用三角函数计算圆上坐标：
            // // X = 半径 * Cos(角度)
            // // Y = 半径 * Sin(角度)
            
            // float x = radius * Mathf.Cos(angleInRadians);
            // float y = radius * Mathf.Sin(angleInRadians);

            // // 设置子对象的本地坐标（相对于父对象中心）
            // // 注意：UGUI 的 RectTransform 锚点默认为中心 (0,0)，所以localPosition就是相对父对象中心的位置
            // child.localPosition = new Vector3(x, y, 0);

            // // 可选：设置图片朝向中心（如果图片需要“看”向圆心）
            // Vector3 direction = transform.position - child.position;
            // child.rotation = Quaternion.LookRotation(Vector3.forward, -direction);
        }
    }

    // 在编辑器模式和运行模式下都会调用 UpdateLayout()
    void LateUpdate()
    {
        if (autoRefresh || !Application.isPlaying)
        {
            UpdateLayout();
        }
    }
}