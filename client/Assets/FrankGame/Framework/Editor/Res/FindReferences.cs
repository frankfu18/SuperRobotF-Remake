using UnityEngine;
using System.Collections;
using UnityEditor;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace FrankGame
{
    public class FindReferences
    {
        private static List<string> withoutExtensions = new List<string>() { ".prefab", ".unity", ".mat", ".asset" };
        private static string[] files = Directory.GetFiles(Application.dataPath, "*.*", SearchOption.AllDirectories)
            .Where(s => withoutExtensions.Contains(Path.GetExtension(s).ToLower())).ToArray();

        private static List<Func<bool>> updateFuncList = new List<Func<bool>>();
        private static List<Func<bool>> removeList = new List<Func<bool>>();

        [MenuItem("Assets/Find References", false, 10)]
        static private void Find()
        {
            updateFuncList.Clear();
            removeList.Clear();

            EditorSettings.serializationMode = SerializationMode.ForceText;
            string path = AssetDatabase.GetAssetPath(Selection.activeObject);
            if (!string.IsNullOrEmpty(path))
            {
                CheckPath(path);

                EditorApplication.update = delegate ()
                {
                    while (updateFuncList.Count > 0)
                    {
                        if (!updateFuncList[0].Invoke())
                        {
                            updateFuncList.RemoveAt(0);
                        }
                    }
                };
            }
        }

        private static void CheckPath(string path)
        {
            if (path.Contains("."))//说明是文件
            {
                FindRef(path);
            }
            else//文件夹，找下面所有的
            {
                string dirPath = FileUtils.AssetPathToFilePath(path);

                DirectoryInfo dirInfo = new DirectoryInfo(dirPath);

                FileInfo[] files = dirInfo.GetFiles();

                foreach (var file in files)
                {
                    FindRef(FileUtils.FilePathToAssetPath(file.FullName));
                }

                DirectoryInfo[] dirs = dirInfo.GetDirectories();
                foreach (var dir in dirs)
                {
                    CheckPath(FileUtils.FilePathToAssetPath(dir.FullName));
                }
            }
        }

        private static void FindRef(string path)
        {
            if (path.Contains(".meta"))
            {
                return;
            }

            string guid = AssetDatabase.AssetPathToGUID(path);

            int startIndex = 0;
            int refCount = 0;

            Func<bool> updateFunc = delegate ()
            {
                if (startIndex == 0)
                {
                    Debug.Log("匹配开始 ：" + path);
                }

                string file = files[startIndex];

                bool isCancel = EditorUtility.DisplayCancelableProgressBar("匹配资源中", file, (float)startIndex / (float)files.Length);

                if (Regex.IsMatch(File.ReadAllText(file), guid))
                {
                    refCount++;

                    Debug.Log(file, AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(GetRelativeAssetsPath(file)));
                }

                startIndex++;
                if (isCancel || startIndex >= files.Length)
                {
                    EditorUtility.ClearProgressBar();
                    EditorApplication.update = null;
                    startIndex = 0;
                    Debug.Log("匹配结束，引用数 ：" + refCount);

                    return false;
                }

                return true;
            };

            updateFuncList.Add(updateFunc);
        }

        //[MenuItem("Assets/Find References", true)]
        //static private bool VFind()
        //{
        //    string path = AssetDatabase.GetAssetPath(Selection.activeObject);
        //    return (!string.IsNullOrEmpty(path));
        //}

        static private string GetRelativeAssetsPath(string path)
        {
            return "Assets" + Path.GetFullPath(path).Replace(Path.GetFullPath(Application.dataPath), "").Replace('\\', '/');
        }
    }
}