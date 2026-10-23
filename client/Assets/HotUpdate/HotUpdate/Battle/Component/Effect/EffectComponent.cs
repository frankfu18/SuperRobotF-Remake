namespace FrankGame.Battle
{
    public class EffectComponent : ComponentBase
    {
        public override ECompType ComponentType => ECompType.Effect;

        public float duration;
        public float delay;
        public bool needAutoDestroy;//需要自己销毁
        public bool active;
        /// <summary>持续挂载特效跟随的目标 Entity EID；0 表示固定位置特效。</summary>
        public int MountTargetEid { get; private set; }

        private float curTime;
        private bool isNeedDestory;
        private System.Action<int> onDestoryCallback;//销毁回调
        
        public EffectComponent(Entity _entity) : base(_entity)
        {
        }

        public void SetDuration(int _time, int _delay)
        {
            this.delay = _delay / 1000.0f;
            this.duration = _time / 1000.0f + this.delay;//需要把延迟算上
            this.needAutoDestroy = _time > 0 ? true : false;
            this.curTime = 0;
            this.active = _delay <= 0;
            this.isNeedDestory = false;
        }

        public void SetDestoryCallback(System.Action<int> _onDestoryCallback)
        {
            this.onDestoryCallback = _onDestoryCallback;
        }

        /// <summary>记录逻辑挂载目标。特效资源仍归属 MapRoot，不修改 Unity Transform 父子关系。</summary>
        public void SetMountTarget(int targetEid)
        {
            MountTargetEid = targetEid > 0 ? targetEid : 0;
        }

        public void Update(float _deltaTime)
        {
            if (!this.needAutoDestroy || this.isNeedDestory)
            {
                return;
            }

            if (!this.active)
            {
                if(this.curTime >= this.delay)
                {
                    this.active = true;

                    if(this.entity.TryGetComponent(ECompType.Asset, out AssetComponent assetCom))
                    {
                        assetCom.SetActive(this.active);
                    }
                }
            }

            this.curTime += _deltaTime;
            if (this.curTime >= duration)
            {
                this.isNeedDestory = true;
            }
        }

        public bool IsNeedDestory()
        {
            return this.isNeedDestory;
        }
        
        protected override string GetString()
        {
            return $"duration:{duration},needDestroy:{needAutoDestroy}";
        }

        public override void OnRecover()
        {
            duration = 0;
            delay = 0;
            curTime = 0;
            active = false;
            needAutoDestroy = false;
            isNeedDestory = false;
            onDestoryCallback = null;
            MountTargetEid = 0;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            this.onDestoryCallback?.Invoke(this.EntityId);
        }
    }
}
