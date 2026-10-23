using System.Collections.Generic;
using UnityEngine;

namespace FrankGame.Battle
{
    /// <summary>战斗模型使用的通用动画语义。</summary>
    public enum EAnimType
    {
        Idle,
        Move,
        Attack,
        Dead,
    }

    /// <summary>一个动画语义对应的资源动画名和循环方式。</summary>
    public struct AnimInfo
    {
        public string animName; // 模型资源内的动画名称。
        public bool loop; // 动画是否循环播放。
    }

    /// <summary>
    /// 英雄和小兵的模型动画表现组件。
    /// FightUnitSystem 只下发通用动画语义，组件负责转换成 ModelPoint 的实际动画名。
    /// </summary>
    public class ModelPointComponent : ComponentBase
    {
        public override ECompType ComponentType => ECompType.ModelPoint;
        public ModelPoint modelPoint; // 资源实例上的模型控制节点，由资源加载完成回调赋值。
        private EAnimType currentAnimType; // 当前正在播放的动画语义。
        private EAnimType pendingAnimType; // 一次性动作结束后需要恢复的循环动画语义。
        private bool hasCurrentAnimation; // 是否已经播放过有效动画。
        private bool hasPendingAnimation; // 是否存在等待一次性动作结束后播放的动画。
        private bool actionPlaying; // 攻击等一次性动作播放期间阻止 Move 或 Idle 提前覆盖。
        private int playVersion; // 动画播放版本，用于丢弃回收或重播后的旧完成回调。
        private static readonly Dictionary<EAnimType, AnimInfo> animInfoDic = new Dictionary<EAnimType, AnimInfo>()
        {
            { EAnimType.Idle, new AnimInfo(){ animName = "idle", loop = true } },
            { EAnimType.Move, new AnimInfo(){ animName = "move", loop = true } },
            { EAnimType.Dead, new AnimInfo(){ animName = "die", loop = false } },
            { EAnimType.Attack, new AnimInfo(){ animName = "attack", loop = false } },
        }; // 通用动画语义到角色模型动画名的映射。
        public ModelPointComponent(Entity entity) : base(entity)
        {
        }

        /// <summary>按通用动画语义播放角色模型动画。</summary>
        public void PlayAnim(EAnimType _animType, System.Action _onPlayDone = null)
        {
            if (!animInfoDic.TryGetValue(_animType, out var animInfo))
            {
                return;
            }

            if (string.IsNullOrEmpty(animInfo.animName))
            {
                return;
            }

            if (modelPoint == null)
            {
                return;
            }

            // 攻击动画属于一次性动作，动作完成前只记录最新的循环动画，不立即覆盖当前画面。
            if (actionPlaying && animInfo.loop)
            {
                pendingAnimType = _animType;
                hasPendingAnimation = true;
                return;
            }

            // Move 和 Idle 是循环动画，相同语义不重复 SetAnimation，避免每帧重置到第零帧。
            if (hasCurrentAnimation && currentAnimType == _animType && animInfo.loop)
            {
                return;
            }

            currentAnimType = _animType;
            hasCurrentAnimation = true;
            actionPlaying = !animInfo.loop;

            if (_animType == EAnimType.Dead)
            {
                hasPendingAnimation = false;
            }

            int currentVersion = ++playVersion;

            PlayAnim(animInfo.animName, animInfo.loop, () =>
            {
                if (currentVersion != playVersion)
                {
                    return;
                }

                _onPlayDone?.Invoke();

                if (animInfo.loop)
                {
                    return;
                }

                actionPlaying = false;
                hasCurrentAnimation = false;

                if (hasPendingAnimation)
                {
                    EAnimType nextAnimType = pendingAnimType;
                    hasPendingAnimation = false;
                    PlayAnim(nextAnimType);
                }
            });
        }

        /// <summary>按资源内的动画名称播放，并在播放结束时回调。</summary>
        public void PlayAnim(string _animName, bool _loop = false, System.Action _onPlayDone = null)
        {
            if (modelPoint == null)
            {
                return;
            }

            modelPoint.PlayAction(_animName, _loop, _ =>
            {
                _onPlayDone?.Invoke();
            });
        }

        public override void OnRecover()
        {
            modelPoint = null;
            currentAnimType = EAnimType.Idle;
            pendingAnimType = EAnimType.Idle;
            hasCurrentAnimation = false;
            hasPendingAnimation = false;
            actionPlaying = false;
            playVersion++;
        }

        protected override string GetString()
        {
            return "";
        }
    }
}
