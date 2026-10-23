using System.Collections;
using System.Collections.Generic;
using editor.cfg.skill;

namespace FrankGame.Battle
{
    public class BuffDestroyImmediate : BuffDestroyBase<ImmediateDestroy>
    {
        public override void SetData(ImmediateDestroy _immediateDestroy, System.Action _onDestoryFunc)
        {
            base.SetData(_immediateDestroy, _onDestoryFunc);
        }

        protected override bool OnCheckDestroy()
        {
            return true;
        }

        protected override void OnReset()
        {

        }

        protected override string GetString()
        {
            return "立刻销毁";
        }

        protected override void OnClear()
        {
            
        }
    }
}