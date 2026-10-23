using System.Collections;
using System.Collections.Generic;

namespace FrankGame.Battle
{
    public interface IDirty
    {
        bool IsDirty();

        void SetDirty(bool dirty);
    }

    public class IntDictionary<T> where T : class, IDirty
    {
        private int _count;
        private List<T> _list;
        private Dictionary<int, int> _dic;//key : key; value : list index
        private Queue<int> _unuseIdxQueue;

        public int Count
        {
            get
            {
                return _count;
            }
        }

        public List<T> Values
        {
            get
            {
                List<T> list = new List<T>();

                int count = _list.Count;
                for (int i = 0; i < count; i++)
                {
                    T item = _list[i];
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }

                return list;
            }
        }

        public List<int> Keys
        {
            get
            {
                return new List<int>(_dic.Keys);
            }
        }

        public T this[int key]
        {
            get
            {
                int idx;
                if (!_dic.TryGetValue(key, out idx))
                {
                    throw new System.Exception("key is null:" + key);
                }

                return _list[idx];
            }
        }

        public IntDictionary(int capacity = 12)
        {
            _list = new List<T>(capacity);
            _dic = new Dictionary<int, int>();
            _unuseIdxQueue = new Queue<int>();
            _count = 0;
        }

        public void Add(int key, T item)
        {
            if (_dic.ContainsKey(key))
            {
                throw new System.Exception("add same key：" + key);
            }

            if(_unuseIdxQueue.Count > 0)
            {
                int idx = _unuseIdxQueue.Dequeue();
                _list[idx] = item;

                _dic.Add(key, idx);
            }
            else
            {
                _dic.Add(key, _list.Count);
                _list.Add(item);
            }

            _count++;
        }

        public bool Remove(int key)
        {
            int idx;
            if (!_dic.TryGetValue(key, out idx))
            {
                return false;
            }

            _list[idx] = null;
            _dic.Remove(key);

            _unuseIdxQueue.Enqueue(idx);

            _count--;

            return true;
        }

        public void Foreach(System.Func<T, bool> onIterate, System.Func<T, bool> onFilter = null)
        {
            int count = _list.Count;
            for (int i = 0; i < count; i++)
            {
                var item = _list[i];
                if (item == null || item.IsDirty())
                {
                    continue;
                }

                if (onFilter == null || onFilter(item))
                {
                    if (onIterate != null && onIterate.Invoke(item))
                    {
                        break;
                    }
                }
            }
        }

        public void Foreach(System.Action<T> onIterate, System.Func<T, bool> onFilter = null)
        {
            int count = _list.Count;
            for (int i = 0; i < count; i++)
            {
                var item = _list[i];
                if (item == null || item.IsDirty())
                {
                    continue;
                }

                if (onFilter == null || onFilter(item))
                {
                    onIterate?.Invoke(item);
                }
            }
        }

        /// <summary>
        /// 携带调用方状态遍历，供逐帧热点使用静态 Lambda，避免捕获局部变量生成闭包。
        /// 遍历期间仍允许把元素标记为 Dirty；新增元素只会在下一次遍历中处理。
        /// </summary>
        public void Foreach<TState>(TState state, System.Func<T, TState, bool> onIterate,
            System.Func<T, TState, bool> onFilter = null)
        {
            int count = _list.Count;
            for (int i = 0; i < count; i++)
            {
                T item = _list[i];
                if (item == null || item.IsDirty())
                {
                    continue;
                }

                if (onFilter == null || onFilter(item, state))
                {
                    if (onIterate != null && onIterate(item, state))
                    {
                        break;
                    }
                }
            }
        }

        /// <summary>Action 版本的携带状态遍历，不需要提前构造捕获委托。</summary>
        public void Foreach<TState>(TState state, System.Action<T, TState> onIterate,
            System.Func<T, TState, bool> onFilter = null)
        {
            int count = _list.Count;
            for (int i = 0; i < count; i++)
            {
                T item = _list[i];
                if (item == null || item.IsDirty())
                {
                    continue;
                }

                if (onFilter == null || onFilter(item, state))
                {
                    onIterate?.Invoke(item, state);
                }
            }
        }

        public bool ContainsKey(int key)
        {
            return _dic.ContainsKey(key);
        }

        public bool TryGetValue(int key, out T item)
        {
            if(_dic.TryGetValue(key, out int idx))
            {
                item = _list[idx];

                return true;
            }
            
            item = default;
            return false;
        }

        public void Clear()
        {
            _list.Clear();
            _dic.Clear();
            _unuseIdxQueue.Clear();
            _count = 0;
        }
    }
}
