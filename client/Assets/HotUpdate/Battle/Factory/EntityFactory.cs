using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrankGame.Battle
{
    public static class EntityFactory
    {
        public static Entity Create(EEntityType _entityType, EntityAdmin _admin)
        {
            Entity entity = null;
            switch (_entityType)
            {
                case EEntityType.Effect:
                    entity = new EffectEntity(_admin);
                    break;
                case EEntityType.Camera:
                    entity = new CameraEntity(_admin);
                    break;
                default:
                    throw new System.Exception("未找到的实体类型: " + _entityType);
            }

            if(entity != null)
            {
                entity.AutoGenId();
                entity.Create();
            }

            return entity;
        }
    }
}
