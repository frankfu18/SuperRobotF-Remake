using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrankGame
{
    public static class ResConfig
    {
        public struct ResInfo
        {
            public EResType resType;
            public string path;
            public int layer;//文件夹的层级
        }

        public const string Prefab_Suffix = ".prefab";
        public const string Texture_Suffix = ".png";
        public const string Asset_Suffix = ".asset";
        public const string Audio_Suffix = ".mp3";
        public const string Shader_Suffix = ".shader";
        public const string Font_Suffix = ".fnt";
        public const string Material_Suffix = ".mat";
        public const string Json_Suffix = ".json";
        public const string Bytes_Suffix = ".bytes";

        public const string UI_Root = "UI/";
        public const string Sprite_Root = "UI/Atlas/";
        public const string SceneSprite_Root = "UI/Sprites/";
        public const string Texture_Root = "UI/Texture/";
        public const string UIPrefab_Root = "UI/Prefab/";
        public const string Config_Root = "config/";
        public const string Audio_Root = "Audio/";
        public const string Shader_Root = "Shader/";
        public const string Font_Root = "UI/Font/";
        public const string Effect_Root = "Vfx/";
        public const string Model_Root = "Model/Prefab/";
        public const string UIScene_Root = "UIScene/";
        public const string Material_Root = "Materials/";
        public const string Spine_Root = "Spine/";
        public const string SpriteAsset_Root = "UI/SpriteAsset/";
        public const string BattleRoot_Root = "Battle/Root/";
        public const string BattlePrefab_Root = "Battle/Prefab/";
        public const string BattleSprite_Root = "Battle/Sprite/";
        public const string BattleSpine_Root = "Battle/Spine/";
        public const string Map_Root = "Map/";
        public const string Map_Prefab_Root = Map_Root + "Prefab/";
        public const string Map_Data_Root = Map_Root + "Data/";

        public static Dictionary<EResType, ResInfo> ResInfoDic = new Dictionary<EResType, ResInfo>()
        {
            {EResType.Sprite, new ResInfo() { resType = EResType.Sprite, path = Sprite_Root, layer = 1 } },
            {EResType.Texture, new ResInfo() { resType = EResType.Texture, path = Texture_Root, layer = 1 }},
            {EResType.UIPrefab, new ResInfo() { resType = EResType.UIPrefab, path = UIPrefab_Root, layer = 1 }},
            {EResType.Config, new ResInfo() { resType = EResType.Config, path = Config_Root, layer = 0 }},
            {EResType.Audio, new ResInfo() { resType = EResType.Audio, path = Audio_Root, layer = 1 }},
            {EResType.Shader, new ResInfo() { resType = EResType.Shader, path = Shader_Root, layer = 0 }},
            {EResType.Font, new ResInfo() { resType = EResType.Font, path = Font_Root, layer = 0 }},
            {EResType.Effect, new ResInfo() { resType = EResType.Effect, path = Effect_Root, layer = 0 }},
            {EResType.Spine, new ResInfo() { resType = EResType.Spine, path = Spine_Root, layer = 0 }},
            {EResType.Material, new ResInfo() { resType = EResType.Material, path = Material_Root, layer = 0 }},
            { EResType.BattleRoot, new ResInfo() { resType = EResType.BattlePrefab, path = BattleRoot_Root, layer = 0 }},
            { EResType.BattlePrefab, new ResInfo() { resType = EResType.BattlePrefab, path = BattlePrefab_Root, layer = 1 }},
            { EResType.BattleSprite, new ResInfo() { resType = EResType.BattleSprite, path = BattleSprite_Root, layer = 1 }},
            { EResType.BattleSpine, new ResInfo() { resType = EResType.BattleSpine, path = BattleSpine_Root, layer = 1 }},
            
            { EResType.MapPrefab, new ResInfo(){ resType = EResType.MapPrefab, path = Map_Prefab_Root, layer = 0} },
            { EResType.MapData, new ResInfo(){ resType = EResType.MapData, path = Map_Data_Root, layer = 0} },
        };

        public static string StreamingAsset_Path
        {
            get
            {
                string filepath = string.Empty;
#if UNITY_EDITOR
                filepath = Application.dataPath + "/StreamingAssets/";
#elif UNITY_IPHONE
        filepath = Application.dataPath +"/Raw/";
#elif UNITY_ANDROID
        filepath = "jar:file://" + Application.dataPath + "!/assets/";
#endif
                return filepath;
            }

        }

        public static string PersistentData_Path
        {
            get
            {
                return Application.persistentDataPath + "/";
            }
        }
    }
}