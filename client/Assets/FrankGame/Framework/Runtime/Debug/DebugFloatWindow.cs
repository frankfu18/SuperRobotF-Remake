using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrankGame
{
    public class DebugFloatWindow : MonoBehaviour
    {
        /// <summary>
        /// 默认调试器漂浮框大小。
        /// </summary>
        internal static readonly Rect DefaultIconRect = new Rect(300f, 10f, 60f, 60f);

        /// <summary>
        /// 默认调试器窗口大小。
        /// </summary>
        internal static readonly Rect DefaultWindowRect_Landscape = new Rect(10f, 10f, 800f, 400f);

        /// <summary>
        /// 默认调试器窗口大小。
        /// </summary>
        internal static readonly Rect DefaultWindowRect_Portrait = new Rect(10f, 10f, 400f, 800f);

        /// <summary>
        /// 默认调试器窗口缩放比例。
        /// </summary>
        internal static readonly float DefaultWindowScale = 2f;

        private static TextEditor s_TextEditor = null;
        private Rect m_DragRect = new Rect(0f, 0f, float.MaxValue, 35f);
        private Rect m_IconRect = DefaultIconRect;
        private Rect m_WindowRect = DefaultWindowRect_Landscape;
        private float m_WindowScale = DefaultWindowScale;

        [SerializeField]
        private bool m_ActiveWindow = true;

        [SerializeField]
        private GUISkin m_Skin = null;

        [SerializeField]
        private bool m_ShowFullWindow = false;

        [SerializeField]
        private ConsoleWindow m_ConsoleWindow;

        private FpsCounter m_FpsCounter = null;

        /// <summary>
        /// 是否激活窗口
        /// </summary>
        public bool ActiveWindow
        {
            get
            {
                return m_ActiveWindow;
            }
            set
            {
                m_ActiveWindow = value;
            }
        }

        /// <summary>
        /// 获取或设置是否显示完整调试器界面。
        /// </summary>
        public bool ShowFullWindow
        {
            get
            {
                return m_ShowFullWindow;
            }
            set
            {
                m_ShowFullWindow = value;
            }
        }

        /// <summary>
        /// 获取或设置调试器漂浮框大小。
        /// </summary>
        public Rect IconRect
        {
            get
            {
                return m_IconRect;
            }
            set
            {
                m_IconRect = value;
            }
        }

        /// <summary>
        /// 获取或设置调试器窗口大小。
        /// </summary>
        public Rect WindowRect
        {
            get
            {
                return m_WindowRect;
            }
            set
            {
                m_WindowRect = value;
            }
        }

        /// <summary>
        /// 获取或设置调试器窗口缩放比例。
        /// </summary>
        public float WindowScale
        {
            get
            {
                return m_WindowScale;
            }
            set
            {
                m_WindowScale = value;
            }
        }

        /// <summary>
        /// 游戏框架组件初始化。
        /// </summary>
        private void Awake()
        {
            if (Screen.width < Screen.height)
            {
                m_WindowRect = DefaultWindowRect_Portrait;
            }
            else
            {
                m_WindowRect = DefaultWindowRect_Landscape;
            }

            m_FpsCounter = new FpsCounter(0.5f);
        }

        private void Start()
        {
            m_ConsoleWindow = new ConsoleWindow(this);
            m_ConsoleWindow.Initialize();
        }

        private void Update()
        {
            if (!m_ActiveWindow)
            {
                return;
            }

            m_FpsCounter.Update(Time.deltaTime, Time.unscaledDeltaTime);
            m_ConsoleWindow.OnUpdate(Time.deltaTime, Time.unscaledDeltaTime);
        }

        private void OnGUI()
        {
            if (!m_ActiveWindow)
            {
                return;
            }

            GUISkin cachedGuiSkin = GUI.skin;
            Matrix4x4 cachedMatrix = GUI.matrix;

            GUI.skin = m_Skin;
            GUI.matrix = Matrix4x4.Scale(new Vector3(m_WindowScale, m_WindowScale, 1f));

            if (m_ShowFullWindow)
            {
                m_WindowRect = GUILayout.Window(0, m_WindowRect, DrawWindow, "<b>CONSOLE WINDOW</b>");
            }
            else
            {
                m_IconRect = GUILayout.Window(0, m_IconRect, DrawDebuggerWindowIcon, "<b>DEBUGGER</b>");
            }

            GUI.matrix = cachedMatrix;
            GUI.skin = cachedGuiSkin;
        }

        /// <summary>
        /// 还原调试器窗口布局。
        /// </summary>
        public void ResetLayout()
        {
            IconRect = DefaultIconRect;
            WindowRect = DefaultWindowRect_Landscape;
            WindowScale = DefaultWindowScale;
        }

        /// <summary>
        /// 获取记录的所有日志。
        /// </summary>
        /// <param name="results">要获取的日志。</param>
        public void GetRecentLogs(List<LogNode> results)
        {
            m_ConsoleWindow.GetRecentLogs(results);
        }

        /// <summary>
        /// 获取记录的最近日志。
        /// </summary>
        /// <param name="results">要获取的日志。</param>
        /// <param name="count">要获取最近日志的数量。</param>
        public void GetRecentLogs(List<LogNode> results, int count)
        {
            m_ConsoleWindow.GetRecentLogs(results, count);
        }

        private void DrawWindow(int windowId)
        {
            GUI.DragWindow(m_DragRect);
            m_ConsoleWindow.OnDraw();
        }

        private void DrawDebuggerWindowIcon(int windowId)
        {
            if (m_ConsoleWindow == null) return;
            GUI.DragWindow(m_DragRect);
            GUILayout.Space(5);
            Color32 color = Color.white;
            m_ConsoleWindow.RefreshCount();
            if (m_ConsoleWindow.FatalCount > 0)
            {
                color = m_ConsoleWindow.GetLogStringColor(LogType.Exception);
            }
            else if (m_ConsoleWindow.ErrorCount > 0)
            {
                color = m_ConsoleWindow.GetLogStringColor(LogType.Error);
            }
            else if (m_ConsoleWindow.WarningCount > 0)
            {
                color = m_ConsoleWindow.GetLogStringColor(LogType.Warning);
            }
            else
            {
                color = m_ConsoleWindow.GetLogStringColor(LogType.Log);
            }

            string title = string.Format("<color=#{0:x2}{1:x2}{2:x2}{3:x2}><b>FPS: {4:F2}</b></color>", color.r, color.g, color.b, color.a, m_FpsCounter.CurrentFps);
            if (GUILayout.Button(title, GUILayout.Width(100f), GUILayout.Height(40f)))
            {
                m_ShowFullWindow = true;
            }
        }

        private static void CopyToClipboard(string content)
        {
            if (s_TextEditor == null)
            {
                s_TextEditor = new TextEditor();
            }

            s_TextEditor.text = content;
            s_TextEditor.OnFocus();
            s_TextEditor.Copy();
            s_TextEditor.text = string.Empty;
        }

        private void OnDestroy()
        {
            m_ConsoleWindow.Shutdown();
        }
    }
}