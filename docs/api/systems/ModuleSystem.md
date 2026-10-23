# ModuleSystem

## Summary

`ModuleSystem` 作为宿主层模块入口，负责将登录/登出动作转发到热更新业务层。

## Type Metadata

- Namespace: `global`
- Type: `class`
- Layer: `Scripts`
- Source Path: [`client/Assets/Scripts/System/Logic/ModuleSystem.cs`](../../../client/Assets/Scripts/System/Logic/ModuleSystem.cs)

## Public APIs

| Method | Signature | Returns | When To Use | Notes |
| --- | --- | --- | --- | --- |
| `Login` | `void Login()` | `void` | 登录链路触发时调用 | 内部调用 `HotUpdateSystem.Login()` |
| `Logout` | `void Logout()` | `void` | 登出链路触发时调用 | 首次未登录时直接返回 |

## Events

- `SysEvent.Login_Out`：登出后发送

## Dependencies

- `HotUpdateSystem`
- `EventNativeSystem`

## Constraints

- 业务模块登录逻辑应由热更新层处理，本系统仅做状态与流程转发。
