using System.Collections;
using System.Collections.Generic;

namespace FrankGame.Battle
{
    /// <summary>
    /// 单例组件
    /// </summary>
    public abstract class InstanceComponent
    {
        public readonly static List<InstanceComponent> insComponentList = new List<InstanceComponent>(); 

        protected World world;

        public InstanceComponent(World _world)
        {
            this.world = _world;

            insComponentList.Add(this);

            Create();
        }

        public void Create()
        {
            OnCreate();
        }

        protected virtual void OnCreate()
        {

        }

        protected abstract string GetString();

        public void Destroy()
        {
            OnDestroy();
        }

        protected abstract void OnDestroy();

        public override string ToString()
        {
            return $"[{this.GetType().Name.Substring(0, this.GetType().Name.Length - 9)}] {GetString()}";
        }

        public static void DestroyAll()
        {
            foreach (var insComponent in insComponentList)
            {
                insComponent.Destroy();
            }

            // 多次进入战斗时不能保留上一局 World 的单例组件引用。
            insComponentList.Clear();
        }
    }
}
