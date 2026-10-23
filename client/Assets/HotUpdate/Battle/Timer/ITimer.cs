using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrankGame.Battle
{
    public interface ITimer
    {
        int Index { get; }

        void SetIndex(int _index);

        void Start();

        void Update(float _deltaTime);

        void Cancel(bool _processComplete);

        bool IsRunning();

        void Dispose();
    }
}
