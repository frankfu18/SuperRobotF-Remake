using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace FrankGame.Battle
{
    public class RefObjectOverviewWin : EditorWindow
    {
        private static RefObjectOverviewWin instance;


        [MenuItem("Game/Battle/查看引用对象池", false, 100)]
        public static void CreateWin()
        {
            if(instance == null)
            {
                instance = CreateWindow<RefObjectOverviewWin>("引用对象池查看器");
            }
            
            instance.Show();
        }

        public void OnEnable()
        {
            
        }

        public void OnDisable()
        {
            
        }


        public void OnGUI()
        {
            foreach (var pool in RefObjectManager.Instance.RefObjectPools)
            {
                ShowRefObjectPoolView(pool);
            }
        }

        private void ShowRefObjectPoolView(IRefObjectPool _pool)
        {
            EditorGUILayout.LabelField($"[{_pool.Name}] 正在使用数量：{_pool.UsingObjectCount}, 池子中数量：{_pool.UnuseObjectCount}, 总数量：{ _pool.UsingObjectCount + _pool.UnuseObjectCount }");
        }
    }
}
