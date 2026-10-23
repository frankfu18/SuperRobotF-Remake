# ConfigSystem

## Summary

`ConfigSystem` 负责配置表加载、访问与常用读取封装，是业务访问 `CfgMgr` 的统一入口。

## Type Metadata

- Namespace: `global`
- Type: `class`
- Layer: `HotUpdate`
- Source Path: [`client/Assets/HotUpdate/Table/ConfigSystem.cs`](../../../client/Assets/HotUpdate/Table/ConfigSystem.cs)

## Public APIs

| Method | Signature | Returns | When To Use | Notes |
| --- | --- | --- | --- | --- |
| `Init` | `void Init()` | `void` | 热更启动时初始化配置 | 内部设置 `CfgMgr.onLoadConfig` 并触发加载 |
| `GetData` | `CfgMgr GetData()` | `CfgMgr` | 获取原始配置管理器 | 通常优先使用业务封装方法 |
| `GetConfigsByFilter<T>` | `List<T> GetConfigsByFilter<T>(List<T> _list, Func<T, bool> _filter)` | `List<T>` | 过滤配置集合 | 泛型约束 `T : EditorWriter` |
| `GetGlobal` | `object GetGlobal(string _key)` | `object` | 读取全局配置 | 根据类型 ID 转换 |
| `ContainsGlobal` | `bool ContainsGlobal(string _key)` | `bool` | 判断全局键是否存在 | - |
| `GetGlobalInt32/Int64/Bool/String/Float` | `typed getters` | `typed` | 读取强类型全局配置 | 内部依赖 `GetGlobal` |
| `GetLanguage` | `bool GetLanguage(SystemLanguage _language, string _key, out string _value)` | `bool` | 读取多语言文本 | 未命中时回退英文 |

## Static Access

- `ConfigSystem.CurrentData`: 快速访问 `CfgMgr`。

## Dependencies

- `CfgMgr`
- `EventNativeSystem`
- `BundleManager`
- `GameConfig.Table.LocalTablePath`

## Constraints

- 业务侧不要直接读取 `*.bytes`，统一通过本系统与 `CfgMgr`。
- `CfgMgr.Load()` 需在业务读取配置前完成。
