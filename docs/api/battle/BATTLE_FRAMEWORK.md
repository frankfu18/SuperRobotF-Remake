# 战斗框架

## 概要

`client/Assets/HotUpdate/Battle` 是当前热更层的战斗框架。后续战斗功能应基于这套框架扩展，不应新建一套平行战斗架构。

这套框架整体是轻量 ECS 风格结构：

```text
BattleManager
  -> World
    -> System
      -> Entity
        -> Component
    -> Medium
    -> Pool
```

## 运行流程

`BattleManager` 是战斗生命周期入口，负责：

- 加载 `Battle` 根 Prefab。
- 创建和销毁 `World`。
- 初始化战斗规则、场景数据和玩家。
- 接收战斗结果回调。

`World` 是单局战斗的运行时上下文，负责：

- 保存战斗时间、速度和运行状态。
- 持有所有战斗系统。
- 驱动 `Update`、`LateUpdate` 和 `FixedUpdate`。
- 管理战斗内部事件和计时器。
- 在战斗结束时清理系统、实体、组件、事件和对象池。

`WorldRoot` 是 Unity 生命周期桥接器，负责把 Unity 的 `Update`、`LateUpdate` 和 `FixedUpdate` 转发给 `World`。

## 分层职责

### System

所有战斗系统都继承自 `SystemBase`。

`System` 负责某个具体领域的规则执行和实体组管理。现有示例包括：

- `PlayerSystem`：玩家战斗阶段流转和战斗结果阶段推进。
- `CardSystem`：卡牌创建、布阵、拖拽、战斗组件添加和卡牌死亡。
- `MapSceneSystem`：战斗地图场景内的交互逻辑。
- `BulletSystem`：子弹实体创建。
- `SkillSystem`：技能实体创建。
- `EffectSystem`：特效实体创建和播放。

新增战斗行为时，应先判断能否归入已有系统。只有在开发者定义好系统名称、职责和边界后，才新增系统。

### Entity

`Entity` 是战斗对象的身份载体。

现有实体类型包括：

- `PlayerEntity`：玩家。
- `CardEntity`：卡牌，如果有。
- `SkillEntity`：技能。
- `BulletEntity`：子弹。
- `EffectEntity`：生成的特效。
- `CameraEntity`：相机管理。

`Entity` 主要负责组合组件和维护生命周期。跨对象的战斗规则应放在 `System` 或 `Medium` 中。

每个具体 Entity 必须单独存放在与类型同名的脚本中，例如 `PlanetEntity` 只能定义在 `PlanetEntity.cs`。禁止把多个 Entity 合并到 `ArenaEntities.cs` 一类聚合脚本中；公共枚举和非 Entity 辅助类型也不能成为继续聚合 Entity 的理由。

新增实体需要：

- 增加 `EEntityType` 枚举值。
- 创建对应 Entity 类。
- 在 `EntityFactory` 中注册。
- 实现 `OnCreate`、`OnDestroy` 和 `OnRecover`。
- 对应 `System` 必须提供清晰的创建方法，例如 `CreateBallEntity(...)`。
- 创建方法内部负责调用 `AddEntity<T>(EEntityType.Xxx)`。
- 创建方法负责初始化该实体创建流程需要的参数、临时组件和表现组件。
- 创建方法必须返回创建出的具体 `Entity`，方便调用方继续设置位置、运动或其他运行时数据。

### Component

`Component` 用于保存实体状态和局部能力。

每个具体 Component 必须单独存放在与类型同名的脚本中，例如 `PlanetComponent` 只能定义在 `PlanetComponent.cs`。禁止把多个 Component 合并到 `ArenaEntityComponents.cs` 一类聚合脚本中；拆分只改变源码组织，不改变 `ECompType`、Factory 注册、对象池和 Entity 组合关系。

现有组件示例包括：

- `TransformComponent`：位置数据。
- `CampComponent`：阵营数据。
- `BloodComponent`：血量数据和血量变化事件。
- `AttributeComponent`：战斗属性。
- `CardComponent`：卡牌运行时数据。
- `SkillComponent`：技能释放规则。
- `AssetComponent`：战斗资源加载和回收。
- `AttackTargetComponent`：攻击目标缓存。
- `BuffEffectComponent` 和 `BuffTriggerComponent`：Buff 运行时行为。

