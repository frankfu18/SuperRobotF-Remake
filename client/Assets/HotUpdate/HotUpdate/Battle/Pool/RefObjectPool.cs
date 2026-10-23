using System;
using System.Collections;
using System.Collections.Generic;

namespace FrankGame.Battle
{
    public class RefObjectPool<T> : IRefObjectPool where T : IClear, new()
    {
        private static RefObjectPool<T> instance;

        public static RefObjectPool<T> Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RefObjectPool<T>();

                    RefObjectManager.Instance.AddPool(instance);
                }

                return instance;
            }
        }

        public string Name => typeof(T).Name;

        public int UsingObjectCount => this.usingObjectCount;
        public int UnuseObjectCount => this.freeObjectStack.Count;

        private Stack<T> freeObjectStack = new Stack<T>();
        private int usingObjectCount;

        private RefObjectPool()
        {

        }

        public T Get()
        {
            usingObjectCount++;

            if (freeObjectStack.Count > 0)
            {
                return freeObjectStack.Pop();
            }

            return new T();
        }

        public void Recycle(T t)
        {
            t.Clear();
            freeObjectStack.Push(t);

            usingObjectCount--;
        }

        public void Clear()
        {
            usingObjectCount = 0;
            freeObjectStack.Clear();
        }
    }
}