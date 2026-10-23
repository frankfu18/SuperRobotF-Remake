using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrankGame.Battle
{
    public static class BattleGameObjectPool
    {
        private static GameObjectPool Pool;

        public static void Get(string _assetName, System.Action<GameObject> _onGetFunc)
        {
            if (Pool == null)
            {
                Transform poolRoot = new GameObject("BattlePool").transform;
                Pool = new GameObjectPool(poolRoot);
            }

            Pool.GetGameObject(EResType.BattlePrefab, _assetName, _onGetFunc);
        }

        public static void Unuse(string _assetName, GameObject _go)
        {
            if (Pool == null)
            {
                Transform poolRoot = new GameObject("BattlePool").transform;
                Pool = new GameObjectPool(poolRoot);
            }

            Pool.Unused(EResType.BattlePrefab, _assetName, _go);
        }
    }
}