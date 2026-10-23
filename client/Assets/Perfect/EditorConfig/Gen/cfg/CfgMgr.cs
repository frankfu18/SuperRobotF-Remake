using System.Xml;
using System;
using System.IO;
using System.Collections.Generic;

namespace editor
{
    namespace cfg
    {
        public sealed class CfgMgr
        {
            public delegate byte[] onLoadConfigDelegate(string file);
            public static string BinDir { get; set; } = "config";

            public static string XmlDir { get; set; } = "csv";
            public static onLoadConfigDelegate onLoadConfig;

            public static volatile CfgMgr Ins;

            private Dictionary<string, bool> configNotLoadDic = new Dictionary<string, bool>();

            private Perfect.Marshal.Octets CreateOctets(string file)
            {
                return new Perfect.Marshal.Octets(onLoadConfig?.Invoke(file));
            }

            private static List<string> GetDirFileNames(string[] dirs)
            {
                List<string> dirNames = new List<string>();
                foreach (string dir in dirs)
                {
                    string[] fileNames = Directory.GetFiles(XmlDir + "/" + dir);
                    dirNames.AddRange(fileNames);
                }

                return dirNames;
            }

            private static XmlElement CreateXmlElement(string file)
            {
                var doc = new XmlDocument();
                doc.Load(file);
                return doc.DocumentElement;
            }

