using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace FrankGame
{
    public class AssetConfigImporter : AssetPostprocessor
    {
        private static List<string> texturePathList = new List<string>
        {
            "Assets/Res/UI/Atlas/",
            "Assets/Res/UI/Texture/",
            "Assets/Res/Map/",
        };

        private static List<string> spinePathList = new List<string>()
        {
            "Assets/Res/Battle/Spine/",
        };

        private static List<string> audioSoundPathList = new List<string>
        {
            "Assets/Res/Audio/sound/",
        };
        private static List<string> audioMusicPathList = new List<string>
        {
            "Assets/Res/Audio/music/",
        };
        private static List<string> battlePathList = new List<string>
        {
            "Assets/Res/Battle/Map/",
        };
        private static List<string> effectPathList = new List<string>
        {
            "Assets/Res/Vfx/",
        };

        private void OnPreprocessTexture()
        {
            foreach (var texPath in texturePathList)
            {
                if (assetPath.StartsWith(texPath))
                {
                    OnTextureSetting(TextureImporterFormat.ASTC_6x6);

                    if (texPath.Contains(ResConfig.Sprite_Root))
                    {
                        OnSpriteSetting();
                    }
                }
            }

            foreach (var texPath in battlePathList)
            {
                if (assetPath.StartsWith(texPath))
                {
                    OnTextureSetting(TextureImporterFormat.ASTC_6x6);
                }
            }

            foreach (var texPath in effectPathList)
            {
                if (assetPath.StartsWith(texPath))
                {
                    OnTextureSetting(TextureImporterFormat.ASTC_6x6);
                }
            }

            foreach (var spinePath in spinePathList) {
                if (!assetPath.StartsWith(spinePath)) continue;
                OnSpineTexture();
            }
        }

        private void OnSpineTexture() {
            if (!assetPath.ToLower().EndsWith(".png")) return;
            var texImporter = AssetImporter.GetAtPath(assetPath) as TextureImporter;
            texImporter.alphaIsTransparency = true;
            texImporter.SaveAndReimport();
        }

        private void OnPostprocessAudio(AudioClip clip)
        {
            foreach (var audioPath in audioSoundPathList)
            {
                if (assetPath.StartsWith(audioPath))
                {
                    OnAudioSetting(false);
                }
            }
            foreach (var audioPath in audioMusicPathList)
            {
                if (assetPath.StartsWith(audioPath))
                {
                    OnAudioSetting(true);
                }
            }
        }

        private void OnTextureSetting(TextureImporterFormat _format, FilterMode _filterMode = FilterMode.Bilinear)
        {
            TextureImporter texImporter = AssetImporter.GetAtPath(assetPath) as TextureImporter;
            texImporter.npotScale = TextureImporterNPOTScale.None;
            texImporter.mipmapEnabled = false;
            texImporter.isReadable = false;
            texImporter.filterMode = _filterMode;
            texImporter.textureType = TextureImporterType.Sprite;
            texImporter.spriteImportMode = SpriteImportMode.Single;

            var androidTexSet = texImporter.GetPlatformTextureSettings("Android");

            androidTexSet.format = _format;
            androidTexSet.maxTextureSize = 2048;
            androidTexSet.overridden = true;
            androidTexSet.compressionQuality = 50;
            androidTexSet.allowsAlphaSplitting = false;

            texImporter.SetPlatformTextureSettings(androidTexSet);

            var webGLSet = texImporter.GetPlatformTextureSettings("WebGL");
            webGLSet.format = _format;
            webGLSet.maxTextureSize = 2048;
            webGLSet.overridden = true;
            webGLSet.compressionQuality = 100;
            webGLSet.allowsAlphaSplitting = false;

            texImporter.SetPlatformTextureSettings(webGLSet);

            // var miniGameSet = texImporter.GetPlatformTextureSettings("MiniGame");
            // if(miniGameSet != null)
            // {
            //     miniGameSet.format = _format;
            //     miniGameSet.maxTextureSize = 2048;
            //     miniGameSet.overridden = true;
            //     miniGameSet.compressionQuality = 100;
            //     miniGameSet.allowsAlphaSplitting = false;
            //
            //     texImporter.SetPlatformTextureSettings(miniGameSet);
            // }
        }

        private void OnAudioSetting(bool _longAudio)
        {
            AudioImporter audioImporter = AssetImporter.GetAtPath(assetPath) as AudioImporter;
            audioImporter.forceToMono = true;
            audioImporter.loadInBackground = false;
            audioImporter.ambisonic = false;

            AudioImporterSampleSettings defaultSet = new AudioImporterSampleSettings();
            defaultSet.loadType = AudioClipLoadType.CompressedInMemory;
            defaultSet.compressionFormat = AudioCompressionFormat.Vorbis;
            defaultSet.quality = 0;
            defaultSet.preloadAudioData = false;

            audioImporter.defaultSampleSettings = defaultSet;

            AudioImporterSampleSettings webSet = audioImporter.GetOverrideSampleSettings("WebGL");
            webSet.loadType = AudioClipLoadType.CompressedInMemory;
            webSet.quality = 0;
            webSet.compressionFormat = AudioCompressionFormat.AAC;

            audioImporter.SetOverrideSampleSettings("WebGL", webSet);

            AudioImporterSampleSettings androidSet = audioImporter.GetOverrideSampleSettings("Android");
            androidSet.quality = 0;
            androidSet.compressionFormat = AudioCompressionFormat.Vorbis;
            if (_longAudio)
            {
                androidSet.loadType = AudioClipLoadType.Streaming;
            }
            else
            {
                androidSet.loadType = AudioClipLoadType.CompressedInMemory;
            }

            audioImporter.SetOverrideSampleSettings("Android", androidSet);
        }

        private void OnSpriteSetting()
        {
            TextureImporter texImporter = AssetImporter.GetAtPath(assetPath) as TextureImporter;

            TextureImporterSettings spriteSetting = new TextureImporterSettings();

            texImporter.ReadTextureSettings(spriteSetting);
            spriteSetting.textureType = TextureImporterType.Sprite;
            spriteSetting.spriteMeshType = SpriteMeshType.FullRect;

            texImporter.SetTextureSettings(spriteSetting);
        }
    }
}