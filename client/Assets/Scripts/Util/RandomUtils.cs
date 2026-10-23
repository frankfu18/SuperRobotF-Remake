using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface IWeight
{
    int Weight { get; }
}

public static class RandomUtils
{
    //随机百分比(0 - 100)
    public static bool RandomPercentageByHundred(int _ratio)
    {
        if (_ratio <= 0)
        {
            return false;
        }

        if (_ratio >= 100)
        {
            return true;
        }

        int rand = Random.Range(0, 100);

        return rand < _ratio;
    }

    //万分比 0~10000
    public static bool RandomPercentageByPermyriad(int _ratio)
    {
        if (_ratio <= 0)
        {
            return false;
        }

        if (_ratio >= 10000)
        {
            return true;
        }

        int rand = Random.Range(0, 10000);

        return rand < _ratio;
    }

    public static int RandomRange(int _min, int _max)
    {
        return Random.Range(_min, _max);
    }

    public static List<T> RandomList<T>(List<T> _totalList, int _count)
    {
        List<T> result = new List<T>();

        if (_count <= 0 || _totalList.Count == 0)
        {
            return result;
        }

        List<T> randList = new List<T>(_totalList);

        for (int i = 0; i < _count; i++)
        {
            int rand = UnityEngine.Random.Range(0, randList.Count);

            result.Add(randList[rand]);

            randList.RemoveAt(rand);
            if (randList.Count <= 0)
            {
                break;
            }
        }

        return result;
    }
    
    public static List<T> RandomListByCount<T>(List<T> _totalList, int _count)
    {
        var result = new List<T>();
        if (_count == 0 || _totalList.Count == 0)
        {
            return result;
        }
        var fullCounter = 0;
        var randList = new List<T>(_totalList);
        for (var i = 0; i < _count; i++)
        {
            var rand = Random.Range(0, randList.Count);
            result.Add(randList[rand]);
            //  循环 N 次后打乱一下列表的内容
            if (result.Count % randList.Count == 0)
            {
                fullCounter++;
                if (fullCounter % 2 == 0)
                {
                    randList.Reverse();
                }
                else
                {
                    randList = randList.OrderBy(_ => rand).ToList();    
                }
            }
        }
        return result;
    }

    public static List<T> RandomList<T>(List<T> _totalList, int _count, System.Func<T, bool> _onFilter)
    {
        List<T> result = new List<T>();

        if (_count == 0 || _totalList.Count == 0)
        {
            return result;
        }

        List<T> randList = new List<T>(_totalList);

        int lastCount = _count;

        while(lastCount > 0 && randList.Count > 0)
        {
            int rand = UnityEngine.Random.Range(0, randList.Count);
            T t = randList[rand];

            if (_onFilter != null && !_onFilter(t))
            {
                randList.RemoveAt(rand);

                continue;
            }
            else
            {
                result.Add(t);

                randList.RemoveAt(rand);

                lastCount--;
            }
        }

        return result;
    }

    public static T RandomOneInList<T>(List<T> _totalList, bool _removeList = false)
    {
        T result = default(T);

        if (_totalList.Count == 0)
        {
            return result;
        }

        int rand = UnityEngine.Random.Range(0, _totalList.Count);
        result = _totalList[rand];

        if (_removeList)
        {
            _totalList.RemoveAt(rand);
        }

        return result;
    }

    public static int[] RandomIntSequence(int total, int count)
    {
        int[] sequence = new int[total];
        int[] output = new int[count];
        for (int i = 0; i < total; i++)
        {
            sequence[i] = i;
        }

        int end = total - 1;
        for (int i = 0; i < count; i++)
        {
            int num = UnityEngine.Random.Range(0, end + 1);
            output[i] = sequence[num];
            sequence[num] = sequence[end];
            end--;
        }

        return output;
    }

    public static List<T> RandomWeight<T>(List<T> _totalList, int _count) where T : IWeight
    {
        List<T> result = new List<T>();

        if (_count == 0 || _totalList.Count == 0)
        {
            return result;
        }

        int curWeight = 0;
        int totalWeight = 0;
        List<int> randomWeightList = new List<int>();
        for (int i = 0; i < _totalList.Count; i++)
        {
            curWeight += _totalList[i].Weight;

            randomWeightList.Add(curWeight);
        }

        totalWeight = curWeight;

        for (int i = 0; i < _count; i++)
        {
            int rand = UnityEngine.Random.Range(0, totalWeight);

            for (int j = 0; j < randomWeightList.Count; j++)
            {
                if (rand < randomWeightList[j])
                {
                    result.Add(_totalList[j]);
                    break;
                }
            }
        }

        return result;
    }

    public static List<int> RandomWeight(List<int> _weightList, int _count, bool _needSame = true)
    {
        List<int> result = new List<int>();

        if (_count == 0 || _weightList.Count == 0)
        {
            return result;
        }

        int curWeight = 0;
        int totalWeight = 0;
        List<int> randomWeightList = new List<int>();
        for (int i = 0; i < _weightList.Count; i++)
        {
            curWeight += _weightList[i];

            randomWeightList.Add(curWeight);
        }

        totalWeight = curWeight;

        for (int i = 0; i < _count; i++)
        {
            int rand = UnityEngine.Random.Range(0, totalWeight);

            for (int j = 0; j < randomWeightList.Count; j++)
            {
                if (randomWeightList[j] < 0)
                {
                    continue;
                }

                if (rand < randomWeightList[j])
                {
                    result.Add(j);
                    if (!_needSame)
                    {
                        totalWeight -= randomWeightList[j];
                        randomWeightList[j] = -1;
                    }
                    break;
                }
            }
        }

        return result;
    }

    public static int RandomWeightOne(List<int> _weightList)
    {
        int result = -1;

        if (_weightList.Count == 0)
        {
            return result;
        }

        int curWeight = 0;
        int totalWeight = 0;
        List<int> randomWeightList = new List<int>();
        for (int i = 0; i < _weightList.Count; i++)
        {
            curWeight += _weightList[i];

            randomWeightList.Add(curWeight);
        }

        totalWeight = curWeight;

        int rand = UnityEngine.Random.Range(0, totalWeight);

        for (int j = 0; j < randomWeightList.Count; j++)
        {
            if (randomWeightList[j] < 0)
            {
                continue;
            }

            if (rand < randomWeightList[j])
            {
                result = j;
                break;
            }
        }

        return result;
    }
}
