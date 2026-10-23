using UnityEngine;

namespace FrankGame.Battle
{
    public partial class World
    {
        private void OnRegisterListener()
        {
            AddEvent(EventConst.Battle_Pause, OnBattlePause);
            AddEvent(EventConst.Battle_Resume, OnBattleResume);
            AddEvent(EventConst.Battle_FightStart, OnStartFight);

            AddEvent(EBattleEvtType.EntityHpDamage, this, OnEntityHpDamage);
            AddEvent(EBattleEvtType.EntityHpHeal, this, OnEntityHpHeal);
        }

        private void OnStartFight(params object[] args)
        {
            // BeginFight();
        }

        private void OnBattlePause(params object[] args)
        {
            Pause();
        }

        private void OnBattleResume(params object[] args)
        {
            // Resume();
        }

        private void OnEntityHpDamage(params object[] _args)
        {
            int eid = (int)_args[0];
            float curHp = (float)_args[1];
            float lastHp = (float)_args[2];
            
        }

        private void OnEntityHpHeal(params object[] _args)
        {
            int eid = (int)_args[0];
            float curHp = (float)_args[1];
            float realAddHp = (float)_args[2];

            bool self = false;

            if (realAddHp > 0)
            {
                if (TryGetEntity(eid, out Entity entity))
                {
                    
                }
            }
        }

        
    }
}
