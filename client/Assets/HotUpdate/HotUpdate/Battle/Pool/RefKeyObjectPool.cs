using System;
using System.Collections;
using System.Collections.Generic;

namespace FrankGame.Battle
{
    public abstract class RefKeyObjectPool<T, P> : IRefObjectPool where T : IKeyPoolElement where P : class
    {
        private static RefKeyObjectPool<T, P> instance;

        public static RefKeyObjectPool<T, P> Instance
        {
            get
            {
                return instance;
            }
        }

        public string Name => this.GetType().Name;

        public int UsingObjectCount => this.usingObjectCount;
        public int UnuseObjectCount
        {
            get
            {
                int count = 0;
                foreach (var kv in freeObjectDic)
                {
                    count += kv.Value.Count;
                }

                return count;
            }
        }

        private Dictionary<int, Stack<T>> freeObjectDic = new Dictionary<int, Stack<T>>();
        private int usingObjectCount;

        public RefKeyObjectPool()
        {
            instance = this;

            RefObjectManager.Instance.AddPool(instance);
        }

        protected virtual T OnCreateObject(int _key, P _param)
        {
            return default;
        }

        protected virtual void OnInitObject(T _t, P _param)
        {
            
        }

        public T Get(int _key, P _param)
        {
            this.usingObjectCount++;

            T t = default;
            if (freeObjectDic.TryGetValue(_key, out Stack<T> freeObjectStack))
            {
                if (freeObjectStack.Count > 0)
                {
                    t = freeObjectStack.Pop();
                }
            }

            if (t == null)
            {
                t = OnCreateObject(_key, _param);
            }
            else
            {
                OnInitObject(t, _param);
            }

            return t;
        }

        public void Recycle(T _t)
        {
            this.usingObjectCount--;

            Stack<T> freeObjectStack;
            if (!freeObjectDic.TryGetValue(_t.PoolKey, out freeObjectStack))
            {
                freeObjectStack = new Stack<T>();

                freeObjectDic.Add(_t.PoolKey, freeObjectStack);
            }

            _t.OnRecover();

            freeObjectStack.Push(_t);
        }

        public void Clear()
        {
            freeObjectDic.Clear();

            RefObjectManager.Instance.RemovePool(instance);
            instance = null;
        }
    }
}
