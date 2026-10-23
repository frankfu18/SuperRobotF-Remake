# HttpSystem

## Summary

`HttpSystem` 是统一 HTTP 服务入口，封装请求、心跳、登录与加解密能力。

## Type Metadata

- Namespace: `global`
- Type: `class`
- Layer: `Scripts`
- Source Path: [`client/Assets/Scripts/System/Logic/HttpSystem.cs`](../../../client/Assets/Scripts/System/Logic/HttpSystem.cs)

## Public APIs

| Method | Signature | Returns | When To Use | Notes |
| --- | --- | --- | --- | --- |
| `Get<D>` | `Action Get<D>(string _uri, Action<D> _onSuccess, RequestBuilder _requestBuilder = null)` | `Action` | 发起 GET 请求 | 通过 `RequestBuilder` 配置重试与错误 |
| `Post<T,D>` | `Action Post<T, D>(string _uri, T _data, Action<D> _onSuccess, RequestBuilder _requestBuilder = null) where T : IRequest, new()` | `Action` | 发起 POST 请求 | 统一请求协议入口 |
| `Encrypt` | `string Encrypt(string _plainText)` | `string` | 发送前加密 | 不支持 AES 时返回原文 |
| `Decrypt` | `string Decrypt(string _cipherText)` | `string` | 响应后解密 | 不支持 AES 时返回原文 |
| `Login` | `void Login()` | `void` | 用户登录流程 | 成功后发送 `SysEvent.User_Login` |

## Events / State

- `ServerState`: `Connecting | Connected | Unconnected`
- `onServerError`: 服务器错误回调
- `onLogin`: 登录开始回调
- `onLoginSuccess`: 登录成功回调

## Dependencies

- `UserHttpClient`
- `RequestBuilder`
- `SecurityUtils`
- `EventNativeSystem`

## Constraints

- 所有业务 HTTP 请求优先通过本系统发起。
- 签名与加密统一复用 `SecurityUtils`，不要重复实现协议。
