using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if BYTE_GAME
using StarkSDKSpace;
#endif

public class AudioSystem : SystemBase<AudioSystem>
{
    private static int Audio_Instance_Id = 0;

    public bool Mute
    {
        get { return mute; }
        set
        {
            mute = value;

            foreach (var source in audioSources)
            {
                source.mute = mute;
            }

            PlayerPrefsSystem.Instance.settingData.audioMute = mute;
            PlayerPrefsSystem.Instance.settingData.Save();
        }
    }
    public bool Vibrate
    {
        get { return vibrate; }
        set
        {
            vibrate = value;

            PlayerPrefsSystem.Instance.settingData.phoneVibrate = vibrate;
            PlayerPrefsSystem.Instance.settingData.Save();
        }
    }

    public bool BgSoundMute
    {
        get { return bgSoundMute; }
        set
        {
            bgSoundMute = value;

            if (musicSource != null)
            {
                musicSource.mute = bgSoundMute;
            }

            PlayerPrefsSystem.Instance.settingData.bgSoundMute = bgSoundMute;
            PlayerPrefsSystem.Instance.settingData.Save();
        }
    }

    public int AudioVolume
    {
        get { return audioVolume; }
        set
        {
            audioVolume = Mathf.Clamp(value, 0, 100);

            foreach (var source in audioSources)
            {
                source.volume = audioVolume / 100.0f;
            }

            PlayerPrefsSystem.Instance.settingData.audioVolume = audioVolume;
            PlayerPrefsSystem.Instance.settingData.Save();
        }
    }

    public int BgSoundVolume
    {
        get { return bgSoundVolume; }
        set
        {
            bgSoundVolume = Mathf.Clamp(value, 0, 100);

            if (musicSource != null)
            {
                musicSource.volume = bgSoundVolume / 100.0f;
            }

            PlayerPrefsSystem.Instance.settingData.bgSoundVolume = bgSoundVolume;
            PlayerPrefsSystem.Instance.settingData.Save();
        }
    }

    private GameObject audioRoot;
    private List<AudioSource> audioSources = new List<AudioSource>();
    private AudioSource musicSource;
    private Dictionary<int, AudioSource> audioSourceDic = new Dictionary<int, AudioSource>();
    private Queue<AudioSource> unuseAudioSourcesQueue = new Queue<AudioSource>();
    private bool mute;
    private bool vibrate;
    private bool bgSoundMute;//背景音乐
    private int audioVolume;//音效音量
    private int bgSoundVolume;//背景音乐音量

#if BYTE_GAME
    private Dictionary<int, StarkAudioManager.InnerAudioContext> audioContextDic = new Dictionary<int, StarkAudioManager.InnerAudioContext>();
    private StarkAudioManager.InnerAudioContext musicContext;
#endif

    protected override void OnInit()
    {
        audioRoot = new GameObject("AudioRoot");
        audioRoot.transform.SetParent(this.root.transform);

#if UNITY_EDITOR && BYTE_GAME
        LogUtil.Log("网络音频地址： " + FrankGame.GameConfig.Audio.NetAudioTestUrl);
        //仅用于本地测试
        StarkAudioManager.SetAudioUrlPrefix(FrankGame.GameConfig.Audio.NetAudioTestUrl);
#endif
    }

    protected override void OnInitDone()
    {
        Mute = PlayerPrefsSystem.Instance.settingData.audioMute;
        Vibrate = PlayerPrefsSystem.Instance.settingData.phoneVibrate;
        BgSoundMute = PlayerPrefsSystem.Instance.settingData.bgSoundMute;
        AudioVolume = PlayerPrefsSystem.Instance.settingData.audioVolume;
        BgSoundVolume = PlayerPrefsSystem.Instance.settingData.bgSoundVolume;
    }

    public int PlaySound(string _name, float _speed = 1, System.Action _onPlayComplete = null, float _duration = -1, float _volume = 1f)
    {
        if (string.IsNullOrEmpty(_name))
        {
            return -1;
        }

        if (FrankGame.GameConfig.Audio.AudioPlayMode == FrankGame.EAudioPlayMode.Net)
        {
            return PlayNetSound(_name, _speed, _onPlayComplete, _duration, _volume);
        }
        else
        {
            return PlayLocalSound(_name, _speed, _onPlayComplete, _duration, _volume);
        }
    }

    private int PlayLocalSound(string _name, float _speed = 1, System.Action _onPlayComplete = null, float _duration = -1, float _volume = 1f)
    {
        if (audioRoot == null)
        {
            return -1;
        }

        int id = ++Audio_Instance_Id;

        AudioSource audioSource = GetAudioSource();

        audioSourceDic.Add(id, audioSource);

        AssetSystem.Instance.LoadSound(_name, (clip) =>
        {
            if (clip == null)
            {
                return;
            }

            audioSource.pitch = _speed;
            audioSource.clip = clip;
            audioSource.loop = false;
            audioSource.mute = mute;
            audioSource.playOnAwake = false;
            audioSource.volume = audioVolume / 100.0f * _volume;
            audioSource.Play();

            float duration = clip.length;
            if (_duration > 0)
            {
                duration = Mathf.Min(duration, _duration);
            }

            TimeSystem.Instance.AddDelay(duration, () =>
            {
                StopSound(id);

                _onPlayComplete?.Invoke();
            }, true);
        });

        return id;
    }

