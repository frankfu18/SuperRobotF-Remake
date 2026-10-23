using System.Collections.Generic;
using UnityEngine;

namespace FrankGame.Battle
{
    public class CircleQuadtree<T> : IClear
    {
        private const int MAX_OBJECTS = 4;
        private const int MAX_LEVELS = 5;

        private int level;
        private List<CircleObject> objects;
        private Rect bounds;
        private CircleQuadtree<T>[] nodes = new CircleQuadtree<T>[4];

        public class CircleObject : IClear
        {
            public Vector2 center;
            public float radius;
            public T data;
            public Rect aabb;

            public CircleObject()
            {

            }

            public CircleObject(Vector2 center, float radius, T data)
            {
                SetData(center, radius, data);
            }

            public CircleObject SetData(Vector2 center, float radius, T data)
            {
                this.center = center;
                this.radius = radius;
                this.data = data;
                this.aabb = new Rect(center.x - radius, center.y - radius, radius * 2, radius * 2);

                return this;
            }

            //圆相交
            public bool Intersect(Vector2 c, float r)
            {
                return MathUtil.CirclesIntersect(this.center, this.radius, c, r);
            }

            //线段相交
            public bool Intersect(Vector2 p0, Vector2 p1)
            {
                return MathUtil.SegmentOverlapsCircle(p0, p1, this.center, this.radius);
            }

            //包围盒相交
            public bool Intersect(Rect rect)
            {
                return rect.IntersectCircle(this.center, this.radius);
            }

            //包含点
            public bool Contains(Vector2 point)
            {
                return MathUtil.PointInCircle(point, this.center, this.radius);
            }

            public void DrawCircle()
            {
                int segments = 64;
                float angleStep = 360f / segments;

                for (int i = 0; i < segments; i++)
                {
                    float angleCurrent = Mathf.Deg2Rad * (i * angleStep);
                    float angleNext = Mathf.Deg2Rad * ((i + 1) * angleStep);

                    Vector2 pointA = center + new Vector2(Mathf.Cos(angleCurrent), Mathf.Sin(angleCurrent)) * radius;
                    Vector2 pointB = center + new Vector2(Mathf.Cos(angleNext), Mathf.Sin(angleNext)) * radius;

                    Debug.DrawLine(pointA, pointB, Color.cyan);
                }
            }

            public void Clear()
            {

            }
        }

        public CircleQuadtree()
        {

        }

        public CircleQuadtree(int level, Rect bounds)
        {
            SetData(level, bounds);
        }

        public CircleQuadtree(int level, Vector2 center, float width, float height)
        {
            SetData(level, new Rect(center.x - width / 2, center.y - height / 2, width, height));
        }

        public CircleQuadtree<T> SetData(int level, Rect bounds)
        {
            this.level = level;
            this.bounds = bounds;
            this.objects = ListObjectPool<CircleObject>.Instance.Get();

            return this;
        }

        public void ClearObjects()
        {
            int objCount = objects.Count;
            for (int i = 0; i < objCount; i++)
            {
                RefObjectPool<CircleObject>.Instance.Recycle(objects[i]);
            }
            objects.Clear();

            for (int i = 0; i < 4; i++)
            {
                if (nodes[i] != null)
                {
                    RefObjectPool<CircleQuadtree<T>>.Instance.Recycle(nodes[i]);

                    nodes[i] = null;
                }
            }
        }

        public void Clear()
        {
            int objCount = objects.Count;
            for (int i = 0; i < objCount; i++)
            {
                RefObjectPool<CircleObject>.Instance.Recycle(objects[i]);
            }

            ListObjectPool<CircleObject>.Instance.Recycle(objects);
            objects = null;

            for (int i = 0; i < 4; i++)
            {
                if (nodes[i] != null)
                {
                    RefObjectPool<CircleQuadtree<T>>.Instance.Recycle(nodes[i]);

                    nodes[i] = null;
                }
            }
        }

