---
name: develop-unity-progression-ui
description: 设计、实现、调试和验证 Unity 系统养成界面的业务交互逻辑。适用于用户或美术负责拼接 UI Prefab，而开发侧需要定义字段契约、接入 AutoGen、编写 UIPanel/UIItem、调用养成 Module、监听 EventConst、刷新列表与状态、处理导航、红点、奖励、消耗、升级、解锁和异常反馈等任务。涉及养成业务时必须结合 develop-unity-progression-systems；涉及配置表时必须结合 develop-unity-config-tables。
---

# Unity 养成 UI 逻辑开发

基于目标项目现有 UI 框架实现局外养成界面的交互逻辑。默认协作边界是：用户或美术负责效果图、切图、节点层级、适配和 Prefab；本 Skill 负责字段契约、自动绑定后的手写代码、Module 交互、事件刷新和验证。

## 强制工作流程

1. 先判断任务属于新界面、已有界面扩展、交互修复、显示刷新、列表 Item、导航、红点还是系统接入。
2. 修改前阅读目标仓库的 `AGENTS.md`、`ai-doc`、`docs`、策划文档和 UI 规范；定位真实 `UISystem`、`UIPanel`、`UIItem`、字段生成器、UI 配置生成器、Module 和事件系统。
3. 涉及养成规则、玩家数据、消耗、奖励、升级、解锁或存档时，同时使用 `$develop-unity-progression-systems`。本 Skill 只负责界面层，不把业务真值搬到 UI。
4. 涉及 XML、xlsx、图标 ID、文本、本地化、奖励、消耗或升级配置时，同时使用 `$develop-unity-config-tables`。
5. 新界面或大范围改版在编码前使用 `assets/养成UI逻辑方案模板.md` 生成方案，先写清界面状态、字段契约、数据源、命令、事件、导航和 Prefab 待办。
6. 用户尚未拼好 Prefab 或尚未生成 `AutoGen` 时，只输出字段契约和编辑器操作清单，不猜测节点层级，不编写依赖未知字段的业务代码。
7. Prefab 和 `AutoGen` 就绪后，先核对生成字段，再在手写 `Panel/Item` 文件中实现逻辑。禁止手改 `*.AutoGen.cs`、`UIConfig.Auto.cs` 等生成文件。
8. 界面打开时从 Module 的公开查询接口构建显示；按钮只调用 Module 的公开命令；数据修改成功后由 Module 发送事件，界面监听事件刷新。
9. 完整处理正常、未解锁、可解锁、可升级、满级、资源不足、空列表、加载中、提交中、失败和重复点击状态。
10. 交付前执行静态检查、C# 编译和 Unity 行为验证，并列出仍需用户完成的 Prefab、锚点、动画和资源工作。

## 职责边界

- 用户负责：效果图确认、切图导入、Prefab 层级、锚点与安全区、布局组件、序列化引用、动效资源和最终视觉验收。
- 本 Skill 负责：字段命名建议、绑定契约、Panel/Item 逻辑、打开参数、按钮回调、Module 查询与命令、事件监听、状态刷新、导航、错误保护和代码验证。
- UI 只能通过 Module 的 `Get...`、`Is...`、`Can...` 等查询读取业务状态，通过 `Send_...` 等命令请求修改。
- UI 禁止直接读取或修改数据库 Parser、存档 Root、Module 私有集合或战斗运行时组件。
- Module 在校验和数据提交成功后发送事件；UI 不提前伪造成功状态，不复制一套业务规则。
- 显示层可以构建临时 ViewData，但不能持有可变数据库对象作为长期真值。

## 项目接入原则

- 当前仓库的事实来源包括 `Assets/HotUpdate/UI`、`Assets/Editor/UI`、`docs/api/ui` 和真实养成 Module；路径变化时以目标项目源码为准。
- 新 Panel、Item 和交互模式优先从项目内最接近的真实界面提炼，不能引入第二套 UI 生命周期、事件总线或窗口栈。
- 当前项目中，Prefab 字段使用 `m_<类型>_<名称>` 命名；Panel/Item 根节点分别以 `Panel`/`Item` 结尾。详细规则读取 `references/Prefab与AutoGen协作.md`。
- 当前项目中，手写逻辑放在 `Assets/HotUpdate/UI/Panel/<Domain>/`，字段绑定放在 `Assets/HotUpdate/UI/AutoGen/`。生成文件只读。
- 当前项目中，打开和关闭界面统一使用 `UISystem.Instance.Open/Close`，禁止业务代码自行实例化 Panel Prefab。

## 参考文档选择

- 界面层、Module、数据库、事件和导航的职责：读取 `references/界面职责与数据流.md`。
- 用户拼 Prefab、字段命名、UI 配置和 AutoGen 协作：读取 `references/Prefab与AutoGen协作.md`。
- 从效果图和需求到完整界面逻辑交付：读取 `references/养成界面从0到1开发流程.md`。
- 按钮、列表、选择、红点、弹窗、提交状态和刷新时机：读取 `references/交互刷新与状态规范.md`。
- 编译、静态检查和 Unity 验收：读取 `references/验证规范.md`。

## 中文注释规则

- 关键界面状态、打开参数、ViewData 字段、Module 命令、事件参数、刷新入口和重复点击保护必须写 UTF-8 中文注释。
- 注释说明数据来源、刷新时机、状态原因和失败行为，不能只机械复述代码。
- 事件回调要写明由哪个 Module 在什么成功条件下发送。
- 发现乱码时只修复本次影响的手写文件；生成文件应修复生成源并重新生成。

## 交付说明

说明已实现的界面行为、使用的 Module API、监听事件、手写文件、生成文件、编译与运行验证结果，以及用户仍需在 Unity 中完成的 Prefab 节点、字段生成、资源、动画和视觉确认。
