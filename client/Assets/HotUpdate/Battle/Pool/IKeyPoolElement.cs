using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrankGame.Battle
{
    public interface IKeyPoolElement
    {
        int PoolKey { get;}

        void OnRecover();
    }
}