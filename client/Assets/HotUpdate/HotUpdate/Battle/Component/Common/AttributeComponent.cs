using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using editor.cfg.attribute;
using editor.cfg.common;

namespace FrankGame.Battle
{
    public class AttributeComponent : ComponentBase, ICompClone<AttributeComponent>
    {
        public override ECompType ComponentType => ECompType.Attribute;

        public Dictionary<EAttributeType, stAttrInfo> baseAttrDic = new Dictionary<EAttributeType, stAttrInfo>();
        public Dictionary<EAttributeType, stAttrInfo> addAttrDic = new Dictionary<EAttributeType, stAttrInfo>();

        public AttributeComponent(Entity _entity) : base(_entity)
        {

        }

        // 配置表落到实体上的初始属性统一走 SetValue，写入 baseAttrDic。
        // 技能、Buff、临时强化等运行时变化才允许走 AddValue/SubValue。
        public void SetValue(EAttributeType _attrType, long _amount)
        {
            stAttrInfo attrInfo;
            if (!baseAttrDic.TryGetValue(_attrType, out attrInfo))
            {
                attrInfo = new stAttrInfo(_attrType);

                baseAttrDic.Add(_attrType, attrInfo);
            }

            attrInfo.SetLong(_amount);

            SendEvent(EBattleEvtType.EntityAttributeChange, this.EntityId, _attrType);
        }

        public void SetValues(List<AttributeLongNumber> _attrs)
        {
            if (_attrs == null)
            {
                return;
            }

            for (int i = 0; i < _attrs.Count; i++)
            {
                AttributeLongNumber attr = _attrs[i];
                if (attr == null || attr.number == null)
                {
                    continue;
                }

                SetValue(attr.type, attr.number.amount);
            }
        }

        /// <summary>读取配置中的多态属性结构并写入基础属性。</summary>
        public void SetValue(AttributeInfo _info)
        {
            ApplyAttributeInfo(_info, false);
        }

        /// <summary>读取英雄升级或强化配置并写入局内增量属性。</summary>
        public void AddValue(AttributeInfo _info)
        {
            ApplyAttributeInfo(_info, true);
        }

        private void ApplyAttributeInfo(AttributeInfo _info, bool _add)
        {
            if (_info == null || _info is editor.cfg.attribute.None)
            {
                return;
            }

            if (_info is editor.cfg.attribute.And andInfo)
            {
                foreach (AttributeInfo child in andInfo.attributes)
                {
                    ApplyAttributeInfo(child, _add);
                }
                return;
            }

            EAttributeType type;
            long amount;
            if (_info is AttributeLongNumber longInfo && longInfo.number != null)
            {
                type = longInfo.type;
                amount = longInfo.number.amount;
            }
            else if (_info is AttributeNumber numberInfo && numberInfo.number != null)
            {
                type = numberInfo.type;
                amount = ConvertConfigNumber(type, numberInfo.number);
            }
            else
            {
                return;
            }

            if (_add) AddValue(type, amount); else SetValue(type, amount);
        }

        private static long ConvertConfigNumber(EAttributeType _type, editor.cfg.common.XNumber _number)
        {
            if (_number is editor.cfg.common.XLong longNumber)
            {
                return longNumber.amount;
            }

            if (!(_number is editor.cfg.common.XFloat floatNumber))
            {
                return 0;
            }

            float scale = 1f;
            if (_type == EAttributeType.AttackSpeed) scale = 1000f;
            else if (_type == EAttributeType.MoveSpeed || _type == EAttributeType.AttackDistance) scale = 100f;
            return Mathf.RoundToInt(floatNumber.amount * scale);
        }

        // 上阵数据传入的属性快照已经是最终基础属性，统一写入 baseAttrDic。
        public void SetValues(Dictionary<EAttributeType, long> _attrs)
        {
            if (_attrs == null)
            {
                return;
            }

            foreach (var attr in _attrs)
            {
                SetValue(attr.Key, attr.Value);
            }
        }

        // 运行时加成只写入 addAttrDic，方便 ClearAddAttrs 清理临时效果时不影响配置基础属性。
        public void AddValue(EAttributeType _attrType, long _amount)
        {
            stAttrInfo attrInfo;
            if (!addAttrDic.TryGetValue(_attrType, out attrInfo))
            {
                attrInfo = new stAttrInfo(_attrType);

                addAttrDic.Add(_attrType, attrInfo);
            }

            attrInfo.AddLong(_amount);

            SendEvent(EBattleEvtType.EntityAttributeChange, this.EntityId, _attrType);
        }

        // 运行时减少只作用于 addAttrDic，不会改动配置写入的基础属性。
        public void SubValue(EAttributeType _attrType, long _amount)
        {
            stAttrInfo attrInfo;
            if (!addAttrDic.TryGetValue(_attrType, out attrInfo))
            {
                return;
            }

            attrInfo.SubLong(_amount);

            if (attrInfo.GetLong() <= 0)
            {
                addAttrDic.Remove(_attrType);
            }

            SendEvent(EBattleEvtType.EntityAttributeChange, this.EntityId, _attrType);
        }

        public long GetLongValue(EAttributeType _attrType)
        {
            long value = 0;
            stAttrInfo attrInfo;
            if (baseAttrDic.TryGetValue(_attrType, out attrInfo))
            {
                value += attrInfo.GetLong();
            }
            if (addAttrDic.TryGetValue(_attrType, out attrInfo))
            {
                value += attrInfo.GetLong();
            }

            return value;
        }

