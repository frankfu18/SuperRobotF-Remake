using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrankGame.Battle
{
    public class WorldRoot : MonoBehaviour
    {
        private World world;

        public void InitWord(World _world)
        {
            this.world = _world;
        }

        private void Update()
        {
            this.world?.Update(Time.deltaTime, Time.unscaledDeltaTime);
        }

        private void LateUpdate()
        {
            this.world?.LateUpdate(Time.deltaTime, Time.unscaledDeltaTime);
        }

        private void FixedUpdate()
        {
            this.world?.FixedUpdate(Time.fixedDeltaTime);
        }

        private void OnDestroy()
        {
            
        }
    }
}
