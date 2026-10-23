using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Linq;

namespace FrankGame
{
    public class TextureScannerWindow : EditorWindow
    {
        private string _searchPath = "Assets";

        // 动态配置参数
        private int _minSizeLimit = 500;
        private int _multipleOf = 4; // 虽然你提到了4，我也把它做成可配置的，更灵活

        private List<TextureData> _invalidTextures = new List<TextureData>();
        private Vector2 _scrollPos;

        // 排序相关
        private enum SortType
        {
            Path,
            Resolution,
            DiskSize,
            MemorySize
        }

        private SortType _currentSort = SortType.Path;
        private bool _sortAscending = true;

        private struct TextureData
        {
            public string Path;
            public int Width;
            public int Height;
            public long DiskSizeBytes;
            public long MemSizeBytes;
            public string DiskSizeStr;
            public string MemorySizeStr;
            public int ResolutionArea => Width * Height;
        }

        [MenuItem("FrankGame/Tools/图片规范检查工具 Pro")]
        public static void ShowWindow()
        {
            GetWindow<TextureScannerWindow>("图片扫描器").minSize = new Vector2(650, 600);
        }

        private void OnGUI()
        {
            GUILayout.Space(10);

            // --- 参数设置区 ---
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            GUILayout.Label("扫描配置", EditorStyles.boldLabel);

            // 路径选择
            EditorGUILayout.BeginHorizontal();
            _searchPath = EditorGUILayout.TextField("扫描目录", _searchPath);
            if (GUILayout.Button("选择", GUILayout.Width(50)))
            {
                string path = EditorUtility.OpenFolderPanel("选择扫描目录", _searchPath, "");
                if (!string.IsNullOrEmpty(path))
                {
                    if (path.Contains(Application.dataPath))
                        _searchPath = "Assets" + path.Replace(Application.dataPath, "");
                }
            }

            EditorGUILayout.EndHorizontal();

            // 尺寸阈值输入
            EditorGUILayout.BeginHorizontal();
            _minSizeLimit = EditorGUILayout.IntField("尺寸阈值 (宽或高 >)", _minSizeLimit);
            _multipleOf = EditorGUILayout.IntField("倍数检查 (非 X 倍数)", _multipleOf);
            EditorGUILayout.EndHorizontal();

            // 简单校验
            if (_minSizeLimit < 0) _minSizeLimit = 0;
            if (_multipleOf < 1) _multipleOf = 1;

            EditorGUILayout.EndVertical();

            // --- 操作区 ---
            if (GUILayout.Button($"开始扫描 (尺寸 > {_minSizeLimit} 或 非 {_multipleOf} 倍数)", GUILayout.Height(35)))
            {
                ScanTextures();
            }

            EditorGUILayout.Space(5);
            DrawHeader();

            // --- 结果列表 ---
            _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);
            if (_invalidTextures != null && _invalidTextures.Count > 0)
            {
                foreach (var data in _invalidTextures)
                {
                    EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);
                    if (GUILayout.Button(data.Path, EditorStyles.label, GUILayout.ExpandWidth(true)))
                    {
                        var obj = AssetDatabase.LoadMainAssetAtPath(data.Path);
                        EditorGUIUtility.PingObject(obj);
                        Selection.activeObject = obj;
                    }

                    GUILayout.Label($"{data.Width}x{data.Height}", GUILayout.Width(80));
                    GUILayout.Label(data.DiskSizeStr, GUILayout.Width(80));
                    GUILayout.Label(data.MemorySizeStr, GUILayout.Width(80));
                    EditorGUILayout.EndHorizontal();
                }
            }
            else
            {
                GUILayout.Space(20);
                GUILayout.Label("未发现违规图片", EditorStyles.centeredGreyMiniLabel);
            }

