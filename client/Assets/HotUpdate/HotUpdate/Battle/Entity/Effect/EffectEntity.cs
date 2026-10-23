using UnityEngine;

namespace FrankGame.Battle
{
    public class EffectEntity : Entity
    {
        public override EEntityType EntityType => EEntityType.Effect;
        public override bool RequiresLateUpdate => false;

        public EffectComponent effectCom;
        public AssetComponent assetCom;

        private Vector2 effectPos;
        
        public EffectEntity(EntityAdmin _admin) : base(_admin)
        {
        }

        
        protected override void OnCreate()
        {
            this.effectCom = AddComponent<EffectComponent>(ECompType.Effect);
            this.assetCom = AddComponent<AssetComponent>(ECompType.Asset);
        }

        protected override void OnUpdate(float _deltaTime, float _unscaledDeltaTime)
        {
            this.effectCom.Update(_deltaTime);
            if (this.effectCom.IsNeedDestory())
            {
                this.world.RemoveEntity(this.ID);
                return;
            }
        }

        protected override void OnLateUpdate(float _deltaTime, float _unscaledDeltaTime)
        {
            
        }

        public void SetDestoryCallback(System.Action<int> _onDestoryCallback)
        {
            this.effectCom.SetDestoryCallback(_onDestoryCallback);
        }

        public void SetPos(Vector2 _pos)
        {
            effectPos = _pos;
        }

        protected override void OnDestroy()
        {
            
        }

        public override void OnRecover()
        {
            effectPos = Vector2.zero;
            effectCom = null;
            assetCom = null;
        }
    }
}
