# AssetSystem

## Summary

`AssetSystem` 提供统一资源加载与卸载入口，屏蔽 Bundle 路径与分层细节。

## Type Metadata

- Namespace: `global`
- Type: `class`
- Layer: `Scripts`
- Source Path: [`client/Assets/Scripts/System/Logic/AssetSystem.cs`](../../../client/Assets/Scripts/System/Logic/AssetSystem.cs)

## Public APIs

| Method | Signature | Returns | When To Use | Notes |
| --- | --- | --- | --- | --- |
| `LoadSound` | `void LoadSound(string _name, Action<AudioClip> _onLoadDone)` | `void` | 加载音频资源 | 内部走 `LoadAsset<AudioClip>` |
| `UnloadSound` | `void UnloadSound(string _name, int _unuseCount = 1, bool _unloadBundle = false)` | `void` | 卸载音频资源 | 可选卸载 Bundle |
| `LoadAsset<T>` | `void LoadAsset<T>(EResType _resType, string _assetPath, string _suffix, Action<T> _onLoadDone)` | `void` | 加载单资源 | 自动计算 bundleName |
| `LoadSubAssets` | `void LoadSubAssets(EResType _resType, string _assetPath, string _suffix, Action<object[]> _onLoadDone)` | `void` | 加载子资源 | 适用于图集等 |
| `LoadResources<T>` | `T LoadResources<T>(string _path)` | `T` | 明确需要 Resources 时 | 非默认推荐路径 |
| `UnloadAsset` | `void UnloadAsset(EResType _resType, string _assetPath, string _suffix, int _unuseCount = 1, bool _unloadBundle = false)` | `void` | 卸载资源引用 | Local 模式下直接返回 |
| `UnloadAllUnuse` | `void UnloadAllUnuse()` | `void` | 批量清理无用资源 | Local 模式下直接返回 |

## Dependencies

- `ResConfig.ResInfoDic`
- `AssetManager`
- `GameConfig.Path.LocalResRoot`

## Constraints

- 资源类型必须先在 `ResConfig.ResInfoDic` 定义映射。
- 业务侧不要自行拼接 Bundle 路径，统一走本系统。