            private System.Collections.Generic.Dictionary<string,cfg.asset.Asset> assetMap;
            public cfg.asset.Asset GetAsset(string key) { return assetMap.TryGetValue(key, out var v) ? v : null; }
            public System.Collections.Generic.Dictionary<string,cfg.asset.Asset> GetAssetMap() { return assetMap; }
            private System.Collections.Generic.List<cfg.asset.Asset> assetList;
            public System.Collections.Generic.List<cfg.asset.Asset> GetAssetList() { return assetList; }
             private System.Collections.Generic.Dictionary<string,cfg.localize.Language> languageMap;
            public cfg.localize.Language GetLanguage(string key) { return languageMap.TryGetValue(key, out var v) ? v : null; }
            public System.Collections.Generic.Dictionary<string,cfg.localize.Language> GetLanguageMap() { return languageMap; }
            private System.Collections.Generic.List<cfg.localize.Language> languageList;
            public System.Collections.Generic.List<cfg.localize.Language> GetLanguageList() { return languageList; }
             private System.Collections.Generic.Dictionary<string,cfg.localize.TextureLanguage> texturelanguageMap;
            public cfg.localize.TextureLanguage GetTextureLanguage(string key) { return texturelanguageMap.TryGetValue(key, out var v) ? v : null; }
            public System.Collections.Generic.Dictionary<string,cfg.localize.TextureLanguage> GetTextureLanguageMap() { return texturelanguageMap; }
            private System.Collections.Generic.List<cfg.localize.TextureLanguage> texturelanguageList;
            public System.Collections.Generic.List<cfg.localize.TextureLanguage> GetTextureLanguageList() { return texturelanguageList; }
             private System.Collections.Generic.Dictionary<string,cfg.sound.Sound> soundMap;
            public cfg.sound.Sound GetSound(string key) { return soundMap.TryGetValue(key, out var v) ? v : null; }
            public System.Collections.Generic.Dictionary<string,cfg.sound.Sound> GetSoundMap() { return soundMap; }
            private System.Collections.Generic.List<cfg.sound.Sound> soundList;
            public System.Collections.Generic.List<cfg.sound.Sound> GetSoundList() { return soundList; }
             private System.Collections.Generic.Dictionary<string,cfg.global.Global> globalMap;
            public cfg.global.Global GetGlobal(string key) { return globalMap.TryGetValue(key, out var v) ? v : null; }
            public System.Collections.Generic.Dictionary<string,cfg.global.Global> GetGlobalMap() { return globalMap; }
            private System.Collections.Generic.List<cfg.global.Global> globalList;
            public System.Collections.Generic.List<cfg.global.Global> GetGlobalList() { return globalList; }
             private System.Collections.Generic.Dictionary<cfg.common.ECurrencyType,cfg.common.Currency> currencyMap;
            public cfg.common.Currency GetCurrency(cfg.common.ECurrencyType key) { return currencyMap.TryGetValue(key, out var v) ? v : null; }
            public System.Collections.Generic.Dictionary<cfg.common.ECurrencyType,cfg.common.Currency> GetCurrencyMap() { return currencyMap; }
            private System.Collections.Generic.List<cfg.common.Currency> currencyList;
            public System.Collections.Generic.List<cfg.common.Currency> GetCurrencyList() { return currencyList; }
             private System.Collections.Generic.Dictionary<cfg.attribute.EAttributeType,cfg.attribute.Attribute> attributeMap;
            public cfg.attribute.Attribute GetAttribute(cfg.attribute.EAttributeType key) { return attributeMap.TryGetValue(key, out var v) ? v : null; }
            public System.Collections.Generic.Dictionary<cfg.attribute.EAttributeType,cfg.attribute.Attribute> GetAttributeMap() { return attributeMap; }
            private System.Collections.Generic.List<cfg.attribute.Attribute> attributeList;
            public System.Collections.Generic.List<cfg.attribute.Attribute> GetAttributeList() { return attributeList; }
             private System.Collections.Generic.Dictionary<int,cfg.item.Item> itemMap;
            public cfg.item.Item GetItem(int key) { return itemMap.TryGetValue(key, out var v) ? v : null; }
            public System.Collections.Generic.Dictionary<int,cfg.item.Item> GetItemMap() { return itemMap; }
            private System.Collections.Generic.List<cfg.item.Item> itemList;
            public System.Collections.Generic.List<cfg.item.Item> GetItemList() { return itemList; }
             private System.Collections.Generic.Dictionary<cfg.item.EItemQuality,cfg.item.Quality> qualityMap;
            public cfg.item.Quality GetQuality(cfg.item.EItemQuality key) { return qualityMap.TryGetValue(key, out var v) ? v : null; }
            public System.Collections.Generic.Dictionary<cfg.item.EItemQuality,cfg.item.Quality> GetQualityMap() { return qualityMap; }
            private System.Collections.Generic.List<cfg.item.Quality> qualityList;
            public System.Collections.Generic.List<cfg.item.Quality> GetQualityList() { return qualityList; }
             private System.Collections.Generic.Dictionary<int,cfg.bonus.BonusData> bonusdataMap;
            public cfg.bonus.BonusData GetBonusData(int key) { return bonusdataMap.TryGetValue(key, out var v) ? v : null; }
            public System.Collections.Generic.Dictionary<int,cfg.bonus.BonusData> GetBonusDataMap() { return bonusdataMap; }
            private System.Collections.Generic.List<cfg.bonus.BonusData> bonusdataList;
            public System.Collections.Generic.List<cfg.bonus.BonusData> GetBonusDataList() { return bonusdataList; }
             private System.Collections.Generic.Dictionary<string,cfg.error.Error> errorMap;
            public cfg.error.Error GetError(string key) { return errorMap.TryGetValue(key, out var v) ? v : null; }
            public System.Collections.Generic.Dictionary<string,cfg.error.Error> GetErrorMap() { return errorMap; }
            private System.Collections.Generic.List<cfg.error.Error> errorList;
            public System.Collections.Generic.List<cfg.error.Error> GetErrorList() { return errorList; }
             private System.Collections.Generic.Dictionary<int,cfg.skill.SkillReleaseConfig> skillreleaseconfigMap;
            public cfg.skill.SkillReleaseConfig GetSkillReleaseConfig(int key) { return skillreleaseconfigMap.TryGetValue(key, out var v) ? v : null; }
            public System.Collections.Generic.Dictionary<int,cfg.skill.SkillReleaseConfig> GetSkillReleaseConfigMap() { return skillreleaseconfigMap; }
            private System.Collections.Generic.List<cfg.skill.SkillReleaseConfig> skillreleaseconfigList;
            public System.Collections.Generic.List<cfg.skill.SkillReleaseConfig> GetSkillReleaseConfigList() { return skillreleaseconfigList; }
             private System.Collections.Generic.Dictionary<int,cfg.skill.BuffConfig> buffconfigMap;
            public cfg.skill.BuffConfig GetBuffConfig(int key) { return buffconfigMap.TryGetValue(key, out var v) ? v : null; }
            public System.Collections.Generic.Dictionary<int,cfg.skill.BuffConfig> GetBuffConfigMap() { return buffconfigMap; }
            private System.Collections.Generic.List<cfg.skill.BuffConfig> buffconfigList;
            public System.Collections.Generic.List<cfg.skill.BuffConfig> GetBuffConfigList() { return buffconfigList; }
             private System.Collections.Generic.Dictionary<int,cfg.skill.BulletConfig> bulletconfigMap;
            public cfg.skill.BulletConfig GetBulletConfig(int key) { return bulletconfigMap.TryGetValue(key, out var v) ? v : null; }
            public System.Collections.Generic.Dictionary<int,cfg.skill.BulletConfig> GetBulletConfigMap() { return bulletconfigMap; }
            private System.Collections.Generic.List<cfg.skill.BulletConfig> bulletconfigList;
            public System.Collections.Generic.List<cfg.skill.BulletConfig> GetBulletConfigList() { return bulletconfigList; }
             private System.Collections.Generic.Dictionary<string,cfg.battle.BattleAsset> battleassetMap;
            public cfg.battle.BattleAsset GetBattleAsset(string key) { return battleassetMap.TryGetValue(key, out var v) ? v : null; }
            public System.Collections.Generic.Dictionary<string,cfg.battle.BattleAsset> GetBattleAssetMap() { return battleassetMap; }
            private System.Collections.Generic.List<cfg.battle.BattleAsset> battleassetList;
            public System.Collections.Generic.List<cfg.battle.BattleAsset> GetBattleAssetList() { return battleassetList; }
             private System.Collections.Generic.Dictionary<int,cfg.function.Jump> jumpMap;
            public cfg.function.Jump GetJump(int key) { return jumpMap.TryGetValue(key, out var v) ? v : null; }
            public System.Collections.Generic.Dictionary<int,cfg.function.Jump> GetJumpMap() { return jumpMap; }
            private System.Collections.Generic.List<cfg.function.Jump> jumpList;
            public System.Collections.Generic.List<cfg.function.Jump> GetJumpList() { return jumpList; }
             

