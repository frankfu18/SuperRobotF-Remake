using System;
using System.Collections.Generic;
using UnityEngine;

public static class GridBorderBuilder
{
    struct Edge : IEquatable<Edge>
    {
        public Vector2Int a, b;
        public Edge(Vector2Int p1, Vector2Int p2)
        {
            // 规范化顺序，保证无向等价
            if (p1.x < p2.x || (p1.x == p2.x && p1.y <= p2.y))
            {
                a = p1; b = p2;
            }
            else
            {
                a = p2; b = p1;
            }
        }
        public bool Equals(Edge other)
        {
            return a == other.a && b == other.b;
        }
        public override int GetHashCode()
        {
            return (a.x * 73856093) ^ (a.y * 19349663) ^ (b.x * 83492791) ^ (b.y * 15485863);
        }
    }

    // cells: 一组格子坐标（格子坐标以整数为单位，例如格子左下为 (x,y)）
    // sizeX,sizeY: 每格世界尺寸
    // origin: 网格(0,0)对应的世界坐标（左下角）
    // 返回：若干条边界环，每条为按顺序的世界坐标顶点
    public static List<List<Vector2>> BuildBorders(IEnumerable<Vector2Int> cells, float sizeX, float sizeY, Vector2 origin)
    {
        var edgeCount = new Dictionary<Edge, int>();
        var cellSet = new HashSet<Vector2Int>(cells);

        foreach (var c in cellSet)
        {
            // 方格四个角，按格子单元坐标（左下为 (x,y)）
            Vector2Int v0 = new Vector2Int(c.x, c.y);
            Vector2Int v1 = new Vector2Int(c.x + 1, c.y);
            Vector2Int v2 = new Vector2Int(c.x + 1, c.y + 1);
            Vector2Int v3 = new Vector2Int(c.x, c.y + 1);

            var edges = new Edge[] {
                new Edge(v0, v1),
                new Edge(v1, v2),
                new Edge(v2, v3),
                new Edge(v3, v0)
            };

            foreach (var e in edges)
            {
                if (edgeCount.TryGetValue(e, out int cnt)) edgeCount[e] = cnt + 1;
                else edgeCount[e] = 1;
            }
        }

        // 只保留边界边（计数==1）
        var borderEdges = new List<Edge>();
        foreach (var kv in edgeCount)
        {
            if (kv.Value == 1) borderEdges.Add(kv.Key);
        }

        // 构建顶点邻接表（有向用于遍历，但边本质上无向）
        var adj = new Dictionary<Vector2Int, List<Vector2Int>>();
        foreach (var e in borderEdges)
        {
            if (!adj.ContainsKey(e.a)) adj[e.a] = new List<Vector2Int>();
            if (!adj.ContainsKey(e.b)) adj[e.b] = new List<Vector2Int>();
            adj[e.a].Add(e.b);
            adj[e.b].Add(e.a);
        }

        var visited = new HashSet<Edge>();
        var loops = new List<List<Vector2>>();

        // 遍历边构成闭环
        foreach (var startEdge in borderEdges)
        {
            if (visited.Contains(startEdge)) continue;

            // 从 startEdge.a -> startEdge.b 开始走
            var loopInts = new List<Vector2Int>();
            Vector2Int start = startEdge.a;
            Vector2Int prev = startEdge.a;
            Vector2Int curr = startEdge.b;

            loopInts.Add(start);
            loopInts.Add(curr);
            visited.Add(new Edge(prev, curr));

            // 继续直到回到 start
            int safety = 0;
            while (curr != start && safety++ < 10000)
            {
                // 在 curr 的邻居中选一个不是 prev 的作为下一个
                var neigh = adj[curr];
                Vector2Int next = default;
                bool found = false;
                foreach (var n in neigh)
                {
                    if (n != prev)
                    {
                        next = n;
                        found = true;
                        break;
                    }
                }
                if (!found) break; // 无法继续（异常情况）
                // 标记边已访问
                visited.Add(new Edge(curr, next));
                // 推进
                prev = curr;
                curr = next;
                if (curr != start) loopInts.Add(curr);
            }

            // 转换为世界坐标并加入结果（注意网格单位->世界单位转换）
            if (loopInts.Count >= 3)
            {
                var loopWorld = new List<Vector2>();
                foreach (var p in loopInts)
                {
                    var w = new Vector2(origin.x + p.x * sizeX, origin.y + p.y * sizeY);
                    loopWorld.Add(w);
                }
                loops.Add(loopWorld);
            }
        }

        return loops;
    }
}