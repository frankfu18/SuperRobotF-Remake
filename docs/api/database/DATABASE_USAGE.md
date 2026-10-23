# Database 使用文档

## Summary

当前项目数据库采用 `Scripts/Database` 的 Root 模型 + 帧末批量提交方案。  
业务层通过 `DatabaseHelper` 读写，不直接处理落盘、版本管理和回滚。

## Related Sources

- Bootstrap: [`client/Assets/Scripts/Database/DatabaseBootstrap.cs`](../../../client/Assets/Scripts/Database/DatabaseBootstrap.cs)
- Facade: [`client/Assets/Scripts/Database/DatabaseHelper.cs`](../../../client/Assets/Scripts/Database/DatabaseHelper.cs)
- Engine: [`client/Assets/Scripts/Database/Core/DatabaseEngine.cs`](../../../client/Assets/Scripts/Database/Core/DatabaseEngine.cs)
- Registry: [`client/Assets/Scripts/Database/Core/DatabaseRegistry.g.cs`](../../../client/Assets/Scripts/Database/Core/DatabaseRegistry.g.cs)
- Store: [`client/Assets/Scripts/Database/Serialization/TransactionalFileStore.cs`](../../../client/Assets/Scripts/Database/Serialization/TransactionalFileStore.cs)
- DB Config: [`client/Assets/PeekabooGame/Framework/Runtime/Config/DBConfig.cs`](../../../client/Assets/PeekabooGame/Framework/Runtime/Config/DBConfig.cs)
- 业务模型示例: [`client/Assets/HotUpdate/DataBase/DataModel`](../../../client/Assets/HotUpdate/DataBase/DataModel)

## 1. 架构概览

- `IDataRoot`：Root 标记接口，提交/回滚最小单位。
- `DatabaseRegistry`：启动时扫描并注册所有 `IDataRoot` 具体类。
- `DatabaseEngine`：管理 Root 内存态、标脏、帧末提交、失败回滚。
- `TransactionalFileStore`：事务化写盘（先写版本目录，再原子更新 manifest）。
- `DatabaseHelper`：业务门面，只暴露 `Get/Update/TryCheck`。

## 2. 初始化时机

项目中有两处初始化保障：

1. `DatabaseBootstrap.Awake()` 自动初始化。  
2. `ModuleHandler.Login()` 显式调用 `DatabaseBootstrap.Instance.Initialize(...)`（幂等，重复调用会直接返回）。

对应源码：

- [`client/Assets/HotUpdate/Module/ModuleHandler.cs`](../../../client/Assets/HotUpdate/Module/ModuleHandler.cs)

## 3. 定义 Root 数据模型

Root 必须满足：

- 实现 `IDataRoot`
- 非抽象类
- 有无参构造
- 仅对字典/集合属性强制要求：必须在定义处初始化默认值，禁止 `null` 初始态

示例：

```csharp
using System.Collections.Generic;
using Database;

namespace db.cfg
{
    public class CurrencyData : IDataRoot
    {
        public Dictionary<ECurrencyType, long> currencyDic = new();
    }
}
```

字典/集合默认值建议：

- `List<T>` 使用 `new()`
- `Dictionary<K,V>` 使用 `new()`
- `HashSet<T>` 使用 `new()`

说明：

- 注册是自动扫描 `AppDomain` 的 `IDataRoot` 类型，无需手工登记。
- Root 名默认使用类型名（`RootRegistration.RootName = modelType.Name`）。

## 4. 业务读写方式

### 4.1 读取

```csharp
var currencyData = DatabaseHelper.Instance.Get<CurrencyData>();
var coin = currencyData.currencyDic.GetValueOrDefault(ECurrencyType.Coin, 0);
```

### 4.2 条件检查

```csharp
bool exists = DatabaseHelper.Instance.TryCheck<UserData>(d => !string.IsNullOrEmpty(d.uid));
```

### 4.3 更新（推荐）

```csharp
DatabaseHelper.Instance.Update<CurrencyData>(data =>
{
    data.currencyDic[ECurrencyType.Coin] = 100;
});
```

### 4.4 更新并返回结果

```csharp
bool ok = DatabaseHelper.Instance.Update<ItemData>(data =>
{
    if (!data.itemInfoDic.TryGetValue(itemId, out var item)) return false;
    if (item.itemCount < cost) return false;
    item.itemCount -= cost;
    return true;
});
```

## 5. 提交与回滚机制

- `Update` 成功后仅标脏，不立即落盘。
- `DatabaseBootstrap.LateUpdate()` 每帧调用 `EndFrame()`，批量提交脏 Root。
- 提交版本号自增，多个 Root 可在同一提交版本内落盘。
- 任一 Root 提交失败：回滚所有 Root 到上一个已提交快照。
- 发生异常帧：该帧在 `LateUpdate` 中先回滚，再跳过提交。

## 6. 落盘目录结构

`DBConfig.RootDirectory`（默认 `db_data`）下：

- `manifest.json`：当前版本与每个 Root 的索引信息
- `commits/v{N}/*.json`：各提交版本的数据文件

存储由 `TransactionalFileStore` 保证原子切换：

1. 序列化本批次 Root
2. 写入 `commits/v{N}` 目录
3. 原子替换 `manifest.json`（生效点）

## 7. 提交事件（可选监听）

```csharp
DatabaseHelper.Instance.CommitSucceeded += (version, roots) =>
{
    // version: 提交版本
    // roots: 本次提交的 Root 名称列表
};

DatabaseHelper.Instance.CommitFailed += (version, roots, ex) =>
{
    // 提交失败诊断
};
```

## 8. 已有业务示例

- 货币：[`client/Assets/HotUpdate/Game/Modules/CurrencyModule.cs`](../../../client/Assets/HotUpdate/Game/Modules/CurrencyModule.cs)
- 用户：[`client/Assets/HotUpdate/Game/Modules/UserModule.cs`](../../../client/Assets/HotUpdate/Game/Modules/UserModule.cs)
- 物品：[`client/Assets/HotUpdate/Game/Modules/ItemModule.cs`](../../../client/Assets/HotUpdate/Game/Modules/ItemModule.cs)

## 9. 使用约束与常见坑

- 不要在业务侧直接读写磁盘文件，统一走 `DatabaseHelper`。
- 写操作统一放 `Update` 回调，避免“读后忘记标脏”。
- `Get<TRoot>()` 返回引用对象，直接改字段不会自动限制；团队约定仍应通过 `Update` 写入。
- 仅字典/集合属性要求在定义处给默认值，禁止 `null` 初始态。
- `Dictionary<string, object>` 这类弱类型结构在 JSON 反序列化时可读性和类型稳定性较差，优先使用强类型字段。
- 生命周期结束前避免遗漏事件解绑（尤其 Commit 事件监听）。