            private CfgMgr() {
                var assetOctets = CreateOctets("asset.bytes");
                if(assetOctets != null)
                {
                assetList = cfg.Extensions.unmarshal_list_cfg_asset_Asset(assetOctets);
                assetMap = new System.Collections.Generic.Dictionary<string,cfg.asset.Asset>();
                foreach(var v in assetList) { assetMap.Add(v.assetId, v); }
                }
                else{
                configNotLoadDic.Add("asset.bytes", false);
                }
                 
                var languageOctets = CreateOctets("language.bytes");
                if(languageOctets != null)
                {
                languageList = cfg.Extensions.unmarshal_list_cfg_localize_Language(languageOctets);
                languageMap = new System.Collections.Generic.Dictionary<string,cfg.localize.Language>();
                foreach(var v in languageList) { languageMap.Add(v.key, v); }
                }
                else{
                configNotLoadDic.Add("language.bytes", false);
                }
                 
                var texturelanguageOctets = CreateOctets("texturelanguage.bytes");
                if(texturelanguageOctets != null)
                {
                texturelanguageList = cfg.Extensions.unmarshal_list_cfg_localize_TextureLanguage(texturelanguageOctets);
                texturelanguageMap = new System.Collections.Generic.Dictionary<string,cfg.localize.TextureLanguage>();
                foreach(var v in texturelanguageList) { texturelanguageMap.Add(v.key, v); }
                }
                else{
                configNotLoadDic.Add("texturelanguage.bytes", false);
                }
                 
                var soundOctets = CreateOctets("sound.bytes");
                if(soundOctets != null)
                {
                soundList = cfg.Extensions.unmarshal_list_cfg_sound_Sound(soundOctets);
                soundMap = new System.Collections.Generic.Dictionary<string,cfg.sound.Sound>();
                foreach(var v in soundList) { soundMap.Add(v.id, v); }
                }
                else{
                configNotLoadDic.Add("sound.bytes", false);
                }
                 
                var globalOctets = CreateOctets("global.bytes");
                if(globalOctets != null)
                {
                globalList = cfg.Extensions.unmarshal_list_cfg_global_Global(globalOctets);
                globalMap = new System.Collections.Generic.Dictionary<string,cfg.global.Global>();
                foreach(var v in globalList) { globalMap.Add(v.key, v); }
                }
                else{
                configNotLoadDic.Add("global.bytes", false);
                }
                 
                var currencyOctets = CreateOctets("currency.bytes");
                if(currencyOctets != null)
                {
                currencyList = cfg.Extensions.unmarshal_list_cfg_common_Currency(currencyOctets);
                currencyMap = new System.Collections.Generic.Dictionary<cfg.common.ECurrencyType,cfg.common.Currency>();
                foreach(var v in currencyList) { currencyMap.Add(v.currencyType, v); }
                }
                else{
                configNotLoadDic.Add("currency.bytes", false);
                }
                 
                var attributeOctets = CreateOctets("attribute.bytes");
                if(attributeOctets != null)
                {
                attributeList = cfg.Extensions.unmarshal_list_cfg_attribute_Attribute(attributeOctets);
                attributeMap = new System.Collections.Generic.Dictionary<cfg.attribute.EAttributeType,cfg.attribute.Attribute>();
                foreach(var v in attributeList) { attributeMap.Add(v.attributeType, v); }
                }
                else{
                configNotLoadDic.Add("attribute.bytes", false);
                }
                 
                var itemOctets = CreateOctets("item.bytes");
                if(itemOctets != null)
                {
                itemList = cfg.Extensions.unmarshal_list_cfg_item_Item(itemOctets);
                itemMap = new System.Collections.Generic.Dictionary<int,cfg.item.Item>();
                foreach(var v in itemList) { itemMap.Add(v.id, v); }
                }
                else{
                configNotLoadDic.Add("item.bytes", false);
                }
                 
                var qualityOctets = CreateOctets("quality.bytes");
                if(qualityOctets != null)
                {
                qualityList = cfg.Extensions.unmarshal_list_cfg_item_Quality(qualityOctets);
                qualityMap = new System.Collections.Generic.Dictionary<cfg.item.EItemQuality,cfg.item.Quality>();
                foreach(var v in qualityList) { qualityMap.Add(v.quality, v); }
                }
                else{
                configNotLoadDic.Add("quality.bytes", false);
                }
                 
                var bonusdataOctets = CreateOctets("bonusdata.bytes");
                if(bonusdataOctets != null)
                {
                bonusdataList = cfg.Extensions.unmarshal_list_cfg_bonus_BonusData(bonusdataOctets);
                bonusdataMap = new System.Collections.Generic.Dictionary<int,cfg.bonus.BonusData>();
                foreach(var v in bonusdataList) { bonusdataMap.Add(v.id, v); }
                }
                else{
                configNotLoadDic.Add("bonusdata.bytes", false);
                }
                 
                var errorOctets = CreateOctets("error.bytes");
                if(errorOctets != null)
                {
                errorList = cfg.Extensions.unmarshal_list_cfg_error_Error(errorOctets);
                errorMap = new System.Collections.Generic.Dictionary<string,cfg.error.Error>();
                foreach(var v in errorList) { errorMap.Add(v.key, v); }
                }
                else{
                configNotLoadDic.Add("error.bytes", false);
                }
                 
                var skillreleaseconfigOctets = CreateOctets("skillreleaseconfig.bytes");
                if(skillreleaseconfigOctets != null)
                {
                skillreleaseconfigList = cfg.Extensions.unmarshal_list_cfg_skill_SkillReleaseConfig(skillreleaseconfigOctets);
                skillreleaseconfigMap = new System.Collections.Generic.Dictionary<int,cfg.skill.SkillReleaseConfig>();
                foreach(var v in skillreleaseconfigList) { skillreleaseconfigMap.Add(v.id, v); }
                }
                else{
                configNotLoadDic.Add("skillreleaseconfig.bytes", false);
                }
                 
                var buffconfigOctets = CreateOctets("buffconfig.bytes");
                if(buffconfigOctets != null)
                {
                buffconfigList = cfg.Extensions.unmarshal_list_cfg_skill_BuffConfig(buffconfigOctets);
                buffconfigMap = new System.Collections.Generic.Dictionary<int,cfg.skill.BuffConfig>();
                foreach(var v in buffconfigList) { buffconfigMap.Add(v.id, v); }
                }
                else{
                configNotLoadDic.Add("buffconfig.bytes", false);
                }
                 
                var bulletconfigOctets = CreateOctets("bulletconfig.bytes");
                if(bulletconfigOctets != null)
                {
                bulletconfigList = cfg.Extensions.unmarshal_list_cfg_skill_BulletConfig(bulletconfigOctets);
                bulletconfigMap = new System.Collections.Generic.Dictionary<int,cfg.skill.BulletConfig>();
                foreach(var v in bulletconfigList) { bulletconfigMap.Add(v.id, v); }
                }
                else{
                configNotLoadDic.Add("bulletconfig.bytes", false);
                }
                 
                var battleassetOctets = CreateOctets("battleasset.bytes");
                if(battleassetOctets != null)
                {
                battleassetList = cfg.Extensions.unmarshal_list_cfg_battle_BattleAsset(battleassetOctets);
                battleassetMap = new System.Collections.Generic.Dictionary<string,cfg.battle.BattleAsset>();
                foreach(var v in battleassetList) { battleassetMap.Add(v.assetId, v); }
                }
                else{
                configNotLoadDic.Add("battleasset.bytes", false);
                }
                 
                var jumpOctets = CreateOctets("jump.bytes");
                if(jumpOctets != null)
                {
                jumpList = cfg.Extensions.unmarshal_list_cfg_function_Jump(jumpOctets);
                jumpMap = new System.Collections.Generic.Dictionary<int,cfg.function.Jump>();
                foreach(var v in jumpList) { jumpMap.Add(v.id, v); }
                }
                else{
                configNotLoadDic.Add("jump.bytes", false);
                }
                 

            }

            public static void Load() {
                Ins = new CfgMgr();
            }

            public bool IsConfigLoad(string configName)
            {
                if(configNotLoadDic.TryGetValue(configName, out bool state))
                {
                    return state;
                }

                return false;
            }
        }
    }
}
