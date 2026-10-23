using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrankGame.Battle
{
    public abstract class Entity : IDirty, IKeyPoolElement
    {
        private static List<ECompType> tempRemoveList = new List<ECompType>();

        public abstract EEntityType EntityType { get;}
        /// <summary>是否需要进入所属 System 的逐帧 Entity 更新索引。</summary>
        public virtual bool RequiresUpdate => true;
        /// <summary>是否需要进入所属 System 的逐帧 Entity LateUpdate 索引。</summary>
        public virtual bool RequiresLateUpdate => true;
        public int ID { get { return this.eid; } }
        public EntityAdmin Admin { get { return this.admin; } }
        public World World { get { return this.world; } }

        public int PoolKey => (int)EntityType;

        protected EntityAdmin admin;
        protected World world;
        protected int eid;
        protected bool dirty;

        private IntDictionary<ComponentBase> componentDic = new IntDictionary<ComponentBase>();

        // 绝大多数战斗实体不使用实体级计时器，只有首次添加计时任务时才创建内部集合。
        private TimerHandler timerHandler;

        public Entity(EntityAdmin _admin)
        {
            Init(_admin);
        }

        public void Init(EntityAdmin _admin)
        {
            this.admin = _admin;
            this.world = _admin.World;
        }

        public void AutoGenId()
        {
            this.eid = this.world.GetGenId();

            SetDirty(false);
        }

        public void Create()
        {
            BattleLog.EntityLog(this, "Create");

            EntityManager.Instance.AddEntity(this);
            this.admin.AddEntityDic(this);

            OnRegisterListener();
            OnCreate();
        }

        protected virtual void OnRegisterListener()
        {

        }

        protected abstract void OnCreate();

        public void Update(float _deltaTime, float _unscaledDeltaTime)
        {
            OnUpdate(_deltaTime, _unscaledDeltaTime);
            
            timerHandler?.Update(_deltaTime);
        }

        protected abstract void OnUpdate(float _deltaTime, float _unscaledDeltaTime);

        public void LateUpdate(float _deltaTime, float _unscaledDeltaTime)
        {
            OnLateUpdate(_deltaTime, _unscaledDeltaTime);
        }

        protected abstract void OnLateUpdate(float _deltaTime, float _unscaledDeltaTime);

        #region Component

        public void AddComponent(ECompType _comType)
        {
            ComponentBase comp = ComponentPool.Instance.Get((int)_comType, this);

            AddComponent(comp);
        }

        public T AddComponent<T>(ECompType _comType) where T : ComponentBase
        {
            ComponentBase comp = ComponentPool.Instance.Get((int)_comType, this);

            return (T)AddComponent(comp);
        }

        public T GetOrAddComponent<T>(ECompType _comType) where T : ComponentBase
        {
            if (TryGetComponent(_comType, out T component))
            {
                return component;
            }

            return AddComponent<T>(_comType);
        }

        private ComponentBase AddComponent(ComponentBase _component)
        {
            if (this.componentDic.ContainsKey((int)_component.ComponentType))
            {
                throw new System.Exception("Component already exists: " + _component.ComponentType);
            }

            this.componentDic.Add((int)_component.ComponentType, _component);

            OnAddComponent(_component);

            return _component;
        }

        protected virtual void OnAddComponent(ComponentBase _component)
        {
            
        }

        public void RemoveComponent(ECompType _comType)
        {
            if (this.componentDic.TryGetValue((int)_comType, out ComponentBase component))
            {
                this.componentDic.Remove((int)_comType);

                OnRemoveComponent(component);

                component.Destroy();
            }
        }

        /// <summary>
        /// 保留的组件类型，移除其他所有组件
        /// </summary>
        /// <param name="_comType"></param>
        public void RemoveOtherComponent(ECompType _comType)
        {
            tempRemoveList.Clear();

            this.componentDic.Foreach((comp) =>
            {
                if(comp.ComponentType != _comType)
                {
                    tempRemoveList.Add(comp.ComponentType);
                }
                return false;
            });

            for(int i = 0; i < tempRemoveList.Count; i++)
            {
                RemoveComponent(tempRemoveList[i]);
            }
        }

        public void RemoveOtherComponent(ECompType _comType1, ECompType _comType2)
        {
            tempRemoveList.Clear();

            this.componentDic.Foreach((comp) =>
            {
                if (comp.ComponentType != _comType1 && comp.ComponentType != _comType2)
                {
                    tempRemoveList.Add(comp.ComponentType);
                }
                return false;
            });

            for (int i = 0; i < tempRemoveList.Count; i++)
            {
                RemoveComponent(tempRemoveList[i]);
            }
        }

        public void RemoveOtherComponent(ECompType _comType1, ECompType _comType2, ECompType _comType3)
        {
            tempRemoveList.Clear();

            this.componentDic.Foreach((comp) =>
            {
                if (comp.ComponentType != _comType1 && comp.ComponentType != _comType2 && comp.ComponentType != _comType3)
                {
                    tempRemoveList.Add(comp.ComponentType);
                }
                return false;
            });

            for (int i = 0; i < tempRemoveList.Count; i++)
            {
                RemoveComponent(tempRemoveList[i]);
            }
        }

        public void RemoveOtherComponent(ECompType _comType1, ECompType _comType2, ECompType _comType3, ECompType _comType4)
        {
            tempRemoveList.Clear();

            this.componentDic.Foreach((comp) =>
            {
                if (comp.ComponentType != _comType1 && comp.ComponentType != _comType2 && comp.ComponentType != _comType3 && comp.ComponentType != _comType4)
                {
                    tempRemoveList.Add(comp.ComponentType);
                }
                return false;
            });

            for (int i = 0; i < tempRemoveList.Count; i++)
            {
                RemoveComponent(tempRemoveList[i]);
            }
        }

        protected virtual void OnRemoveComponent(ComponentBase _component)
        {

        }

        public T GetComponent<T>(ECompType _comType) where T : ComponentBase
        {
            if(TryGetComponent(_comType, out  T component))
            {
                return component;
            }
            else
            {
                BattleLog.Error("Entity:" + this.ID + " 组件不存在：" + _comType);
            }

            return null;
        }

        public bool HasComponent(ECompType _comType)
        {
            return this.componentDic.ContainsKey((int)_comType);
        }

        public bool HasComponent(ECompType _comType1, ECompType _comType2)
        {
            return this.componentDic.ContainsKey((int)_comType1) 
                && this.componentDic.ContainsKey((int)_comType2);
        }

        public bool HasComponent(ECompType _comType1, ECompType _comType2, ECompType _comType3)
        {
            return this.componentDic.ContainsKey((int)_comType1)
                && this.componentDic.ContainsKey((int)_comType2)
                && this.componentDic.ContainsKey((int)_comType3);
        }

        public bool HasComponent(ECompType _comType1, ECompType _comType2, ECompType _comType3, ECompType _comType4)
        {
            return this.componentDic.ContainsKey((int)_comType1)
                && this.componentDic.ContainsKey((int)_comType2)
                && this.componentDic.ContainsKey((int)_comType3)
                && this.componentDic.ContainsKey((int)_comType4);
        }

        public bool HasComponent(ECompType _comType1, ECompType _comType2, ECompType _comType3, ECompType _comType4, ECompType _comType5)
        {
            return this.componentDic.ContainsKey((int)_comType1)
                && this.componentDic.ContainsKey((int)_comType2)
                && this.componentDic.ContainsKey((int)_comType3)
                && this.componentDic.ContainsKey((int)_comType4)
                && this.componentDic.ContainsKey((int)_comType5);
        }

        public bool TryGetComponent<T>(ECompType _comType, out T _component) where T : ComponentBase
        {
            if(this.componentDic.TryGetValue((int)_comType, out ComponentBase comp))
            {
                if(comp is T)
                {
                    _component = comp as T;
                    return true;
                }
            }

            _component = null;
            return false;
        }

        public List<ComponentBase> GetComponents()
        {
            return this.componentDic.Values;
        }

        #endregion

        #region Time

        protected int AddDelay(float _delay, System.Action _onComplete)
        {
            timerHandler ??= new TimerHandler();
            return this.timerHandler.AddDelayTimer(_delay, _onComplete);
        }

        protected int AddIntervalTimer(float _interval, System.Action _onComplete, int _repeatCount = -1)
        {
            timerHandler ??= new TimerHandler();
            return this.timerHandler.AddIntervalTimer(_interval, _onComplete, _repeatCount);
        }

        #endregion

        #region Event

        public void SendEvent(EBattleEvtType _evtName, params object[] _args)
        {
            this.world.SendEvent(_evtName, _args);
        }

        public void AddEvent(EBattleEvtType _evtName, CallBackParams _callback)
        {
            this.world.AddEvent(_evtName, this, _callback);
        }

        #endregion

        public void Pause()
        {
            this.componentDic.Foreach((comp) => comp.Pause());

            OnPause();
        }

        protected virtual void OnPause()
        {

        }

        public void Resume()
        {
            this.componentDic.Foreach((comp) => comp.Resume());

            OnResume();
        }

        protected virtual void OnResume()
        {

        }

        public void SpeedChange(float _speed)
        {
            this.componentDic.Foreach((comp) => comp.SpeedChange(_speed));

            OnSpeedChange();
        }
        
        protected virtual void OnSpeedChange()
        {

        }

        public void SetDirty(bool _dirty)
        {
            this.dirty = _dirty;
        }

        public bool IsDirty()
        {
            return this.dirty;
        }

        //内部调用销毁Entity
        public void Destroy()
        {
            if (IsDirty())
            {
                return;
            }

            BattleLog.EntityLog(this, "Destroy");

            this.admin.RemoveEntity(this);
        }

        //单纯释放，禁止调用
        public void Dispose()
        {
            if (IsDirty())
            {
                return;
            }

            SetDirty(true);

            OnDestroy();

            this.world.RemoveEvent(this);

            this.timerHandler?.Dispose();
            this.timerHandler = null;

            this.componentDic.Foreach((comp) =>
            {
                comp.Destroy();

                return false;
            });
            this.componentDic.Clear();

            EntityManager.Instance.RemoveEntity(this);
        }

        protected abstract void OnDestroy();

        public override string ToString()
        {
            return $"{this.GetType().Name} ID:{this.ID}";
        }

        public abstract void OnRecover();
    }
}
