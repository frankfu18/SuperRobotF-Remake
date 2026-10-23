# Module Workflow

## Summary

业务模块统一继承 `ModuleBase<T>`，通过 [ModuleHandler](./ModuleHandler.md) 进入统一生命周期，避免模块各自维护独立流程。

## Type Metadata

- Layer: `HotUpdate`
- Core Type: `ModuleBase<T>`
- Dispatcher: [ModuleHandler](./ModuleHandler.md)
- Source Path: [`client/Assets/HotUpdate/Module/ModuleHandler.cs`](../../../client/Assets/HotUpdate/Module/ModuleHandler.cs)

## Lifecycle Contract

模块必须接入以下链路：

1. `Init`
2. `LoadConfigDone`
3. `Login`
4. `LoginDone`
5. `Logout`
6. `Dispose`

## API Entry

- Module Dispatcher API: [ModuleHandler.md](./ModuleHandler.md)
- Module Base Source: [`client/Assets/HotUpdate/Module/ModuleBase.cs`](../../../client/Assets/HotUpdate/Module/ModuleBase.cs)
- Module Interface Source: [`client/Assets/HotUpdate/Module/IModule.cs`](../../../client/Assets/HotUpdate/Module/IModule.cs)

## Collaboration Rules

- 模块间通信优先事件系统，不使用隐式单例耦合。
- 模块状态存储在模块自身，跨模块依赖通过系统接口或事件解耦。
- 生命周期中注册的监听必须在对应退出节点反注册。

## Constraints

- 新增模块后必须在 `ModuleHandler.moduleList` 接入调度。
- 热更新业务逻辑放在 [`client/Assets/HotUpdate/Game/Modules`](../../../client/Assets/HotUpdate/Game/Modules)。
- 禁止把业务模块逻辑写入框架层。
