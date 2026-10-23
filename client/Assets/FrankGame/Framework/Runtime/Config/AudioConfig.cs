using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrankGame
{
    public enum EAudioPlayMode
    {
        Local,
        Net,
    }

    [CreateAssetMenu(menuName = "FrankGame/ScriptableObject/AudioConfig", fileName = "AudioConfig")]
    public class AudioConfig : SingleScriptableObject<AudioConfig>
    {
        [Header("音频播放方式")]
        public EAudioPlayMode AudioPlayMode = EAudioPlayMode.Local;

        [Header("测试网络音频路径(仅用作本地测试)")]
        public string NetAudioTestUrl = null;
    }
}