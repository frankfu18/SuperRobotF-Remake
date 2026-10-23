# Systems Overview

## Summary

系统层提供跨业务复用的基础能力，由 [SystemManager](./SystemManager.md) 统一管理生命周期。业务代码应优先通过系统接口访问能力，不直接绕开底层实现。

## Type Metadata

- Layer: `Scripts/Framework`
- Entry: [SystemManager](./SystemManager.md)
- Lifecycle: `Init -> InitDone -> Update -> Dispose`

## Core Systems

### SystemManager

- API Doc: [SystemManager.md](./SystemManager.md)
- Source Path: [`client/Assets/Scripts/System/SystemManager.cs`](../../../client/Assets/Scripts/System/SystemManager.cs)
- Responsibility: 管理系统注册顺序与生命周期调用。
- Built-in Order:
  - [AssetSystem](./AssetSystem.md)
  - [TimeSystem](./TimeSystem.md)
  - [PlayerPrefsSystem](./PlayerPrefsSystem.md)
  - [AudioSystem](./AudioSystem.md)
  - [ModuleSystem](./ModuleSystem.md)
  - [HttpSystem](./HttpSystem.md)

### AssetSystem

- API Doc: [AssetSystem.md](./AssetSystem.md)
- Source Path: [`client/Assets/Scripts/System/Logic/AssetSystem.cs`](../../../client/Assets/Scripts/System/Logic/AssetSystem.cs)
- Responsibility: 统一资源加载、卸载、子资源读取、音频资源读取。
- Load Mode: `ELoadMode.Local / AssetBundle / Net`
- Related: `AssetManager`, `BundleManager`

### TimeSystem

- API Doc: [TimeSystem.md](./TimeSystem.md)
- Source Path: [`client/Assets/Scripts/System/Logic/TimeSystem.cs`](../../../client/Assets/Scripts/System/Logic/TimeSystem.cs)
- Responsibility: 定时器调度与时间相关工具能力。
- Related: `TimerManager`

### PlayerPrefsSystem

- API Doc: [PlayerPrefsSystem.md](./PlayerPrefsSystem.md)
- Source Path: [`client/Assets/Scripts/System/Logic/PlayerPrefsSystem.cs`](../../../client/Assets/Scripts/System/Logic/PlayerPrefsSystem.cs)
- Responsibility: 本地轻量持久化封装。

### AudioSystem

- API Doc: [AudioSystem.md](./AudioSystem.md)
- Source Path: [`client/Assets/Scripts/System/Logic/AudioSystem.cs`](../../../client/Assets/Scripts/System/Logic/AudioSystem.cs)
- Responsibility: 音频播放与管理，支持本地和网络模式。

### ModuleSystem

- API Doc: [ModuleSystem.md](./ModuleSystem.md)
- Source Path: [`client/Assets/Scripts/System/Logic/ModuleSystem.cs`](../../../client/Assets/Scripts/System/Logic/ModuleSystem.cs)
- Responsibility: 模块体系系统层支撑与调度衔接。

### HttpSystem

- API Doc: [HttpSystem.md](./HttpSystem.md)
- Source Path: [`client/Assets/Scripts/System/Logic/HttpSystem.cs`](../../../client/Assets/Scripts/System/Logic/HttpSystem.cs)
- Responsibility: HTTP 请求统一入口，支持重试、超时、请求头与安全校验链路。
- Related: `RequestBuilder`, `SecurityUtils`, `ServerClient/UserHttpClient`

### HotUpdateSystem

- API Doc: [HotUpdateSystem.md](./HotUpdateSystem.md)
- Source Path: [`client/Assets/Scripts/System/Logic/HotUpdateSystem.cs`](../../../client/Assets/Scripts/System/Logic/HotUpdateSystem.cs)
- Responsibility: 热更程序集加载与业务入口反射调用。

### ConfigSystem

- API Doc: [ConfigSystem.md](./ConfigSystem.md)
- Source Path: [`client/Assets/HotUpdate/Table/ConfigSystem.cs`](../../../client/Assets/HotUpdate/Table/ConfigSystem.cs)
- Responsibility: 配置加载与统一访问入口。

## Constraints

- 禁止绕开 `SystemManager` 自建并行系统编排。
- 资源读取优先 [AssetSystem](./AssetSystem.md)，不要直接散落调用 `Resources`。
- 网络请求优先 [HttpSystem](./HttpSystem.md)，不要在业务侧重复实现请求封装。
