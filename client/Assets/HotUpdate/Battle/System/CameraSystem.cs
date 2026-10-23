using editor.cfg.battle;
using UnityEngine;

namespace FrankGame.Battle
{
    /// <summary>正交俯视相机；玩家位于配置死区内时固定，越界后只沿边界方向跟随。</summary>
    public class CameraSystem : SystemBase
    {
        /// <summary>速度过低时不震屏；比例基准为DamageRule.collisionEffectMaxReferenceSpeed。</summary>
        private const float NoShakeSpeedRatio = 0.1f;
        /// <summary>达到该比例后从小震升级为中震。</summary>
        private const float MediumShakeSpeedRatio = 0.4f;
        /// <summary>达到该比例后从中震升级为大震。</summary>
        private const float LargeShakeSpeedRatio = 0.75f;
        /// <summary>小震局部振幅占当前正交半高的比例。</summary>
        private const float SmallShakeStrengthRatio = 0.006f;
        /// <summary>中震局部振幅占当前正交半高的比例。</summary>
        private const float MediumShakeStrengthRatio = 0.012f;
        /// <summary>大震局部振幅占当前正交半高的比例。</summary>
        private const float LargeShakeStrengthRatio = 0.02f;
        /// <summary>小震持续时间，单位：秒。</summary>
        private const float SmallShakeDurationSecond = 0.1f;
        /// <summary>中震持续时间，单位：秒。</summary>
        private const float MediumShakeDurationSecond = 0.16f;
        /// <summary>大震持续时间，单位：秒。</summary>
        private const float LargeShakeDurationSecond = 0.24f;
        /// <summary>小震在持续时间内的DOTween振动次数。</summary>
        private const int SmallShakeVibrato = 4;
        /// <summary>中震在持续时间内的DOTween振动次数。</summary>
        private const int MediumShakeVibrato = 7;
        /// <summary>大震在持续时间内的DOTween振动次数。</summary>
        private const int LargeShakeVibrato = 10;
        private const float OrthographicSizeLerpSpeed = 5f;
        private const float DefaultCameraWorldHeight = 15f;

        /// <summary>只在CameraSystem内部使用的震屏档位；数值顺序同时用于强弱覆盖比较。</summary>
        private enum CameraShakeTier
        {
            None = 0,
            Small = 1,
            Medium = 2,
            Large = 3,
        }

        private CameraEntity cameraEntity;
        private Vector2 followCenter;
        private Vector2 followDeadZoneHalfExtents;
        private float baseOrthographicSize;
        private float targetOrthographicSize;
        private float cameraWorldHeight;
        /// <summary>当前震屏按战斗世界时间计算的结束时刻，用于判断旧档位是否仍可阻挡弱震。</summary>
        private float activeShakeEndTime;
        private bool followCenterInitialized;
        /// <summary>当前正在播放的震屏档位；暂停、准备和销毁时必须恢复为None。</summary>
        private CameraShakeTier activeShakeTier;

        public CameraSystem(World world) : base(world) { }

        protected override void OnCreateEntity(Entity entity) { }
        protected override void OnRemoveEntity(Entity entity) { }

        protected override void OnStart()
        {
        }

        protected override void OnUpdate(float deltaTime, float unscaledDeltaTime) { }

        protected override void OnLateUpdate(float deltaTime, float unscaledDeltaTime)
        {
        }

        public void SetCameraPos(Vector3 position)
        {
            if (cameraEntity?.cameraCom?.cameraParent != null)
                cameraEntity.cameraCom.cameraParent.position = position;
        }

        public void SetCameraAngle(Vector3 angle)
        {
            if (cameraEntity?.cameraCom?.cameraParent != null)
                cameraEntity.cameraCom.cameraParent.eulerAngles = angle;
        }

        public void ResetToPreparePos()
        {
            ResetShake();
        }
        
        public void EnterFightStartOverview() { }
        public void StartFightCameraMove(System.Action onComplete)
        {
            onComplete?.Invoke();
        }

        

        /// <summary>按档位启动DOTween震屏；正在播放的更强档位不会被较弱碰撞覆盖。</summary>
        private void PlayShake(CameraShakeTier tier)
        {
            if (cameraEntity?.tweenCom?.transform == null || tier == CameraShakeTier.None) return;

            // 世界时间与DOTween默认缩放时间保持一致；超时后即使完成回调未参与档位管理，也允许新震动进入。
            if (activeShakeTier != CameraShakeTier.None && world.Time >= activeShakeEndTime)
                activeShakeTier = CameraShakeTier.None;
            if (activeShakeTier > tier) return;

            float strengthRatio;
            float duration;
            int vibrato;
            switch (tier)
            {
                case CameraShakeTier.Large:
                    strengthRatio = LargeShakeStrengthRatio;
                    duration = LargeShakeDurationSecond;
                    vibrato = LargeShakeVibrato;
                    break;
                case CameraShakeTier.Medium:
                    strengthRatio = MediumShakeStrengthRatio;
                    duration = MediumShakeDurationSecond;
                    vibrato = MediumShakeVibrato;
                    break;
                default:
                    strengthRatio = SmallShakeStrengthRatio;
                    duration = SmallShakeDurationSecond;
                    vibrato = SmallShakeVibrato;
                    break;
            }

            float strength = Mathf.Max(0f, cameraEntity.cameraCom.camera.orthographicSize * strengthRatio);
            TweenMedium.Shake(cameraEntity, duration, new Vector3(strength, strength, 0f), vibrato,
                cameraEntity.cameraCom.initPos);
            activeShakeTier = tier;
            activeShakeEndTime = world.Time + duration;
        }

        /// <summary>停止相机位置Tween并恢复稳定局部坐标，防止暂停或对象池复用遗留半帧偏移。</summary>
        private void ResetShake()
        {
            if (cameraEntity?.tweenCom != null)
            {
                cameraEntity.tweenCom.StopPosTween();
                if (cameraEntity.tweenCom.transform != null)
                    cameraEntity.tweenCom.transform.localPosition = cameraEntity.cameraCom.initPos;
            }
            activeShakeTier = CameraShakeTier.None;
            activeShakeEndTime = 0f;
        }

        public void CameraMoveSlow() { }

        protected override void OnPause()
        {
            ResetShake();
        }

        protected override void OnDestroy()
        {
            ResetShake();
            cameraEntity = null;
            followDeadZoneHalfExtents = Vector2.zero;
            baseOrthographicSize = 0f;
            targetOrthographicSize = 0f;
            activeShakeEndTime = 0f;
            followCenterInitialized = false;
            activeShakeTier = CameraShakeTier.None;
        }
    }
}
