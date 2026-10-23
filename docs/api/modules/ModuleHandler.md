# ModuleHandler

## Summary

`ModuleHandler` 负责业务模块编排，包括初始化、登录链路、登出与释放。

## Type Metadata

- Namespace: `global`
- Type: `class`
- Layer: `HotUpdate`
- Source Path: [`client/Assets/HotUpdate/Module/ModuleHandler.cs`](../../../client/Assets/HotUpdate/Module/ModuleHandler.cs)

## Public APIs

| Method | Signature | Returns | When To Use | Notes |
| --- | --- | --- | --- | --- |
| `Run` | `void Run()` | `void` | 热更运行阶段 | 执行模块 `Init` 和 `LoadConfigDone` |
| `Login` | `void Login()` | `void` | 登录流程 | 加载数据库后发送登录成功事件 |
| `Logout` | `void Logout()` | `void` | 登出流程 | 调用全部模块 `Logout` |
| `Dispose` | `void Dispose()` | `void` | 退出或销毁阶段 | 调用全部模块 `Dispose` |

## Managed Lifecycle

- `Init`
- `LoadConfigDone`
- `Login`
- `LoginDone`
- `Logout`
- `Dispose`

## Constraints

- 新模块必须加入 `moduleList` 才能参与生命周期调度。
- 模块监听与资源应在 `Logout/Dispose` 阶段成对清理。
