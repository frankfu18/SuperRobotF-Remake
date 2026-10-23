# API 文档索引

> 推荐阅读顺序：[INDEX](./INDEX.md) -> [systems](./systems/SYSTEM_OVERVIEW.md) -> [modules](./modules/MODULE_WORKFLOW.md) -> [ui](./ui/UIPanel.md) -> [events](./events/EVENT_SYSTEM.md) -> [database](./database/DATABASE_USAGE.md) -> [tools](./tools/TOOLCHAIN.md)

## 1. 脚手架能力总览

### 1.1 启动与流程

- 启动入口：`GameRoot : GameEntry`
- 启动驱动：`GameLauncher + GameLaunchProcedure`
- 静态流程：`StartUp -> CheckVersion(可选) -> CheckPackageAssets/CheckHotfix/ResolveAssets`
- 业务流程：`SystemInitProcedure -> FontLocalizationProcedure -> HotUpdateProcedure -> LoginProcedure`

### 1.2 核心系统

- [SystemManager](./systems/SystemManager.md)：系统生命周期编排
- [AssetSystem](./systems/AssetSystem.md)：资源加载/卸载（本地、AB、网络）
- [TimeSystem](./systems/TimeSystem.md)：定时器与时间调度
- [PlayerPrefsSystem](./systems/PlayerPrefsSystem.md)：本地持久化
- [AudioSystem](./systems/AudioSystem.md)：音频播放
- [ModuleSystem](./systems/ModuleSystem.md)：模块系统入口能力
- [HttpSystem](./systems/HttpSystem.md)：HTTP 请求能力
- [HotUpdateSystem](./systems/HotUpdateSystem.md)：热更程序集加载与反射入口
- [ConfigSystem](./systems/ConfigSystem.md)：配置加载与访问入口

### 1.3 业务层与热更新

- 热更入口：`HotUpdateSystem.LoadHotUpdateAssembly()`
- 热更业务入口：`HotUpdateMain.Run/Login/Logout`
- 业务代码主目录：`client/Assets/HotUpdate`

## 2. 分册入口

- 系统文档：[SYSTEM_OVERVIEW](./systems/SYSTEM_OVERVIEW.md)
- 系统 API：
  - [SystemManager](./systems/SystemManager.md)
  - [AssetSystem](./systems/AssetSystem.md)
  - [TimeSystem](./systems/TimeSystem.md)
  - [PlayerPrefsSystem](./systems/PlayerPrefsSystem.md)
  - [AudioSystem](./systems/AudioSystem.md)
  - [ModuleSystem](./systems/ModuleSystem.md)
  - [HttpSystem](./systems/HttpSystem.md)
  - [HotUpdateSystem](./systems/HotUpdateSystem.md)
  - [ConfigSystem](./systems/ConfigSystem.md)
- 模块文档：[MODULE_WORKFLOW](./modules/MODULE_WORKFLOW.md)
- 模块 API：[ModuleHandler](./modules/ModuleHandler.md)
- UI 文档：[UIPanel](./ui/UIPanel.md)
- 事件文档：[EVENT_SYSTEM](./events/EVENT_SYSTEM.md)
- 数据库文档：[DATABASE_USAGE](./database/DATABASE_USAGE.md)
- 工具链文档：[TOOLCHAIN](./tools/TOOLCHAIN.md)
- 工具 API：[BuildAssetsWindow](./tools/BuildAssetsWindow.md)
- 格式规范：[STYLE_GUIDE](./STYLE_GUIDE.md)
- 新文档模板：[API_CLASS_TEMPLATE](./templates/API_CLASS_TEMPLATE.md)

## 3. AI 阅读约定

- 所有 API 文档应包含：职责、生命周期、公开接口、调用示例、约束、常见错误。
- 使用统一字段名，优先机器可解析：
  - `Namespace`
  - `Type`
  - `Lifecycle`
  - `Public APIs`
  - `Dependencies`
  - `Constraints`
  - `Example`
- 若接口来自自动生成代码，必须标注 `Generated: true` 与生成命令来源。
