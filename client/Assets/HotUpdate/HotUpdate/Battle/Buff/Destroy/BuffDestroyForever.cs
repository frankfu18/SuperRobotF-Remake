using editor.cfg.skill;

namespace FrankGame.Battle
{
    public class BuffDestroyForever : BuffDestroyBase<Forever>
    {
        public override void SetData(Forever _forever, System.Action _onDestoryFunc)
        {
            base.SetData(_forever, _onDestoryFunc);
        }

        protected override bool OnCheckDestroy()
        {
            return false;
        }

        protected override void OnReset()
        {
            
        }

        protected override string GetString()
        {
            return "永久";
        }

        protected override void OnClear()
        {
            
        }
    }
}