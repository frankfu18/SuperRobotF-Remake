//using editor.cfg.equip;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrankGame.Battle
{
    public class Drawer : MonoBehaviour
    {
        //public float rotationDegrees = 0f;    // 绕Y轴旋转角度

        public bool isDrawEmenyQuadTree = false;
        public float scale = 1f;

        private bool[][] segments = new bool[10][]
            {
            new bool[] { true,  true,  true,  true,  true,  true,  false }, // 0
            new bool[] { false, true,  true,  false, false, false, false }, // 1
            new bool[] { true,  true,  false, true,  true,  false, true  }, // 2
            new bool[] { true,  true,  true,  true,  false, false, true  }, // 3
            new bool[] { false, true,  true,  false, false, true,  true  }, // 4
            new bool[] { true,  false, true,  true,  false, true,  true  }, // 5
            new bool[] { true,  false, true,  true,  true,  true,  true  }, // 6
            new bool[] { true,  true,  true,  false, false, false, false }, // 7
            new bool[] { true,  true,  true,  true,  true,  true,  true  }, // 8
            new bool[] { true,  true,  true,  true,  false, true,  true  }, // 9
            };

        void Start()
        {

        }

        
        void Update()
        {
            //rotationDegrees += 180f * Time.deltaTime;

            EntityManager.Instance.entityDic.Foreach((entity) => 
            {
                if(entity.TryGetComponent(ECompType.Transform, out TransformComponent tranCom))
                {
                    DrawDigits(new Vector3(tranCom.position.x, 0, tranCom.position.y), entity.ID.ToString(), 0.2f, 0.1f, Color.red);
                    
                }
                
                return false;
            });

            if (isDrawEmenyQuadTree)
            {
                World world = World.GetCurrent();

                if(world != null)
                {
                    
                }
            }
        }

        void DrawSquare(Vector3 center, float size, Color color)
        {
            float half = size / 2f;

            Vector3 p1 = center + new Vector3(-half, -half); // 左下
            Vector3 p2 = center + new Vector3(-half, half);  // 左上
            Vector3 p3 = center + new Vector3(half, half);   // 右上
            Vector3 p4 = center + new Vector3(half, -half);  // 右下

            Debug.DrawLine(p1, p2, color);
            Debug.DrawLine(p2, p3, color);
            Debug.DrawLine(p3, p4, color);
            Debug.DrawLine(p4, p1, color);
        }

        private void DrawCircle(Vector3 center, float radius, int segments, Color color)
        {
            center *= scale;
            radius *= scale;

            float angleStep = 360f / segments;

            for (int i = 0; i < segments; i++)
            {
                float angleCurrent = Mathf.Deg2Rad * (i * angleStep);
                float angleNext = Mathf.Deg2Rad * ((i + 1) * angleStep);

                Vector3 pointA = center + new Vector3(Mathf.Cos(angleCurrent), 0, Mathf.Sin(angleCurrent)) * radius;
                Vector3 pointB = center + new Vector3(Mathf.Cos(angleNext), 0, Mathf.Sin(angleNext)) * radius;

                Debug.DrawLine(pointA, pointB, color);
            }
        }

        private void DrawTriangle(Vector3 center, float size, Color color)
        {
            float height = Mathf.Sqrt(3) / 2 * size; // 等边三角形的高

            // 顶点在 XZ 平面
            Vector3 p1 = center + new Vector3(0, 2f / 3f * height);          // 顶部点
            Vector3 p2 = center + new Vector3(-size / 2f, -1f / 3f * height); // 左下
            Vector3 p3 = center + new Vector3(size / 2f, -1f / 3f * height);  // 右下

            Debug.DrawLine(p1, p2, color);
            Debug.DrawLine(p2, p3, color);
            Debug.DrawLine(p3, p1, color);
        }

        private void DrawRotatedTriangle(Vector3 center, float size, float angleDeg, Color color)
        {
            float height = Mathf.Sqrt(3f) / 2f * size;

            // 初始顶点位置（以Z+为“上”）
            Vector3 p1 = new Vector3(0, 2f / 3f * height);          // 顶部
            Vector3 p2 = new Vector3(-size / 2f, -1f / 3f * height); // 左下
            Vector3 p3 = new Vector3(size / 2f, -1f / 3f * height);  // 右下

            // 旋转角度
            Quaternion rotation = Quaternion.Euler(0f, 0f, angleDeg);

            // 应用旋转 & 平移到中心点
            p1 = center + rotation * p1;
            p2 = center + rotation * p2;
            p3 = center + rotation * p3;

            // 画线
            Debug.DrawLine(p1, p2, color);
            Debug.DrawLine(p2, p3, color);
            Debug.DrawLine(p3, p1, color);
        }

        void DrawDigits(Vector3 origin, string digits, float size, float spacing, Color color)
        {
            origin *= scale;
            size *= scale;
            spacing *= scale;

            float step = size * 0.6f + spacing;
            int count = digits.Length;

            // 起始偏移
            float totalWidth = step * count;
            float offset = -totalWidth / 2f + step / 2f;

            for (int i = 0; i < count; i++)
            {
                char c = digits[i];
                if (char.IsDigit(c))
                {
                    int digit = c - '0';
                    Vector3 pos = origin + new Vector3((i * step) + offset, 0, 0);
                    DrawDigit(pos, digit, size, color);
                }
            }
        }

        private void DrawDigit(Vector3 center, int digit, float size, Color color)
        {
            if (digit < 0 || digit > 9) return;

            float w = size * 0.6f;
            float h = size;

            // 七段坐标（XY平面，如果你想改到XZ平面，改y为z）
            Vector3 A1 = center + new Vector3(-w / 2, 0, h / 2);
            Vector3 A2 = center + new Vector3(w / 2, 0, h / 2);

            Vector3 B1 = center + new Vector3(w / 2, 0, h / 2);
            Vector3 B2 = center + new Vector3(w / 2, 0, 0);

            Vector3 C1 = center + new Vector3(w / 2, 0, 0);
            Vector3 C2 = center + new Vector3(w / 2, 0, -h / 2);

            Vector3 D1 = center + new Vector3(-w / 2, 0, -h / 2);
            Vector3 D2 = center + new Vector3(w / 2, 0, -h / 2);

            Vector3 E1 = center + new Vector3(-w / 2, 0, 0);
            Vector3 E2 = center + new Vector3(-w / 2, 0, -h / 2);

            Vector3 F1 = center + new Vector3(-w / 2, 0, h / 2);
            Vector3 F2 = center + new Vector3(-w / 2, 0, 0);

            Vector3 G1 = center + new Vector3(-w / 2, 0, 0);
            Vector3 G2 = center + new Vector3(w / 2, 0, 0);

            bool[] seg = segments[digit];

            if (seg[0]) Debug.DrawLine(A1, A2, color); // A
            if (seg[1]) Debug.DrawLine(B1, B2, color); // B
            if (seg[2]) Debug.DrawLine(C1, C2, color); // C
            if (seg[3]) Debug.DrawLine(D1, D2, color); // D
            if (seg[4]) Debug.DrawLine(E1, E2, color); // E
            if (seg[5]) Debug.DrawLine(F1, F2, color); // F
            if (seg[6]) Debug.DrawLine(G1, G2, color); // G
        }
    }
}
