using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrankGame.Battle
{
    public abstract class ComponentBase : IComponent, IDirty, IKeyPoolElement
    {
        public int EntityId
        {
            get
            {
                return this.entityId;
            }
        }

        public abstract ECompType ComponentType { get; }

        public int PoolKey => (int)ComponentType;

        protected Entity entity;
        protected World world;
        private int entityId;
        private bool dirty;

        public ComponentBase(Entity _entity)
        {
            Init(_entity);
        }

        public void Init(Entity _entity)
        {
            this.entity = _entity;
            this.entityId = this.entity.ID;
            this.world = this.entity.World;
        }

        public void Create()
        {
            dirty = false;

            OnCreate();
        }

        protected virtual void OnCreate()
        {

        }

        public void Pause()
        {
            OnPause();
        }

        protected virtual void OnPause()
        {

        }

        public void Resume()
        {
            OnResume();
        }

        protected virtual void OnResume()
        {

        }

        public void SpeedChange(float _speed)
        {
            OnSpeedChange(_speed);
        }
        
        protected virtual void OnSpeedChange(float _speed)
        {

        }

        public bool IsDirty()
        {
            return dirty;
        }

        public void SetDirty(bool dirty)
        {
            this.dirty = dirty;
        }

        protected abstract string GetString();

        public void SendEvent(EBattleEvtType _evtName, params object[] _args)
        {
            this.world.SendEvent(_evtName, _args);
        }

        public void Destroy()
        {
            if (dirty)
            {
                return;
            }

            dirty = true;

            OnDestroy();

            ComponentPool.Instance.Recycle(this);

            this.entity = null;
        }

        protected virtual void OnDestroy()
        {

        }

        public override string ToString()
        {
            return $"[{this.GetType().Name.Substring(0, this.GetType().Name.Length - 9)}] {GetString()}";
        }

        public abstract void OnRecover();
    }
}
