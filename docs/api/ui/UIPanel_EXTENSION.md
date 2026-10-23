# UI Panel 指南

## Summary

本指南说明如何在当前脚手架中扩展新 Panel，包含 Panel 创建、新类型定义以及 UIItem 使用方式。

## Related Sources

- UISystem: [`client/Assets/HotUpdate/UI/UISystem.cs`](../../../client/Assets/HotUpdate/UI/UISystem.cs)
- UIPanel: [`client/Assets/HotUpdate/UI/Code/UIPanel.cs`](../../../client/Assets/HotUpdate/UI/Code/UIPanel.cs)
- UIContainer: [`client/Assets/HotUpdate/UI/Code/UIContainer.cs`](../../../client/Assets/HotUpdate/UI/Code/UIContainer.cs)
- UIItem: [`client/Assets/HotUpdate/UI/Code/UIItem.cs`](../../../client/Assets/HotUpdate/UI/Code/UIItem.cs)
- UIList: [`client/Assets/HotUpdate/UI/Code/UIList.cs`](../../../client/Assets/HotUpdate/UI/Code/UIList.cs)
- UI 配置定义: [`client/Assets/HotUpdate/UI/Config/UIConfig.cs`](../../../client/Assets/HotUpdate/UI/Config/UIConfig.cs)
- 字段类型定义: [`client/Assets/Scripts/UI/Code/UIFieldCfg.cs`](../../../client/Assets/Scripts/UI/Code/UIFieldCfg.cs)
- 字段解析实现: [`client/Assets/HotUpdate/UI/Code/UIFieldDicMgr.cs`](../../../client/Assets/HotUpdate/UI/Code/UIFieldDicMgr.cs)

## 0. 当前支持类型与使用方法

`UIPanel`/`UIItem` 字段绑定本质是 `GetVaule<T>("字段名")`，可用类型由 `UIFieldCfg.FieldType` + `UIFieldDicMgr` 决定。

### 0.1 已实现并可直接使用

| FieldType | C# 类型 | 示例 |
| --- | --- | --- |
| `GameObject` | `GameObject` | `var go = GetVaule<GameObject>("m_GameObject_Container");` |
| `RectTransform` | `RectTransform` | `var rt = GetVaule<RectTransform>("m_RectTransform_Root");` |
| `Text` | `UnityEngine.UI.Text` | `var txt = GetVaule<Text>("m_Text_Title");` |
| `TextMeshProUGUI` | `TMPro.TextMeshProUGUI` | `var tmp = GetVaule<TextMeshProUGUI>("m_TMP_Text_Title");` |
| `Image` | `UnityEngine.UI.Image` | `var img = GetVaule<Image>("m_Image_Icon");` |
| `RawImage` | `UnityEngine.UI.RawImage` | `var raw = GetVaule<RawImage>("m_RawImage_Preview");` |
| `Button` | `UnityEngine.UI.Button` | `var btn = GetVaule<Button>("m_Button_Close");` |
| `Slider` | `UnityEngine.UI.Slider` | `var s = GetVaule<Slider>("m_Slider_Volume");` |
| `Toggle` | `UnityEngine.UI.Toggle` | `var t = GetVaule<Toggle>("m_Toggle_On");` |
| `InputField` | `UnityEngine.UI.InputField` | `var i = GetVaule<InputField>("m_InputField_Name");` |
| `TMP_InputField` | `TMPro.TMP_InputField` | `var i = GetVaule<TMP_InputField>("m_TMP_InputField_Name");` |
| `Dropdown` | `UnityEngine.UI.Dropdown` | `var d = GetVaule<Dropdown>("m_Dropdown_Tab");` |
| `TMP_Dropdown` | `TMPro.TMP_Dropdown` | `var d = GetVaule<TMP_Dropdown>("m_TMP_Dropdown_Tab");` |
| `ScrollRect` | `UnityEngine.UI.ScrollRect` | `var sr = GetVaule<ScrollRect>("m_ScrollRect_List");` |
| `GridLayoutGroup` | `UnityEngine.UI.GridLayoutGroup` | `var g = GetVaule<GridLayoutGroup>("m_GridLayout_Items");` |
| `HorizontalLayoutGroup` | `UnityEngine.UI.HorizontalLayoutGroup` | `var h = GetVaule<HorizontalLayoutGroup>("m_HorizontalLayoutGroup_Root");` |
| `VerticalLayoutGroup` | `UnityEngine.UI.VerticalLayoutGroup` | `var v = GetVaule<VerticalLayoutGroup>("m_VerticalLayoutGroup_Root");` |
| `Canvas` | `UnityEngine.Canvas` | `var c = GetVaule<Canvas>("m_Canvas_Sub");` |
| `CanvasGroup` | `UnityEngine.CanvasGroup` | `var cg = GetVaule<CanvasGroup>("m_CanvasGroup_Fade");` |
| `CanvasScaler` | `UnityEngine.UI.CanvasScaler` | `var cs = GetVaule<CanvasScaler>("m_CanvasScaler_Root");` |
| `Animator` | `UnityEngine.Animator` | `var anim = GetVaule<Animator>("m_Animator_Panel");` |
| `LineRender/LineRenderer` | `UnityEngine.LineRenderer` | `var lr = GetVaule<LineRenderer>("m_LineRenderer_Path");` |
| `SkeletonGraphic` | `Spine.Unity.SkeletonGraphic` | `var sg = GetVaule<SkeletonGraphic>("m_SkeletonGraphic_Role");` |
| `UIParticle` | `Coffee.UIExtensions.UIParticle` | `var p = GetVaule<UIParticle>("m_UIParticle_Effect");` |
| `UIList` | `UIList` | `var list = GetVaule<UIList>("m_UIList_Items");` |
| `UISwitch` | `UISwitch` | `var sw = GetVaule<UISwitch>("m_UISwitch_Mode");` |
| `Component` | 运行时类型（`field.param`） | `var com = GetVaule<Component>("m_Component_Custom");` |

