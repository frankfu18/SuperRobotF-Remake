using UnityEngine;

namespace FrankGame.Battle
{
    public partial class World
    {
        private static int idGenerator;
        private static int battleSeedSequence;
        private static World currentWorld;

        public Transform Root => root;
        public float PlaySpeed => playSpeed;
        public bool IsPlaying => isStart;
        public float Time => curTime;
        public float UnscaleTime => curUnscaledTime;
        public float DeltaTime => deltaTime;
        public float FixedDeltaTime => fixedDeltaTime;

        public CameraSystem cameraSys;
        public EffectSystem effectSys;

        public MapRoot mapRoot;

        private readonly Transform root;
        private WorldRoot worldRoot;
        private readonly ComponentPool componentPool = new ComponentPool();
        private readonly EntityPool entityPool = new EntityPool();
        private EventHandler eventHandler = new EventHandler();
        private TimerHandler timerHandler = new TimerHandler();
        private bool isStart;
        private bool resultSent;
        private float playSpeed = 1f;
        private float curTime;
        private float curUnscaledTime;
        private float deltaTime;
        private float unscaledDeltaTime;
        private float fixedDeltaTime;

        public World(Transform root)
        {
            this.root = root;
            worldRoot = root.gameObject.AddComponent<WorldRoot>();
            worldRoot.InitWord(this);
            currentWorld = this;
            Init();
        }

        private void Init()
        {
            cameraSys = new CameraSystem(this);
            effectSys = new EffectSystem(this);

            OnRegisterListener();
        }

        public void InitScene(MapRoot value)
        {
            mapRoot = value;
        }
        
        public void Start()
        {
            if (isStart) return;
            isStart = true;
            resultSent = false;
            curTime = 0f;
            curUnscaledTime = 0f;
            deltaTime = 0f;
            unscaledDeltaTime = 0f;
            SystemBase.StartAllSystem();
        }

        public void Update(float frameDeltaTime, float frameUnscaledDeltaTime)
        {
            if (!isStart || playSpeed <= 0f) return;
            deltaTime = playSpeed * frameDeltaTime;
            unscaledDeltaTime = playSpeed * frameUnscaledDeltaTime;
            curTime += deltaTime;
            curUnscaledTime += unscaledDeltaTime;
            timerHandler.Update(deltaTime);

            effectSys.Update(deltaTime, unscaledDeltaTime);
        }

        public void LateUpdate(float frameDeltaTime, float frameUnscaledDeltaTime)
        {
            if (!isStart || playSpeed <= 0f) return;
            cameraSys.LateUpdate(deltaTime, unscaledDeltaTime);
        }

        public void FixedUpdate(float frameFixedDeltaTime)
        {
            if (!isStart || playSpeed <= 0f) return;
            fixedDeltaTime = playSpeed * frameFixedDeltaTime;
        }

        public void SendEvent(EBattleEvtType eventName, params object[] args)
        {
            eventHandler.Send((int)eventName, args);
        }
        public void SendEvent(EventConst eventName, params object[] args)
        {
            EventCenterSystem.Instance.SendEvent((int)eventName, args);
        }
        public void AddEvent(EBattleEvtType eventName, object target, CallBackParams callback)
        {
            eventHandler.Add((int)eventName, target, callback);
        }
        public void AddEvent(EventConst eventName, CallBackParams callback)
        {
            EventCenterSystem.Instance.AddEvent(eventName, this, callback);
        }
        public void RemoveEvent(Entity entity)
        {
            eventHandler.Remove(entity);
        }
        public void RemoveEvent(EBattleEvtType eventName, object target, CallBackParams callback)
        {
            eventHandler.Remove((int)eventName, target, callback);
        }
        public void RemoveEvent(EventConst eventName, CallBackParams callback)
        {
            EventCenterSystem.Instance.RemoveEvent(eventName, this, callback);
        }

        public bool TryGetEntity(int eid, out Entity entity)
        {
            return EntityManager.Instance.TryGetEntity(eid, out entity);
        }
        public bool TryGetEntity<T>(int eid, out T entity) where T : Entity
        {
            return EntityManager.Instance.TryGetEntity(eid, out entity);
        }

        public bool RemoveEntity(int eid)
        {
            if (!TryGetEntity(eid, out Entity entity)) return false;
            entity.Destroy();
            return true;
        }

        public void Pause()
        {
            if (!isStart) return;
            isStart = false;
            SystemBase.PauseAllSystem();
        }

        public void SetPlaySpeed(float speed)
        {
            playSpeed = Mathf.Max(0f, speed);
            SystemBase.SpeedChangeAllSystem(playSpeed);
        }

        public int AddDelay(float delay, System.Action callback)
        {
            return timerHandler.AddDelayTimer(delay, callback);
        }
        public int AddIntervalTimer(float interval, System.Action callback, int repeatCount = -1)
        {
            return timerHandler.AddIntervalTimer(interval, callback, repeatCount);
        }
        public int GetGenId()
        {
            return ++idGenerator;
        }
        public static World GetCurrent()
        {
            return currentWorld;
        }

        public void Destroy()
        {
            currentWorld = null;
            isStart = false;
            if (worldRoot != null)
            {
                Object.Destroy(worldRoot);
                worldRoot = null;
            }
            SystemBase.ClearAll();
            InstanceComponent.DestroyAll();
            eventHandler?.Dispose();
            eventHandler = null;
            timerHandler?.Dispose();
            timerHandler = null;
            EventCenterSystem.Instance.RemoveEvent(this);
            EntityManager.Instance.Destroy();
            ComponentPool.Instance.Clear();
            EntityPool.Instance.Clear();
        }
    }
}
