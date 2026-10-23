# TimeSystem

## Summary

`TimeSystem` 提供统一定时器调度能力，并转发每日刷新事件。

## Type Metadata

- Namespace: `global`
- Type: `class`
- Layer: `Scripts`
- Source Path: [`client/Assets/Scripts/System/Logic/TimeSystem.cs`](../../../client/Assets/Scripts/System/Logic/TimeSystem.cs)

## Public APIs

| Method | Signature | Returns | When To Use | Notes |
| --- | --- | --- | --- | --- |
| `AddDelay` | `Timer AddDelay(float _delay, OnTimerCallback _func, bool _ignoreScale = false)` | `Timer` | 延时执行一次 | 可选忽略时间缩放 |
| `AddIntervalUpdate` | `Timer AddIntervalUpdate(float _interval, OnTimerCallback _func, bool _immediately = false, bool _ignoreScale = false)` | `Timer` | 固定间隔重复执行 | 可立即触发首帧 |
| `AddIntervalUpdate` | `Timer AddIntervalUpdate(float _interval, float _duration, OnTimerCallback _func, bool _immediately = false)` | `Timer` | 限定持续时长的间隔执行 | 持续结束后停止 |
| `AddCount` | `Timer AddCount(float _interval, int _count, OnTimerCallback _func, OnTimerCallback _complete = null)` | `Timer` | 指定次数执行 | 完成回调可选 |
| `RemoveTimer` | `void RemoveTimer(Timer _timer)` | `void` | 手动移除定时器 | 提前终止任务 |

## Constants

- `SecondsToMillisecond = 1000`
- `MinuteToSeconds = 60`
- `HourToSeconds = 3600`

## Dependencies

- `TimerManager`
- `EventNativeSystem`

## Constraints

- 定时任务统一通过本系统创建和回收，避免业务层分散维护计时状态。
