# PlayerPrefsSystem

## Summary

`PlayerPrefsSystem` 封装本地持久化读写，并管理 `SettingData` 的加载流程。

## Type Metadata

- Namespace: `global`
- Type: `class`
- Layer: `Scripts`
- Source Path: [`client/Assets/Scripts/System/Logic/PlayerPrefsSystem.cs`](../../../client/Assets/Scripts/System/Logic/PlayerPrefsSystem.cs)

## Public APIs

| Method | Signature | Returns | When To Use | Notes |
| --- | --- | --- | --- | --- |
| `HasKey` | `bool HasKey(string _key)` | `bool` | 判断键是否存在 | - |
| `SetInt` | `void SetInt(string _key, int _value)` | `void` | 写入 int | - |
| `SetString` | `void SetString(string _key, string _value)` | `void` | 写入 string | - |
| `SetFloat` | `void SetFloat(string _key, float _value)` | `void` | 写入 float | - |
| `SetBool` | `void SetBool(string _key, bool _value)` | `void` | 写入 bool | 内部以 int 存储 |
| `GetInt` | `int GetInt(string _key, int _defaultValue)` | `int` | 读取 int | - |
| `GetString` | `string GetString(string _key, string _defaultValue)` | `string` | 读取 string | - |
| `GetFloat` | `float GetFloat(string _key, float _defaultValue)` | `float` | 读取 float | - |
| `GetBool` | `bool GetBool(string _key, bool _defaultValue)` | `bool` | 读取 bool | 内部从 int 转换 |

## Runtime Data

- `settingData`：全局设置数据对象，`OnInit` 时创建，`SysEvent.Config_Load_Done` 后加载。

## Dependencies

- `UnityEngine.PlayerPrefs`
- `SettingData`
- `EventNativeSystem`

## Constraints

- 配置型开关优先走 `settingData` 与对应系统属性，不直接在业务层散写键值。
