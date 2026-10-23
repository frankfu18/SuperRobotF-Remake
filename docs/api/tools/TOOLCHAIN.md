# Toolchain

## Summary

本分册记录配置生成、协议生成、资源打包等工具入口与使用顺序，确保构建链路可复现。

## Config & Protocol Generation

### 配置生成

- Command: [`client/gen_config.bat`](../../../client/gen_config.bat)
- Input: [`cfg/cfg_sc.xml`](../../../cfg/cfg_sc.xml)（聚合配置定义）
- Output:
  - [`client/Assets/Res/config/*.bytes`](../../../client/Assets/Res/config)
  - [`client/Assets/Perfect/EditorConfig/Gen/cfg/*.cs`](../../../client/Assets/Perfect/EditorConfig/Gen/cfg)

### 协议生成

- Command: [`client/gen_msg.bat`](../../../client/gen_msg.bat)

### 一键生成

- Command: [`client/gen_all.bat`](../../../client/gen_all.bat)

### 依赖工具

- [`tool/perfect_gen.jar`](../../../tool/perfect_gen.jar)

## Build Pipeline Notes

- Build API Doc: [BuildAssetsWindow.md](./BuildAssetsWindow.md)
- Build Source Path: [`client/Assets/PeekabooGame/Framework/Editor/Build/BuildAssetsWindow.cs`](../../../client/Assets/PeekabooGame/Framework/Editor/Build/BuildAssetsWindow.cs)
- 配置资源打包由编辑器工具链路执行（`BuildAssetsWindow.BuildConfig`）。
- 配置索引会合并进总索引，供启动期资源解析与热更使用。

## Constraints

- 修改 [`cfg/*.xml`](../../../cfg) 或 [`cfg/*.xlsx`](../../../cfg) 后，必须执行生成并验证配置可读取。
- 自动生成文件不要手写业务逻辑。