            EditorGUILayout.EndScrollView();
        }

        private void DrawHeader()
        {
            EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);
            if (DrawHeaderButton("资源路径", SortType.Path, -1)) ApplySort(SortType.Path);
            if (DrawHeaderButton("分辨率", SortType.Resolution, 80)) ApplySort(SortType.Resolution);
            if (DrawHeaderButton("磁盘大小", SortType.DiskSize, 80)) ApplySort(SortType.DiskSize);
            if (DrawHeaderButton("内存估算", SortType.MemorySize, 80)) ApplySort(SortType.MemorySize);
            EditorGUILayout.EndHorizontal();
        }

        private bool DrawHeaderButton(string label, SortType type, float width)
        {
            string arrow = (_currentSort == type) ? (_sortAscending ? " ▲" : " ▼") : "";
            GUIStyle style = new GUIStyle(EditorStyles.toolbarButton) { fontStyle = FontStyle.Bold };
            return width > 0
                ? GUILayout.Button(label + arrow, style, GUILayout.Width(width))
                : GUILayout.Button(label + arrow, style, GUILayout.ExpandWidth(true));
        }

        private void ScanTextures()
        {
            _invalidTextures.Clear();
            string[] guids = AssetDatabase.FindAssets("t:Texture", new[] { _searchPath });

            MethodInfo getStorageMemorySizeMethod = typeof(TextureUtil).GetMethod("GetStorageMemorySizeLong",
                BindingFlags.Static | BindingFlags.Public);

            for (int i = 0; i < guids.Length; i++)
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                EditorUtility.DisplayProgressBar("正在扫描资产", path, (float)i / guids.Length);

                // 高级工程师Tips: 获取Importer而不加载Texture2D有时更快，但为了准确的内存估算，我们还是Load一下
                Texture2D tex = AssetDatabase.LoadAssetAtPath<Texture2D>(path);

                if (tex != null)
                {
                    int w = tex.width;
                    int h = tex.height;

                    // 使用用户定义的阈值进行判断
                    bool isTooLarge = (w > _minSizeLimit || h > _minSizeLimit);
                    bool isInvalidMultiple = (w % _multipleOf != 0 || h % _multipleOf != 0);

                    if (isTooLarge && isInvalidMultiple)
                    {
                        FileInfo fi = new FileInfo(Path.Combine(System.Environment.CurrentDirectory, path));
                        long diskBytes = fi.Exists ? fi.Length : 0;
                        long memBytes = (long)getStorageMemorySizeMethod.Invoke(null, new object[] { tex });

                        _invalidTextures.Add(new TextureData
                        {
                            Path = path,
                            Width = w,
                            Height = h,
                            DiskSizeBytes = diskBytes,
                            MemSizeBytes = memBytes,
                            DiskSizeStr = FormatBytes(diskBytes),
                            MemorySizeStr = FormatBytes(memBytes)
                        });
                    }
                }

                if (i % 30 == 0)
                {
                    Resources.UnloadUnusedAssets();
                    System.GC.Collect();
                }
            }

            ApplySort(_currentSort);
            EditorUtility.ClearProgressBar();
        }

        private void ApplySort(SortType type)
        {
            if (_currentSort == type) _sortAscending = !_sortAscending;
            else
            {
                _currentSort = type;
                _sortAscending = false;
            }

            switch (_currentSort)
            {
                case SortType.Path:
                    _invalidTextures = _sortAscending
                        ? _invalidTextures.OrderBy(d => d.Path).ToList()
                        : _invalidTextures.OrderByDescending(d => d.Path).ToList();
                    break;
                case SortType.Resolution:
                    _invalidTextures = _sortAscending
                        ? _invalidTextures.OrderBy(d => d.ResolutionArea).ToList()
                        : _invalidTextures.OrderByDescending(d => d.ResolutionArea).ToList();
                    break;
                case SortType.DiskSize:
                    _invalidTextures = _sortAscending
                        ? _invalidTextures.OrderBy(d => d.DiskSizeBytes).ToList()
                        : _invalidTextures.OrderByDescending(d => d.DiskSizeBytes).ToList();
                    break;
                case SortType.MemorySize:
                    _invalidTextures = _sortAscending
                        ? _invalidTextures.OrderBy(d => d.MemSizeBytes).ToList()
                        : _invalidTextures.OrderByDescending(d => d.MemSizeBytes).ToList();
                    break;
            }
        }

        private string FormatBytes(long bytes)
        {
            if (bytes < 1024) return bytes + " B";
            if (bytes < 1024 * 1024) return (bytes / 1024f).ToString("F2") + " KB";
            return (bytes / (1024f * 1024f)).ToString("F2") + " MB";
        }
    }

    public static class TextureUtil
    {
        private static System.Type type;
        static TextureUtil() => type = System.Type.GetType("UnityEditor.TextureUtil, UnityEditor");

        public static long GetStorageMemorySizeLong(Texture t)
        {
            MethodInfo method = type.GetMethod("GetStorageMemorySizeLong", BindingFlags.Static | BindingFlags.Public);
            return (long)method.Invoke(null, new object[] { t });
        }
    }
}