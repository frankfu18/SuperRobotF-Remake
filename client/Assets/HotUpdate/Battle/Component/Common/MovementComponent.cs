using UnityEngine;

namespace FrankGame.Battle
{
    public class MovementComponent : ComponentBase
    {
        public override ECompType ComponentType => ECompType.Movement;
        public bool moving; // 当前是否正在移动。
        public Vector2 startPos; // 本次移动开始位置。
        public Vector2 moveDir; // 当前逻辑移动方向。
        public float moveSpeed; // 当前逻辑移动速度。
        public bool hasMoveIntent; // 本帧是否提交了移动意图，统一解算完成前不写入逻辑位置。
        public Vector2 desiredPosition; // 本帧未处理碰撞前的期望位置。
        public Vector2 resolvedPosition; // 本帧经过防御塔绕行和单位分离后的待提交位置。
        public Vector2 separationOffset; // 当前分离迭代累计的位置修正，迭代结束后统一应用。
        public int avoidTowerEntityId; // 当前正在绕行的防御塔实体ID；0表示没有绕塔。
        public int avoidTowerSide; // 固定绕行侧；1表示上方，-1表示下方。
        public float avoidTowerTravelDirection; // 进入绕行时的水平前进方向，只能为-1或1。
        public MovementComponent(Entity _entity) : base(_entity)
        {
        }

        public void Move(bool _move)
        {
            this.moving = _move;
        }

        // 每帧开始时冻结位置基准，后续所有单位都基于同一时刻计算移动和碰撞。
        public void BeginFrame(Vector2 position)
        {
            moving = false;
            startPos = position;
            desiredPosition = position;
            resolvedPosition = position;
            separationOffset = Vector2.zero;
            hasMoveIntent = false;
        }

        // 只记录移动意图，等待 FightUnitSystem 完成全体碰撞解算后统一提交。
        public void SetMoveIntent(Vector2 position)
        {
            desiredPosition = position;
            resolvedPosition = position;
            hasMoveIntent = true;
        }

        public void ClearMoveIntent()
        {
            hasMoveIntent = false;
            desiredPosition = startPos;
            resolvedPosition = startPos;
            separationOffset = Vector2.zero;
        }

        // 离开防御塔阻挡范围或停止移动时，清除固定绕行侧。
        public void ClearTowerAvoidance()
        {
            avoidTowerEntityId = 0;
            avoidTowerSide = 0;
            avoidTowerTravelDirection = 0;
        }

        public override void OnRecover()
        {
            moving = false;
            startPos = Vector2.zero;
            moveDir = Vector2.zero;
            moveSpeed = 0;
            hasMoveIntent = false;
            desiredPosition = Vector2.zero;
            resolvedPosition = Vector2.zero;
            separationOffset = Vector2.zero;
            ClearTowerAvoidance();
        }

        protected override string GetString()
        {
            return $"moving:{moving},intent:{hasMoveIntent},startPos:{startPos},desired:{desiredPosition},resolved:{resolvedPosition},moveDir:{moveDir},moveSpeed:{moveSpeed},avoidTower:{avoidTowerEntityId},side:{avoidTowerSide}";
        }
    }
}
