using System.Diagnostics;
using System.IO;
using UnityEngine;
using UnityEditor;

namespace FrankGame
{
    public class PerfectEditor
    {
        [MenuItem("FrankGame/生成表格配置")]
        public static void CreateConfig()
        {
            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                Application.OpenURL(Application.dataPath.Replace("Assets", "/") + "gen_config.bat");
            }
            else if (Application.platform == RuntimePlatform.OSXEditor)
            {
                var pwd = Application.dataPath.Replace("Assets", "");
                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = "bash";
                psi.WorkingDirectory = pwd;
                psi.Arguments = "gen_config.sh";
                psi.WindowStyle = ProcessWindowStyle.Minimized;
                psi.CreateNoWindow = true;
                psi.UseShellExecute = false;
                var process = Process.Start(psi);
                process.WaitForExit();
                process.Close();
            }
        }

        [MenuItem("FrankGame/生成网络协议")]
        public static void CreateMessage()
        {
            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                Application.OpenURL(Application.dataPath.Replace("Assets", "/") + "gen_msg.bat");
            }
            else if (Application.platform == RuntimePlatform.OSXEditor)
            {
                var pwd = Application.dataPath.Replace("Assets", "");
                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = "bash";
                psi.WorkingDirectory = pwd;
                psi.Arguments = "gen_msg.sh";
                psi.WindowStyle = ProcessWindowStyle.Minimized;
                psi.CreateNoWindow = true;
                psi.UseShellExecute = false;
                var process = Process.Start(psi);
                process.WaitForExit();
                process.Close();
            }
        }

        [MenuItem("FrankGame/生成所有")]
        public static void CreateConfigAndMessage()
        {
            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                Application.OpenURL(Application.dataPath.Replace("Assets", "/") + "gen_all.bat");
            }
            else if (Application.platform == RuntimePlatform.OSXEditor)
            {
                var pwd = Application.dataPath.Replace("Assets", "");
                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = "bash";
                psi.WorkingDirectory = pwd;
                psi.Arguments = "gen_all.sh";
                psi.WindowStyle = ProcessWindowStyle.Minimized;
                psi.CreateNoWindow = true;
                psi.UseShellExecute = false;
                var process = Process.Start(psi);
                process.WaitForExit();
                process.Close();
            }
        }
    }
}