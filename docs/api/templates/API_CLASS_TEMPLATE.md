# {TypeName}

## Summary

一句话说明类型职责与边界。

## Type Metadata

- Namespace: `{Namespace}`
- Type: `{Class | Interface | Enum}`
- Layer: `{Framework | Scripts | HotUpdate}`
- Thread: `{MainThread | Any}`
- Generated: `{true | false}`
- Source Path: [`{client/Assets/...}`](../../../{client/Assets/...})

## Lifecycle

1. `{Init/Construct}`
2. `{Runtime callbacks}`
3. `{Dispose/Release}`

## Public APIs

| Method | Signature | Returns | When To Use | Notes |
| --- | --- | --- | --- | --- |
| `MethodName` | `void MethodName(int x)` | `void` | 说明调用场景 | 补充注意事项 |

### Parameters

- `x` (`int`): 参数含义与取值约束。

## Dependencies

- `DependencyA`: 依赖原因
- `DependencyB`: 依赖原因

## Usage Example

```csharp
public sealed class ExampleUsage
{
    public void Run()
    {
        // 示例应体现真实调用时机与上下文
        // var data = SomeSystem.Instance.MethodName(1);
    }
}
```

## Constraints

- 约束 1
- 约束 2

## Common Pitfalls

- 常见错误 1：错误描述 -> 正确做法
- 常见错误 2：错误描述 -> 正确做法
