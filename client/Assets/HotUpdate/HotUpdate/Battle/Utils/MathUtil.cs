using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrankGame.Battle
{
    public static class MathUtil
    {
        public static float DistanceSqr(Vector3 pos1, Vector3 pos2)
        {
            return (pos1 - pos2).sqrMagnitude;
        }

        public static float Distance(Vector2 pos1, Vector2 pos2)
        {
            return Vector2.Distance(pos1, pos2);
        }

        public static float Distance(Vector3 pos1, Vector3 pos2)
        {
            return Vector3.Distance(pos1, pos2);
        }

        public static float Distance(Entity entity, Vector2 pos)
        {
            TransformComponent tranCom;
            if (!entity.TryGetComponent(ECompType.Transform, out tranCom))
            {
                return 0;
            }

            return Distance(tranCom.position, pos);
        }

        public static float Distance(Entity entity1, Entity entity2)
        {
            TransformComponent tranCom1, tranCom2;
            if (!entity1.TryGetComponent(ECompType.Transform, out tranCom1) || !entity2.TryGetComponent(ECompType.Transform, out tranCom2))
            {
                return 0;
            }

            return Distance(tranCom1.position, tranCom2.position);
        }

        public static Vector3 CalculateDir(Vector3 from, Vector3 to)
        {
            return (to - from).normalized;
        }

        public static Vector3 CalculateDir(Entity fromEntity, Vector3 to)
        {
            TransformComponent tranCom;
            if (!fromEntity.TryGetComponent(ECompType.Transform, out tranCom))
            {
                return Vector3.zero;
            }

            return CalculateDir(tranCom.position, to);
        }

        public static Vector3 CalculateDir(Entity fromEntity, Entity toEntity)
        {
            TransformComponent fromTranCom, toTranCom;
            if (!fromEntity.TryGetComponent(ECompType.Transform, out fromTranCom) || !toEntity.TryGetComponent(ECompType.Transform, out toTranCom))
            {
                return Vector3.zero;
            }

            return CalculateDir(fromTranCom.position, toTranCom.position);
        }

        //线段与圆是否相交
        public static bool SegmentOverlapsCircle(Vector2 a, Vector2 b, Vector2 center, float radius)
        {
            Vector2 ab = b - a;
            float abLenSq = ab.sqrMagnitude;
            if (abLenSq == 0f) 
                return (center - a).sqrMagnitude <= radius * radius;

            // 投影到线段的参数 t∈[0,1]
            float t = Vector2.Dot(center - a, ab) / abLenSq;
            t = Mathf.Clamp01(t);

            Vector2 closest = a + t * ab;

            return (center - closest).sqrMagnitude <= radius * radius;
        }

        //线段和包围盒相交
        public static bool IntersectSegment(this Rect rect, Vector2 p0, Vector2 p1)
        {
            Vector2 dir = p1 - p0;

            // 方向分量倒数
            float invDx = Mathf.Abs(dir.x) < 1e-8f ? float.PositiveInfinity : 1f / dir.x;
            float invDy = Mathf.Abs(dir.y) < 1e-8f ? float.PositiveInfinity : 1f / dir.y;

            // X方向
            float t1 = (rect.xMin - p0.x) * invDx;
            float t2 = (rect.xMax - p0.x) * invDx;

            float tmin = Mathf.Min(t1, t2);
            float tmax = Mathf.Max(t1, t2);

            // Y方向
            float t3 = (rect.yMin - p0.y) * invDy;
            float t4 = (rect.yMax - p0.y) * invDy;

            tmin = Mathf.Max(tmin, Mathf.Min(t3, t4));
            tmax = Mathf.Min(tmax, Mathf.Max(t3, t4));

            // 线段有效区间：0 <= t <= 1
            if (tmax >= Mathf.Max(0f, tmin) && tmin <= 1f && tmax >= 0f)
            {
                float tHit = Mathf.Clamp01(tmin < 0f ? tmax : tmin);
                return true;
            }

            return false;
        }

        // 判断两个圆是否相交（含相切）
        public static bool CirclesIntersect(Vector2 c1, float r1, Vector2 c2, float r2)
        {
            float dx = c1.x - c2.x;
            float dy = c1.y - c2.y;
            float distSq = dx * dx + dy * dy;
            float rSum = r1 + r2;
            float rDiff = Mathf.Abs(r1 - r2);

            if (distSq > rSum * rSum)
                return false;  // 相离
            else if (distSq <= rDiff * rDiff)
                return true;   // 包含
            else
                return true; // 相交
        }

        /// <summary>
        /// 判断圆和矩形是否相交
        /// </summary>
        public static bool IntersectCircle(this Rect rect, Vector2 center, float radius)
        {
            float closestX = Mathf.Clamp(center.x, rect.xMin, rect.xMax);
            float closestY = Mathf.Clamp(center.y, rect.yMin, rect.yMax);

            float dx = center.x - closestX;
            float dy = center.y - closestY;

            return (dx * dx + dy * dy) <= radius * radius;
        }

        //点在圆内（包括相切）
        public static bool PointInCircle(Vector2 point, Vector2 center, float radius)
        {
            float sqrDist = (point - center).sqrMagnitude;

            return sqrDist <= radius * radius;
        }

        // 判断点是否在多边形内（射线法）
        public static bool PointInPolygon(Vector2 p, List<Vector2> poly)
        {
            bool inside = false;
            for (int i = 0, j = poly.Count - 1; i < poly.Count; j = i++)
            {
                Vector2 pi = poly[i];
                Vector2 pj = poly[j];

                bool intersect = ((pi.y > p.y) != (pj.y > p.y)) &&
                                 (p.x < (pj.x - pi.x) * (p.y - pi.y) / (pj.y - pi.y + 1e-12f) + pi.x);

                if (intersect) inside = !inside;
            }
            return inside;
        }

        // 计算点到线段的最短距离
        public static float DistancePointToSegment(Vector2 p, Vector2 a, Vector2 b)
        {
            Vector2 ab = b - a;
            float t = Vector2.Dot(p - a, ab) / ab.sqrMagnitude;
            t = Mathf.Clamp01(t);
            Vector2 proj = a + t * ab;
            return Vector2.Distance(p, proj);
        }
    }
}
