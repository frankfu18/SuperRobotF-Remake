using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrankGame.Battle
{
    public abstract class SystemBase : EntityAdmin
    {
        private static List<SystemBase> Systems = new List<SystemBase>();

        public SystemBase(World _world) : base(_world)
        {
            this.world = _world;

            Systems.Add(this);

            OnInit();
            OnRegisterListener();
        }

        protected virtual void OnInit()
        {
            
        }

        protected virtual void OnRegisterListener()
        {

        }

        public void Start()
        {
            OnStart();
        }

        public void Prepare(System.Action _onPrepareDone)
        {
            OnPrepare(_onPrepareDone);
        }

        protected virtual void OnPrepare(System.Action _onPrepareDone)
        {
            _onPrepareDone?.Invoke();
        }

        protected abstract void OnStart();

        public void StartDone()
        {
            OnStartDone();
        }

        protected virtual void OnStartDone()
        {

        }

        public override void Update(float _deltaTime, float _unscaledDeltaTime)
        {
            base.Update(_deltaTime, _unscaledDeltaTime);

            OnUpdate(_deltaTime, _unscaledDeltaTime);
            OnUpdateAfter(_deltaTime, _unscaledDeltaTime);
        }

        public virtual void AlwaysUpdate(float _deltaTime, float _unscaledDeltaTime)
        {

        }

        protected abstract void OnUpdate(float _deltaTime, float _unscaledDeltaTime);

        protected virtual void OnUpdateAfter(float _deltaTime, float _unscaledDeltaTime)
        {

        }

        protected override void OnUpdate(Entity _entity, float _deltaTime, float _unscaledDeltaTime)
        {

        }

        public override void LateUpdate(float _deltaTime, float _unscaledDeltaTime)
        {
            base.LateUpdate(_deltaTime, _unscaledDeltaTime);

            OnLateUpdate(_deltaTime, _unscaledDeltaTime);
        }

        public override void FixedUpdate(float _fixedDeltaTime)
        {
            base.FixedUpdate(_fixedDeltaTime);
        }

        protected abstract void OnLateUpdate(float _deltaTime, float _unscaledDeltaTime);

        protected override void OnLateUpdate(Entity _entity, float _deltaTime, float _unscaledDeltaTime)
        {

        }

        public void SendEvent(EBattleEvtType _evtName, params object[] _args)
        {
            this.world.SendEvent(_evtName, _args);
        }

        public void SendEvent(EventConst _evtName, params object[] _args)
        {
            this.world.SendEvent(_evtName, _args);
        }

        public void AddEvent(EBattleEvtType _evtName, object _target, CallBackParams _callback)
        {
            this.world.AddEvent(_evtName, _target, _callback);
        }

        public void AddEvent(EventConst _evtName, CallBackParams _callback)
        {
            this.world.AddEvent(_evtName, _callback);
        }

        public void Pause()
        {
            Foreach((entity) => entity.Pause());

            OnPause();
        }

        protected virtual void OnPause()
        {

        }

        public void Resume()
        {
            Foreach((entity) => entity.Resume());

            OnResume();
        }

        protected virtual void OnResume()
        {

        }

        public static void PauseAllSystem()
        {
            Systems.ForEach(sys => sys.Pause());
        }

        public static void SpeedChangeAllSystem(float _speed)
        {
            Systems.ForEach(sys => sys.SpeedChange(_speed));
        }

        public void SpeedChange(float _speed)
        {
            Foreach((entity) => entity.SpeedChange(_speed));

            OnSpeedChange();
        }
        
        protected virtual void OnSpeedChange()
        {

        }

        public void Destroy()
        {
            OnDestroy();

            Dispose();
        }

        protected abstract void OnDestroy();

        ////////////所有系统遍历方法，不准在Update里调用/////////////////////
        public static void ResumeAllSystem()
        {
            Systems.ForEach(sys => sys.Resume());
        }

        public static void StartAllSystem()
        {
            int totalSysCount = Systems.Count;
            int doneSysCount = 0;

            Systems.ForEach((sys) => 
            {
                sys.Prepare(() =>
                {
                    doneSysCount++;
                    if(doneSysCount == totalSysCount)
                    {
                        Systems.ForEach(sys => sys.Start());
                        Systems.ForEach(sys => sys.StartDone());
                    }
                });
            });
        }

        public static void ClearAll()
        {
            Systems.ForEach(sys => sys.Destroy());
            Systems.Clear();
        }

        //////////////////////////////////////////////////////////////////
    }
}
