using System.Collections;
using System.Collections.Generic;

namespace FrankGame.Battle
{
    public class ListObjectPool<T> where T : class
    {
        private static ListObjectPool<T> instance;

        public static ListObjectPool<T> Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new ListObjectPool<T>();
                }

                return instance;
            }
        }

        private Stack<List<T>> freeListStack = new Stack<List<T>>();

        private ListObjectPool()
        {

        }

        public List<T> Get()
        {
            if(freeListStack.Count > 0)
            {
                return freeListStack.Pop();
            }

            return new List<T>();
        }

        public void Recycle(List<T> list)
        {
            list.Clear();
            freeListStack.Push(list);
        }

        public void Clear()
        {
            freeListStack.Clear();
        }
    }
}