新增组件需要：

- 增加 `ECompType` 枚举值。
- 创建对应 Component 类。
- 在 `ComponentFactory` 中注册。
- 实现 `OnRecover`，清理复用时的旧状态。

### Medium

`Medium` 类是共享战斗规则工具层，适合放无状态或轻状态的领域计算。

现有示例包括：

- `DamageMedium`：伤害、治疗、命中计算。
- `CampMedium`：阵营判断。
- `MapMedium`：地图操作。
- `SkillMedium`：技能目标选择。
- `TweenMedium`：补间表现辅助。

共享战斗规则不应散落在 UI、Panel 或无关 Manager 中。

### Pool

战斗实体和组件都走对象池：

- `EntityPool`
- `ComponentPool`
- `RefKeyObjectPool`

由于对象会被复用，每个新增 Entity 和 Component 都必须在 `OnRecover` 中清理运行时状态。

## 资源链路

战斗表现资源应使用现有战斗资源链路：

```text
AssetComponent
  -> BattleAssetManager
    -> BattleGameObjectPool
      -> AssetSystem / AssetHandler
```

战斗逻辑应使用配置中的资源 ID，不应硬编码 Prefab 路径。

## 事件规则

战斗事件分为两类。

### 战斗内部事件

战斗内部事件使用 `EBattleEvtType`，例如：

- `EntityHpDamage`
- `EntityHpHeal`
- `EntityAttributeChange`
- `AttackMiss`

这些事件用于战斗框架内部通信。

### 外部或 UI 事件

外部或 UI 事件使用 `EventConst`，例如：

- `Battle_Start`
- `Battle_Prepare`
- `Battle_PlayerStateChange`
- `Battle_StartFight`

这些事件用于通知 UI 面板或业务模块。

### 集中注册规则

所有战斗内部事件注册必须集中在 `World` 相关文件中。

`World` 是战斗事件中枢，负责注册战斗内部事件，并把事件回调分发到对应系统的公开方法中。

规则：

- `Entity` 和 `Component` 可以发送战斗事件。
- 复杂跨系统处理应通过 `World` 分发。
- 战斗内部事件优先使用 `EBattleEvtType`。
- 面向 UI 的变化应通过 `EventConst` 发送。
- 如果事件注册过多，可以拆到 `World.Event.cs` 这类 partial 文件中，但 `World` 仍然是事件入口。

## Entity、Component 和 System 定义权

后续战斗开发中，`Entity`、`Component` 和 `System` 的定义权归开发者。

AI 只在已经确认的 `Entity`、`Component` 和 `System` 结构内实现逻辑。

如果实现过程中缺少某个 Entity、Component 或 System，AI 必须先列出缺口和建议职责，等待开发者确认后再新增。

## 开发规则

后续战斗开发应遵守：

- 战斗规则放在 `Battle/System`、`Battle/Entity`、`Battle/Component` 和 `Battle/Medium` 下。
- 不在 `BattlePanel` 中写战斗结果结算。
- 不让零散 MonoBehaviour 独立运行核心战斗规则。
- 新增实体和组件必须注册到对应枚举和 Factory。
- 所有 `Entity` 创建入口统一收口到管理该实体的 `System`。
- 外部模块不直接调用 `AddEntity` 创建战斗实体，也不绕过所属 `System` 创建实体。
- 如果新实体没有对应 `System.Create...` 方法，应先补该创建方法，再写调用逻辑。
- 战斗表现资源使用 `AssetComponent` 和 `BattleAssetManager`。
- 通过事件通知 UI，不从战斗逻辑直接修改 Panel 字段。
- 优先扩展现有战斗框架，不创建平行框架。

## 一句话总结

这套战斗框架是由 `World` 驱动的轻量 ECS 风格框架：`System` 执行规则，`Entity` 表示战斗对象，`Component` 保存状态和能力，`Medium` 承载共享规则计算，对象池复用运行时对象。
