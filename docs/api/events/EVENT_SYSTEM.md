# EventSystem 事件使用

## Summary

本项目事件机制分两层：

- `SendEvent/AddEvent/RemoveEvent`（`params object[]` 风格）
- `Broadcast/AddListener/RemoveListener`（强类型委托风格）

底层统一由 `EventManager` 处理。

## Related Sources

- EventCenterSystem: [`client/Assets/HotUpdate/Event/EventCenterSystem.cs`](../../../client/Assets/HotUpdate/Event/EventCenterSystem.cs)
- EventNativeSystem: [`client/Assets/Scripts/Event/EventNativeSystem.cs`](../../../client/Assets/Scripts/Event/EventNativeSystem.cs)
- EventManager: [`client/Assets/PeekabooGame/Event/EventManager.cs`](../../../client/Assets/PeekabooGame/Event/EventManager.cs)
- EventHandler: [`client/Assets/PeekabooGame/Event/EventHandler.cs`](../../../client/Assets/PeekabooGame/Event/EventHandler.cs)
- EventMsgHandler: [`client/Assets/PeekabooGame/Event/EventMsgHandler.cs`](../../../client/Assets/PeekabooGame/Event/EventMsgHandler.cs)
- EventConst 定义: [`client/Assets/HotUpdate/Event/EventConst.cs`](../../../client/Assets/HotUpdate/Event/EventConst.cs)
- SysEvent 定义: [`client/Assets/Scripts/Event/SysEvent.cs`](../../../client/Assets/Scripts/Event/SysEvent.cs)

## 1. 事件定义

### 1.1 EventConst（业务事件）

- 定义位置：`client/Assets/HotUpdate/Event/EventConst.cs`
- 用途：UI/业务模块内高频事件（例如 `Panel_Open`、`Currency_Refresh`）

### 1.2 SysEvent（系统事件）

- 定义位置：`client/Assets/Scripts/Event/SysEvent.cs`
- 用途：系统启动、登录、配置、网络状态等宿主层事件（例如 `Config_Load_Done`、`Login_Success`）

## 2. 注册事件

### 2.1 通用注册（params 风格）

```csharp
EventSystem.Instance.AddEvent(EventConst.Currency_Refresh, this, OnCurrencyRefresh);
EventNativeSystem.Instance.AddEvent(SysEvent.Config_Load_Done, this, OnConfigLoaded);

private void OnCurrencyRefresh(params object[] args) { }
private void OnConfigLoaded(params object[] args) { }
```

说明：

- 回调签名是 `CallBackParams`，即 `void Callback(params object[] args)`。
- `_target` 作为解绑锚点，建议始终传 `this`。

### 2.2 UI/Module 基类内注册

- `UIContainer` 提供 `AddEventListener(...)` 封装，`Unbind` 时自动移除。
- `ModuleBase<T>` 提供 `AddEvent(...)` 封装，`Logout` 时会 `RemoveEvent(this)`。

### 2.3 强类型注册（Broadcast 通道）

```csharp
EventCenterSystem.Instance.AddListener<int>(eventId, OnIntEvent);

private void OnIntEvent(int value) { }
```

## 3. 发送事件

### 3.1 SendEvent（params 风格）

```csharp
EventCenterSystem.Instance.SendEvent(EventConst.Panel_Open, UIConfig.MainPanel);
EventNativeSystem.Instance.SendEvent(SysEvent.Loading_Open);
```

### 3.2 Broadcast（强类型风格）

```csharp
EventCenterSystem.Instance.Broadcast<int>(eventId, 10);
EventSystem.Instance.Broadcast<int, string>(eventId2, 1, "ok");
```

## 4. 解绑事件

### 4.1 按目标解绑（推荐）

```csharp
EventCenterSystem.Instance.RemoveEvent(this);
EventNativeSystem.Instance.RemoveEvent(this);
```

### 4.2 按事件 + 目标解绑

```csharp
EventCenterSystem.Instance.RemoveEvent(EventConst.Panel_Open, this);
EventNativeSystem.Instance.RemoveEvent(SysEvent.Config_Load_Done, this);
```

### 4.3 强类型监听解绑

```csharp
EventCenterSystem.Instance.RemoveListener<int>(eventId, OnIntEvent);
```

## 5. 推荐实践

- 业务层优先使用 `EventConst + EventCenterSystem`。
- 宿主/系统层优先使用 `SysEvent + EventNativeSystem`。
- 事件参数顺序和类型写在事件定义注释里，发送与接收保持一致。
- 生命周期结束时一定解绑，避免悬挂回调。

## 6. 常见问题

- 同一事件重复注册同一目标同一回调：`EventHandler` 会去重，新增会失败。
- 回调里直接强转参数越界或类型不匹配：先判长和判空再转型。
- 只 `AddListener` 不 `RemoveListener`：会造成持续引用和重复触发。
