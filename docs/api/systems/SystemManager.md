# SystemManager

## Summary

`SystemManager` 负责系统初始化顺序、初始化完成回调与每帧更新调度。

## Type Metadata

- Namespace: `global`
- Type: `class`
- Layer: `Scripts`
- Source Path: [`client/Assets/Scripts/System/SystemManager.cs`](../../../client/Assets/Scripts/System/SystemManager.cs)

## Lifecycle

1. `Init(MonoBehaviour mono)`
2. `Run()`
3. `Update(float deltaTime, float unscaledDeltaTime)`

## Public APIs

| Method | Signature | Returns | When To Use | Notes |
| --- | --- | --- | --- | --- |
| `Init` | `void Init(MonoBehaviour _mono)` | `void` | 游戏入口初始化系统 | 依序调用每个系统 `Init` |
| `Run` | `void Run()` | `void` | 所有系统 `Init` 后调用 | 仅执行一次 `InitDone` |
| `Update` | `void Update(float _deltaTime, float _unscaledDeltaTime)` | `void` | 每帧调用 | 仅在 `Run` 后生效 |

## Dependencies

- `ISystem`
- `AssetSystem`
- `TimeSystem`
- `PlayerPrefsSystem`
- `AudioSystem`
- `ModuleSystem`
- `HttpSystem`

## Constraints

- 系统初始化顺序由 `systemList` 决定，调整顺序前需评估依赖关系。
