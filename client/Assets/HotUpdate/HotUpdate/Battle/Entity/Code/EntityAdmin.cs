using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrankGame.Battle
{
    public abstract class EntityAdmin
    {
        public World World
        {
            get { return this.world; }
        }

        protected World world;

        private IntDictionary<Entity> entities = new IntDictionary<Entity>(1024);
        // Entity 按真实生命周期需求进入不同索引，避免空 Update/LateUpdate 在大规模 Stone 下产生遍历成本。
        private IntDictionary<Entity> updateEntities;
        private IntDictionary<Entity> lateUpdateEntities;

        private float deltaTime;
        private float unscaledDeltaTime;

        public EntityAdmin(World _world)
        {
            this.world = _world;
        }

        public virtual void Update(float _deltaTime, float _unscaledDeltaTime)
        {
            this.deltaTime = _deltaTime;
            this.unscaledDeltaTime = _unscaledDeltaTime;

            this.updateEntities?.Foreach(this,
                static (entity, admin) => admin.UpdateEntity(entity));
        }

        private void UpdateEntity(Entity entity)
        {
            entity.Update(this.deltaTime, this.unscaledDeltaTime);

            OnUpdate(entity, this.deltaTime, this.unscaledDeltaTime);
        }

        protected abstract void OnUpdate(Entity _entity, float _deltaTime, float _unscaledDeltaTime);

        public virtual void LateUpdate(float _deltaTime, float _unscaledDeltaTime)
        {
            this.deltaTime = _deltaTime;
            this.unscaledDeltaTime = _unscaledDeltaTime;
            this.lateUpdateEntities?.Foreach(this,
                static (entity, admin) => admin.LateUpdateEntity(entity));
        }

        private void LateUpdateEntity(Entity entity)
        {
            entity.LateUpdate(this.deltaTime, this.unscaledDeltaTime);

            OnLateUpdate(entity, this.deltaTime, this.unscaledDeltaTime);
        }

        protected abstract void OnLateUpdate(Entity _entity, float _deltaTime, float _unscaledDeltaTime);

        public virtual void FixedUpdate(float _fixedDeltaTime)
        {
            
        }

        public T AddEntity<T>(EEntityType _entityType) where T : Entity
        {
            return AddEntity(_entityType) as T;
        }

        public Entity AddEntity(EEntityType _entityType)
        {
            Entity entity = EntityPool.Instance.Get((int)_entityType, this);

            OnCreateEntity(entity);

            return entity;
        }

        public void AddEntityDic(Entity _entity)
        {
            this.entities.Add(_entity.ID, _entity);
            if (_entity.RequiresUpdate)
            {
                this.updateEntities ??= new IntDictionary<Entity>(64);
                this.updateEntities.Add(_entity.ID, _entity);
            }
            if (_entity.RequiresLateUpdate)
            {
                this.lateUpdateEntities ??= new IntDictionary<Entity>(32);
                this.lateUpdateEntities.Add(_entity.ID, _entity);
            }
        }

        protected abstract void OnCreateEntity(Entity _entity);

        public bool RemoveEntity(int _eid)
        {
            if(TryGetEntity(_eid, out Entity entity))
            {
                this.RemoveEntity(entity);

                return true;
            }

            return false;
        }

        public bool RemoveEntity(Entity _entity)
        {
            if (_entity.IsDirty())
            {
                return false;
            }

            if (!entities.ContainsKey(_entity.ID))
            {
                return false;
            }

            OnRemoveEntity(_entity);

            _entity.Dispose();

            this.entities.Remove(_entity.ID);
            this.updateEntities?.Remove(_entity.ID);
            this.lateUpdateEntities?.Remove(_entity.ID);

            return true;
        }

        protected abstract void OnRemoveEntity(Entity _entity);

        public void Foreach(Func<Entity, bool> _onIterate, Func<Entity, bool> _onFilter = null)
        {
            this.entities.Foreach(_onIterate, _onFilter);
        }

        public void Foreach(Action<Entity> _onIterate, Func<Entity, bool> _onFilter = null)
        {
            this.entities.Foreach(_onIterate, _onFilter);
        }

        public bool TryGetEntity(int _eid, out Entity _entity)
        {
            if(this.entities.TryGetValue(_eid, out _entity))
            {
                return true;
            }

            return false;
        }

        public bool TryGetEntity<T>(int _eid, out T _entity) where T : Entity
        {
            if (this.entities.TryGetValue(_eid, out Entity entity))
            {
                if(entity is T)
                {
                    _entity = (T)entity;
                    return true;
                }
            }

            _entity = null;
            return false;
        }

        public void Dispose()
        {
            this.entities.Foreach(static entity => entity.Destroy());
            this.entities.Clear();
            this.updateEntities?.Clear();
            this.lateUpdateEntities?.Clear();
            this.updateEntities = null;
            this.lateUpdateEntities = null;
        }
    }
}
