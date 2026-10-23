using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace FrankGame.Battle
{
    public class EntityOverviewWin : EditorWindow
    {
        private static EntityOverviewWin instance;

        private Vector2 entityScrollViewPos;
        private Vector2 insComScrollViewPos = Vector2.zero;
        private Dictionary<int, bool> entityPopupDic = new Dictionary<int, bool>();
        private EEntityType selectEntityType = EEntityType.None;
        private ECompType selectCompType = ECompType.None;

        [MenuItem("Game/Battle/【战斗调试器】", false, 1)]
        public static void CreateWin()
        {
            if(instance == null)
            {
                instance = CreateWindow<EntityOverviewWin>("战斗调试器");
            }
            
            instance.Show();
        }

        public void OnEnable()
        {
            EntityManager.Instance.entityDic.Foreach((entity) => 
            { 
                OnAddEntity(entity);
                return false;
            });
            EntityManager.Instance.onAddEntity += OnAddEntity;
            EntityManager.Instance.onRemoveEntity += OnRemoveEntity;
        }

        public void OnDisable()
        {
            entityPopupDic.Clear();
            EntityManager.Instance.onAddEntity -= OnAddEntity;
            EntityManager.Instance.onRemoveEntity -= OnRemoveEntity;
        }


        public void OnGUI()
        {
            World world = World.GetCurrent();
            if(world == null)
            {
                EditorGUILayout.LabelField("请运行游戏！");
                return;
            }

            EditorGUILayout.LabelField("【所有实体】");
            EditorGUILayout.BeginHorizontal();
            selectEntityType = (EEntityType)EditorGUILayout.EnumPopup("选择的Entity类型：", selectEntityType);
            selectCompType = (ECompType)EditorGUILayout.EnumPopup("筛选的Component类型：", selectCompType);
            
            if(World.GetCurrent() != null && World.GetCurrent().Root != null)
            {
                Drawer drawer = World.GetCurrent().Root.GetComponent<Drawer>();
                if (GUILayout.Button(drawer != null ? "关闭场景渲染" : "打开场景渲染"))
                {
                    if (drawer != null)
                    {
                        GameObject.Destroy(drawer);
                    }
                    else
                    {
                        World.GetCurrent().Root.gameObject.AddComponent<Drawer>();
                    }
                }

                if(drawer != null)
                {
                    if (GUILayout.Button(drawer.isDrawEmenyQuadTree ? "关闭敌人四叉树" : "渲染敌人四叉树"))
                    {
                        drawer.isDrawEmenyQuadTree = !drawer.isDrawEmenyQuadTree;
                    }
                }
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("暂停"))
            {
                world.Pause();
            }
            if (GUILayout.Button("继续"))
            {
                // world.Resume();
            }
            world.SetPlaySpeed(EditorGUILayout.FloatField("播放速度：", World.GetCurrent().PlaySpeed));
            EditorGUILayout.EndHorizontal();

            this.entityScrollViewPos = EditorGUILayout.BeginScrollView(this.entityScrollViewPos);
            EntityManager.Instance.entityDic.Foreach(ShowEntityView, (e) =>
            {
                if(selectEntityType == EEntityType.None && selectCompType == ECompType.None)
                {
                    return true;
                }

                if (selectEntityType == EEntityType.None)
                {
                    if (selectCompType == ECompType.None)
                    {
                        return true;
                    }
                    else
                    {
                        return e.HasComponent(selectCompType);
                    }
                }
                else if (e.EntityType == selectEntityType)
                {
                    if(selectCompType == ECompType.None)
                    {
                        return true;
                    }
                    else
                    {
                        return e.HasComponent(selectCompType);
                    }
                }
                else
                {
                    return false;
                }
            });

            EditorGUILayout.EndScrollView();

            EditorGUILayout.LabelField("【单例组件】");
            if (InstanceComponent.insComponentList.Count > 0)
            {
                this.insComScrollViewPos = EditorGUILayout.BeginScrollView(this.insComScrollViewPos);
                foreach (var insCom in InstanceComponent.insComponentList)
                {
                    EditorGUILayout.LabelField(" " + insCom.ToString());
                }
                EditorGUILayout.EndScrollView();
            }
            else
            {
                EditorGUILayout.LabelField("还没有单例组件!");
            }
        }

        private bool ShowEntityView(Entity entity)
        {
            if(entityPopupDic.TryGetValue(entity.ID, out bool foldout))
            {
                foldout = EditorGUILayout.Foldout(foldout, "ID: " + entity.ID + ", Type: " + entity.EntityType);
                if (foldout)
                {
                    var coms = entity.GetComponents();
                    foreach (var com in coms)
                    {
                        if (com.ComponentType == ECompType.Asset)
                        {
                            EditorGUILayout.BeginHorizontal();
                        }

                        EditorGUILayout.LabelField("    " + com.ToString());
                        if (com.ComponentType == ECompType.Asset)
                        {
                            if (GUILayout.Button("选中", GUILayout.Width(100)))
                            {
                                var asset = com as AssetComponent;
                                if(asset.IsLoaded())
                                {
                                    asset.GetAsset((go) =>
                                    {
                                        Selection.activeGameObject = (GameObject)EditorUtility.InstanceIDToObject(go.GetInstanceID());
                                    });
                                }
                            }
                        }

                        if (com.ComponentType == ECompType.Asset)
                        {
                            EditorGUILayout.EndHorizontal();
                        }
                    }
                }

                entityPopupDic[entity.ID] = foldout;
            }

            return false;
        }

        private void OnAddEntity(Entity entity)
        {
            entityPopupDic.Add(entity.ID, false);
        }

        private void OnRemoveEntity(Entity entity)
        {
            entityPopupDic.Remove(entity.ID);
        }
    }
}
