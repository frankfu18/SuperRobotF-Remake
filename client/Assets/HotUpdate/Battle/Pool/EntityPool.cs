using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrankGame.Battle
{
    public class EntityPool : RefKeyObjectPool<Entity, EntityAdmin>
    {
        protected override Entity OnCreateObject(int _key, EntityAdmin _admin)
        {
            return EntityFactory.Create((EEntityType)_key, _admin);
        }

        protected override void OnInitObject(Entity _entity, EntityAdmin _admin)
        {
            _entity.Init(_admin);
            _entity.AutoGenId();
            _entity.Create();
        }
    }
}
