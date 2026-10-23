# HotUpdateSystem

## Summary

`HotUpdateSystem` 负责加载热更新程序集，并通过反射调用 `HotUpdateMain.Run/Login/Logout`。

## Type Metadata

- Namespace: `global`
- Type: `class`
- Layer: `Scripts`
- Source Path: [`client/Assets/Scripts/System/Logic/HotUpdateSystem.cs`](../../../client/Assets/Scripts/System/Logic/HotUpdateSystem.cs)

## Public APIs

| Method | Signature | Returns | When To Use | Notes |
| --- | --- | --- | --- | --- |
| `LoadHotUpdateAssembly` | `void LoadHotUpdateAssembly()` | `void` | 进入热更业务前调用 | 调用 `HotUpdateMain.Run` |
| `Login` | `void Login()` | `void` | 业务登录阶段 | 反射调用 `HotUpdateMain.Login` |
| `Logout` | `void Logout()` | `void` | 业务登出阶段 | 反射调用 `HotUpdateMain.Logout` |

## Dependencies

- `System.Reflection.Assembly`
- `BundleManager`
- `HotUpdateMain`

## Constraints

- `HotUpdateMain` 必须提供 `Run/Login/Logout` 静态方法。
- 热更新业务代码应放在热更新边界目录，避免混入宿主层。
