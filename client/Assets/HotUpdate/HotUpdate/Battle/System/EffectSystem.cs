using UnityEngine;

namespace FrankGame.Battle
{
    public class EffectSystem: SystemBase
    {
        public EffectSystem(World _world) : base(_world)
        {
        }

        protected override void OnCreateEntity(Entity _entity)
        {
            
        }

        protected override void OnRemoveEntity(Entity _entity)
        {
            
        }

        protected override void OnStart()
        {
            
        }

        protected override void OnUpdate(float _deltaTime, float _unscaledDeltaTime)
        {
            
        }

        protected override void OnLateUpdate(float _deltaTime, float _unscaledDeltaTime)
        {
            
        }

        public EffectEntity CreateEffect(string _battleAssetId, Vector2 _pos, bool _reverse = false)
        {
            var effectEntity = CreateEffect(_battleAssetId);
            // effectEntity.assetCom.GetAsset((go) =>
            // {
            //     if (go == null) return;
            //     go.transform.localPosition = GetPresentationWorldPosition(_pos);
            //     go.transform.localEulerAngles = _reverse ? new Vector3(0, 180, 0) : Vector3.zero;
            // });
            
            return effectEntity;
        }

        /// <summary>
        /// 创建跟随目标 Entity 的持续特效。特效实例仍挂在 MapRoot 下，EffectEntity 每帧同步目标逻辑位置，
        /// 生命周期继续读取 BattleAsset 中配置的特效持续时间。
        /// </summary>
        public EffectEntity CreateMountedEffect(string battleAssetId, Entity target)
        {
            if (target == null || target.IsDirty()
                || string.IsNullOrWhiteSpace(battleAssetId) || battleAssetId == "null"
                || !target.TryGetComponent(ECompType.Transform, out TransformComponent targetTransform))
            {
                return null;
            }

            EffectEntity effectEntity = CreateEffect(battleAssetId, targetTransform.position);
            int targetEid = target.ID;
            return effectEntity;
        }


        /// <summary>将二维逻辑方向 (x,y) 转换为 Unity 水平面方向 (x,0,z)。</summary>
        public static Vector3 ToWorldDirection(Vector2 logicalDirection)
        {
            return new Vector3(logicalDirection.x, 0f, logicalDirection.y);
        }

        public EffectEntity CreateEffect(string _battleAssetId)
        {
            var effectEntity = AddEntity<EffectEntity>(EEntityType.Effect);
            effectEntity.effectCom.active = effectEntity.effectCom.delay <= 0;

            effectEntity.assetCom.SetAsset(_battleAssetId);
            effectEntity.assetCom.GetAsset((go) =>
            {
                if (go == null) return;
                go.transform.SetParent(this.world.mapRoot.transform);
                go.transform.localEulerAngles = Vector3.zero;
                go.transform.localScale = Vector3.one;

                effectEntity.assetCom.SetActive(effectEntity.effectCom.active);
            });

            return effectEntity;
        }

        protected override void OnDestroy()
        {
            
        }
    }
}
