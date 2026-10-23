using editor.cfg.skill;
using UnityEngine;

namespace FrankGame.Battle
{
    public class BuffDestroyDuration : BuffDestroyBase<DurationDestroy>
    {
        private float durationSecond; // 配置的完整持续时间。
        private float remainSecond; // 当前剩余时间。
        public override void SetData(DurationDestroy _durationDestroy, System.Action _onDestoryFunc)
        {
            base.SetData(_durationDestroy, _onDestoryFunc);

            this.durationSecond = Mathf.Max(0, _durationDestroy.durationSecond);
            this.remainSecond = this.durationSecond;
        }

        protected override void OnUpdate(float _deltaTime)
        {
            if (this.remainSecond <= 0)
            {
                return;
            }

            this.remainSecond = Mathf.Max(0, this.remainSecond - Mathf.Max(0, _deltaTime));
            if (this.remainSecond <= 0)
            {
                ExecuteDestroy();
            }
        }

        protected override bool OnCheckDestroy()
        {
            return this.durationSecond <= 0;
        }

        protected override void OnReset()
        {
            this.remainSecond = this.durationSecond;
        }

        protected override string GetString()
        {
            return $"剩余:{this.remainSecond:0.##}秒";
        }

        protected override void OnClear()
        {
            this.durationSecond = 0;
            this.remainSecond = 0;
        }
    }
}
