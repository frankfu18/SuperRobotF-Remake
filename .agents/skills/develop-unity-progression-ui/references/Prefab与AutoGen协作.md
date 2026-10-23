# Prefab 与 AutoGen 协作

## 目录

1. 双方交付边界
2. 新界面字段契约
3. 当前项目命名规则
4. Prefab 完成后的生成流程
5. 生成后代码接入
6. 常见错误

## 1. 双方交付边界

用户或美术负责：

- 根据效果图拼接 Prefab。
- 设置锚点、轴心、安全区、分辨率适配和布局组件。
- 放置切图、字体、材质、特效、动画和默认占位内容。
- 按字段契约命名需要代码访问的节点。
- 在 Unity 中执行字段和 UI 配置生成。

代码侧负责：

- 在拼 Prefab 前给出字段契约和交互状态清单。
- AutoGen 生成后核对类型、名称、Item/List 和可选字段。
- 编写手写 Panel/Item、Module 交互、事件刷新和验证。
- 不擅自重排用户完成的 Prefab 层级和视觉参数。

## 2. 新界面字段契约

新界面编码前先输出表格：

| 字段名 | 组件类型 | 必需 | 默认状态 | 代码用途 | 用户需要完成 |
| --- | --- | --- | --- | --- | --- |
| `m_Button_Close` | `Button` | 是 | 可点击 | 关闭界面 | 绑定按钮区域 |
| `m_TMP_Text_Title` | `TextMeshProUGUI` | 是 | 显示占位文本 | 刷新标题 | 设置字体与样式 |
| `m_List_ExampleItem_Items` | `UIList` | 是 | 空列表 | 显示业务列表 | 配置 Content 和 Cell |
| `m_GameObject_Empty` | `GameObject` | 否 | 隐藏 | 显示空态 | 拼接空态节点 |

字段契约还要补充：

- Panel 根节点名和 Prefab 资源路径。
- UI 层级、是否单例、是否播放打开动画。
- Item 根节点名和数据结构。
- 正常、锁定、满级、资源不足、空态、提交中等节点组合。
- 需要用户绑定的自定义组件类型。

Prefab 或 AutoGen 尚未完成时，停止编写依赖这些字段的业务代码。

## 3. 当前项目命名规则

### 根节点

- Panel 根节点以 `Panel` 结尾，例如 `<Domain>Panel`。
- Item 根节点以 `Item` 结尾，例如 `<Domain>Item`。
- Panel 类名、`UIConfig` 名和根节点名保持一致。

### 字段

当前生成器使用 `m_<类型>_<名称>`：

- `m_GameObject_Content`
- `m_RectTransform_Root`
- `m_Button_Confirm`
- `m_Image_Icon`
- `m_TMP_Text_Count`
- `m_Slider_Progress`
- `m_Toggle_Select`
- `m_Item_<ItemType>_<Name>`
- `m_List_<ItemType>_<Name>`
- `m_Com_<ComponentType>_<Name>`

同一根节点下需要生成的字段名必须唯一。数字结尾字段会被生成器聚合成列表时，应检查生成结果顺序是否符合预期。

### 文件路径

- Prefab：`Assets/Res/UI/Prefab/<Domain>/<Panel>.prefab`
- 手写 Panel/Item：`Assets/HotUpdate/UI/Panel/<Domain>/<Type>.cs`
- 自动字段：`Assets/HotUpdate/UI/AutoGen/<Type>.AutoGen.cs`
- UI 配置源：`Assets/Editor/UI/UIEditorConfig.json`
- UI 配置生成文件：`Assets/HotUpdate/UI/Config/UIConfig.Auto.cs`

## 4. Prefab 完成后的生成流程

### 生成字段绑定

1. 在 Unity Hierarchy 或 Project 中选中 Panel/Item 根对象。
2. 确认根对象名以 `Panel` 或 `Item` 结尾。
3. 执行菜单 `GameObject/[UI] 生成脚本`。
4. 检查根对象上的 `UIFieldCfg` 和生成的 `*.AutoGen.cs`。
5. 确认生成文件没有重复字段、不支持类型或错误 Item 参数。

### 注册 Panel

1. 执行菜单 `PeekabooGame/UI/UI配置`。
2. 在 UI 名称中加入与 Panel 类名一致的名称。
3. 配置层级、Prefab 路径、单例、打开动画和 Resources 加载方式。
4. 先保存配置，再点击生成文件。
5. 检查 `UIConfig.Auto.cs` 包含枚举、`UIInfo` 和 `CreateUIPanel` 分支。

所有 `.Auto.cs` 和 `.AutoGen.cs` 都通过生成源修改，禁止手写补分支。

## 5. 生成后代码接入

- 先读取 AutoGen，确认字段和访问级别，再写手动 partial 类。
- `OnRegisterListener` 绑定按钮和事件。
- `OnShow` 执行完整刷新。
- Item 通过 `Show(typedViewData)` 显示，不直接保存数据库对象。
- 打开界面统一走 `UISystem.Instance.Open(UIConfig.<Panel>, data)`。
- 关闭界面统一走 Panel 的 `Close()` 或 `UISystem.Instance.Close(...)`。

## 6. 常见错误

- Prefab 字段改名后未重新生成，手写代码仍引用旧字段。
- Item 节点参数没有对应真实 Item 类，生成后无法编译。
- List 缺少 Content 或 Cell，生成器不能正确初始化。
- 直接编辑 AutoGen，下一次生成后逻辑丢失。
- Panel 类名、UIConfig 名或 Prefab 路径不一致，运行时反射创建失败。
- 代码在 Prefab 就绪前猜字段，导致双方重复返工。