    private int PlayNetSound(string _name, float _speed = 1, System.Action _onPlayComplete = null, float _duration = -1, float _volume = 1f)
    {
#if BYTE_GAME
        int id = ++Audio_Instance_Id;

        var contextOptions = new StarkAudioManager.InnerAudioContextOptions();
        contextOptions.src = _name + FrankGame.ResConfig.Audio_Suffix;
        contextOptions.autoPlay = true;
        contextOptions.loop = false;
        contextOptions.needDownload = true;

        var audioContext = StarkAudioManager.CreateInnerAudioContext(contextOptions);

        audioContextDic.Add(id, audioContext);

        audioContext.Play();
        audioContext.Volume = audioVolume / 100.0f * _volume;
        audioContext.onEnded = () =>
        {
            _onPlayComplete?.Invoke();

            audioContext.Destroy();

            audioContextDic.Remove(id);
        };

        if (_duration > 0)
        {
            TimeSystem.Instance.AddDelay(_duration, () =>
            {
                StopSound(id);

                _onPlayComplete?.Invoke();
            });
        }

        return id;
#else
        return -1;
#endif
    }

    public void StopSound(int _soundId)
    {
        if (FrankGame.GameConfig.Audio.AudioPlayMode == FrankGame.EAudioPlayMode.Net)
        {
            StopNetSound(_soundId);
        }
        else
        {
            StopLocalSound(_soundId);
        }
    }

    private void StopLocalSound(int _soundId)
    {
        AudioSource audioSource;
        if (audioSourceDic.TryGetValue(_soundId, out audioSource))
        {
            audioSource.Stop();
            audioSource.clip = null;

            audioSourceDic.Remove(_soundId);

            unuseAudioSourcesQueue.Enqueue(audioSource);
        }
    }

    private void StopNetSound(int _soundId)
    {
#if BYTE_GAME
        StarkAudioManager.InnerAudioContext audioContext;
        if (audioContextDic.TryGetValue(_soundId, out audioContext))
        {
            audioContext.Stop();
            audioContext.Destroy();

            audioContextDic.Remove(_soundId);
        }
#endif
    }

    public void PlayMusic(string _name)
    {
        if (FrankGame.GameConfig.Audio.AudioPlayMode == FrankGame.EAudioPlayMode.Net)
        {
            PlayNetMusic(_name);
        }
        else
        {
            PlayLocalMusic(_name);
        }
    }

    private void PlayLocalMusic(string _name)
    {
        if (musicSource == null)
        {
            musicSource = audioRoot.AddComponent<AudioSource>();
        }

        AssetSystem.Instance.LoadSound(_name, (clip) =>
        {
            musicSource.pitch = 1;
            musicSource.clip = clip;
            musicSource.loop = true;
            musicSource.mute = BgSoundMute;
            musicSource.volume = BgSoundVolume / 100.0f;
            musicSource.playOnAwake = false;
            musicSource.Play();
        });
    }

    private void PlayNetMusic(string _name)
    {
#if BYTE_GAME
        if (musicContext != null)
        {
            musicContext.Stop();
            musicContext = null;
        }

        var contextOptions = new StarkAudioManager.InnerAudioContextOptions();
        contextOptions.src = _name + FrankGame.ResConfig.Audio_Suffix;
        contextOptions.autoPlay = true;
        contextOptions.loop = true;
        contextOptions.volume = audioVolume / 100.0f;
        contextOptions.needDownload = true;

        musicContext = StarkAudioManager.CreateInnerAudioContext(contextOptions);
        musicContext.Play();
#endif
    }

    public void StopMusic()
    {
        if (FrankGame.GameConfig.Audio.AudioPlayMode == FrankGame.EAudioPlayMode.Net)
        {
            StopNetMusic();
        }
        else
        {
            StopLocalMusic();
        }
    }

    private void StopLocalMusic()
    {
        if (musicSource == null)
        {
            return;
        }

        musicSource.Stop();
        musicSource.clip = null;
    }

    private void StopNetMusic()
    {
#if BYTE_GAME
        if (musicContext == null)
        {
            return;
        }

        musicContext.Stop();
        musicContext = null;
#endif
    }

    private AudioSource GetAudioSource()
    {
        AudioSource audioSource = null;
        if (unuseAudioSourcesQueue.Count > 0)
        {
            audioSource = unuseAudioSourcesQueue.Dequeue();
        }
        else
        {
            audioSource = audioRoot.AddComponent<AudioSource>();
        }

        return audioSource;
    }

    public void CallVibrate()
    {
        if (vibrate)
        {
#if UNITY_ANDROID
            Handheld.Vibrate();
#endif
        }
    }
}
