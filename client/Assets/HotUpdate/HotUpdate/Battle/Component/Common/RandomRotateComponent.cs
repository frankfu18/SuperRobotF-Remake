using UnityEngine;

namespace FrankGame.Battle
{
    /// <summary>保存单个伴星模型连续随机摆动的纯表现状态，不参与轨道、碰撞和战斗计算。</summary>
    public sealed class RandomRotateComponent : ComponentBase
    {
        private const float MinimumDurationSecond = 0.01f;
        private const uint FallbackRandomState = 0x6D2B79F5u;

        public override ECompType ComponentType => ECompType.RandomRotate;

        /// <summary>当前 X 轴局部摆动角度，单位：度。</summary>
        public float CurrentXDegree => currentXDegree;

        /// <summary>当前 Z 轴局部摆动角度，单位：度。</summary>
        public float CurrentZDegree => currentZDegree;

        /// <summary>X、Z 轴随机目标相对初始姿态允许达到的最大绝对角度。</summary>
        private float maxAngleDegree;

        /// <summary>每段连续摆动允许使用的最短时间，单位：秒。</summary>
        private float durationMinSecond;

        /// <summary>每段连续摆动允许使用的最长时间，单位：秒。</summary>
        private float durationMaxSecond;

        /// <summary>当前段开始时的 X、Z 姿态；下一段必须从上一段终点继续，避免姿态跳变。</summary>
        private float startXDegree;
        private float startZDegree;

        /// <summary>当前段独立抽取的 X、Z 目标姿态，Y 轴由表现同步代码固定为零。</summary>
        private float targetXDegree;
        private float targetZDegree;

        /// <summary>当前已经插值出的 X、Z 姿态，离屏和失效期间继续保留但停止推进。</summary>
        private float currentXDegree;
        private float currentZDegree;

        /// <summary>当前段的总时长与已经消费的时长，均以战斗缩放时间计。</summary>
        private float segmentDurationSecond;
        private float segmentElapsedSecond;

        /// <summary>组件独占的确定性随机状态，不读取 World、玩家、AI、Stone 或进化随机流。</summary>
        private uint randomState;
        private bool initialized;

        public RandomRotateComponent(Entity entity) : base(entity) { }

        /// <summary>
        /// 使用已校验的表现参数和实体独立种子初始化摆动序列。初始姿态为零，并立即抽取第一段目标。
        /// </summary>
        /// <param name="seed">由本局种子、实体 EID 和伴星身份共同派生的非玩法随机种子。</param>
        /// <param name="angleDegree">X、Z 轴最大绝对摆动角度，单位：度。</param>
        /// <param name="minSecond">单段最短时长，单位：秒。</param>
        /// <param name="maxSecond">单段最长时长，单位：秒。</param>
        public void Initialize(int seed, float angleDegree, float minSecond, float maxSecond)
        {
            maxAngleDegree = Mathf.Max(0f, angleDegree);
            durationMinSecond = Mathf.Max(MinimumDurationSecond, minSecond);
            durationMaxSecond = Mathf.Max(MinimumDurationSecond, maxSecond);
            if (durationMinSecond > durationMaxSecond)
            {
                float swap = durationMinSecond;
                durationMinSecond = durationMaxSecond;
                durationMaxSecond = swap;
            }

            randomState = unchecked((uint)seed);
            if (randomState == 0u)
            {
                randomState = FallbackRandomState;
            }
            startXDegree = 0f;
            startZDegree = 0f;
            currentXDegree = 0f;
            currentZDegree = 0f;
            segmentDurationSecond = 0f;
            segmentElapsedSecond = 0f;
            initialized = true;
            BeginNextSegment();
        }

        /// <summary>
        /// 推进连续摆动。大帧间隔会逐段消费剩余时间，确保每段精确到达目标且随机序列可复现。
        /// </summary>
        /// <param name="deltaTime">本帧允许推进的战斗缩放时间；冻结期间调用方不传入时间。</param>
        public void UpdateRotation(float deltaTime)
        {
            if (!initialized || deltaTime <= 0f)
            {
                return;
            }

            float remainingSecond = deltaTime;
            while (remainingSecond > 0f)
            {
                float segmentRemainSecond = Mathf.Max(0f,
                    segmentDurationSecond - segmentElapsedSecond);
                float consumedSecond = Mathf.Min(remainingSecond, segmentRemainSecond);
                segmentElapsedSecond += consumedSecond;
                remainingSecond -= consumedSecond;

                // SmoothStep 让每段在起点和终点都自然减速；只插值角度，不修改逻辑位置和碰撞。
                float progress = Mathf.Clamp01(segmentElapsedSecond / segmentDurationSecond);
                float smoothProgress = Mathf.SmoothStep(0f, 1f, progress);
                currentXDegree = Mathf.Lerp(startXDegree, targetXDegree, smoothProgress);
                currentZDegree = Mathf.Lerp(startZDegree, targetZDegree, smoothProgress);

                if (segmentElapsedSecond < segmentDurationSecond)
                {
                    break;
                }

                // 段结束时先精确落点，再以该落点作为下一段起点，避免累计误差和跨段跳变。
                currentXDegree = targetXDegree;
                currentZDegree = targetZDegree;
                BeginNextSegment();
            }
        }

        /// <summary>从组件独立随机流抽取下一段 X/Z 目标与持续时间，并继承当前姿态作为起点。</summary>
        private void BeginNextSegment()
        {
            startXDegree = currentXDegree;
            startZDegree = currentZDegree;
            targetXDegree = Mathf.Lerp(-maxAngleDegree, maxAngleDegree, NextRandom01());
            targetZDegree = Mathf.Lerp(-maxAngleDegree, maxAngleDegree, NextRandom01());
            segmentDurationSecond = Mathf.Lerp(durationMinSecond, durationMaxSecond, NextRandom01());
            segmentElapsedSecond = 0f;
        }

        /// <summary>使用 xorshift32 生成稳定的 0～1 随机数，避免消耗任何玩法随机序列。</summary>
        private float NextRandom01()
        {
            uint value = randomState;
            value ^= value << 13;
            value ^= value >> 17;
            value ^= value << 5;
            randomState = value == 0u ? FallbackRandomState : value;
            return (randomState & 0x00FFFFFFu) / 16777215f;
        }

        public override void OnRecover()
        {
            maxAngleDegree = 0f;
            durationMinSecond = 0f;
            durationMaxSecond = 0f;
            startXDegree = 0f;
            startZDegree = 0f;
            targetXDegree = 0f;
            targetZDegree = 0f;
            currentXDegree = 0f;
            currentZDegree = 0f;
            segmentDurationSecond = 0f;
            segmentElapsedSecond = 0f;
            randomState = 0u;
            initialized = false;
        }

        protected override string GetString()
        {
            return $"当前角度:({currentXDegree:F2},0,{currentZDegree:F2}),目标角度:({targetXDegree:F2},0,{targetZDegree:F2})";
        }
    }
}
