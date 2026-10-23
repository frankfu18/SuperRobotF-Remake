# BuildAssetsWindow

## Summary

`BuildAssetsWindow` 是编辑器内打包入口窗口，负责配置、资源、热更 DLL 与索引的打包流程编排。

## Type Metadata

- Namespace: `PeekabooGame`
- Type: `EditorWindow`
- Layer: `Framework(Editor)`
- Source Path: [`client/Assets/PeekabooGame/Framework/Editor/Build/BuildAssetsWindow.cs`](../../../client/Assets/PeekabooGame/Framework/Editor/Build/BuildAssetsWindow.cs)

## Entry

- Menu: `PeekabooGame/打包/*`
- Window Open Method: `OpenWindow()`

## Key Workflow Methods

| Method | Signature | Returns | When To Use | Notes |
| --- | --- | --- | --- | --- |
| `CreateDir` | `void CreateDir()` | `void` | 打包前准备目录 | 初始化 `source/release/config/bundle/dll` 路径 |
| `BuildConfig` | `void BuildConfig(int _curProgress, int _totalProgerss)` | `void` | 打包配置资源 | 生成配置 AB 与索引 |
| `BuildAsset` | `void BuildAsset(int _curProgress, int _totalProgerss)` | `void` | 打包资源 AB | 生成资源索引 |
| `BuildHotUpdateDll` | `void BuildHotUpdateDll(int _curProgress, int _totalProgerss, bool _all)` | `void` | 打包热更 DLL | 仅热更开关开启时可用 |
| `MergeTotalIndexFile` | `void MergeTotalIndexFile()` | `void` | 合并总索引 | 启动与热更阶段依赖 |
| `CopyToStreaming` | `void CopyToStreaming()` | `void` | 拷贝到 StreamingAssets | 用于本地包运行 |

## Constraints

- 打包前需确保版本名已设置。
- 修改配置后先执行配置生成，再进行打包与索引合并。
