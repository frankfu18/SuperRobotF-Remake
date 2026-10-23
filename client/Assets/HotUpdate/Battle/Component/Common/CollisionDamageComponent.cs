namespace FrankGame.Battle
{
    /// <summary>保存实体用于逻辑碰撞的最终伤害，以及伤害结算所需的玩家归属。</summary>
    public class CollisionDamageComponent : ComponentBase
    {
        /// <summary>获取组件工厂和对象池使用的碰撞伤害组件类型。</summary>
        public override ECompType ComponentType => ECompType.CollisionDamage;

        /// <summary>本实体一次有效碰撞造成的最终基础伤害。</summary>
        public float damage;

        /// <summary>伤害归属的 PlayerEntity EID；0 表示环境伤害。</summary>
        public int attackerPlayerEid;

        public CollisionDamageComponent(Entity entity) : base(entity) { }

        /// <summary>更新碰撞伤害快照；阶段属性刷新时必须同步调用。</summary>
        public void SetDamage(float value, int playerEid)
        {
            damage = value > 0f ? value : 0f;
            attackerPlayerEid = playerEid;
        }

        public override void OnRecover()
        {
            damage = 0f;
            attackerPlayerEid = 0;
        }

        protected override string GetString()
        {
            return $"damage:{damage},attackerPlayerEid:{attackerPlayerEid}";
        }
    }
}
