# AudioSystem

## Summary

`AudioSystem` 统一管理音效与背景音乐播放，支持本地资源和网络音频两种模式。

## Type Metadata

- Namespace: `global`
- Type: `class`
- Layer: `Scripts`
- Source Path: [`client/Assets/Scripts/System/Logic/AudioSystem.cs`](../../../client/Assets/Scripts/System/Logic/AudioSystem.cs)

## Public APIs

| Method | Signature | Returns | When To Use | Notes |
| --- | --- | --- | --- | --- |
| `PlaySound` | `int PlaySound(string _name, float _speed = 1, Action _onPlayComplete = null, float _duration = -1, float _volume = 1f)` | `int` | 播放音效 | 返回实例 ID，用于停止 |
| `StopSound` | `void StopSound(int _soundId)` | `void` | 停止指定音效 | 自动按播放模式分发 |
| `PlayMusic` | `void PlayMusic(string _name)` | `void` | 播放背景音乐 | 自动按播放模式分发 |
| `StopMusic` | `void StopMusic()` | `void` | 停止背景音乐 | - |
| `CallVibrate` | `void CallVibrate()` | `void` | 触发震动 | 仅在允许且平台支持时生效 |

## Runtime Settings

- `Mute`：音效静音开关（同步到 `PlayerPrefsSystem.settingData`）
- `Vibrate`：震动开关（同步到 `PlayerPrefsSystem.settingData`）
- `BgSoundMute`：背景音乐静音开关
- `AudioVolume`：音效音量（0-100）
- `BgSoundVolume`：背景音乐音量（0-100）

## Dependencies

- `AssetSystem`
- `TimeSystem`
- `PlayerPrefsSystem`
- `GameConfig.Audio.AudioPlayMode`

## Constraints

- 音频配置持久化由系统属性 setter 自动处理，不要在业务侧重复写入同一配置。