        private void Split()
        {
            float halfW = bounds.width / 2f;
            float halfH = bounds.height / 2f;
            float x = bounds.x;
            float y = bounds.y;

            nodes[0] = RefObjectPool<CircleQuadtree<T>>.Instance.Get().SetData(level + 1, new Rect(x + halfW, y, halfW, halfH));         // top-right
            nodes[1] = RefObjectPool<CircleQuadtree<T>>.Instance.Get().SetData(level + 1, new Rect(x, y, halfW, halfH));                 // top-left
            nodes[2] = RefObjectPool<CircleQuadtree<T>>.Instance.Get().SetData(level + 1, new Rect(x, y + halfH, halfW, halfH));         // bottom-left
            nodes[3] = RefObjectPool<CircleQuadtree<T>>.Instance.Get().SetData(level + 1, new Rect(x + halfW, y + halfH, halfW, halfH)); // bottom-right
        }

        private int GetIndex(Rect aabb)
        {
            float midX = bounds.x + bounds.width / 2f;
            float midY = bounds.y + bounds.height / 2f;

            bool top = aabb.yMax < midY;
            bool bottom = aabb.yMin >= midY;
            bool left = aabb.xMax < midX;
            bool right = aabb.xMin >= midX;

            if (top)
            {
                if (right) return 0; // top-right
                if (left) return 1;  // top-left
            }
            else if (bottom)
            {
                if (left) return 2;  // bottom-left
                if (right) return 3; // bottom-right
            }

            return -1; // 不能完全放入子节点
        }

        public bool Insert(Vector2 center, float radius, T data)
        {
            var aabb = new Rect(center.x - radius, center.y - radius, radius * 2, radius * 2);
            if (!bounds.Overlaps(aabb))
                return false;

            if (nodes[0] != null)
            {
                int index = GetIndex(aabb);
                if (index != -1)
                {
                    return nodes[index].Insert(center, radius, data);
                }
            }

            objects.Add(RefObjectPool<CircleObject>.Instance.Get().SetData(center, radius, data));

            if (objects.Count > MAX_OBJECTS && level < MAX_LEVELS)
            {
                if (nodes[0] == null)
                    Split();

                int i = 0;
                while (i < objects.Count)
                {
                    var obj = objects[i];
                    int index = GetIndex(obj.aabb);
                    if (index != -1)
                    {
                        // 回收到对象池前先复制数据；回收会清空或复用对象，不能继续读取 obj。
                        Vector2 moveCenter = obj.center;
                        float moveRadius = obj.radius;
                        T moveData = obj.data;
                        objects.RemoveAtNoCost(i);
                        RefObjectPool<CircleObject>.Instance.Recycle(obj);
                        nodes[index].Insert(moveCenter, moveRadius, moveData);
                    }
                    else
                    {
                        i++;
                    }
                }
            }

            return true;
        }

        //判断圆碰撞
        public List<T> Query(Vector2 center, float radius)
        {
            if(radius <= 0)
            {
                return Query(center);
            }

            List<T> results = new List<T>();

            if (!bounds.IntersectCircle(center, radius))
                return results;

            foreach (var obj in objects)
            {
                if (obj.Intersect(center, radius))
                {
                    results.Add(obj.data);
                }
            }

            if (nodes[0] != null)
            {
                foreach (var node in nodes)
                {
                    results.AddRange(node.Query(center, radius));
                }
            }

            return results;
        }

        public bool Query(Vector2 center, float radius, ref List<T> results)
        {
            if (radius <= 0)
            {
                return Query(center, ref results);
            }

            if (!bounds.IntersectCircle(center, radius))
                return false;

            foreach (var obj in objects)
            {
                if (obj.Intersect(center, radius))
                {
                    results.Add(obj.data);
                }
            }

            if (nodes[0] != null)
            {
                foreach (var node in nodes)
                {
                    node.Query(center, radius, ref results);
                }
            }

            return results.Count > 0;
        }

