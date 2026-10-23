using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : Singlet<AudioHandler>
{
    public int PlayAudioById(string _soundId, float _speed = 1)
    {
        var soundCfg = ConfigSystem.CurrentData.GetSound(_soundId);
        if (soundCfg == null)
        {
            LogUtil.Error("音频ID未找到：" + _soundId);
            return -1;
        }

        if (soundCfg.audio.GetTypeId() == editor.cfg.sound.SoundEffect.TYPE_ID)
        {
            return AudioSystem.Instance.PlaySound(((editor.cfg.sound.SoundEffect)soundCfg.audio).path, _speed);
        }

        if (soundCfg.audio.GetTypeId() == editor.cfg.sound.Music.TYPE_ID)
        {
            AudioSystem.Instance.PlayMusic(((editor.cfg.sound.Music)soundCfg.audio).path);

            return 0;
        }

        LogUtil.Error("音频类型未找到：" + _soundId);
        return -1;
    }
}
