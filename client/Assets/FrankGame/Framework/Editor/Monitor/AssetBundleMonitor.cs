using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace FrankGame
{
    public class AssetBundleMonitor : EditorWindow
    {
        private Vector2 bundleScrollPos;
        private Vector2 detailScrollPos;
        private string searchFlag;
        private string curKey;
        private BundleInfo selectBundleInfo;

        [MenuItem("FrankGame/Monitor/AssetBundle")]
        public static void Open()
        {
            var window = (AssetBundleMonitor)EditorWindow.GetWindow(typeof(AssetBundleMonitor), false, "资源监视器");
            window.Show();
        }

        private void OnGUI()
        {
            if (!Application.isPlaying || GameConfig.Package.ResLoadMode == ELoadMode.Local)
            {
                EditorGUILayout.LabelField("请使用Bundle加载！");
                return;
            }

            if (GUILayout.Button("释放无用资源"))
            {
                AssetManager.Instance.UnloadAllUnuse();
            }

            EditorGUILayout.BeginHorizontal();
            searchFlag = EditorGUILayout.TextField(searchFlag);
            if (GUILayout.Button("搜索"))
            {
                curKey = searchFlag;
            }
            EditorGUILayout.EndHorizontal();

            bundleScrollPos = EditorGUILayout.BeginScrollView(bundleScrollPos, GUILayout.Height(400));
            foreach (var dic in BundleManager.Instance.BundleNameDic)
            {
                if (!string.IsNullOrEmpty(curKey) && !dic.Key.Contains(curKey.ToLower()))
                {
                    continue;
                }

                BundleInfo(dic.Key, dic.Value);
            }

            EditorGUILayout.EndScrollView();

            detailScrollPos = EditorGUILayout.BeginScrollView(detailScrollPos, GUILayout.Height(200));
            if (selectBundleInfo != null)
            {
                EditorGUILayout.LabelField("Bundle：" + selectBundleInfo.bundleName);
                foreach (var dic in selectBundleInfo.assetInfoDic)
                {
                    DrawAssetInfo(dic.Key, dic.Value);
                }
            }
            EditorGUILayout.EndScrollView();
        }

        private void BundleInfo(string _name, BundleInfo _bundleInfo)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Bundle名字：" + _name,GUILayout.Width(300));
            EditorGUILayout.LabelField("计数：" + _bundleInfo.RefCount);
            EditorGUILayout.LabelField("废弃：" + _bundleInfo.IsDirty);
            EditorGUILayout.LabelField("是否加载：" + _bundleInfo.IsLoadDone());
            if (GUILayout.Button("查看", GUILayout.Width(50)))
            {
                selectBundleInfo = _bundleInfo;
            }
            EditorGUILayout.EndHorizontal();
        }

        private void DrawAssetInfo(string _name, AssetInfo _assetInfo)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("资源名：" + _name);
            EditorGUILayout.LabelField("引用数：" + _assetInfo.RefCount);
            EditorGUILayout.LabelField("是否加载：" + _assetInfo.IsLoadDone());
            EditorGUILayout.EndHorizontal();
        }
    }
}