### 0.2 特殊类型（有约束）

- `UIItem`：当前解析结果是 `UIFieldCfg`，业务层应优先使用 `InitUIItem<T>()` 创建 Item。
- `UIFieldCfg`：返回嵌套字段字典对象，通常不直接在 Panel 业务里使用。

### 0.3 枚举中存在但当前未接入解析

以下类型在 `UIFieldCfg.FieldType` 中存在，但 `UIFieldDicMgr` 未真正写入字典：

- `UITab`
- `UIFrameAnimator`
- `UIEffect`
- `UIButton`
- `ParticleSystem`
- `ContentSizeFitter`
- `MeshRenderer`

这几类暂不建议用于 `AutoGen` 字段绑定，如需启用先扩展 `UIFieldDicMgr`。

### 0.4 推荐调用方式

- 必需字段：`GetVaule<T>("字段名")`
- 可选字段：`TryGetVaule<T>("字段名", out var x)`
- Item（单个）：`InitUIItem<T>("m_GameObject_Item")`
- Item（列表）：`InitUIList("m_UIList_Items", typeof(YourItemType))`
- 子 Canvas 排序：`GetChildCanvas("m_Canvas_Sub")`

## 1. Panel 创建

### 1.1 预制体与命名

- 预制体路径遵循：`client/Assets/Res/UI/Prefab/<模块>/<Panel>.prefab`
- 字段绑定命名遵循：`m_类型_名称`（例如 `m_Button_Close`）
- 面板类名建议与 `UIConfig` 枚举名保持一致

### 1.2 代码文件

- 自动生成绑定文件（禁止手写业务）:
  - `client/Assets/HotUpdate/UI/AutoGen/<Panel>.AutoGen.cs`
- 业务逻辑文件（手写）:
  - `client/Assets/HotUpdate/UI/Panel/<模块>/<Panel>.cs`

示例（业务 partial）:

```csharp
public partial class ShopPanel
{
    protected override void OnRegisterListener()
    {
        base.OnRegisterListener();
        AddButtonClick(m_Button_Close, Close);
    }

    protected override void OnShow()
    {
        base.OnShow();
        // 刷新界面
    }
}
```

## 2. 新类型定义（UIConfig / UIInfo）

### 2.1 在 UIConfig 增加枚举

在 [`client/Assets/HotUpdate/UI/Config/UIConfig.cs`](../../../client/Assets/HotUpdate/UI/Config/UIConfig.cs) 的 `UIConfig` 中新增项：

```csharp
public enum UIConfig
{
    // ...
    ShopPanel,
}
```

### 2.2 在 UIInfoTable 注册信息

同文件 `UIInfoTable` 中新增映射：

```csharp
{
    UIConfig.ShopPanel,
    new UIInfo()
    {
        layer = EUILayer.Top,
        uiConfigId = UIConfig.ShopPanel,
        prefabName = "Shop/ShopPanel",
        instance = true,
        showPlayAnimation = true
    }
},
```

### 2.3 类型名约束（必须）

`UISystem.Open` 内部通过 `Type.GetType(_uiInfo.uiConfigId.ToString())` 创建 Panel 实例，因此默认要求：

- `UIConfig` 枚举值名 == Panel 类名
- 类可被 `Type.GetType` 解析（当前项目使用无命名空间类）

## 3. 打开与关闭

统一使用 `UISystem`：

```csharp
UISystem.Instance.Open(UIConfig.ShopPanel);
UISystem.Instance.Open(UIConfig.ShopPanel, data, panel => { /* on open */ });
UISystem.Instance.Close(UIConfig.ShopPanel);
```

## 4. Item 使用

### 4.1 定义 Item 类型

创建一个 `UIItem` 子类：

```csharp
public class ShopItem : UIItem
{
    protected override void OnCreate()
    {
        // 初始化组件引用
    }

    protected override void OnShow()
    {
        // 用 this.data 刷新显示
    }
}
```

### 4.2 在 Panel 中使用单个 Item

```csharp
private ShopItem item;

protected override void OnCreate()
{
    base.OnCreate();
    item = InitUIItem<ShopItem>("m_GameObject_Item");
}
```

### 4.3 在 Panel 中使用列表 Item（UIList）

```csharp
private UIList list;

protected override void OnCreate()
{
    base.OnCreate();
    list = InitUIList("m_UIList_Items", typeof(ShopItem));
}

protected override void OnShow()
{
    base.OnShow();
    list.ShowList(itemDataList);
}
```

可选能力：

- `list.SetToScrollPosition(index)`
- `list.SetSelect(index)` / `list.CancelSelect()`
- `list.GetItem<ShopItem>(index)`

## 5. 生命周期建议

- `OnCreate`：初始化 UIList / UIItem / 按钮绑定
- `OnShow`：根据 data 刷新显示
- `OnHide`：停止临时表现或动画
- `OnClose` / `OnDestroy`：释放外部资源
- 事件监听需成对注册与反注册

## 6. 常见错误

- 在 `AutoGen` 文件手写业务逻辑，后续会被生成覆盖。
- `UIConfig` 名称与 Panel 类名不一致，导致 `Open` 反射失败。
- 绕过 `UISystem` 手动实例化面板，造成层级和引用计数异常。
