using System.Collections;
using System.Collections.Generic;

namespace FrankGame.Battle
{
    public static class ListExtend
    {
        private static System.Random InsRandom = new System.Random();

        /// <summary>
        /// 消耗删除List中指定位置的元素，时间复杂度O(1)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="index"></param>
        public static void RemoveAtNoCost<T>(this List<T> list, int index)
        {
            int lastIdx = list.Count - 1;

            if(lastIdx != index)
            {
                //和最后一个做交换
                T temp = list[lastIdx];
                list[lastIdx] = list[index];
                list[index] = temp;
            }

            list.RemoveAt(lastIdx);
        }

        /// <summary>
        /// 随机打乱List顺序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void Shuffle<T>(this List<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = InsRandom.Next(n + 1);

                T temp = list[n];
                list[n] = list[k];
                list[k] = temp;
            }
        }
    }
}
