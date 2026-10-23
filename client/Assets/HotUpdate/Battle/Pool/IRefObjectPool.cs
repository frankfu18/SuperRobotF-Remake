using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrankGame.Battle
{
    public interface IRefObjectPool
    {
        string Name { get; }

        int UsingObjectCount { get; }
        int UnuseObjectCount { get; }

        void Clear();
    }
}