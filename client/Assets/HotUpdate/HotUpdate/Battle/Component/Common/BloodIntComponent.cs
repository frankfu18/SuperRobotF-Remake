using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrankGame.Battle
{
    public class BloodIntComponent : ComponentBase
    {
        public override ECompType ComponentType => ECompType.Blood;

        public int maxHp;
        public int curHp;
        private bool isDead;

        public BloodIntComponent(Entity _entity) : base(_entity)
        {

        }

        public void SetMaxHp(int _maxHp)
        {
            this.maxHp = _maxHp;
            this.curHp = this.maxHp;
            this.isDead = false;
        }

        public void SetCurHp(int _hp)
        {
            if (this.isDead)
            {
                BattleLog.Error("已经死亡，不能设置了！ id:", this.EntityId);
                return;
            }

            this.curHp = _hp;
            if (this.curHp > this.maxHp)
            {
                this.curHp = this.maxHp;
            }
        }

        public void AddHp(int _value)
        {
            if (this.isDead)
            {
                BattleLog.Error("已经死亡，不能加血了！ id:", this.EntityId);
                return;
            }

            if (_value <= 0)
            {
                return;
            }

            this.curHp += _value;
            if (this.curHp > this.maxHp)
            {
                this.curHp = this.maxHp;
            }

            SendEvent(EBattleEvtType.EntityHpIntHeal, this.EntityId, this.curHp, _value);
        }

        public void ReduceHp(int _damage)
        {
            if (this.isDead)
            {
                BattleLog.Error("已经死亡，不能减血了！ id:", this.EntityId);
                return;
            }

            float lastHp = this.curHp;

            this.curHp -= _damage;
            if(this.curHp <= 0)
            {
                this.curHp = 0;
                this.isDead = true;
            }

            SendEvent(EBattleEvtType.EntityHpIntDamage, this.EntityId, this.curHp, _damage);
        }

        public bool IsMax()
        {
            if (this.maxHp > 0)
            {
                return this.curHp >= this.maxHp;
            }
            
            return true;
        }

        public float GetHpRatio()
        {
            if(this.maxHp > 0)
            {
                return Mathf.Max((float)this.curHp / this.maxHp, 0);
            }

            return 0;
        }

        public bool IsDead()
        {
            return this.isDead;
        }

        protected override string GetString()
        {
            return $"maxHp:{maxHp}, curHp:{curHp},ratio:{(GetHpRatio() * 100).ToString("0.00")}isDead:{isDead}";
        }

        public override void OnRecover()
        {
            this.maxHp = 0;
            this.curHp = 0;
            this.isDead = false;
        }
    }
}