        //判断矩形包围盒碰撞
        public List<T> Query(Rect range)
        {
            List<T> results = new List<T>();

            if (!bounds.Overlaps(range))
                return results;

            foreach (var obj in objects)
            {
                if (range.IntersectCircle(obj.center, obj.radius))
                {
                    results.Add(obj.data);
                }
            }

            if (nodes[0] != null)
            {
                foreach (var node in nodes)
                {
                    results.AddRange(node.Query(range));
                }
            }

            return results;
        }

        public bool Query(Rect range, ref List<T> results)
        {
            if (!bounds.Overlaps(range))
                return false;

            foreach (var obj in objects)
            {
                if (range.IntersectCircle(obj.center, obj.radius))
                {
                    results.Add(obj.data);
                }
            }

            if (nodes[0] != null)
            {
                foreach (var node in nodes)
                {
                    node.Query(range, ref results);
                }
            }

            return results.Count > 0;
        }

        //判断点碰撞
        public List<T> Query(Vector2 point)
        {
            List<T> results = new List<T>();

            if (!bounds.Contains(point))
                return results;

            foreach (var obj in objects)
            {
                if (obj.Contains(point))
                {
                    results.Add(obj.data);
                }
            }

            if (nodes[0] != null)
            {
                foreach (var node in nodes)
                {
                    results.AddRange(node.Query(point));
                }
            }

            return results;
        }

        public bool Query(Vector2 point, ref List<T> results)
        {
            if (!bounds.Contains(point))
                return false;

            foreach (var obj in objects)
            {
                if (obj.Contains(point))
                {
                    results.Add(obj.data);
                }
            }

            if (nodes[0] != null)
            {
                foreach (var node in nodes)
                {
                    node.Query(point, ref results);
                }
            }

            return results.Count > 0;
        }

        //判断线段碰撞
        public List<T> Query(Vector2 p0, Vector2 p1)
        {
            List<T> results = new List<T>();

            if (!bounds.IntersectSegment(p0, p1))
                return results;

            foreach (var obj in objects)
            {
                if (obj.Intersect(p0, p1))
                {
                    results.Add(obj.data);
                }
            }

            if (nodes[0] != null)
            {
                foreach (var node in nodes)
                {
                    results.AddRange(node.Query(p0, p1));
                }
            }

            return results;
        }

        public bool Query(Vector2 p0, Vector2 p1, ref List<T> results)
        {
            if (!bounds.IntersectSegment(p0, p1))
                return false;

            foreach (var obj in objects)
            {
                if (obj.Intersect(p0, p1))
                {
                    results.Add(obj.data);
                }
            }

            if (nodes[0] != null)
            {
                foreach (var node in nodes)
                {
                    node.Query(p0, p1, ref results);
                }
            }

            return results.Count > 0;
        }

        public void DrawDebug()
        {
            Vector3 topLeft = new Vector3(bounds.min.x, bounds.max.y, 0);
            Vector3 topRight = new Vector3(bounds.max.x, bounds.max.y, 0);
            Vector3 bottomLeft = new Vector3(bounds.min.x, bounds.min.y, 0);
            Vector3 bottomRight = new Vector3(bounds.max.x, bounds.min.y, 0);

            Debug.DrawLine(topLeft, topRight, Color.yellow);
            Debug.DrawLine(topRight, bottomRight, Color.yellow);
            Debug.DrawLine(bottomRight, bottomLeft, Color.yellow);
            Debug.DrawLine(bottomLeft, topLeft, Color.yellow);

            if (nodes[0] != null)
            {
                for (int i = 0; i < nodes.Length; i++)
                {
                    var node = nodes[i];
                    node.DrawDebug();
                    
                    for (int j = 0; j < node.objects.Count; j++)
                    {
                        node.objects[j].DrawCircle();
                    }
                }
            }
        }
    }
}
