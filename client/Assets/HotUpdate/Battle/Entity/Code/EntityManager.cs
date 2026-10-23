using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrankGame.Battle
{
    public class EntityManager : Singlet<EntityManager>
    {
        public IntDictionary<Entity> entityDic = new IntDictionary<Entity>();

        public System.Action<Entity> onAddEntity;
        public System.Action<Entity> onRemoveEntity;

        public void AddEntity(Entity entity)
        {
            entityDic.Add(entity.ID, entity);

            this.onAddEntity?.Invoke(entity);
        }

        public bool RemoveEntity(int eid)
        {
            if(TryGetEntity(eid, out Entity entity))
            {
                return RemoveEntity(entity);
            }

            return false;
        }

        public bool RemoveEntity(Entity entity)
        {
            if (entityDic.Remove(entity.ID))
            {
                this.onRemoveEntity?.Invoke(entity);

                EntityPool.Instance.Recycle(entity);

                return true;
            }

            return false;
        }

        public bool TryGetEntity(int eid, out Entity entity)
        {
            if (this.entityDic.TryGetValue(eid, out entity))
            {
                return true;
            }

            return false;
        }

        public bool TryGetEntity<T>(int eid, out T t) where T : Entity
        {
            if (this.entityDic.TryGetValue(eid, out Entity entity))
            {
                t = entity as T;
                return t != null;
            }

            t = null;
            return false;
        }

        public void Destroy()
        {
            this.entityDic.Clear();
        }
    }
}