        public long GetBaseLongValue(EAttributeType _attrType)
        {
            long value = 0;
            stAttrInfo attrInfo;
            if (baseAttrDic.TryGetValue(_attrType, out attrInfo))
            {
                value += attrInfo.GetLong();
            }

            return value;
        }

        public int GetIntValue(EAttributeType _attrType)
        {
            return (int)GetLongValue(_attrType);
        }

        public float GetFloatValue(EAttributeType _attrType)
        {
            float value = 0;
            stAttrInfo attrInfo;
            if (baseAttrDic.TryGetValue(_attrType, out attrInfo))
            {
                value += attrInfo.GetFloat();
            }
            if (addAttrDic.TryGetValue(_attrType, out attrInfo))
            {
                value += attrInfo.GetFloat();
            }

            return value;
        }
        
        public float GetRatio(EAttributeType _attrType)
        {
            float value = 0;
            stAttrInfo attrInfo;
            if (baseAttrDic.TryGetValue(_attrType, out attrInfo))
            {
                value += attrInfo.GetRatio();
            }
            if (addAttrDic.TryGetValue(_attrType, out attrInfo))
            {
                value += attrInfo.GetRatio();
            }

            return value;
        }

        public float GetSecond(EAttributeType _attrType)
        {
            float value = 0;
            stAttrInfo attrInfo;
            if (baseAttrDic.TryGetValue(_attrType, out attrInfo))
            {
                value += attrInfo.GetSecond();
            }
            if (addAttrDic.TryGetValue(_attrType, out attrInfo))
            {
                value += attrInfo.GetSecond();
            }

            return value;
        }

        public float GetRoundPreSecondValue(EAttributeType _attrType)
        {
            float value = 0;
            stAttrInfo attrInfo;
            if (baseAttrDic.TryGetValue(_attrType, out attrInfo))
            {
                value += attrInfo.GetRoundPreSecond();
            }
            if (addAttrDic.TryGetValue(_attrType, out attrInfo))
            {
                value += attrInfo.GetRoundPreSecond();
            }

            return value;
        }

        private float ApplyAddSubRatio(float _baseValue, EAttributeType _addType, EAttributeType _subType)
        {
            float addRatio = GetRatio(_addType) - GetRatio(_subType);
            return _baseValue * (1 + addRatio);
        }

        public float GetAttack()
        {
            float value = GetIntValue(EAttributeType.Attack);
            // 攻击力支持 AttackAdd / AttackSub 运行时比例修正。
            return ApplyAddSubRatio(value, EAttributeType.AttackAdd, EAttributeType.AttackSub);
        }
        
        public float GetAttackSpeed()
        {
            float value = GetSecond(EAttributeType.AttackSpeed);
            float addRatio = GetRatio(EAttributeType.AttackSpeedAdd) - GetRatio(EAttributeType.AttackSpeedSub);

            return Mathf.Max(0.1f, value * (1 - addRatio));
        }

        public float GetMoveSpeed()
        {
            float value = GetFloatValue(EAttributeType.MoveSpeed);
            // 角色移动速度支持 MoveSpeedAdd / MoveSpeedSub 运行时比例修正。
            return ApplyAddSubRatio(value, EAttributeType.MoveSpeedAdd, EAttributeType.MoveSpeedSub);
        }

        public float GetAttackDistance()
        {
            return GetFloatValue(EAttributeType.AttackDistance);
        }

        public int GetHp()
        {
            float value = GetIntValue(EAttributeType.HP);
            // 生命上限支持 HPAdd / HPSub 运行时比例修正。
            return Mathf.RoundToInt(ApplyAddSubRatio(value, EAttributeType.HPAdd, EAttributeType.HPSub));
        }
        public float GetMoveSpeedReduceRatio()
        {
            return Mathf.Clamp01(GetRatio(EAttributeType.MoveSpeedSub));
        }

        public void ClearAddAttrs()
        {
            this.addAttrDic.Clear();
        }
        
        protected override string GetString()
        {
            string str = "baseAttr:";
            foreach (var attrInfo in baseAttrDic.Values)
            {
                str += $"[{attrInfo.AttrType}|{LanguageSystem.Instance.GetLanguage(attrInfo.GetAttrName())}:{attrInfo.GetValueString()}] ";
            }
            str += "addAttr:";
            foreach (var attrInfo in addAttrDic.Values)
            {
                str += $"[{attrInfo.AttrType}|{LanguageSystem.Instance.GetLanguage(attrInfo.GetAttrName())}:{attrInfo.GetValueString()}] ";
            }

            return str;
        }

        public override void OnRecover()
        {
            this.baseAttrDic.Clear();
            this.addAttrDic.Clear();
        }

        public void Clone(AttributeComponent _t)
        {
            this.baseAttrDic.Clear();
            foreach (var attr in _t.baseAttrDic.Values)
            {
                stAttrInfo attrInfo = new stAttrInfo(attr.AttrType, attr.GetLong());

                this.baseAttrDic.Add(attrInfo.AttrType, attrInfo);
            }
            this.addAttrDic.Clear();
            foreach (var attr in _t.addAttrDic.Values)
            {
                stAttrInfo attrInfo = new stAttrInfo(attr.AttrType, attr.GetLong());

                this.addAttrDic.Add(attrInfo.AttrType, attrInfo);
            }
        }
    }
}
