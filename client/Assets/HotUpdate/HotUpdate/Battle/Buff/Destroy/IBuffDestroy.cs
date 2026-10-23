using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrankGame.Battle
{
    public interface IBuffDestroy
    {
        void Update(float _deltaTime);

        void CheckDestroy();

        void Reset();
    }
}