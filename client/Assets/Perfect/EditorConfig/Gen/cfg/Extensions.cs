using System.Xml;
namespace editor
{
    namespace cfg
    {
        public static class Extensions
        {
            public static cfg.skill.SkillReleaseRule unmarshal_cfg_skill_SkillReleaseRule(Perfect.Marshal.Octets os) {
                            var id = os.ReadInt();
                            cfg.skill.SkillReleaseRule x;
                            switch(id) {
                            case 0: return null;
                            case 2326 : x = new cfg.skill.NoRelease(os); break;case 59032 : x = new cfg.skill.ImmediateRelease(os); break;case 31335 : x = new cfg.skill.TargetInRangeRelease(os); break;default: throw new Perfect.Marshal.MarshalException("unknown bean id:" + id);}
                            return x;
                         }
            public static cfg.battle.EffectAssetInfo unmarshal_cfg_battle_EffectAssetInfo(Perfect.Marshal.Octets os) {
                            return new cfg.battle.EffectAssetInfo(os);
                         }
            public static cfg.skill.HpRatioLessIntervalTrigger unmarshal_cfg_skill_HpRatioLessIntervalTrigger(Perfect.Marshal.Octets os) {
                            return new cfg.skill.HpRatioLessIntervalTrigger(os);
                         }
            public static cfg.global.GInt64 unmarshal_cfg_global_GInt64(Perfect.Marshal.Octets os) {
                            return new cfg.global.GInt64(os);
                         }
            public static System.Collections.Generic.List<cfg.skill.SkillReleaseConfig> unmarshal_list_cfg_skill_SkillReleaseConfig(Perfect.Marshal.Octets os) {
                            var n = System.Math.Min(os.ReadSize(), os.Size + 1);
                            var x = new System.Collections.Generic.List<cfg.skill.SkillReleaseConfig>(n);
                            for(int i = 0 ; i < n ; i++) {
                            x.Add(cfg.Extensions.unmarshal_cfg_skill_SkillReleaseConfig(os));
                            }return x;
                         }
            public static cfg.item.ItemInfo unmarshal_cfg_item_ItemInfo(Perfect.Marshal.Octets os) {
                            var id = os.ReadInt();
                            cfg.item.ItemInfo x;
                            switch(id) {
                            case 0: return null;
                            case 7166 : x = new cfg.item.None(os); break;case 46305 : x = new cfg.item.GiftInfo(os); break;case 46661 : x = new cfg.item.ArenaBoxInfo(os); break;default: throw new Perfect.Marshal.MarshalException("unknown bean id:" + id);}
                            return x;
                         }
            public static cfg.skill.EffectiveDamageToHeal unmarshal_cfg_skill_EffectiveDamageToHeal(Perfect.Marshal.Octets os) {
                            return new cfg.skill.EffectiveDamageToHeal(os);
                         }
            public static cfg.function.Jump unmarshal_cfg_function_Jump(Perfect.Marshal.Octets os) {
                            return new cfg.function.Jump(os);
                         }
            public static cfg.item.None unmarshal_cfg_item_None(Perfect.Marshal.Octets os) {
                            return new cfg.item.None(os);
                         }
            public static cfg.localize.TexSizeElement unmarshal_cfg_localize_TexSizeElement(Perfect.Marshal.Octets os) {
                            return new cfg.localize.TexSizeElement(os);
                         }
            public static cfg.error.Error unmarshal_cfg_error_Error(Perfect.Marshal.Octets os) {
                            return new cfg.error.Error(os);
                         }
            public static cfg.skill.NoRelease unmarshal_cfg_skill_NoRelease(Perfect.Marshal.Octets os) {
                            return new cfg.skill.NoRelease(os);
                         }
            public static cfg.skill.Single unmarshal_cfg_skill_Single(Perfect.Marshal.Octets os) {
                            return new cfg.skill.Single(os);
                         }
            public static System.Collections.Generic.List<cfg.localize.TexLocalizeElement> unmarshal_list_cfg_localize_TexLocalizeElement(Perfect.Marshal.Octets os) {
                            var n = System.Math.Min(os.ReadSize(), os.Size + 1);
                            var x = new System.Collections.Generic.List<cfg.localize.TexLocalizeElement>(n);
                            for(int i = 0 ; i < n ; i++) {
                            x.Add(cfg.Extensions.unmarshal_cfg_localize_TexLocalizeElement(os));
                            }return x;
                         }
            public static cfg.skill.BulletHitDamageEffect unmarshal_cfg_skill_BulletHitDamageEffect(Perfect.Marshal.Octets os) {
                            return new cfg.skill.BulletHitDamageEffect(os);
                         }
            public static cfg.skill.ImmediateBullet unmarshal_cfg_skill_ImmediateBullet(Perfect.Marshal.Octets os) {
                            return new cfg.skill.ImmediateBullet(os);
                         }
            public static cfg.localize.TexAssetElement unmarshal_cfg_localize_TexAssetElement(Perfect.Marshal.Octets os) {
                            return new cfg.localize.TexAssetElement(os);
                         }
            public static cfg.skill.AddBulletHitEffect unmarshal_cfg_skill_AddBulletHitEffect(Perfect.Marshal.Octets os) {
                            return new cfg.skill.AddBulletHitEffect(os);
                         }
            public static cfg.skill.BuffConfig unmarshal_cfg_skill_BuffConfig(Perfect.Marshal.Octets os) {
                            return new cfg.skill.BuffConfig(os);
                         }
            public static cfg.global.Global unmarshal_cfg_global_Global(Perfect.Marshal.Octets os) {
                            return new cfg.global.Global(os);
                         }
            public static cfg.skill.BulletConfig unmarshal_cfg_skill_BulletConfig(Perfect.Marshal.Octets os) {
                            return new cfg.skill.BulletConfig(os);
                         }
            public static cfg.skill.BuffDestroyType unmarshal_cfg_skill_BuffDestroyType(Perfect.Marshal.Octets os) {
                            var id = os.ReadInt();
                            cfg.skill.BuffDestroyType x;
                            switch(id) {
                            case 0: return null;
                            case 17080 : x = new cfg.skill.ImmediateDestroy(os); break;case 16224 : x = new cfg.skill.Forever(os); break;case 20871 : x = new cfg.skill.DurationDestroy(os); break;default: throw new Perfect.Marshal.MarshalException("unknown bean id:" + id);}
                            return x;
                         }
            public static cfg.skill.AddBuff unmarshal_cfg_skill_AddBuff(Perfect.Marshal.Octets os) {
                            return new cfg.skill.AddBuff(os);
                         }
            public static cfg.battle.None unmarshal_cfg_battle_None(Perfect.Marshal.Octets os) {
                            return new cfg.battle.None(os);
                         }
            public static cfg.bonus.Items unmarshal_cfg_bonus_Items(Perfect.Marshal.Octets os) {
                            return new cfg.bonus.Items(os);
                         }
            public static cfg.localize.TexLocalizeElements unmarshal_cfg_localize_TexLocalizeElements(Perfect.Marshal.Octets os) {
                            return new cfg.localize.TexLocalizeElements(os);
                         }
            public static System.Collections.Generic.List<cfg.condition.Condition> unmarshal_list_cfg_condition_Condition(Perfect.Marshal.Octets os) {
                            var n = System.Math.Min(os.ReadSize(), os.Size + 1);
                            var x = new System.Collections.Generic.List<cfg.condition.Condition>(n);
                            for(int i = 0 ; i < n ; i++) {
                            x.Add(cfg.Extensions.unmarshal_cfg_condition_Condition(os));
                            }return x;
                         }
            public static cfg.skill.KnockbackTriggerTarget unmarshal_cfg_skill_KnockbackTriggerTarget(Perfect.Marshal.Octets os) {
                            return new cfg.skill.KnockbackTriggerTarget(os);
                         }
            public static cfg.localize.TexLocalizeElement unmarshal_cfg_localize_TexLocalizeElement(Perfect.Marshal.Octets os) {
                            var id = os.ReadInt();
                            cfg.localize.TexLocalizeElement x;
                            switch(id) {
                            case 0: return null;
                            case 31510 : x = new cfg.localize.TexAssetElement(os); break;case 17934 : x = new cfg.localize.TexLocalizeElements(os); break;case 32342 : x = new cfg.localize.TexPositionElement(os); break;case 23750 : x = new cfg.localize.TexScaleElement(os); break;case 14865 : x = new cfg.localize.TexSizeElement(os); break;default: throw new Perfect.Marshal.MarshalException("unknown bean id:" + id);}
                            return x;
                         }
            public static cfg.skill.SkillReleaseConfig unmarshal_cfg_skill_SkillReleaseConfig(Perfect.Marshal.Octets os) {
                            return new cfg.skill.SkillReleaseConfig(os);
                         }
            public static cfg.skill.SkillShowOnceEffect unmarshal_cfg_skill_SkillShowOnceEffect(Perfect.Marshal.Octets os) {
                            return new cfg.skill.SkillShowOnceEffect(os);
                         }
            public static cfg.skill.CollisionHitTargetTrigger unmarshal_cfg_skill_CollisionHitTargetTrigger(Perfect.Marshal.Octets os) {
                            return new cfg.skill.CollisionHitTargetTrigger(os);
                         }
            public static cfg.skill.FixedDamage unmarshal_cfg_skill_FixedDamage(Perfect.Marshal.Octets os) {
                            return new cfg.skill.FixedDamage(os);
                         }
            public static System.Collections.Generic.List<cfg.bonus.WeightBonus> unmarshal_list_cfg_bonus_WeightBonus(Perfect.Marshal.Octets os) {
                            var n = System.Math.Min(os.ReadSize(), os.Size + 1);
                            var x = new System.Collections.Generic.List<cfg.bonus.WeightBonus>(n);
                            for(int i = 0 ; i < n ; i++) {
                            x.Add(cfg.Extensions.unmarshal_cfg_bonus_WeightBonus(os));
                            }return x;
                         }
            public static cfg.condition.None unmarshal_cfg_condition_None(Perfect.Marshal.Octets os) {
                            return new cfg.condition.None(os);
                         }
            public static cfg.sound.Audio unmarshal_cfg_sound_Audio(Perfect.Marshal.Octets os) {
                            var id = os.ReadInt();
                            cfg.sound.Audio x;
                            switch(id) {
                            case 0: return null;
                            case 11565 : x = new cfg.sound.SoundEffect(os); break;case 58755 : x = new cfg.sound.Music(os); break;default: throw new Perfect.Marshal.MarshalException("unknown bean id:" + id);}
                            return x;
                         }
            public static cfg.skill.BulletHitEffects unmarshal_cfg_skill_BulletHitEffects(Perfect.Marshal.Octets os) {
                            return new cfg.skill.BulletHitEffects(os);
                         }
            public static cfg.global.GString unmarshal_cfg_global_GString(Perfect.Marshal.Octets os) {
                            return new cfg.global.GString(os);
                         }
            public static System.Collections.Generic.Dictionary<cfg.localize.ELanguageType,cfg.localize.TexLocalizeElement> unmarshal_map_cfg_localize_ELanguageType_cfg_localize_TexLocalizeElement(Perfect.Marshal.Octets os) {
                            var n = System.Math.Min(os.ReadSize(), os.Size + 1);
                            var x = new System.Collections.Generic.Dictionary<cfg.localize.ELanguageType,cfg.localize.TexLocalizeElement>(n * 7 / 5);
                            for(int i = 0 ; i < n ; i++) {
                            x.Add((cfg.localize.ELanguageType)(os.ReadInt()), cfg.Extensions.unmarshal_cfg_localize_TexLocalizeElement(os));
                            }return x;
                         }
            public static cfg.skill.MountPoint unmarshal_cfg_skill_MountPoint(Perfect.Marshal.Octets os) {
                            return new cfg.skill.MountPoint(os);
                         }
            public static System.Collections.Generic.List<cfg.localize.Language> unmarshal_list_cfg_localize_Language(Perfect.Marshal.Octets os) {
                            var n = System.Math.Min(os.ReadSize(), os.Size + 1);
                            var x = new System.Collections.Generic.List<cfg.localize.Language>(n);
                            for(int i = 0 ; i < n ; i++) {
                            x.Add(cfg.Extensions.unmarshal_cfg_localize_Language(os));
                            }return x;
                         }
            public static cfg.skill.BuffAssetInfo unmarshal_cfg_skill_BuffAssetInfo(Perfect.Marshal.Octets os) {
                            var id = os.ReadInt();
                            cfg.skill.BuffAssetInfo x;
                            switch(id) {
                            case 0: return null;
                            case 27089 : x = new cfg.skill.NoneAsset(os); break;case 51394 : x = new cfg.skill.MountPoint(os); break;case 50362 : x = new cfg.skill.OnceShow(os); break;default: throw new Perfect.Marshal.MarshalException("unknown bean id:" + id);}
                            return x;
                         }
            public static cfg.skill.OnceShow unmarshal_cfg_skill_OnceShow(Perfect.Marshal.Octets os) {
                            return new cfg.skill.OnceShow(os);
                         }
            public static cfg.function.JumpBattleInfo unmarshal_cfg_function_JumpBattleInfo(Perfect.Marshal.Octets os) {
                            return new cfg.function.JumpBattleInfo(os);
                         }
            public static System.Collections.Generic.List<cfg.localize.TextureLanguage> unmarshal_list_cfg_localize_TextureLanguage(Perfect.Marshal.Octets os) {
                            var n = System.Math.Min(os.ReadSize(), os.Size + 1);
                            var x = new System.Collections.Generic.List<cfg.localize.TextureLanguage>(n);
                            for(int i = 0 ; i < n ; i++) {
                            x.Add(cfg.Extensions.unmarshal_cfg_localize_TextureLanguage(os));
                            }return x;
                         }
            public static cfg.bonus.CopyBonus unmarshal_cfg_bonus_CopyBonus(Perfect.Marshal.Octets os) {
                            return new cfg.bonus.CopyBonus(os);
                         }
            public static cfg.skill.SkillShowNoEffect unmarshal_cfg_skill_SkillShowNoEffect(Perfect.Marshal.Octets os) {
                            return new cfg.skill.SkillShowNoEffect(os);
                         }
            public static cfg.common.XBigDecimal unmarshal_cfg_common_XBigDecimal(Perfect.Marshal.Octets os) {
                            return new cfg.common.XBigDecimal(os);
                         }
            public static cfg.skill.AttributeChange unmarshal_cfg_skill_AttributeChange(Perfect.Marshal.Octets os) {
                            return new cfg.skill.AttributeChange(os);
                         }
            public static cfg.battle.BattleAssetInfo unmarshal_cfg_battle_BattleAssetInfo(Perfect.Marshal.Octets os) {
                            var id = os.ReadInt();
                            cfg.battle.BattleAssetInfo x;
                            switch(id) {
                            case 0: return null;
                            case 26411 : x = new cfg.battle.None(os); break;case 37120 : x = new cfg.battle.EffectAssetInfo(os); break;default: throw new Perfect.Marshal.MarshalException("unknown bean id:" + id);}
                            return x;
                         }
            public static cfg.skill.NoneAsset unmarshal_cfg_skill_NoneAsset(Perfect.Marshal.Octets os) {
                            return new cfg.skill.NoneAsset(os);
                         }
            public static System.Collections.Generic.List<cfg.sound.Sound> unmarshal_list_cfg_sound_Sound(Perfect.Marshal.Octets os) {
                            var n = System.Math.Min(os.ReadSize(), os.Size + 1);
                            var x = new System.Collections.Generic.List<cfg.sound.Sound>(n);
                            for(int i = 0 ; i < n ; i++) {
                            x.Add(cfg.Extensions.unmarshal_cfg_sound_Sound(os));
                            }return x;
                         }
            public static cfg.skill.BuffTriggerType unmarshal_cfg_skill_BuffTriggerType(Perfect.Marshal.Octets os) {
                            var id = os.ReadInt();
                            cfg.skill.BuffTriggerType x;
                            switch(id) {
                            case 0: return null;
                            case 27531 : x = new cfg.skill.ImmediateTrigger(os); break;case 8067 : x = new cfg.skill.IntervalTrigger(os); break;case 52473 : x = new cfg.skill.CollisionHitTargetTrigger(os); break;case 16931 : x = new cfg.skill.BeforeCollisionDamageTrigger(os); break;case 22894 : x = new cfg.skill.BeCollidedTrigger(os); break;case 22375 : x = new cfg.skill.DealEffectiveDamageTrigger(os); break;case 20225 : x = new cfg.skill.HpRatioLessIntervalTrigger(os); break;default: throw new Perfect.Marshal.MarshalException("unknown bean id:" + id);}
                            return x;
                         }
            public static cfg.condition.Currency unmarshal_cfg_condition_Currency(Perfect.Marshal.Octets os) {
                            return new cfg.condition.Currency(os);
                         }
            public static cfg.condition.Condition unmarshal_cfg_condition_Condition(Perfect.Marshal.Octets os) {
                            var id = os.ReadInt();
                            cfg.condition.Condition x;
                            switch(id) {
                            case 0: return null;
                            case 51733 : x = new cfg.condition.None(os); break;case 53049 : x = new cfg.condition.Currency(os); break;case 65080 : x = new cfg.condition.Item(os); break;case 28544 : x = new cfg.condition.BuyMonthlyCard(os); break;case 23955 : x = new cfg.condition.OwnItem(os); break;case 28076 : x = new cfg.condition.And(os); break;case 24596 : x = new cfg.condition.Or(os); break;default: throw new Perfect.Marshal.MarshalException("unknown bean id:" + id);}
                            return x;
                         }
            public static System.Collections.Generic.List<cfg.asset.Asset> unmarshal_list_cfg_asset_Asset(Perfect.Marshal.Octets os) {
                            var n = System.Math.Min(os.ReadSize(), os.Size + 1);
                            var x = new System.Collections.Generic.List<cfg.asset.Asset>(n);
                            for(int i = 0 ; i < n ; i++) {
                            x.Add(cfg.Extensions.unmarshal_cfg_asset_Asset(os));
                            }return x;
                         }
            public static cfg.localize.TexPositionElement unmarshal_cfg_localize_TexPositionElement(Perfect.Marshal.Octets os) {
                            return new cfg.localize.TexPositionElement(os);
                         }
            public static cfg.bonus.Item unmarshal_cfg_bonus_Item(Perfect.Marshal.Octets os) {
                            return new cfg.bonus.Item(os);
                         }
            public static System.Collections.Generic.List<cfg.bonus.Bonus> unmarshal_list_cfg_bonus_Bonus(Perfect.Marshal.Octets os) {
                            var n = System.Math.Min(os.ReadSize(), os.Size + 1);
                            var x = new System.Collections.Generic.List<cfg.bonus.Bonus>(n);
                            for(int i = 0 ; i < n ; i++) {
                            x.Add(cfg.Extensions.unmarshal_cfg_bonus_Bonus(os));
                            }return x;
                         }
            public static System.Collections.Generic.List<cfg.attribute.Attribute> unmarshal_list_cfg_attribute_Attribute(Perfect.Marshal.Octets os) {
                            var n = System.Math.Min(os.ReadSize(), os.Size + 1);
                            var x = new System.Collections.Generic.List<cfg.attribute.Attribute>(n);
                            for(int i = 0 ; i < n ; i++) {
                            x.Add(cfg.Extensions.unmarshal_cfg_attribute_Attribute(os));
                            }return x;
                         }
            public static cfg.skill.Forever unmarshal_cfg_skill_Forever(Perfect.Marshal.Octets os) {
                            return new cfg.skill.Forever(os);
                         }
            public static cfg.item.Item unmarshal_cfg_item_Item(Perfect.Marshal.Octets os) {
                            return new cfg.item.Item(os);
                         }
            public static cfg.common.Currency unmarshal_cfg_common_Currency(Perfect.Marshal.Octets os) {
                            return new cfg.common.Currency(os);
                         }
            public static cfg.common.CurrencyCost unmarshal_cfg_common_CurrencyCost(Perfect.Marshal.Octets os) {
                            return new cfg.common.CurrencyCost(os);
                         }
            public static cfg.skill.StackReset unmarshal_cfg_skill_StackReset(Perfect.Marshal.Octets os) {
                            return new cfg.skill.StackReset(os);
                         }
            public static cfg.skill.AddShield unmarshal_cfg_skill_AddShield(Perfect.Marshal.Octets os) {
                            return new cfg.skill.AddShield(os);
                         }
            public static System.Collections.Generic.List<cfg.bonus.BonusData> unmarshal_list_cfg_bonus_BonusData(Perfect.Marshal.Octets os) {
                            var n = System.Math.Min(os.ReadSize(), os.Size + 1);
                            var x = new System.Collections.Generic.List<cfg.bonus.BonusData>(n);
                            for(int i = 0 ; i < n ; i++) {
                            x.Add(cfg.Extensions.unmarshal_cfg_bonus_BonusData(os));
                            }return x;
                         }
            public static cfg.bonus.Empty unmarshal_cfg_bonus_Empty(Perfect.Marshal.Octets os) {
                            return new cfg.bonus.Empty(os);
                         }
            public static cfg.attribute.AttributeInfo unmarshal_cfg_attribute_AttributeInfo(Perfect.Marshal.Octets os) {
                            var id = os.ReadInt();
                            cfg.attribute.AttributeInfo x;
                            switch(id) {
                            case 0: return null;
                            case 24919 : x = new cfg.attribute.None(os); break;case 20666 : x = new cfg.attribute.AttributeNumber(os); break;case 35280 : x = new cfg.attribute.AttributeLongNumber(os); break;case 43740 : x = new cfg.attribute.And(os); break;default: throw new Perfect.Marshal.MarshalException("unknown bean id:" + id);}
                            return x;
                         }
            public static cfg.sound.Sound unmarshal_cfg_sound_Sound(Perfect.Marshal.Octets os) {
                            return new cfg.sound.Sound(os);
                         }
            public static cfg.Vector2Int unmarshal_cfg_Vector2Int(Perfect.Marshal.Octets os) {
                            return new cfg.Vector2Int(os);
                         }
            public static System.Collections.Generic.List<cfg.bonus.Currency> unmarshal_list_cfg_bonus_Currency(Perfect.Marshal.Octets os) {
                            var n = System.Math.Min(os.ReadSize(), os.Size + 1);
                            var x = new System.Collections.Generic.List<cfg.bonus.Currency>(n);
                            for(int i = 0 ; i < n ; i++) {
                            x.Add(cfg.Extensions.unmarshal_cfg_bonus_Currency(os));
                            }return x;
                         }
            public static cfg.attribute.AttributeLongNumber unmarshal_cfg_attribute_AttributeLongNumber(Perfect.Marshal.Octets os) {
                            return new cfg.attribute.AttributeLongNumber(os);
                         }
            public static cfg.common.XFloat unmarshal_cfg_common_XFloat(Perfect.Marshal.Octets os) {
                            return new cfg.common.XFloat(os);
                         }
            public static cfg.common.XInt unmarshal_cfg_common_XInt(Perfect.Marshal.Octets os) {
                            return new cfg.common.XInt(os);
                         }
            public static cfg.skill.SkillReleaseEmpty unmarshal_cfg_skill_SkillReleaseEmpty(Perfect.Marshal.Octets os) {
                            return new cfg.skill.SkillReleaseEmpty(os);
                         }
            public static cfg.skill.BulletHitBuffEffect unmarshal_cfg_skill_BulletHitBuffEffect(Perfect.Marshal.Octets os) {
                            return new cfg.skill.BulletHitBuffEffect(os);
                         }
            public static cfg.asset.Asset unmarshal_cfg_asset_Asset(Perfect.Marshal.Octets os) {
                            return new cfg.asset.Asset(os);
                         }
            public static cfg.asset.AssetInfo unmarshal_cfg_asset_AssetInfo(Perfect.Marshal.Octets os) {
                            var id = os.ReadInt();
                            cfg.asset.AssetInfo x;
                            switch(id) {
                            case 0: return null;
                            case 6998 : x = new cfg.asset.Texture(os); break;case 3106 : x = new cfg.asset.Sprite(os); break;case 42126 : x = new cfg.asset.Spine(os); break;default: throw new Perfect.Marshal.MarshalException("unknown bean id:" + id);}
                            return x;
                         }
            public static cfg.skill.StackNone unmarshal_cfg_skill_StackNone(Perfect.Marshal.Octets os) {
                            return new cfg.skill.StackNone(os);
                         }
            public static cfg.function.AdGetCurrencyInfo unmarshal_cfg_function_AdGetCurrencyInfo(Perfect.Marshal.Octets os) {
                            return new cfg.function.AdGetCurrencyInfo(os);
                         }
            public static cfg.skill.StackNum unmarshal_cfg_skill_StackNum(Perfect.Marshal.Octets os) {
                            return new cfg.skill.StackNum(os);
                         }
            public static System.Collections.Generic.List<cfg.attribute.AttributeInfo> unmarshal_list_cfg_attribute_AttributeInfo(Perfect.Marshal.Octets os) {
                            var n = System.Math.Min(os.ReadSize(), os.Size + 1);
                            var x = new System.Collections.Generic.List<cfg.attribute.AttributeInfo>(n);
                            for(int i = 0 ; i < n ; i++) {
                            x.Add(cfg.Extensions.unmarshal_cfg_attribute_AttributeInfo(os));
                            }return x;
                         }
            public static cfg.condition.BuyMonthlyCard unmarshal_cfg_condition_BuyMonthlyCard(Perfect.Marshal.Octets os) {
                            return new cfg.condition.BuyMonthlyCard(os);
                         }
            public static System.Collections.Generic.List<cfg.skill.BulletConfig> unmarshal_list_cfg_skill_BulletConfig(Perfect.Marshal.Octets os) {
                            var n = System.Math.Min(os.ReadSize(), os.Size + 1);
                            var x = new System.Collections.Generic.List<cfg.skill.BulletConfig>(n);
                            for(int i = 0 ; i < n ; i++) {
                            x.Add(cfg.Extensions.unmarshal_cfg_skill_BulletConfig(os));
                            }return x;
                         }
            public static cfg.skill.IntervalTrigger unmarshal_cfg_skill_IntervalTrigger(Perfect.Marshal.Octets os) {
                            return new cfg.skill.IntervalTrigger(os);
                         }
            public static System.Collections.Generic.List<cfg.item.Quality> unmarshal_list_cfg_item_Quality(Perfect.Marshal.Octets os) {
                            var n = System.Math.Min(os.ReadSize(), os.Size + 1);
                            var x = new System.Collections.Generic.List<cfg.item.Quality>(n);
                            for(int i = 0 ; i < n ; i++) {
                            x.Add(cfg.Extensions.unmarshal_cfg_item_Quality(os));
                            }return x;
                         }
            public static cfg.sound.Music unmarshal_cfg_sound_Music(Perfect.Marshal.Octets os) {
                            return new cfg.sound.Music(os);
                         }
            public static cfg.global.GBool unmarshal_cfg_global_GBool(Perfect.Marshal.Octets os) {
                            return new cfg.global.GBool(os);
                         }
            public static cfg.skill.ImmuneCurrentDamage unmarshal_cfg_skill_ImmuneCurrentDamage(Perfect.Marshal.Octets os) {
                            return new cfg.skill.ImmuneCurrentDamage(os);
                         }
            public static cfg.skill.DurationDestroy unmarshal_cfg_skill_DurationDestroy(Perfect.Marshal.Octets os) {
                            return new cfg.skill.DurationDestroy(os);
                         }
            public static cfg.bonus.RandomEquipFragmentQuality unmarshal_cfg_bonus_RandomEquipFragmentQuality(Perfect.Marshal.Octets os) {
                            return new cfg.bonus.RandomEquipFragmentQuality(os);
                         }
            public static cfg.skill.AddBoostEnergy unmarshal_cfg_skill_AddBoostEnergy(Perfect.Marshal.Octets os) {
                            return new cfg.skill.AddBoostEnergy(os);
                         }
            public static cfg.bonus.MultiBonus unmarshal_cfg_bonus_MultiBonus(Perfect.Marshal.Octets os) {
                            return new cfg.bonus.MultiBonus(os);
                         }
            public static cfg.function.JumpShopInfo unmarshal_cfg_function_JumpShopInfo(Perfect.Marshal.Octets os) {
                            return new cfg.function.JumpShopInfo(os);
                         }
            public static cfg.global.GFloat unmarshal_cfg_global_GFloat(Perfect.Marshal.Octets os) {
                            return new cfg.global.GFloat(os);
                         }
            public static cfg.asset.Spine unmarshal_cfg_asset_Spine(Perfect.Marshal.Octets os) {
                            return new cfg.asset.Spine(os);
                         }
            public static cfg.bonus.RandomBonus unmarshal_cfg_bonus_RandomBonus(Perfect.Marshal.Octets os) {
                            return new cfg.bonus.RandomBonus(os);
                         }
            public static cfg.skill.ImmediateTrigger unmarshal_cfg_skill_ImmediateTrigger(Perfect.Marshal.Octets os) {
                            return new cfg.skill.ImmediateTrigger(os);
                         }
            public static cfg.localize.TextureLanguage unmarshal_cfg_localize_TextureLanguage(Perfect.Marshal.Octets os) {
                            return new cfg.localize.TextureLanguage(os);
                         }
            public static cfg.common.XLong unmarshal_cfg_common_XLong(Perfect.Marshal.Octets os) {
                            return new cfg.common.XLong(os);
                         }
            public static cfg.skill.ReverseMoveDirection unmarshal_cfg_skill_ReverseMoveDirection(Perfect.Marshal.Octets os) {
                            return new cfg.skill.ReverseMoveDirection(os);
                         }
            public static cfg.skill.BulletTargetType unmarshal_cfg_skill_BulletTargetType(Perfect.Marshal.Octets os) {
                            var id = os.ReadInt();
                            cfg.skill.BulletTargetType x;
                            switch(id) {
                            case 0: return null;
                            case 56255 : x = new cfg.skill.SingleTarget(os); break;case 25258 : x = new cfg.skill.RangeTarget(os); break;case 64845 : x = new cfg.skill.DirectionTarget(os); break;default: throw new Perfect.Marshal.MarshalException("unknown bean id:" + id);}
                            return x;
                         }
            public static cfg.skill.SkillShowEffect unmarshal_cfg_skill_SkillShowEffect(Perfect.Marshal.Octets os) {
                            var id = os.ReadInt();
                            cfg.skill.SkillShowEffect x;
                            switch(id) {
                            case 0: return null;
                            case 58940 : x = new cfg.skill.SkillShowNoEffect(os); break;case 555 : x = new cfg.skill.SkillShowOnceEffect(os); break;default: throw new Perfect.Marshal.MarshalException("unknown bean id:" + id);}
                            return x;
                         }
            public static cfg.skill.ImmediateRelease unmarshal_cfg_skill_ImmediateRelease(Perfect.Marshal.Octets os) {
                            return new cfg.skill.ImmediateRelease(os);
                         }
            public static cfg.skill.SkillReleaseBuff unmarshal_cfg_skill_SkillReleaseBuff(Perfect.Marshal.Octets os) {
                            return new cfg.skill.SkillReleaseBuff(os);
                         }
            public static cfg.bonus.WeightRandomBonus unmarshal_cfg_bonus_WeightRandomBonus(Perfect.Marshal.Octets os) {
                            return new cfg.bonus.WeightRandomBonus(os);
                         }
            public static cfg.global.GInt32 unmarshal_cfg_global_GInt32(Perfect.Marshal.Octets os) {
                            return new cfg.global.GInt32(os);
                         }
            public static cfg.Vector2 unmarshal_cfg_Vector2(Perfect.Marshal.Octets os) {
                            return new cfg.Vector2(os);
                         }
            public static cfg.bonus.Currencies unmarshal_cfg_bonus_Currencies(Perfect.Marshal.Octets os) {
                            return new cfg.bonus.Currencies(os);
                         }
            public static cfg.common.AdCost unmarshal_cfg_common_AdCost(Perfect.Marshal.Octets os) {
                            return new cfg.common.AdCost(os);
                         }
            public static cfg.skill.Continuous unmarshal_cfg_skill_Continuous(Perfect.Marshal.Octets os) {
                            return new cfg.skill.Continuous(os);
                         }
            public static cfg.Vector3 unmarshal_cfg_Vector3(Perfect.Marshal.Octets os) {
                            return new cfg.Vector3(os);
                         }
            public static cfg.skill.BuffStatcking unmarshal_cfg_skill_BuffStatcking(Perfect.Marshal.Octets os) {
                            var id = os.ReadInt();
                            cfg.skill.BuffStatcking x;
                            switch(id) {
                            case 0: return null;
                            case 33081 : x = new cfg.skill.StackNone(os); break;case 35619 : x = new cfg.skill.StackReset(os); break;case 43531 : x = new cfg.skill.StackNum(os); break;default: throw new Perfect.Marshal.MarshalException("unknown bean id:" + id);}
                            return x;
                         }
            public static cfg.global.GlobalInfo unmarshal_cfg_global_GlobalInfo(Perfect.Marshal.Octets os) {
                            var id = os.ReadInt();
                            cfg.global.GlobalInfo x;
                            switch(id) {
                            case 0: return null;
                            case 31643 : x = new cfg.global.GInt32(os); break;case 31738 : x = new cfg.global.GInt64(os); break;case 11475 : x = new cfg.global.GString(os); break;case 16276 : x = new cfg.global.GFloat(os); break;case 44923 : x = new cfg.global.GBool(os); break;default: throw new Perfect.Marshal.MarshalException("unknown bean id:" + id);}
                            return x;
                         }
            public static System.Collections.Generic.List<cfg.global.Global> unmarshal_list_cfg_global_Global(Perfect.Marshal.Octets os) {
                            var n = System.Math.Min(os.ReadSize(), os.Size + 1);
                            var x = new System.Collections.Generic.List<cfg.global.Global>(n);
                            for(int i = 0 ; i < n ; i++) {
                            x.Add(cfg.Extensions.unmarshal_cfg_global_Global(os));
                            }return x;
                         }
            public static System.Collections.Generic.List<cfg.skill.BulletHitEffectType> unmarshal_list_cfg_skill_BulletHitEffectType(Perfect.Marshal.Octets os) {
                            var n = System.Math.Min(os.ReadSize(), os.Size + 1);
                            var x = new System.Collections.Generic.List<cfg.skill.BulletHitEffectType>(n);
                            for(int i = 0 ; i < n ; i++) {
                            x.Add(cfg.Extensions.unmarshal_cfg_skill_BulletHitEffectType(os));
                            }return x;
                         }
            public static cfg.common.None unmarshal_cfg_common_None(Perfect.Marshal.Octets os) {
                            return new cfg.common.None(os);
                         }
            public static cfg.common.MoneyCost unmarshal_cfg_common_MoneyCost(Perfect.Marshal.Octets os) {
                            return new cfg.common.MoneyCost(os);
                         }
            public static cfg.skill.ReleaseBulletAtCollisionPoint unmarshal_cfg_skill_ReleaseBulletAtCollisionPoint(Perfect.Marshal.Octets os) {
                            return new cfg.skill.ReleaseBulletAtCollisionPoint(os);
                         }
            public static cfg.skill.FlyBullet unmarshal_cfg_skill_FlyBullet(Perfect.Marshal.Octets os) {
                            return new cfg.skill.FlyBullet(os);
                         }
            public static cfg.common.ItemCost unmarshal_cfg_common_ItemCost(Perfect.Marshal.Octets os) {
                            return new cfg.common.ItemCost(os);
                         }
            public static cfg.bonus.Bonus unmarshal_cfg_bonus_Bonus(Perfect.Marshal.Octets os) {
                            var id = os.ReadInt();
                            cfg.bonus.Bonus x;
                            switch(id) {
                            case 0: return null;
                            case 18584 : x = new cfg.bonus.Empty(os); break;case 31944 : x = new cfg.bonus.RefBonus(os); break;case 46927 : x = new cfg.bonus.Currency(os); break;case 19353 : x = new cfg.bonus.Currencies(os); break;case 21920 : x = new cfg.bonus.ItemId(os); break;case 9164 : x = new cfg.bonus.Item(os); break;case 43728 : x = new cfg.bonus.Items(os); break;case 23945 : x = new cfg.bonus.RandomEquipFragmentQuality(os); break;case 11336 : x = new cfg.bonus.WeightBonus(os); break;case 6547 : x = new cfg.bonus.RandomBonus(os); break;case 17310 : x = new cfg.bonus.WeightRandomBonus(os); break;case 57487 : x = new cfg.bonus.MultiBonus(os); break;case 56236 : x = new cfg.bonus.RepeatBonus(os); break;case 37837 : x = new cfg.bonus.CopyBonus(os); break;default: throw new Perfect.Marshal.MarshalException("unknown bean id:" + id);}
                            return x;
                         }
            public static System.Collections.Generic.List<cfg.common.Currency> unmarshal_list_cfg_common_Currency(Perfect.Marshal.Octets os) {
                            var n = System.Math.Min(os.ReadSize(), os.Size + 1);
                            var x = new System.Collections.Generic.List<cfg.common.Currency>(n);
                            for(int i = 0 ; i < n ; i++) {
                            x.Add(cfg.Extensions.unmarshal_cfg_common_Currency(os));
                            }return x;
                         }
            public static System.Collections.Generic.List<cfg.skill.BuffConfig> unmarshal_list_cfg_skill_BuffConfig(Perfect.Marshal.Octets os) {
                            var n = System.Math.Min(os.ReadSize(), os.Size + 1);
                            var x = new System.Collections.Generic.List<cfg.skill.BuffConfig>(n);
                            for(int i = 0 ; i < n ; i++) {
                            x.Add(cfg.Extensions.unmarshal_cfg_skill_BuffConfig(os));
                            }return x;
                         }
            public static cfg.attribute.Attribute unmarshal_cfg_attribute_Attribute(Perfect.Marshal.Octets os) {
                            return new cfg.attribute.Attribute(os);
                         }
            public static cfg.asset.Texture unmarshal_cfg_asset_Texture(Perfect.Marshal.Octets os) {
                            return new cfg.asset.Texture(os);
                         }
            public static cfg.skill.DealEffectiveDamageTrigger unmarshal_cfg_skill_DealEffectiveDamageTrigger(Perfect.Marshal.Octets os) {
                            return new cfg.skill.DealEffectiveDamageTrigger(os);
                         }
            public static cfg.skill.BeCollidedTrigger unmarshal_cfg_skill_BeCollidedTrigger(Perfect.Marshal.Octets os) {
                            return new cfg.skill.BeCollidedTrigger(os);
                         }
            public static cfg.attribute.None unmarshal_cfg_attribute_None(Perfect.Marshal.Octets os) {
                            return new cfg.attribute.None(os);
                         }
            public static cfg.skill.BuffEffectContent unmarshal_cfg_skill_BuffEffectContent(Perfect.Marshal.Octets os) {
                            var id = os.ReadInt();
                            cfg.skill.BuffEffectContent x;
                            switch(id) {
                            case 0: return null;
                            case 32442 : x = new cfg.skill.AttributeChange(os); break;case 53274 : x = new cfg.skill.AddBuff(os); break;case 46714 : x = new cfg.skill.ReverseMoveDirection(os); break;case 42118 : x = new cfg.skill.ImmuneCurrentDamage(os); break;case 38901 : x = new cfg.skill.KnockbackTriggerTarget(os); break;case 60019 : x = new cfg.skill.ReleaseBulletAtCollisionPoint(os); break;case 60175 : x = new cfg.skill.AddBulletHitEffect(os); break;case 37682 : x = new cfg.skill.SkillReleaseIntervalSub(os); break;case 4920 : x = new cfg.skill.FixedDamage(os); break;case 8587 : x = new cfg.skill.AddBoostEnergy(os); break;case 7926 : x = new cfg.skill.EffectiveDamageToHeal(os); break;case 45475 : x = new cfg.skill.AddShield(os); break;default: throw new Perfect.Marshal.MarshalException("unknown bean id:" + id);}
                            return x;
                         }
            public static cfg.function.JumpInfo unmarshal_cfg_function_JumpInfo(Perfect.Marshal.Octets os) {
                            var id = os.ReadInt();
                            cfg.function.JumpInfo x;
                            switch(id) {
                            case 0: return null;
                            case 22130 : x = new cfg.function.JumpShopInfo(os); break;case 27082 : x = new cfg.function.JumpBattleInfo(os); break;case 39943 : x = new cfg.function.AdGetCurrencyInfo(os); break;default: throw new Perfect.Marshal.MarshalException("unknown bean id:" + id);}
                            return x;
                         }
            public static System.Collections.Generic.List<cfg.common.Cost> unmarshal_list_cfg_common_Cost(Perfect.Marshal.Octets os) {
                            var n = System.Math.Min(os.ReadSize(), os.Size + 1);
                            var x = new System.Collections.Generic.List<cfg.common.Cost>(n);
                            for(int i = 0 ; i < n ; i++) {
                            x.Add(cfg.Extensions.unmarshal_cfg_common_Cost(os));
                            }return x;
                         }
            public static cfg.skill.SingleTarget unmarshal_cfg_skill_SingleTarget(Perfect.Marshal.Octets os) {
                            return new cfg.skill.SingleTarget(os);
                         }
            public static cfg.common.Or unmarshal_cfg_common_Or(Perfect.Marshal.Octets os) {
                            return new cfg.common.Or(os);
                         }
            public static cfg.localize.TexScaleElement unmarshal_cfg_localize_TexScaleElement(Perfect.Marshal.Octets os) {
                            return new cfg.localize.TexScaleElement(os);
                         }
            public static cfg.bonus.BonusData unmarshal_cfg_bonus_BonusData(Perfect.Marshal.Octets os) {
                            return new cfg.bonus.BonusData(os);
                         }
            public static System.Collections.Generic.List<cfg.battle.BattleAsset> unmarshal_list_cfg_battle_BattleAsset(Perfect.Marshal.Octets os) {
                            var n = System.Math.Min(os.ReadSize(), os.Size + 1);
                            var x = new System.Collections.Generic.List<cfg.battle.BattleAsset>(n);
                            for(int i = 0 ; i < n ; i++) {
                            x.Add(cfg.Extensions.unmarshal_cfg_battle_BattleAsset(os));
                            }return x;
                         }
            public static cfg.bonus.WeightBonus unmarshal_cfg_bonus_WeightBonus(Perfect.Marshal.Octets os) {
                            return new cfg.bonus.WeightBonus(os);
                         }
            public static cfg.skill.ImmediateDestroy unmarshal_cfg_skill_ImmediateDestroy(Perfect.Marshal.Octets os) {
                            return new cfg.skill.ImmediateDestroy(os);
                         }
            public static cfg.bonus.RefBonus unmarshal_cfg_bonus_RefBonus(Perfect.Marshal.Octets os) {
                            return new cfg.bonus.RefBonus(os);
                         }
            public static cfg.attribute.AttributeNumber unmarshal_cfg_attribute_AttributeNumber(Perfect.Marshal.Octets os) {
                            return new cfg.attribute.AttributeNumber(os);
                         }
            public static cfg.skill.SkillReleaseType unmarshal_cfg_skill_SkillReleaseType(Perfect.Marshal.Octets os) {
                            var id = os.ReadInt();
                            cfg.skill.SkillReleaseType x;
                            switch(id) {
                            case 0: return null;
                            case 25001 : x = new cfg.skill.SkillReleaseBullet(os); break;case 7587 : x = new cfg.skill.SkillReleaseBuff(os); break;case 52118 : x = new cfg.skill.SkillReleaseEmpty(os); break;case 35087 : x = new cfg.skill.SkillReleaseSplitSatellite(os); break;default: throw new Perfect.Marshal.MarshalException("unknown bean id:" + id);}
                            return x;
                         }
            public static cfg.bonus.Currency unmarshal_cfg_bonus_Currency(Perfect.Marshal.Octets os) {
                            return new cfg.bonus.Currency(os);
                         }
            public static cfg.sound.SoundEffect unmarshal_cfg_sound_SoundEffect(Perfect.Marshal.Octets os) {
                            return new cfg.sound.SoundEffect(os);
                         }
            public static cfg.bonus.RepeatBonus unmarshal_cfg_bonus_RepeatBonus(Perfect.Marshal.Octets os) {
                            return new cfg.bonus.RepeatBonus(os);
                         }
            public static cfg.condition.Item unmarshal_cfg_condition_Item(Perfect.Marshal.Octets os) {
                            return new cfg.condition.Item(os);
                         }
            public static cfg.skill.BulletReleaseCount unmarshal_cfg_skill_BulletReleaseCount(Perfect.Marshal.Octets os) {
                            var id = os.ReadInt();
                            cfg.skill.BulletReleaseCount x;
                            switch(id) {
                            case 0: return null;
                            case 36854 : x = new cfg.skill.Single(os); break;case 65387 : x = new cfg.skill.Continuous(os); break;case 58314 : x = new cfg.skill.Circle(os); break;default: throw new Perfect.Marshal.MarshalException("unknown bean id:" + id);}
                            return x;
                         }
            public static cfg.skill.SkillReleaseBullet unmarshal_cfg_skill_SkillReleaseBullet(Perfect.Marshal.Octets os) {
                            return new cfg.skill.SkillReleaseBullet(os);
                         }
            public static cfg.skill.RangeTarget unmarshal_cfg_skill_RangeTarget(Perfect.Marshal.Octets os) {
                            return new cfg.skill.RangeTarget(os);
                         }
            public static cfg.skill.Circle unmarshal_cfg_skill_Circle(Perfect.Marshal.Octets os) {
                            return new cfg.skill.Circle(os);
                         }
            public static cfg.attribute.And unmarshal_cfg_attribute_And(Perfect.Marshal.Octets os) {
                            return new cfg.attribute.And(os);
                         }
            public static cfg.common.Cost unmarshal_cfg_common_Cost(Perfect.Marshal.Octets os) {
                            var id = os.ReadInt();
                            cfg.common.Cost x;
                            switch(id) {
                            case 0: return null;
                            case 12910 : x = new cfg.common.None(os); break;case 37017 : x = new cfg.common.AdCost(os); break;case 21220 : x = new cfg.common.CurrencyCost(os); break;case 46930 : x = new cfg.common.ItemCost(os); break;case 8726 : x = new cfg.common.And(os); break;case 23102 : x = new cfg.common.Or(os); break;case 50266 : x = new cfg.common.MoneyCost(os); break;default: throw new Perfect.Marshal.MarshalException("unknown bean id:" + id);}
                            return x;
                         }
            public static cfg.item.GiftInfo unmarshal_cfg_item_GiftInfo(Perfect.Marshal.Octets os) {
                            return new cfg.item.GiftInfo(os);
                         }
            public static cfg.bonus.ItemId unmarshal_cfg_bonus_ItemId(Perfect.Marshal.Octets os) {
                            return new cfg.bonus.ItemId(os);
                         }
            public static cfg.skill.BeforeCollisionDamageTrigger unmarshal_cfg_skill_BeforeCollisionDamageTrigger(Perfect.Marshal.Octets os) {
                            return new cfg.skill.BeforeCollisionDamageTrigger(os);
                         }
            public static cfg.asset.Sprite unmarshal_cfg_asset_Sprite(Perfect.Marshal.Octets os) {
                            return new cfg.asset.Sprite(os);
                         }
            public static cfg.skill.TargetInRangeRelease unmarshal_cfg_skill_TargetInRangeRelease(Perfect.Marshal.Octets os) {
                            return new cfg.skill.TargetInRangeRelease(os);
                         }
            public static cfg.skill.BulletHitEffectType unmarshal_cfg_skill_BulletHitEffectType(Perfect.Marshal.Octets os) {
                            var id = os.ReadInt();
                            cfg.skill.BulletHitEffectType x;
                            switch(id) {
                            case 0: return null;
                            case 33271 : x = new cfg.skill.BulletHitDamageEffect(os); break;case 7475 : x = new cfg.skill.BulletHitEffects(os); break;case 22420 : x = new cfg.skill.BulletHitBuffEffect(os); break;default: throw new Perfect.Marshal.MarshalException("unknown bean id:" + id);}
                            return x;
                         }
            public static cfg.battle.BattleAsset unmarshal_cfg_battle_BattleAsset(Perfect.Marshal.Octets os) {
                            return new cfg.battle.BattleAsset(os);
                         }
            public static cfg.skill.TargetFlyBullet unmarshal_cfg_skill_TargetFlyBullet(Perfect.Marshal.Octets os) {
                            return new cfg.skill.TargetFlyBullet(os);
                         }
            public static cfg.skill.BulletType unmarshal_cfg_skill_BulletType(Perfect.Marshal.Octets os) {
                            var id = os.ReadInt();
                            cfg.skill.BulletType x;
                            switch(id) {
                            case 0: return null;
                            case 41809 : x = new cfg.skill.FlyBullet(os); break;case 41756 : x = new cfg.skill.TargetFlyBullet(os); break;case 4599 : x = new cfg.skill.ImmediateBullet(os); break;default: throw new Perfect.Marshal.MarshalException("unknown bean id:" + id);}
                            return x;
                         }
            public static cfg.condition.Or unmarshal_cfg_condition_Or(Perfect.Marshal.Octets os) {
                            return new cfg.condition.Or(os);
                         }
            public static cfg.common.And unmarshal_cfg_common_And(Perfect.Marshal.Octets os) {
                            return new cfg.common.And(os);
                         }
            public static cfg.common.XNumber unmarshal_cfg_common_XNumber(Perfect.Marshal.Octets os) {
                            var id = os.ReadInt();
                            cfg.common.XNumber x;
                            switch(id) {
                            case 0: return null;
                            case 13651 : x = new cfg.common.XInt(os); break;case 5234 : x = new cfg.common.XLong(os); break;case 24592 : x = new cfg.common.XFloat(os); break;case 52174 : x = new cfg.common.XBigDecimal(os); break;default: throw new Perfect.Marshal.MarshalException("unknown bean id:" + id);}
                            return x;
                         }
            public static System.Collections.Generic.List<cfg.bonus.Item> unmarshal_list_cfg_bonus_Item(Perfect.Marshal.Octets os) {
                            var n = System.Math.Min(os.ReadSize(), os.Size + 1);
                            var x = new System.Collections.Generic.List<cfg.bonus.Item>(n);
                            for(int i = 0 ; i < n ; i++) {
                            x.Add(cfg.Extensions.unmarshal_cfg_bonus_Item(os));
                            }return x;
                         }
            public static System.Collections.Generic.List<cfg.error.Error> unmarshal_list_cfg_error_Error(Perfect.Marshal.Octets os) {
                            var n = System.Math.Min(os.ReadSize(), os.Size + 1);
                            var x = new System.Collections.Generic.List<cfg.error.Error>(n);
                            for(int i = 0 ; i < n ; i++) {
                            x.Add(cfg.Extensions.unmarshal_cfg_error_Error(os));
                            }return x;
                         }
            public static cfg.skill.SkillReleaseSplitSatellite unmarshal_cfg_skill_SkillReleaseSplitSatellite(Perfect.Marshal.Octets os) {
                            return new cfg.skill.SkillReleaseSplitSatellite(os);
                         }
            public static cfg.skill.SkillReleaseIntervalSub unmarshal_cfg_skill_SkillReleaseIntervalSub(Perfect.Marshal.Octets os) {
                            return new cfg.skill.SkillReleaseIntervalSub(os);
                         }
            public static cfg.condition.OwnItem unmarshal_cfg_condition_OwnItem(Perfect.Marshal.Octets os) {
                            return new cfg.condition.OwnItem(os);
                         }
            public static cfg.skill.DirectionTarget unmarshal_cfg_skill_DirectionTarget(Perfect.Marshal.Octets os) {
                            return new cfg.skill.DirectionTarget(os);
                         }
            public static cfg.condition.And unmarshal_cfg_condition_And(Perfect.Marshal.Octets os) {
                            return new cfg.condition.And(os);
                         }
            public static cfg.localize.Language unmarshal_cfg_localize_Language(Perfect.Marshal.Octets os) {
                            return new cfg.localize.Language(os);
                         }
            public static System.Collections.Generic.List<int> unmarshal_list_int(Perfect.Marshal.Octets os) {
                            var n = System.Math.Min(os.ReadSize(), os.Size + 1);
                            var x = new System.Collections.Generic.List<int>(n);
                            for(int i = 0 ; i < n ; i++) {
                            x.Add(os.ReadInt());
                            }return x;
                         }
            public static cfg.item.Quality unmarshal_cfg_item_Quality(Perfect.Marshal.Octets os) {
                            return new cfg.item.Quality(os);
                         }
            public static System.Collections.Generic.List<cfg.function.Jump> unmarshal_list_cfg_function_Jump(Perfect.Marshal.Octets os) {
                            var n = System.Math.Min(os.ReadSize(), os.Size + 1);
                            var x = new System.Collections.Generic.List<cfg.function.Jump>(n);
                            for(int i = 0 ; i < n ; i++) {
                            x.Add(cfg.Extensions.unmarshal_cfg_function_Jump(os));
                            }return x;
                         }
            public static cfg.item.ArenaBoxInfo unmarshal_cfg_item_ArenaBoxInfo(Perfect.Marshal.Octets os) {
                            return new cfg.item.ArenaBoxInfo(os);
                         }
            public static System.Collections.Generic.List<cfg.item.Item> unmarshal_list_cfg_item_Item(Perfect.Marshal.Octets os) {
                            var n = System.Math.Min(os.ReadSize(), os.Size + 1);
                            var x = new System.Collections.Generic.List<cfg.item.Item>(n);
                            for(int i = 0 ; i < n ; i++) {
                            x.Add(cfg.Extensions.unmarshal_cfg_item_Item(os));
                            }return x;
                         }


            public static void xml_marshal_cfg_skill_SkillReleaseRule(cfg.skill.SkillReleaseRule x, XmlWriter ele) {
                            if(x == null) { ele.WriteAttributeString("type", "null"); return; }x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_battle_EffectAssetInfo(cfg.battle.EffectAssetInfo x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_skill_HpRatioLessIntervalTrigger(cfg.skill.HpRatioLessIntervalTrigger x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_global_GInt64(cfg.global.GInt64 x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_list_cfg_skill_SkillReleaseConfig(System.Collections.Generic.List<cfg.skill.SkillReleaseConfig> x, XmlWriter ele) {
                            foreach(var _e in x) {
                            ele.WriteStartElement("item");
                            cfg.Extensions.xml_marshal_cfg_skill_SkillReleaseConfig(_e,ele);
                            ele.WriteEndElement();
                            }
                         }
            public static void xml_marshal_cfg_item_ItemInfo(cfg.item.ItemInfo x, XmlWriter ele) {
                            if(x == null) { ele.WriteAttributeString("type", "null"); return; }x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_skill_EffectiveDamageToHeal(cfg.skill.EffectiveDamageToHeal x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_function_Jump(cfg.function.Jump x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_item_None(cfg.item.None x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_localize_TexSizeElement(cfg.localize.TexSizeElement x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_error_Error(cfg.error.Error x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_skill_NoRelease(cfg.skill.NoRelease x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_skill_Single(cfg.skill.Single x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_list_cfg_localize_TexLocalizeElement(System.Collections.Generic.List<cfg.localize.TexLocalizeElement> x, XmlWriter ele) {
                            foreach(var _e in x) {
                            ele.WriteStartElement("item");
                            cfg.Extensions.xml_marshal_cfg_localize_TexLocalizeElement(_e,ele);
                            ele.WriteEndElement();
                            }
                         }
            public static void xml_marshal_cfg_skill_BulletHitDamageEffect(cfg.skill.BulletHitDamageEffect x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_skill_ImmediateBullet(cfg.skill.ImmediateBullet x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_localize_TexAssetElement(cfg.localize.TexAssetElement x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_skill_AddBulletHitEffect(cfg.skill.AddBulletHitEffect x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_skill_BuffConfig(cfg.skill.BuffConfig x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_global_Global(cfg.global.Global x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_skill_BulletConfig(cfg.skill.BulletConfig x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_skill_BuffDestroyType(cfg.skill.BuffDestroyType x, XmlWriter ele) {
                            if(x == null) { ele.WriteAttributeString("type", "null"); return; }x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_skill_AddBuff(cfg.skill.AddBuff x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_battle_None(cfg.battle.None x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_bonus_Items(cfg.bonus.Items x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_localize_TexLocalizeElements(cfg.localize.TexLocalizeElements x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_list_cfg_condition_Condition(System.Collections.Generic.List<cfg.condition.Condition> x, XmlWriter ele) {
                            foreach(var _e in x) {
                            ele.WriteStartElement("item");
                            cfg.Extensions.xml_marshal_cfg_condition_Condition(_e,ele);
                            ele.WriteEndElement();
                            }
                         }
            public static void xml_marshal_cfg_skill_KnockbackTriggerTarget(cfg.skill.KnockbackTriggerTarget x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_localize_TexLocalizeElement(cfg.localize.TexLocalizeElement x, XmlWriter ele) {
                            if(x == null) { ele.WriteAttributeString("type", "null"); return; }x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_skill_SkillReleaseConfig(cfg.skill.SkillReleaseConfig x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_skill_SkillShowOnceEffect(cfg.skill.SkillShowOnceEffect x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_skill_CollisionHitTargetTrigger(cfg.skill.CollisionHitTargetTrigger x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_skill_FixedDamage(cfg.skill.FixedDamage x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_list_cfg_bonus_WeightBonus(System.Collections.Generic.List<cfg.bonus.WeightBonus> x, XmlWriter ele) {
                            foreach(var _e in x) {
                            ele.WriteStartElement("item");
                            cfg.Extensions.xml_marshal_cfg_bonus_WeightBonus(_e,ele);
                            ele.WriteEndElement();
                            }
                         }
            public static void xml_marshal_cfg_condition_None(cfg.condition.None x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_sound_Audio(cfg.sound.Audio x, XmlWriter ele) {
                            if(x == null) { ele.WriteAttributeString("type", "null"); return; }x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_skill_BulletHitEffects(cfg.skill.BulletHitEffects x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_global_GString(cfg.global.GString x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_map_cfg_localize_ELanguageType_cfg_localize_TexLocalizeElement(System.Collections.Generic.Dictionary<cfg.localize.ELanguageType,cfg.localize.TexLocalizeElement> x, XmlWriter ele) {
                            foreach(var _e in x) {
                            ele.WriteStartElement("item");
                            ele.WriteStartElement("key");
                            ele.WriteEnum(_e.Key);
                            ele.WriteEndElement();
                            ele.WriteStartElement("value");
                            cfg.Extensions.xml_marshal_cfg_localize_TexLocalizeElement(_e.Value,ele);
                            ele.WriteEndElement();
                            ele.WriteEndElement();
                            }
                         }
            public static void xml_marshal_cfg_skill_MountPoint(cfg.skill.MountPoint x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_list_cfg_localize_Language(System.Collections.Generic.List<cfg.localize.Language> x, XmlWriter ele) {
                            foreach(var _e in x) {
                            ele.WriteStartElement("item");
                            cfg.Extensions.xml_marshal_cfg_localize_Language(_e,ele);
                            ele.WriteEndElement();
                            }
                         }
            public static void xml_marshal_cfg_skill_BuffAssetInfo(cfg.skill.BuffAssetInfo x, XmlWriter ele) {
                            if(x == null) { ele.WriteAttributeString("type", "null"); return; }x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_skill_OnceShow(cfg.skill.OnceShow x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_function_JumpBattleInfo(cfg.function.JumpBattleInfo x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_list_cfg_localize_TextureLanguage(System.Collections.Generic.List<cfg.localize.TextureLanguage> x, XmlWriter ele) {
                            foreach(var _e in x) {
                            ele.WriteStartElement("item");
                            cfg.Extensions.xml_marshal_cfg_localize_TextureLanguage(_e,ele);
                            ele.WriteEndElement();
                            }
                         }
            public static void xml_marshal_cfg_bonus_CopyBonus(cfg.bonus.CopyBonus x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_skill_SkillShowNoEffect(cfg.skill.SkillShowNoEffect x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_common_XBigDecimal(cfg.common.XBigDecimal x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_skill_AttributeChange(cfg.skill.AttributeChange x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_battle_BattleAssetInfo(cfg.battle.BattleAssetInfo x, XmlWriter ele) {
                            if(x == null) { ele.WriteAttributeString("type", "null"); return; }x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_skill_NoneAsset(cfg.skill.NoneAsset x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_list_cfg_sound_Sound(System.Collections.Generic.List<cfg.sound.Sound> x, XmlWriter ele) {
                            foreach(var _e in x) {
                            ele.WriteStartElement("item");
                            cfg.Extensions.xml_marshal_cfg_sound_Sound(_e,ele);
                            ele.WriteEndElement();
                            }
                         }
            public static void xml_marshal_cfg_skill_BuffTriggerType(cfg.skill.BuffTriggerType x, XmlWriter ele) {
                            if(x == null) { ele.WriteAttributeString("type", "null"); return; }x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_condition_Currency(cfg.condition.Currency x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_condition_Condition(cfg.condition.Condition x, XmlWriter ele) {
                            if(x == null) { ele.WriteAttributeString("type", "null"); return; }x.MarshalXml(ele);
                         }
            public static void xml_marshal_list_cfg_asset_Asset(System.Collections.Generic.List<cfg.asset.Asset> x, XmlWriter ele) {
                            foreach(var _e in x) {
                            ele.WriteStartElement("item");
                            cfg.Extensions.xml_marshal_cfg_asset_Asset(_e,ele);
                            ele.WriteEndElement();
                            }
                         }
            public static void xml_marshal_cfg_localize_TexPositionElement(cfg.localize.TexPositionElement x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_bonus_Item(cfg.bonus.Item x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_list_cfg_bonus_Bonus(System.Collections.Generic.List<cfg.bonus.Bonus> x, XmlWriter ele) {
                            foreach(var _e in x) {
                            ele.WriteStartElement("item");
                            cfg.Extensions.xml_marshal_cfg_bonus_Bonus(_e,ele);
                            ele.WriteEndElement();
                            }
                         }
            public static void xml_marshal_list_cfg_attribute_Attribute(System.Collections.Generic.List<cfg.attribute.Attribute> x, XmlWriter ele) {
                            foreach(var _e in x) {
                            ele.WriteStartElement("item");
                            cfg.Extensions.xml_marshal_cfg_attribute_Attribute(_e,ele);
                            ele.WriteEndElement();
                            }
                         }
            public static void xml_marshal_cfg_skill_Forever(cfg.skill.Forever x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_item_Item(cfg.item.Item x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_common_Currency(cfg.common.Currency x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_common_CurrencyCost(cfg.common.CurrencyCost x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_skill_StackReset(cfg.skill.StackReset x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_skill_AddShield(cfg.skill.AddShield x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_list_cfg_bonus_BonusData(System.Collections.Generic.List<cfg.bonus.BonusData> x, XmlWriter ele) {
                            foreach(var _e in x) {
                            ele.WriteStartElement("item");
                            cfg.Extensions.xml_marshal_cfg_bonus_BonusData(_e,ele);
                            ele.WriteEndElement();
                            }
                         }
            public static void xml_marshal_cfg_bonus_Empty(cfg.bonus.Empty x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_attribute_AttributeInfo(cfg.attribute.AttributeInfo x, XmlWriter ele) {
                            if(x == null) { ele.WriteAttributeString("type", "null"); return; }x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_sound_Sound(cfg.sound.Sound x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_Vector2Int(cfg.Vector2Int x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_list_cfg_bonus_Currency(System.Collections.Generic.List<cfg.bonus.Currency> x, XmlWriter ele) {
                            foreach(var _e in x) {
                            ele.WriteStartElement("item");
                            cfg.Extensions.xml_marshal_cfg_bonus_Currency(_e,ele);
                            ele.WriteEndElement();
                            }
                         }
            public static void xml_marshal_cfg_attribute_AttributeLongNumber(cfg.attribute.AttributeLongNumber x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_common_XFloat(cfg.common.XFloat x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_common_XInt(cfg.common.XInt x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_skill_SkillReleaseEmpty(cfg.skill.SkillReleaseEmpty x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_skill_BulletHitBuffEffect(cfg.skill.BulletHitBuffEffect x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_asset_Asset(cfg.asset.Asset x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_asset_AssetInfo(cfg.asset.AssetInfo x, XmlWriter ele) {
                            if(x == null) { ele.WriteAttributeString("type", "null"); return; }x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_skill_StackNone(cfg.skill.StackNone x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_function_AdGetCurrencyInfo(cfg.function.AdGetCurrencyInfo x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_skill_StackNum(cfg.skill.StackNum x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_list_cfg_attribute_AttributeInfo(System.Collections.Generic.List<cfg.attribute.AttributeInfo> x, XmlWriter ele) {
                            foreach(var _e in x) {
                            ele.WriteStartElement("item");
                            cfg.Extensions.xml_marshal_cfg_attribute_AttributeInfo(_e,ele);
                            ele.WriteEndElement();
                            }
                         }
            public static void xml_marshal_cfg_condition_BuyMonthlyCard(cfg.condition.BuyMonthlyCard x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_list_cfg_skill_BulletConfig(System.Collections.Generic.List<cfg.skill.BulletConfig> x, XmlWriter ele) {
                            foreach(var _e in x) {
                            ele.WriteStartElement("item");
                            cfg.Extensions.xml_marshal_cfg_skill_BulletConfig(_e,ele);
                            ele.WriteEndElement();
                            }
                         }
            public static void xml_marshal_cfg_skill_IntervalTrigger(cfg.skill.IntervalTrigger x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_list_cfg_item_Quality(System.Collections.Generic.List<cfg.item.Quality> x, XmlWriter ele) {
                            foreach(var _e in x) {
                            ele.WriteStartElement("item");
                            cfg.Extensions.xml_marshal_cfg_item_Quality(_e,ele);
                            ele.WriteEndElement();
                            }
                         }
            public static void xml_marshal_cfg_sound_Music(cfg.sound.Music x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_global_GBool(cfg.global.GBool x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_skill_ImmuneCurrentDamage(cfg.skill.ImmuneCurrentDamage x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_skill_DurationDestroy(cfg.skill.DurationDestroy x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_bonus_RandomEquipFragmentQuality(cfg.bonus.RandomEquipFragmentQuality x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_skill_AddBoostEnergy(cfg.skill.AddBoostEnergy x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_bonus_MultiBonus(cfg.bonus.MultiBonus x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_function_JumpShopInfo(cfg.function.JumpShopInfo x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_global_GFloat(cfg.global.GFloat x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_asset_Spine(cfg.asset.Spine x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_bonus_RandomBonus(cfg.bonus.RandomBonus x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_skill_ImmediateTrigger(cfg.skill.ImmediateTrigger x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_localize_TextureLanguage(cfg.localize.TextureLanguage x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_common_XLong(cfg.common.XLong x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_skill_ReverseMoveDirection(cfg.skill.ReverseMoveDirection x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_skill_BulletTargetType(cfg.skill.BulletTargetType x, XmlWriter ele) {
                            if(x == null) { ele.WriteAttributeString("type", "null"); return; }x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_skill_SkillShowEffect(cfg.skill.SkillShowEffect x, XmlWriter ele) {
                            if(x == null) { ele.WriteAttributeString("type", "null"); return; }x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_skill_ImmediateRelease(cfg.skill.ImmediateRelease x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_skill_SkillReleaseBuff(cfg.skill.SkillReleaseBuff x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_bonus_WeightRandomBonus(cfg.bonus.WeightRandomBonus x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_global_GInt32(cfg.global.GInt32 x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_Vector2(cfg.Vector2 x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_bonus_Currencies(cfg.bonus.Currencies x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_common_AdCost(cfg.common.AdCost x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_skill_Continuous(cfg.skill.Continuous x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_Vector3(cfg.Vector3 x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_skill_BuffStatcking(cfg.skill.BuffStatcking x, XmlWriter ele) {
                            if(x == null) { ele.WriteAttributeString("type", "null"); return; }x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_global_GlobalInfo(cfg.global.GlobalInfo x, XmlWriter ele) {
                            if(x == null) { ele.WriteAttributeString("type", "null"); return; }x.MarshalXml(ele);
                         }
            public static void xml_marshal_list_cfg_global_Global(System.Collections.Generic.List<cfg.global.Global> x, XmlWriter ele) {
                            foreach(var _e in x) {
                            ele.WriteStartElement("item");
                            cfg.Extensions.xml_marshal_cfg_global_Global(_e,ele);
                            ele.WriteEndElement();
                            }
                         }
            public static void xml_marshal_list_cfg_skill_BulletHitEffectType(System.Collections.Generic.List<cfg.skill.BulletHitEffectType> x, XmlWriter ele) {
                            foreach(var _e in x) {
                            ele.WriteStartElement("item");
                            cfg.Extensions.xml_marshal_cfg_skill_BulletHitEffectType(_e,ele);
                            ele.WriteEndElement();
                            }
                         }
            public static void xml_marshal_cfg_common_None(cfg.common.None x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_common_MoneyCost(cfg.common.MoneyCost x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_skill_ReleaseBulletAtCollisionPoint(cfg.skill.ReleaseBulletAtCollisionPoint x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_skill_FlyBullet(cfg.skill.FlyBullet x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_common_ItemCost(cfg.common.ItemCost x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_bonus_Bonus(cfg.bonus.Bonus x, XmlWriter ele) {
                            if(x == null) { ele.WriteAttributeString("type", "null"); return; }x.MarshalXml(ele);
                         }
            public static void xml_marshal_list_cfg_common_Currency(System.Collections.Generic.List<cfg.common.Currency> x, XmlWriter ele) {
                            foreach(var _e in x) {
                            ele.WriteStartElement("item");
                            cfg.Extensions.xml_marshal_cfg_common_Currency(_e,ele);
                            ele.WriteEndElement();
                            }
                         }
            public static void xml_marshal_list_cfg_skill_BuffConfig(System.Collections.Generic.List<cfg.skill.BuffConfig> x, XmlWriter ele) {
                            foreach(var _e in x) {
                            ele.WriteStartElement("item");
                            cfg.Extensions.xml_marshal_cfg_skill_BuffConfig(_e,ele);
                            ele.WriteEndElement();
                            }
                         }
            public static void xml_marshal_cfg_attribute_Attribute(cfg.attribute.Attribute x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_asset_Texture(cfg.asset.Texture x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_skill_DealEffectiveDamageTrigger(cfg.skill.DealEffectiveDamageTrigger x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_skill_BeCollidedTrigger(cfg.skill.BeCollidedTrigger x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_attribute_None(cfg.attribute.None x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_skill_BuffEffectContent(cfg.skill.BuffEffectContent x, XmlWriter ele) {
                            if(x == null) { ele.WriteAttributeString("type", "null"); return; }x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_function_JumpInfo(cfg.function.JumpInfo x, XmlWriter ele) {
                            if(x == null) { ele.WriteAttributeString("type", "null"); return; }x.MarshalXml(ele);
                         }
            public static void xml_marshal_list_cfg_common_Cost(System.Collections.Generic.List<cfg.common.Cost> x, XmlWriter ele) {
                            foreach(var _e in x) {
                            ele.WriteStartElement("item");
                            cfg.Extensions.xml_marshal_cfg_common_Cost(_e,ele);
                            ele.WriteEndElement();
                            }
                         }
            public static void xml_marshal_cfg_skill_SingleTarget(cfg.skill.SingleTarget x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_common_Or(cfg.common.Or x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_localize_TexScaleElement(cfg.localize.TexScaleElement x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_bonus_BonusData(cfg.bonus.BonusData x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_list_cfg_battle_BattleAsset(System.Collections.Generic.List<cfg.battle.BattleAsset> x, XmlWriter ele) {
                            foreach(var _e in x) {
                            ele.WriteStartElement("item");
                            cfg.Extensions.xml_marshal_cfg_battle_BattleAsset(_e,ele);
                            ele.WriteEndElement();
                            }
                         }
            public static void xml_marshal_cfg_bonus_WeightBonus(cfg.bonus.WeightBonus x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_skill_ImmediateDestroy(cfg.skill.ImmediateDestroy x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_bonus_RefBonus(cfg.bonus.RefBonus x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_attribute_AttributeNumber(cfg.attribute.AttributeNumber x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_skill_SkillReleaseType(cfg.skill.SkillReleaseType x, XmlWriter ele) {
                            if(x == null) { ele.WriteAttributeString("type", "null"); return; }x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_bonus_Currency(cfg.bonus.Currency x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_sound_SoundEffect(cfg.sound.SoundEffect x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_bonus_RepeatBonus(cfg.bonus.RepeatBonus x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_condition_Item(cfg.condition.Item x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_skill_BulletReleaseCount(cfg.skill.BulletReleaseCount x, XmlWriter ele) {
                            if(x == null) { ele.WriteAttributeString("type", "null"); return; }x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_skill_SkillReleaseBullet(cfg.skill.SkillReleaseBullet x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_skill_RangeTarget(cfg.skill.RangeTarget x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_skill_Circle(cfg.skill.Circle x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_attribute_And(cfg.attribute.And x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_common_Cost(cfg.common.Cost x, XmlWriter ele) {
                            if(x == null) { ele.WriteAttributeString("type", "null"); return; }x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_item_GiftInfo(cfg.item.GiftInfo x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_bonus_ItemId(cfg.bonus.ItemId x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_skill_BeforeCollisionDamageTrigger(cfg.skill.BeforeCollisionDamageTrigger x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_asset_Sprite(cfg.asset.Sprite x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_skill_TargetInRangeRelease(cfg.skill.TargetInRangeRelease x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_skill_BulletHitEffectType(cfg.skill.BulletHitEffectType x, XmlWriter ele) {
                            if(x == null) { ele.WriteAttributeString("type", "null"); return; }x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_battle_BattleAsset(cfg.battle.BattleAsset x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_skill_TargetFlyBullet(cfg.skill.TargetFlyBullet x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_skill_BulletType(cfg.skill.BulletType x, XmlWriter ele) {
                            if(x == null) { ele.WriteAttributeString("type", "null"); return; }x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_condition_Or(cfg.condition.Or x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_common_And(cfg.common.And x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_common_XNumber(cfg.common.XNumber x, XmlWriter ele) {
                            if(x == null) { ele.WriteAttributeString("type", "null"); return; }x.MarshalXml(ele);
                         }
            public static void xml_marshal_list_cfg_bonus_Item(System.Collections.Generic.List<cfg.bonus.Item> x, XmlWriter ele) {
                            foreach(var _e in x) {
                            ele.WriteStartElement("item");
                            cfg.Extensions.xml_marshal_cfg_bonus_Item(_e,ele);
                            ele.WriteEndElement();
                            }
                         }
            public static void xml_marshal_list_cfg_error_Error(System.Collections.Generic.List<cfg.error.Error> x, XmlWriter ele) {
                            foreach(var _e in x) {
                            ele.WriteStartElement("item");
                            cfg.Extensions.xml_marshal_cfg_error_Error(_e,ele);
                            ele.WriteEndElement();
                            }
                         }
            public static void xml_marshal_cfg_skill_SkillReleaseSplitSatellite(cfg.skill.SkillReleaseSplitSatellite x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_skill_SkillReleaseIntervalSub(cfg.skill.SkillReleaseIntervalSub x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_condition_OwnItem(cfg.condition.OwnItem x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_skill_DirectionTarget(cfg.skill.DirectionTarget x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_condition_And(cfg.condition.And x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_cfg_localize_Language(cfg.localize.Language x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_list_int(System.Collections.Generic.List<int> x, XmlWriter ele) {
                            foreach(var _e in x) {
                            ele.WriteStartElement("item");
                            ele.WriteInt(_e);
                            ele.WriteEndElement();
                            }
                         }
            public static void xml_marshal_cfg_item_Quality(cfg.item.Quality x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_list_cfg_function_Jump(System.Collections.Generic.List<cfg.function.Jump> x, XmlWriter ele) {
                            foreach(var _e in x) {
                            ele.WriteStartElement("item");
                            cfg.Extensions.xml_marshal_cfg_function_Jump(_e,ele);
                            ele.WriteEndElement();
                            }
                         }
            public static void xml_marshal_cfg_item_ArenaBoxInfo(cfg.item.ArenaBoxInfo x, XmlWriter ele) {
                            x.MarshalXml(ele);
                         }
            public static void xml_marshal_list_cfg_item_Item(System.Collections.Generic.List<cfg.item.Item> x, XmlWriter ele) {
                            foreach(var _e in x) {
                            ele.WriteStartElement("item");
                            cfg.Extensions.xml_marshal_cfg_item_Item(_e,ele);
                            ele.WriteEndElement();
                            }
                         }

            public static cfg.skill.SkillReleaseRule xml_unmarshal_cfg_skill_SkillReleaseRule(XmlElement ele) {
                            var type = ele.GetAttribute("type");
                            cfg.skill.SkillReleaseRule x;
                            switch(type) {
                            case "null": return null;
                            case "NoRelease" : x = new cfg.skill.NoRelease(ele); break;
                            case "ImmediateRelease" : x = new cfg.skill.ImmediateRelease(ele); break;
                            case "TargetInRangeRelease" : x = new cfg.skill.TargetInRangeRelease(ele); break;
                            default: throw new Perfect.Marshal.MarshalException("unknown bean type:" + type);}
                            return x;
                         }
            public static cfg.battle.EffectAssetInfo xml_unmarshal_cfg_battle_EffectAssetInfo(XmlElement ele) {
                            var x = new cfg.battle.EffectAssetInfo(ele);
                            return x;
                         }
            public static cfg.skill.HpRatioLessIntervalTrigger xml_unmarshal_cfg_skill_HpRatioLessIntervalTrigger(XmlElement ele) {
                            var x = new cfg.skill.HpRatioLessIntervalTrigger(ele);
                            return x;
                         }
            public static cfg.global.GInt64 xml_unmarshal_cfg_global_GInt64(XmlElement ele) {
                            var x = new cfg.global.GInt64(ele);
                            return x;
                         }
            public static System.Collections.Generic.List<cfg.skill.SkillReleaseConfig> xml_unmarshal_list_cfg_skill_SkillReleaseConfig(XmlElement ele) {
                            var x = new System.Collections.Generic.List<cfg.skill.SkillReleaseConfig>();
                            foreach(var c in ele.GetChildren()) {
                            x.Add(cfg.Extensions.xml_unmarshal_cfg_skill_SkillReleaseConfig(c));
                            }return x;
                         }
            public static cfg.item.ItemInfo xml_unmarshal_cfg_item_ItemInfo(XmlElement ele) {
                            var type = ele.GetAttribute("type");
                            cfg.item.ItemInfo x;
                            switch(type) {
                            case "null": return null;
                            case "None" : x = new cfg.item.None(ele); break;
                            case "GiftInfo" : x = new cfg.item.GiftInfo(ele); break;
                            case "ArenaBoxInfo" : x = new cfg.item.ArenaBoxInfo(ele); break;
                            default: throw new Perfect.Marshal.MarshalException("unknown bean type:" + type);}
                            return x;
                         }
            public static cfg.skill.EffectiveDamageToHeal xml_unmarshal_cfg_skill_EffectiveDamageToHeal(XmlElement ele) {
                            var x = new cfg.skill.EffectiveDamageToHeal(ele);
                            return x;
                         }
            public static cfg.function.Jump xml_unmarshal_cfg_function_Jump(XmlElement ele) {
                            var x = new cfg.function.Jump(ele);
                            return x;
                         }
            public static cfg.item.None xml_unmarshal_cfg_item_None(XmlElement ele) {
                            var x = new cfg.item.None(ele);
                            return x;
                         }
            public static cfg.localize.TexSizeElement xml_unmarshal_cfg_localize_TexSizeElement(XmlElement ele) {
                            var x = new cfg.localize.TexSizeElement(ele);
                            return x;
                         }
            public static cfg.error.Error xml_unmarshal_cfg_error_Error(XmlElement ele) {
                            var x = new cfg.error.Error(ele);
                            return x;
                         }
            public static cfg.skill.NoRelease xml_unmarshal_cfg_skill_NoRelease(XmlElement ele) {
                            var x = new cfg.skill.NoRelease(ele);
                            return x;
                         }
            public static cfg.skill.Single xml_unmarshal_cfg_skill_Single(XmlElement ele) {
                            var x = new cfg.skill.Single(ele);
                            return x;
                         }
            public static System.Collections.Generic.List<cfg.localize.TexLocalizeElement> xml_unmarshal_list_cfg_localize_TexLocalizeElement(XmlElement ele) {
                            var x = new System.Collections.Generic.List<cfg.localize.TexLocalizeElement>();
                            foreach(var c in ele.GetChildren()) {
                            x.Add(cfg.Extensions.xml_unmarshal_cfg_localize_TexLocalizeElement(c));
                            }return x;
                         }
            public static cfg.skill.BulletHitDamageEffect xml_unmarshal_cfg_skill_BulletHitDamageEffect(XmlElement ele) {
                            var x = new cfg.skill.BulletHitDamageEffect(ele);
                            return x;
                         }
            public static cfg.skill.ImmediateBullet xml_unmarshal_cfg_skill_ImmediateBullet(XmlElement ele) {
                            var x = new cfg.skill.ImmediateBullet(ele);
                            return x;
                         }
            public static cfg.localize.TexAssetElement xml_unmarshal_cfg_localize_TexAssetElement(XmlElement ele) {
                            var x = new cfg.localize.TexAssetElement(ele);
                            return x;
                         }
            public static cfg.skill.AddBulletHitEffect xml_unmarshal_cfg_skill_AddBulletHitEffect(XmlElement ele) {
                            var x = new cfg.skill.AddBulletHitEffect(ele);
                            return x;
                         }
            public static cfg.skill.BuffConfig xml_unmarshal_cfg_skill_BuffConfig(XmlElement ele) {
                            var x = new cfg.skill.BuffConfig(ele);
                            return x;
                         }
            public static cfg.global.Global xml_unmarshal_cfg_global_Global(XmlElement ele) {
                            var x = new cfg.global.Global(ele);
                            return x;
                         }
            public static cfg.skill.BulletConfig xml_unmarshal_cfg_skill_BulletConfig(XmlElement ele) {
                            var x = new cfg.skill.BulletConfig(ele);
                            return x;
                         }
            public static cfg.skill.BuffDestroyType xml_unmarshal_cfg_skill_BuffDestroyType(XmlElement ele) {
                            var type = ele.GetAttribute("type");
                            cfg.skill.BuffDestroyType x;
                            switch(type) {
                            case "null": return null;
                            case "ImmediateDestroy" : x = new cfg.skill.ImmediateDestroy(ele); break;
                            case "Forever" : x = new cfg.skill.Forever(ele); break;
                            case "DurationDestroy" : x = new cfg.skill.DurationDestroy(ele); break;
                            default: throw new Perfect.Marshal.MarshalException("unknown bean type:" + type);}
                            return x;
                         }
            public static cfg.skill.AddBuff xml_unmarshal_cfg_skill_AddBuff(XmlElement ele) {
                            var x = new cfg.skill.AddBuff(ele);
                            return x;
                         }
            public static cfg.battle.None xml_unmarshal_cfg_battle_None(XmlElement ele) {
                            var x = new cfg.battle.None(ele);
                            return x;
                         }
            public static cfg.bonus.Items xml_unmarshal_cfg_bonus_Items(XmlElement ele) {
                            var x = new cfg.bonus.Items(ele);
                            return x;
                         }
            public static cfg.localize.TexLocalizeElements xml_unmarshal_cfg_localize_TexLocalizeElements(XmlElement ele) {
                            var x = new cfg.localize.TexLocalizeElements(ele);
                            return x;
                         }
            public static System.Collections.Generic.List<cfg.condition.Condition> xml_unmarshal_list_cfg_condition_Condition(XmlElement ele) {
                            var x = new System.Collections.Generic.List<cfg.condition.Condition>();
                            foreach(var c in ele.GetChildren()) {
                            x.Add(cfg.Extensions.xml_unmarshal_cfg_condition_Condition(c));
                            }return x;
                         }
            public static cfg.skill.KnockbackTriggerTarget xml_unmarshal_cfg_skill_KnockbackTriggerTarget(XmlElement ele) {
                            var x = new cfg.skill.KnockbackTriggerTarget(ele);
                            return x;
                         }
            public static cfg.localize.TexLocalizeElement xml_unmarshal_cfg_localize_TexLocalizeElement(XmlElement ele) {
                            var type = ele.GetAttribute("type");
                            cfg.localize.TexLocalizeElement x;
                            switch(type) {
                            case "null": return null;
                            case "TexAssetElement" : x = new cfg.localize.TexAssetElement(ele); break;
                            case "TexLocalizeElements" : x = new cfg.localize.TexLocalizeElements(ele); break;
                            case "TexPositionElement" : x = new cfg.localize.TexPositionElement(ele); break;
                            case "TexScaleElement" : x = new cfg.localize.TexScaleElement(ele); break;
                            case "TexSizeElement" : x = new cfg.localize.TexSizeElement(ele); break;
                            default: throw new Perfect.Marshal.MarshalException("unknown bean type:" + type);}
                            return x;
                         }
            public static cfg.skill.SkillReleaseConfig xml_unmarshal_cfg_skill_SkillReleaseConfig(XmlElement ele) {
                            var x = new cfg.skill.SkillReleaseConfig(ele);
                            return x;
                         }
            public static cfg.skill.SkillShowOnceEffect xml_unmarshal_cfg_skill_SkillShowOnceEffect(XmlElement ele) {
                            var x = new cfg.skill.SkillShowOnceEffect(ele);
                            return x;
                         }
            public static cfg.skill.CollisionHitTargetTrigger xml_unmarshal_cfg_skill_CollisionHitTargetTrigger(XmlElement ele) {
                            var x = new cfg.skill.CollisionHitTargetTrigger(ele);
                            return x;
                         }
            public static cfg.skill.FixedDamage xml_unmarshal_cfg_skill_FixedDamage(XmlElement ele) {
                            var x = new cfg.skill.FixedDamage(ele);
                            return x;
                         }
            public static System.Collections.Generic.List<cfg.bonus.WeightBonus> xml_unmarshal_list_cfg_bonus_WeightBonus(XmlElement ele) {
                            var x = new System.Collections.Generic.List<cfg.bonus.WeightBonus>();
                            foreach(var c in ele.GetChildren()) {
                            x.Add(cfg.Extensions.xml_unmarshal_cfg_bonus_WeightBonus(c));
                            }return x;
                         }
            public static cfg.condition.None xml_unmarshal_cfg_condition_None(XmlElement ele) {
                            var x = new cfg.condition.None(ele);
                            return x;
                         }
            public static cfg.sound.Audio xml_unmarshal_cfg_sound_Audio(XmlElement ele) {
                            var type = ele.GetAttribute("type");
                            cfg.sound.Audio x;
                            switch(type) {
                            case "null": return null;
                            case "SoundEffect" : x = new cfg.sound.SoundEffect(ele); break;
                            case "Music" : x = new cfg.sound.Music(ele); break;
                            default: throw new Perfect.Marshal.MarshalException("unknown bean type:" + type);}
                            return x;
                         }
            public static cfg.skill.BulletHitEffects xml_unmarshal_cfg_skill_BulletHitEffects(XmlElement ele) {
                            var x = new cfg.skill.BulletHitEffects(ele);
                            return x;
                         }
            public static cfg.global.GString xml_unmarshal_cfg_global_GString(XmlElement ele) {
                            var x = new cfg.global.GString(ele);
                            return x;
                         }
            public static System.Collections.Generic.Dictionary<cfg.localize.ELanguageType,cfg.localize.TexLocalizeElement> xml_unmarshal_map_cfg_localize_ELanguageType_cfg_localize_TexLocalizeElement(XmlElement ele) {
                            var x = new System.Collections.Generic.Dictionary<cfg.localize.ELanguageType,cfg.localize.TexLocalizeElement>();
                            foreach(var c in ele.GetChildren()) {
                            var key = default(cfg.localize.ELanguageType);var value = default(cfg.localize.TexLocalizeElement);foreach(var c2 in c.GetChildren())
                            {
                            switch(c2.Name)
                            {
                            case "key": key = c2.ReadEnum<cfg.localize.ELanguageType>(); break;
                            case "value": value = cfg.Extensions.xml_unmarshal_cfg_localize_TexLocalizeElement(c2); break;
                            }
                            }
                            x.Add(key, value);
                            }return x;
                         }
            public static cfg.skill.MountPoint xml_unmarshal_cfg_skill_MountPoint(XmlElement ele) {
                            var x = new cfg.skill.MountPoint(ele);
                            return x;
                         }
            public static System.Collections.Generic.List<cfg.localize.Language> xml_unmarshal_list_cfg_localize_Language(XmlElement ele) {
                            var x = new System.Collections.Generic.List<cfg.localize.Language>();
                            foreach(var c in ele.GetChildren()) {
                            x.Add(cfg.Extensions.xml_unmarshal_cfg_localize_Language(c));
                            }return x;
                         }
            public static cfg.skill.BuffAssetInfo xml_unmarshal_cfg_skill_BuffAssetInfo(XmlElement ele) {
                            var type = ele.GetAttribute("type");
                            cfg.skill.BuffAssetInfo x;
                            switch(type) {
                            case "null": return null;
                            case "NoneAsset" : x = new cfg.skill.NoneAsset(ele); break;
                            case "MountPoint" : x = new cfg.skill.MountPoint(ele); break;
                            case "OnceShow" : x = new cfg.skill.OnceShow(ele); break;
                            default: throw new Perfect.Marshal.MarshalException("unknown bean type:" + type);}
                            return x;
                         }
            public static cfg.skill.OnceShow xml_unmarshal_cfg_skill_OnceShow(XmlElement ele) {
                            var x = new cfg.skill.OnceShow(ele);
                            return x;
                         }
            public static cfg.function.JumpBattleInfo xml_unmarshal_cfg_function_JumpBattleInfo(XmlElement ele) {
                            var x = new cfg.function.JumpBattleInfo(ele);
                            return x;
                         }
            public static System.Collections.Generic.List<cfg.localize.TextureLanguage> xml_unmarshal_list_cfg_localize_TextureLanguage(XmlElement ele) {
                            var x = new System.Collections.Generic.List<cfg.localize.TextureLanguage>();
                            foreach(var c in ele.GetChildren()) {
                            x.Add(cfg.Extensions.xml_unmarshal_cfg_localize_TextureLanguage(c));
                            }return x;
                         }
            public static cfg.bonus.CopyBonus xml_unmarshal_cfg_bonus_CopyBonus(XmlElement ele) {
                            var x = new cfg.bonus.CopyBonus(ele);
                            return x;
                         }
            public static cfg.skill.SkillShowNoEffect xml_unmarshal_cfg_skill_SkillShowNoEffect(XmlElement ele) {
                            var x = new cfg.skill.SkillShowNoEffect(ele);
                            return x;
                         }
            public static cfg.common.XBigDecimal xml_unmarshal_cfg_common_XBigDecimal(XmlElement ele) {
                            var x = new cfg.common.XBigDecimal(ele);
                            return x;
                         }
            public static cfg.skill.AttributeChange xml_unmarshal_cfg_skill_AttributeChange(XmlElement ele) {
                            var x = new cfg.skill.AttributeChange(ele);
                            return x;
                         }
            public static cfg.battle.BattleAssetInfo xml_unmarshal_cfg_battle_BattleAssetInfo(XmlElement ele) {
                            var type = ele.GetAttribute("type");
                            cfg.battle.BattleAssetInfo x;
                            switch(type) {
                            case "null": return null;
                            case "None" : x = new cfg.battle.None(ele); break;
                            case "EffectAssetInfo" : x = new cfg.battle.EffectAssetInfo(ele); break;
                            default: throw new Perfect.Marshal.MarshalException("unknown bean type:" + type);}
                            return x;
                         }
            public static cfg.skill.NoneAsset xml_unmarshal_cfg_skill_NoneAsset(XmlElement ele) {
                            var x = new cfg.skill.NoneAsset(ele);
                            return x;
                         }
            public static System.Collections.Generic.List<cfg.sound.Sound> xml_unmarshal_list_cfg_sound_Sound(XmlElement ele) {
                            var x = new System.Collections.Generic.List<cfg.sound.Sound>();
                            foreach(var c in ele.GetChildren()) {
                            x.Add(cfg.Extensions.xml_unmarshal_cfg_sound_Sound(c));
                            }return x;
                         }
            public static cfg.skill.BuffTriggerType xml_unmarshal_cfg_skill_BuffTriggerType(XmlElement ele) {
                            var type = ele.GetAttribute("type");
                            cfg.skill.BuffTriggerType x;
                            switch(type) {
                            case "null": return null;
                            case "ImmediateTrigger" : x = new cfg.skill.ImmediateTrigger(ele); break;
                            case "IntervalTrigger" : x = new cfg.skill.IntervalTrigger(ele); break;
                            case "CollisionHitTargetTrigger" : x = new cfg.skill.CollisionHitTargetTrigger(ele); break;
                            case "BeforeCollisionDamageTrigger" : x = new cfg.skill.BeforeCollisionDamageTrigger(ele); break;
                            case "BeCollidedTrigger" : x = new cfg.skill.BeCollidedTrigger(ele); break;
                            case "DealEffectiveDamageTrigger" : x = new cfg.skill.DealEffectiveDamageTrigger(ele); break;
                            case "HpRatioLessIntervalTrigger" : x = new cfg.skill.HpRatioLessIntervalTrigger(ele); break;
                            default: throw new Perfect.Marshal.MarshalException("unknown bean type:" + type);}
                            return x;
                         }
            public static cfg.condition.Currency xml_unmarshal_cfg_condition_Currency(XmlElement ele) {
                            var x = new cfg.condition.Currency(ele);
                            return x;
                         }
            public static cfg.condition.Condition xml_unmarshal_cfg_condition_Condition(XmlElement ele) {
                            var type = ele.GetAttribute("type");
                            cfg.condition.Condition x;
                            switch(type) {
                            case "null": return null;
                            case "None" : x = new cfg.condition.None(ele); break;
                            case "Currency" : x = new cfg.condition.Currency(ele); break;
                            case "Item" : x = new cfg.condition.Item(ele); break;
                            case "BuyMonthlyCard" : x = new cfg.condition.BuyMonthlyCard(ele); break;
                            case "OwnItem" : x = new cfg.condition.OwnItem(ele); break;
                            case "And" : x = new cfg.condition.And(ele); break;
                            case "Or" : x = new cfg.condition.Or(ele); break;
                            default: throw new Perfect.Marshal.MarshalException("unknown bean type:" + type);}
                            return x;
                         }
            public static System.Collections.Generic.List<cfg.asset.Asset> xml_unmarshal_list_cfg_asset_Asset(XmlElement ele) {
                            var x = new System.Collections.Generic.List<cfg.asset.Asset>();
                            foreach(var c in ele.GetChildren()) {
                            x.Add(cfg.Extensions.xml_unmarshal_cfg_asset_Asset(c));
                            }return x;
                         }
            public static cfg.localize.TexPositionElement xml_unmarshal_cfg_localize_TexPositionElement(XmlElement ele) {
                            var x = new cfg.localize.TexPositionElement(ele);
                            return x;
                         }
            public static cfg.bonus.Item xml_unmarshal_cfg_bonus_Item(XmlElement ele) {
                            var x = new cfg.bonus.Item(ele);
                            return x;
                         }
            public static System.Collections.Generic.List<cfg.bonus.Bonus> xml_unmarshal_list_cfg_bonus_Bonus(XmlElement ele) {
                            var x = new System.Collections.Generic.List<cfg.bonus.Bonus>();
                            foreach(var c in ele.GetChildren()) {
                            x.Add(cfg.Extensions.xml_unmarshal_cfg_bonus_Bonus(c));
                            }return x;
                         }
            public static System.Collections.Generic.List<cfg.attribute.Attribute> xml_unmarshal_list_cfg_attribute_Attribute(XmlElement ele) {
                            var x = new System.Collections.Generic.List<cfg.attribute.Attribute>();
                            foreach(var c in ele.GetChildren()) {
                            x.Add(cfg.Extensions.xml_unmarshal_cfg_attribute_Attribute(c));
                            }return x;
                         }
            public static cfg.skill.Forever xml_unmarshal_cfg_skill_Forever(XmlElement ele) {
                            var x = new cfg.skill.Forever(ele);
                            return x;
                         }
            public static cfg.item.Item xml_unmarshal_cfg_item_Item(XmlElement ele) {
                            var x = new cfg.item.Item(ele);
                            return x;
                         }
            public static cfg.common.Currency xml_unmarshal_cfg_common_Currency(XmlElement ele) {
                            var x = new cfg.common.Currency(ele);
                            return x;
                         }
            public static cfg.common.CurrencyCost xml_unmarshal_cfg_common_CurrencyCost(XmlElement ele) {
                            var x = new cfg.common.CurrencyCost(ele);
                            return x;
                         }
            public static cfg.skill.StackReset xml_unmarshal_cfg_skill_StackReset(XmlElement ele) {
                            var x = new cfg.skill.StackReset(ele);
                            return x;
                         }
            public static cfg.skill.AddShield xml_unmarshal_cfg_skill_AddShield(XmlElement ele) {
                            var x = new cfg.skill.AddShield(ele);
                            return x;
                         }
            public static System.Collections.Generic.List<cfg.bonus.BonusData> xml_unmarshal_list_cfg_bonus_BonusData(XmlElement ele) {
                            var x = new System.Collections.Generic.List<cfg.bonus.BonusData>();
                            foreach(var c in ele.GetChildren()) {
                            x.Add(cfg.Extensions.xml_unmarshal_cfg_bonus_BonusData(c));
                            }return x;
                         }
            public static cfg.bonus.Empty xml_unmarshal_cfg_bonus_Empty(XmlElement ele) {
                            var x = new cfg.bonus.Empty(ele);
                            return x;
                         }
            public static cfg.attribute.AttributeInfo xml_unmarshal_cfg_attribute_AttributeInfo(XmlElement ele) {
                            var type = ele.GetAttribute("type");
                            cfg.attribute.AttributeInfo x;
                            switch(type) {
                            case "null": return null;
                            case "None" : x = new cfg.attribute.None(ele); break;
                            case "AttributeNumber" : x = new cfg.attribute.AttributeNumber(ele); break;
                            case "AttributeLongNumber" : x = new cfg.attribute.AttributeLongNumber(ele); break;
                            case "And" : x = new cfg.attribute.And(ele); break;
                            default: throw new Perfect.Marshal.MarshalException("unknown bean type:" + type);}
                            return x;
                         }
            public static cfg.sound.Sound xml_unmarshal_cfg_sound_Sound(XmlElement ele) {
                            var x = new cfg.sound.Sound(ele);
                            return x;
                         }
            public static cfg.Vector2Int xml_unmarshal_cfg_Vector2Int(XmlElement ele) {
                            var x = new cfg.Vector2Int(ele);
                            return x;
                         }
            public static System.Collections.Generic.List<cfg.bonus.Currency> xml_unmarshal_list_cfg_bonus_Currency(XmlElement ele) {
                            var x = new System.Collections.Generic.List<cfg.bonus.Currency>();
                            foreach(var c in ele.GetChildren()) {
                            x.Add(cfg.Extensions.xml_unmarshal_cfg_bonus_Currency(c));
                            }return x;
                         }
            public static cfg.attribute.AttributeLongNumber xml_unmarshal_cfg_attribute_AttributeLongNumber(XmlElement ele) {
                            var x = new cfg.attribute.AttributeLongNumber(ele);
                            return x;
                         }
            public static cfg.common.XFloat xml_unmarshal_cfg_common_XFloat(XmlElement ele) {
                            var x = new cfg.common.XFloat(ele);
                            return x;
                         }
            public static cfg.common.XInt xml_unmarshal_cfg_common_XInt(XmlElement ele) {
                            var x = new cfg.common.XInt(ele);
                            return x;
                         }
            public static cfg.skill.SkillReleaseEmpty xml_unmarshal_cfg_skill_SkillReleaseEmpty(XmlElement ele) {
                            var x = new cfg.skill.SkillReleaseEmpty(ele);
                            return x;
                         }
            public static cfg.skill.BulletHitBuffEffect xml_unmarshal_cfg_skill_BulletHitBuffEffect(XmlElement ele) {
                            var x = new cfg.skill.BulletHitBuffEffect(ele);
                            return x;
                         }
            public static cfg.asset.Asset xml_unmarshal_cfg_asset_Asset(XmlElement ele) {
                            var x = new cfg.asset.Asset(ele);
                            return x;
                         }
            public static cfg.asset.AssetInfo xml_unmarshal_cfg_asset_AssetInfo(XmlElement ele) {
                            var type = ele.GetAttribute("type");
                            cfg.asset.AssetInfo x;
                            switch(type) {
                            case "null": return null;
                            case "Texture" : x = new cfg.asset.Texture(ele); break;
                            case "Sprite" : x = new cfg.asset.Sprite(ele); break;
                            case "Spine" : x = new cfg.asset.Spine(ele); break;
                            default: throw new Perfect.Marshal.MarshalException("unknown bean type:" + type);}
                            return x;
                         }
            public static cfg.skill.StackNone xml_unmarshal_cfg_skill_StackNone(XmlElement ele) {
                            var x = new cfg.skill.StackNone(ele);
                            return x;
                         }
            public static cfg.function.AdGetCurrencyInfo xml_unmarshal_cfg_function_AdGetCurrencyInfo(XmlElement ele) {
                            var x = new cfg.function.AdGetCurrencyInfo(ele);
                            return x;
                         }
            public static cfg.skill.StackNum xml_unmarshal_cfg_skill_StackNum(XmlElement ele) {
                            var x = new cfg.skill.StackNum(ele);
                            return x;
                         }
            public static System.Collections.Generic.List<cfg.attribute.AttributeInfo> xml_unmarshal_list_cfg_attribute_AttributeInfo(XmlElement ele) {
                            var x = new System.Collections.Generic.List<cfg.attribute.AttributeInfo>();
                            foreach(var c in ele.GetChildren()) {
                            x.Add(cfg.Extensions.xml_unmarshal_cfg_attribute_AttributeInfo(c));
                            }return x;
                         }
            public static cfg.condition.BuyMonthlyCard xml_unmarshal_cfg_condition_BuyMonthlyCard(XmlElement ele) {
                            var x = new cfg.condition.BuyMonthlyCard(ele);
                            return x;
                         }
            public static System.Collections.Generic.List<cfg.skill.BulletConfig> xml_unmarshal_list_cfg_skill_BulletConfig(XmlElement ele) {
                            var x = new System.Collections.Generic.List<cfg.skill.BulletConfig>();
                            foreach(var c in ele.GetChildren()) {
                            x.Add(cfg.Extensions.xml_unmarshal_cfg_skill_BulletConfig(c));
                            }return x;
                         }
            public static cfg.skill.IntervalTrigger xml_unmarshal_cfg_skill_IntervalTrigger(XmlElement ele) {
                            var x = new cfg.skill.IntervalTrigger(ele);
                            return x;
                         }
            public static System.Collections.Generic.List<cfg.item.Quality> xml_unmarshal_list_cfg_item_Quality(XmlElement ele) {
                            var x = new System.Collections.Generic.List<cfg.item.Quality>();
                            foreach(var c in ele.GetChildren()) {
                            x.Add(cfg.Extensions.xml_unmarshal_cfg_item_Quality(c));
                            }return x;
                         }
            public static cfg.sound.Music xml_unmarshal_cfg_sound_Music(XmlElement ele) {
                            var x = new cfg.sound.Music(ele);
                            return x;
                         }
            public static cfg.global.GBool xml_unmarshal_cfg_global_GBool(XmlElement ele) {
                            var x = new cfg.global.GBool(ele);
                            return x;
                         }
            public static cfg.skill.ImmuneCurrentDamage xml_unmarshal_cfg_skill_ImmuneCurrentDamage(XmlElement ele) {
                            var x = new cfg.skill.ImmuneCurrentDamage(ele);
                            return x;
                         }
            public static cfg.skill.DurationDestroy xml_unmarshal_cfg_skill_DurationDestroy(XmlElement ele) {
                            var x = new cfg.skill.DurationDestroy(ele);
                            return x;
                         }
            public static cfg.bonus.RandomEquipFragmentQuality xml_unmarshal_cfg_bonus_RandomEquipFragmentQuality(XmlElement ele) {
                            var x = new cfg.bonus.RandomEquipFragmentQuality(ele);
                            return x;
                         }
            public static cfg.skill.AddBoostEnergy xml_unmarshal_cfg_skill_AddBoostEnergy(XmlElement ele) {
                            var x = new cfg.skill.AddBoostEnergy(ele);
                            return x;
                         }
            public static cfg.bonus.MultiBonus xml_unmarshal_cfg_bonus_MultiBonus(XmlElement ele) {
                            var x = new cfg.bonus.MultiBonus(ele);
                            return x;
                         }
            public static cfg.function.JumpShopInfo xml_unmarshal_cfg_function_JumpShopInfo(XmlElement ele) {
                            var x = new cfg.function.JumpShopInfo(ele);
                            return x;
                         }
            public static cfg.global.GFloat xml_unmarshal_cfg_global_GFloat(XmlElement ele) {
                            var x = new cfg.global.GFloat(ele);
                            return x;
                         }
            public static cfg.asset.Spine xml_unmarshal_cfg_asset_Spine(XmlElement ele) {
                            var x = new cfg.asset.Spine(ele);
                            return x;
                         }
            public static cfg.bonus.RandomBonus xml_unmarshal_cfg_bonus_RandomBonus(XmlElement ele) {
                            var x = new cfg.bonus.RandomBonus(ele);
                            return x;
                         }
            public static cfg.skill.ImmediateTrigger xml_unmarshal_cfg_skill_ImmediateTrigger(XmlElement ele) {
                            var x = new cfg.skill.ImmediateTrigger(ele);
                            return x;
                         }
            public static cfg.localize.TextureLanguage xml_unmarshal_cfg_localize_TextureLanguage(XmlElement ele) {
                            var x = new cfg.localize.TextureLanguage(ele);
                            return x;
                         }
            public static cfg.common.XLong xml_unmarshal_cfg_common_XLong(XmlElement ele) {
                            var x = new cfg.common.XLong(ele);
                            return x;
                         }
            public static cfg.skill.ReverseMoveDirection xml_unmarshal_cfg_skill_ReverseMoveDirection(XmlElement ele) {
                            var x = new cfg.skill.ReverseMoveDirection(ele);
                            return x;
                         }
            public static cfg.skill.BulletTargetType xml_unmarshal_cfg_skill_BulletTargetType(XmlElement ele) {
                            var type = ele.GetAttribute("type");
                            cfg.skill.BulletTargetType x;
                            switch(type) {
                            case "null": return null;
                            case "SingleTarget" : x = new cfg.skill.SingleTarget(ele); break;
                            case "RangeTarget" : x = new cfg.skill.RangeTarget(ele); break;
                            case "DirectionTarget" : x = new cfg.skill.DirectionTarget(ele); break;
                            default: throw new Perfect.Marshal.MarshalException("unknown bean type:" + type);}
                            return x;
                         }
            public static cfg.skill.SkillShowEffect xml_unmarshal_cfg_skill_SkillShowEffect(XmlElement ele) {
                            var type = ele.GetAttribute("type");
                            cfg.skill.SkillShowEffect x;
                            switch(type) {
                            case "null": return null;
                            case "SkillShowNoEffect" : x = new cfg.skill.SkillShowNoEffect(ele); break;
                            case "SkillShowOnceEffect" : x = new cfg.skill.SkillShowOnceEffect(ele); break;
                            default: throw new Perfect.Marshal.MarshalException("unknown bean type:" + type);}
                            return x;
                         }
            public static cfg.skill.ImmediateRelease xml_unmarshal_cfg_skill_ImmediateRelease(XmlElement ele) {
                            var x = new cfg.skill.ImmediateRelease(ele);
                            return x;
                         }
            public static cfg.skill.SkillReleaseBuff xml_unmarshal_cfg_skill_SkillReleaseBuff(XmlElement ele) {
                            var x = new cfg.skill.SkillReleaseBuff(ele);
                            return x;
                         }
            public static cfg.bonus.WeightRandomBonus xml_unmarshal_cfg_bonus_WeightRandomBonus(XmlElement ele) {
                            var x = new cfg.bonus.WeightRandomBonus(ele);
                            return x;
                         }
            public static cfg.global.GInt32 xml_unmarshal_cfg_global_GInt32(XmlElement ele) {
                            var x = new cfg.global.GInt32(ele);
                            return x;
                         }
            public static cfg.Vector2 xml_unmarshal_cfg_Vector2(XmlElement ele) {
                            var x = new cfg.Vector2(ele);
                            return x;
                         }
            public static cfg.bonus.Currencies xml_unmarshal_cfg_bonus_Currencies(XmlElement ele) {
                            var x = new cfg.bonus.Currencies(ele);
                            return x;
                         }
            public static cfg.common.AdCost xml_unmarshal_cfg_common_AdCost(XmlElement ele) {
                            var x = new cfg.common.AdCost(ele);
                            return x;
                         }
            public static cfg.skill.Continuous xml_unmarshal_cfg_skill_Continuous(XmlElement ele) {
                            var x = new cfg.skill.Continuous(ele);
                            return x;
                         }
            public static cfg.Vector3 xml_unmarshal_cfg_Vector3(XmlElement ele) {
                            var x = new cfg.Vector3(ele);
                            return x;
                         }
            public static cfg.skill.BuffStatcking xml_unmarshal_cfg_skill_BuffStatcking(XmlElement ele) {
                            var type = ele.GetAttribute("type");
                            cfg.skill.BuffStatcking x;
                            switch(type) {
                            case "null": return null;
                            case "StackNone" : x = new cfg.skill.StackNone(ele); break;
                            case "StackReset" : x = new cfg.skill.StackReset(ele); break;
                            case "StackNum" : x = new cfg.skill.StackNum(ele); break;
                            default: throw new Perfect.Marshal.MarshalException("unknown bean type:" + type);}
                            return x;
                         }
            public static cfg.global.GlobalInfo xml_unmarshal_cfg_global_GlobalInfo(XmlElement ele) {
                            var type = ele.GetAttribute("type");
                            cfg.global.GlobalInfo x;
                            switch(type) {
                            case "null": return null;
                            case "GInt32" : x = new cfg.global.GInt32(ele); break;
                            case "GInt64" : x = new cfg.global.GInt64(ele); break;
                            case "GString" : x = new cfg.global.GString(ele); break;
                            case "GFloat" : x = new cfg.global.GFloat(ele); break;
                            case "GBool" : x = new cfg.global.GBool(ele); break;
                            default: throw new Perfect.Marshal.MarshalException("unknown bean type:" + type);}
                            return x;
                         }
            public static System.Collections.Generic.List<cfg.global.Global> xml_unmarshal_list_cfg_global_Global(XmlElement ele) {
                            var x = new System.Collections.Generic.List<cfg.global.Global>();
                            foreach(var c in ele.GetChildren()) {
                            x.Add(cfg.Extensions.xml_unmarshal_cfg_global_Global(c));
                            }return x;
                         }
            public static System.Collections.Generic.List<cfg.skill.BulletHitEffectType> xml_unmarshal_list_cfg_skill_BulletHitEffectType(XmlElement ele) {
                            var x = new System.Collections.Generic.List<cfg.skill.BulletHitEffectType>();
                            foreach(var c in ele.GetChildren()) {
                            x.Add(cfg.Extensions.xml_unmarshal_cfg_skill_BulletHitEffectType(c));
                            }return x;
                         }
            public static cfg.common.None xml_unmarshal_cfg_common_None(XmlElement ele) {
                            var x = new cfg.common.None(ele);
                            return x;
                         }
            public static cfg.common.MoneyCost xml_unmarshal_cfg_common_MoneyCost(XmlElement ele) {
                            var x = new cfg.common.MoneyCost(ele);
                            return x;
                         }
            public static cfg.skill.ReleaseBulletAtCollisionPoint xml_unmarshal_cfg_skill_ReleaseBulletAtCollisionPoint(XmlElement ele) {
                            var x = new cfg.skill.ReleaseBulletAtCollisionPoint(ele);
                            return x;
                         }
            public static cfg.skill.FlyBullet xml_unmarshal_cfg_skill_FlyBullet(XmlElement ele) {
                            var x = new cfg.skill.FlyBullet(ele);
                            return x;
                         }
            public static cfg.common.ItemCost xml_unmarshal_cfg_common_ItemCost(XmlElement ele) {
                            var x = new cfg.common.ItemCost(ele);
                            return x;
                         }
            public static cfg.bonus.Bonus xml_unmarshal_cfg_bonus_Bonus(XmlElement ele) {
                            var type = ele.GetAttribute("type");
                            cfg.bonus.Bonus x;
                            switch(type) {
                            case "null": return null;
                            case "Empty" : x = new cfg.bonus.Empty(ele); break;
                            case "RefBonus" : x = new cfg.bonus.RefBonus(ele); break;
                            case "Currency" : x = new cfg.bonus.Currency(ele); break;
                            case "Currencies" : x = new cfg.bonus.Currencies(ele); break;
                            case "ItemId" : x = new cfg.bonus.ItemId(ele); break;
                            case "Item" : x = new cfg.bonus.Item(ele); break;
                            case "Items" : x = new cfg.bonus.Items(ele); break;
                            case "RandomEquipFragmentQuality" : x = new cfg.bonus.RandomEquipFragmentQuality(ele); break;
                            case "WeightBonus" : x = new cfg.bonus.WeightBonus(ele); break;
                            case "RandomBonus" : x = new cfg.bonus.RandomBonus(ele); break;
                            case "WeightRandomBonus" : x = new cfg.bonus.WeightRandomBonus(ele); break;
                            case "MultiBonus" : x = new cfg.bonus.MultiBonus(ele); break;
                            case "RepeatBonus" : x = new cfg.bonus.RepeatBonus(ele); break;
                            case "CopyBonus" : x = new cfg.bonus.CopyBonus(ele); break;
                            default: throw new Perfect.Marshal.MarshalException("unknown bean type:" + type);}
                            return x;
                         }
            public static System.Collections.Generic.List<cfg.common.Currency> xml_unmarshal_list_cfg_common_Currency(XmlElement ele) {
                            var x = new System.Collections.Generic.List<cfg.common.Currency>();
                            foreach(var c in ele.GetChildren()) {
                            x.Add(cfg.Extensions.xml_unmarshal_cfg_common_Currency(c));
                            }return x;
                         }
            public static System.Collections.Generic.List<cfg.skill.BuffConfig> xml_unmarshal_list_cfg_skill_BuffConfig(XmlElement ele) {
                            var x = new System.Collections.Generic.List<cfg.skill.BuffConfig>();
                            foreach(var c in ele.GetChildren()) {
                            x.Add(cfg.Extensions.xml_unmarshal_cfg_skill_BuffConfig(c));
                            }return x;
                         }
            public static cfg.attribute.Attribute xml_unmarshal_cfg_attribute_Attribute(XmlElement ele) {
                            var x = new cfg.attribute.Attribute(ele);
                            return x;
                         }
            public static cfg.asset.Texture xml_unmarshal_cfg_asset_Texture(XmlElement ele) {
                            var x = new cfg.asset.Texture(ele);
                            return x;
                         }
            public static cfg.skill.DealEffectiveDamageTrigger xml_unmarshal_cfg_skill_DealEffectiveDamageTrigger(XmlElement ele) {
                            var x = new cfg.skill.DealEffectiveDamageTrigger(ele);
                            return x;
                         }
            public static cfg.skill.BeCollidedTrigger xml_unmarshal_cfg_skill_BeCollidedTrigger(XmlElement ele) {
                            var x = new cfg.skill.BeCollidedTrigger(ele);
                            return x;
                         }
            public static cfg.attribute.None xml_unmarshal_cfg_attribute_None(XmlElement ele) {
                            var x = new cfg.attribute.None(ele);
                            return x;
                         }
            public static cfg.skill.BuffEffectContent xml_unmarshal_cfg_skill_BuffEffectContent(XmlElement ele) {
                            var type = ele.GetAttribute("type");
                            cfg.skill.BuffEffectContent x;
                            switch(type) {
                            case "null": return null;
                            case "AttributeChange" : x = new cfg.skill.AttributeChange(ele); break;
                            case "AddBuff" : x = new cfg.skill.AddBuff(ele); break;
                            case "ReverseMoveDirection" : x = new cfg.skill.ReverseMoveDirection(ele); break;
                            case "ImmuneCurrentDamage" : x = new cfg.skill.ImmuneCurrentDamage(ele); break;
                            case "KnockbackTriggerTarget" : x = new cfg.skill.KnockbackTriggerTarget(ele); break;
                            case "ReleaseBulletAtCollisionPoint" : x = new cfg.skill.ReleaseBulletAtCollisionPoint(ele); break;
                            case "AddBulletHitEffect" : x = new cfg.skill.AddBulletHitEffect(ele); break;
                            case "SkillReleaseIntervalSub" : x = new cfg.skill.SkillReleaseIntervalSub(ele); break;
                            case "FixedDamage" : x = new cfg.skill.FixedDamage(ele); break;
                            case "AddBoostEnergy" : x = new cfg.skill.AddBoostEnergy(ele); break;
                            case "EffectiveDamageToHeal" : x = new cfg.skill.EffectiveDamageToHeal(ele); break;
                            case "AddShield" : x = new cfg.skill.AddShield(ele); break;
                            default: throw new Perfect.Marshal.MarshalException("unknown bean type:" + type);}
                            return x;
                         }
            public static cfg.function.JumpInfo xml_unmarshal_cfg_function_JumpInfo(XmlElement ele) {
                            var type = ele.GetAttribute("type");
                            cfg.function.JumpInfo x;
                            switch(type) {
                            case "null": return null;
                            case "JumpShopInfo" : x = new cfg.function.JumpShopInfo(ele); break;
                            case "JumpBattleInfo" : x = new cfg.function.JumpBattleInfo(ele); break;
                            case "AdGetCurrencyInfo" : x = new cfg.function.AdGetCurrencyInfo(ele); break;
                            default: throw new Perfect.Marshal.MarshalException("unknown bean type:" + type);}
                            return x;
                         }
            public static System.Collections.Generic.List<cfg.common.Cost> xml_unmarshal_list_cfg_common_Cost(XmlElement ele) {
                            var x = new System.Collections.Generic.List<cfg.common.Cost>();
                            foreach(var c in ele.GetChildren()) {
                            x.Add(cfg.Extensions.xml_unmarshal_cfg_common_Cost(c));
                            }return x;
                         }
            public static cfg.skill.SingleTarget xml_unmarshal_cfg_skill_SingleTarget(XmlElement ele) {
                            var x = new cfg.skill.SingleTarget(ele);
                            return x;
                         }
            public static cfg.common.Or xml_unmarshal_cfg_common_Or(XmlElement ele) {
                            var x = new cfg.common.Or(ele);
                            return x;
                         }
            public static cfg.localize.TexScaleElement xml_unmarshal_cfg_localize_TexScaleElement(XmlElement ele) {
                            var x = new cfg.localize.TexScaleElement(ele);
                            return x;
                         }
            public static cfg.bonus.BonusData xml_unmarshal_cfg_bonus_BonusData(XmlElement ele) {
                            var x = new cfg.bonus.BonusData(ele);
                            return x;
                         }
            public static System.Collections.Generic.List<cfg.battle.BattleAsset> xml_unmarshal_list_cfg_battle_BattleAsset(XmlElement ele) {
                            var x = new System.Collections.Generic.List<cfg.battle.BattleAsset>();
                            foreach(var c in ele.GetChildren()) {
                            x.Add(cfg.Extensions.xml_unmarshal_cfg_battle_BattleAsset(c));
                            }return x;
                         }
            public static cfg.bonus.WeightBonus xml_unmarshal_cfg_bonus_WeightBonus(XmlElement ele) {
                            var x = new cfg.bonus.WeightBonus(ele);
                            return x;
                         }
            public static cfg.skill.ImmediateDestroy xml_unmarshal_cfg_skill_ImmediateDestroy(XmlElement ele) {
                            var x = new cfg.skill.ImmediateDestroy(ele);
                            return x;
                         }
            public static cfg.bonus.RefBonus xml_unmarshal_cfg_bonus_RefBonus(XmlElement ele) {
                            var x = new cfg.bonus.RefBonus(ele);
                            return x;
                         }
            public static cfg.attribute.AttributeNumber xml_unmarshal_cfg_attribute_AttributeNumber(XmlElement ele) {
                            var x = new cfg.attribute.AttributeNumber(ele);
                            return x;
                         }
            public static cfg.skill.SkillReleaseType xml_unmarshal_cfg_skill_SkillReleaseType(XmlElement ele) {
                            var type = ele.GetAttribute("type");
                            cfg.skill.SkillReleaseType x;
                            switch(type) {
                            case "null": return null;
                            case "SkillReleaseBullet" : x = new cfg.skill.SkillReleaseBullet(ele); break;
                            case "SkillReleaseBuff" : x = new cfg.skill.SkillReleaseBuff(ele); break;
                            case "SkillReleaseEmpty" : x = new cfg.skill.SkillReleaseEmpty(ele); break;
                            case "SkillReleaseSplitSatellite" : x = new cfg.skill.SkillReleaseSplitSatellite(ele); break;
                            default: throw new Perfect.Marshal.MarshalException("unknown bean type:" + type);}
                            return x;
                         }
            public static cfg.bonus.Currency xml_unmarshal_cfg_bonus_Currency(XmlElement ele) {
                            var x = new cfg.bonus.Currency(ele);
                            return x;
                         }
            public static cfg.sound.SoundEffect xml_unmarshal_cfg_sound_SoundEffect(XmlElement ele) {
                            var x = new cfg.sound.SoundEffect(ele);
                            return x;
                         }
            public static cfg.bonus.RepeatBonus xml_unmarshal_cfg_bonus_RepeatBonus(XmlElement ele) {
                            var x = new cfg.bonus.RepeatBonus(ele);
                            return x;
                         }
            public static cfg.condition.Item xml_unmarshal_cfg_condition_Item(XmlElement ele) {
                            var x = new cfg.condition.Item(ele);
                            return x;
                         }
            public static cfg.skill.BulletReleaseCount xml_unmarshal_cfg_skill_BulletReleaseCount(XmlElement ele) {
                            var type = ele.GetAttribute("type");
                            cfg.skill.BulletReleaseCount x;
                            switch(type) {
                            case "null": return null;
                            case "Single" : x = new cfg.skill.Single(ele); break;
                            case "Continuous" : x = new cfg.skill.Continuous(ele); break;
                            case "Circle" : x = new cfg.skill.Circle(ele); break;
                            default: throw new Perfect.Marshal.MarshalException("unknown bean type:" + type);}
                            return x;
                         }
            public static cfg.skill.SkillReleaseBullet xml_unmarshal_cfg_skill_SkillReleaseBullet(XmlElement ele) {
                            var x = new cfg.skill.SkillReleaseBullet(ele);
                            return x;
                         }
            public static cfg.skill.RangeTarget xml_unmarshal_cfg_skill_RangeTarget(XmlElement ele) {
                            var x = new cfg.skill.RangeTarget(ele);
                            return x;
                         }
            public static cfg.skill.Circle xml_unmarshal_cfg_skill_Circle(XmlElement ele) {
                            var x = new cfg.skill.Circle(ele);
                            return x;
                         }
            public static cfg.attribute.And xml_unmarshal_cfg_attribute_And(XmlElement ele) {
                            var x = new cfg.attribute.And(ele);
                            return x;
                         }
            public static cfg.common.Cost xml_unmarshal_cfg_common_Cost(XmlElement ele) {
                            var type = ele.GetAttribute("type");
                            cfg.common.Cost x;
                            switch(type) {
                            case "null": return null;
                            case "None" : x = new cfg.common.None(ele); break;
                            case "AdCost" : x = new cfg.common.AdCost(ele); break;
                            case "CurrencyCost" : x = new cfg.common.CurrencyCost(ele); break;
                            case "ItemCost" : x = new cfg.common.ItemCost(ele); break;
                            case "And" : x = new cfg.common.And(ele); break;
                            case "Or" : x = new cfg.common.Or(ele); break;
                            case "MoneyCost" : x = new cfg.common.MoneyCost(ele); break;
                            default: throw new Perfect.Marshal.MarshalException("unknown bean type:" + type);}
                            return x;
                         }
            public static cfg.item.GiftInfo xml_unmarshal_cfg_item_GiftInfo(XmlElement ele) {
                            var x = new cfg.item.GiftInfo(ele);
                            return x;
                         }
            public static cfg.bonus.ItemId xml_unmarshal_cfg_bonus_ItemId(XmlElement ele) {
                            var x = new cfg.bonus.ItemId(ele);
                            return x;
                         }
            public static cfg.skill.BeforeCollisionDamageTrigger xml_unmarshal_cfg_skill_BeforeCollisionDamageTrigger(XmlElement ele) {
                            var x = new cfg.skill.BeforeCollisionDamageTrigger(ele);
                            return x;
                         }
            public static cfg.asset.Sprite xml_unmarshal_cfg_asset_Sprite(XmlElement ele) {
                            var x = new cfg.asset.Sprite(ele);
                            return x;
                         }
            public static cfg.skill.TargetInRangeRelease xml_unmarshal_cfg_skill_TargetInRangeRelease(XmlElement ele) {
                            var x = new cfg.skill.TargetInRangeRelease(ele);
                            return x;
                         }
            public static cfg.skill.BulletHitEffectType xml_unmarshal_cfg_skill_BulletHitEffectType(XmlElement ele) {
                            var type = ele.GetAttribute("type");
                            cfg.skill.BulletHitEffectType x;
                            switch(type) {
                            case "null": return null;
                            case "BulletHitDamageEffect" : x = new cfg.skill.BulletHitDamageEffect(ele); break;
                            case "BulletHitEffects" : x = new cfg.skill.BulletHitEffects(ele); break;
                            case "BulletHitBuffEffect" : x = new cfg.skill.BulletHitBuffEffect(ele); break;
                            default: throw new Perfect.Marshal.MarshalException("unknown bean type:" + type);}
                            return x;
                         }
            public static cfg.battle.BattleAsset xml_unmarshal_cfg_battle_BattleAsset(XmlElement ele) {
                            var x = new cfg.battle.BattleAsset(ele);
                            return x;
                         }
            public static cfg.skill.TargetFlyBullet xml_unmarshal_cfg_skill_TargetFlyBullet(XmlElement ele) {
                            var x = new cfg.skill.TargetFlyBullet(ele);
                            return x;
                         }
            public static cfg.skill.BulletType xml_unmarshal_cfg_skill_BulletType(XmlElement ele) {
                            var type = ele.GetAttribute("type");
                            cfg.skill.BulletType x;
                            switch(type) {
                            case "null": return null;
                            case "FlyBullet" : x = new cfg.skill.FlyBullet(ele); break;
                            case "TargetFlyBullet" : x = new cfg.skill.TargetFlyBullet(ele); break;
                            case "ImmediateBullet" : x = new cfg.skill.ImmediateBullet(ele); break;
                            default: throw new Perfect.Marshal.MarshalException("unknown bean type:" + type);}
                            return x;
                         }
            public static cfg.condition.Or xml_unmarshal_cfg_condition_Or(XmlElement ele) {
                            var x = new cfg.condition.Or(ele);
                            return x;
                         }
            public static cfg.common.And xml_unmarshal_cfg_common_And(XmlElement ele) {
                            var x = new cfg.common.And(ele);
                            return x;
                         }
            public static cfg.common.XNumber xml_unmarshal_cfg_common_XNumber(XmlElement ele) {
                            var type = ele.GetAttribute("type");
                            cfg.common.XNumber x;
                            switch(type) {
                            case "null": return null;
                            case "XInt" : x = new cfg.common.XInt(ele); break;
                            case "XLong" : x = new cfg.common.XLong(ele); break;
                            case "XFloat" : x = new cfg.common.XFloat(ele); break;
                            case "XBigDecimal" : x = new cfg.common.XBigDecimal(ele); break;
                            default: throw new Perfect.Marshal.MarshalException("unknown bean type:" + type);}
                            return x;
                         }
            public static System.Collections.Generic.List<cfg.bonus.Item> xml_unmarshal_list_cfg_bonus_Item(XmlElement ele) {
                            var x = new System.Collections.Generic.List<cfg.bonus.Item>();
                            foreach(var c in ele.GetChildren()) {
                            x.Add(cfg.Extensions.xml_unmarshal_cfg_bonus_Item(c));
                            }return x;
                         }
            public static System.Collections.Generic.List<cfg.error.Error> xml_unmarshal_list_cfg_error_Error(XmlElement ele) {
                            var x = new System.Collections.Generic.List<cfg.error.Error>();
                            foreach(var c in ele.GetChildren()) {
                            x.Add(cfg.Extensions.xml_unmarshal_cfg_error_Error(c));
                            }return x;
                         }
            public static cfg.skill.SkillReleaseSplitSatellite xml_unmarshal_cfg_skill_SkillReleaseSplitSatellite(XmlElement ele) {
                            var x = new cfg.skill.SkillReleaseSplitSatellite(ele);
                            return x;
                         }
            public static cfg.skill.SkillReleaseIntervalSub xml_unmarshal_cfg_skill_SkillReleaseIntervalSub(XmlElement ele) {
                            var x = new cfg.skill.SkillReleaseIntervalSub(ele);
                            return x;
                         }
            public static cfg.condition.OwnItem xml_unmarshal_cfg_condition_OwnItem(XmlElement ele) {
                            var x = new cfg.condition.OwnItem(ele);
                            return x;
                         }
            public static cfg.skill.DirectionTarget xml_unmarshal_cfg_skill_DirectionTarget(XmlElement ele) {
                            var x = new cfg.skill.DirectionTarget(ele);
                            return x;
                         }
            public static cfg.condition.And xml_unmarshal_cfg_condition_And(XmlElement ele) {
                            var x = new cfg.condition.And(ele);
                            return x;
                         }
            public static cfg.localize.Language xml_unmarshal_cfg_localize_Language(XmlElement ele) {
                            var x = new cfg.localize.Language(ele);
                            return x;
                         }
            public static System.Collections.Generic.List<int> xml_unmarshal_list_int(XmlElement ele) {
                            var x = new System.Collections.Generic.List<int>();
                            foreach(var c in ele.GetChildren()) {
                            x.Add(c.ReadInt());
                            }return x;
                         }
            public static cfg.item.Quality xml_unmarshal_cfg_item_Quality(XmlElement ele) {
                            var x = new cfg.item.Quality(ele);
                            return x;
                         }
            public static System.Collections.Generic.List<cfg.function.Jump> xml_unmarshal_list_cfg_function_Jump(XmlElement ele) {
                            var x = new System.Collections.Generic.List<cfg.function.Jump>();
                            foreach(var c in ele.GetChildren()) {
                            x.Add(cfg.Extensions.xml_unmarshal_cfg_function_Jump(c));
                            }return x;
                         }
            public static cfg.item.ArenaBoxInfo xml_unmarshal_cfg_item_ArenaBoxInfo(XmlElement ele) {
                            var x = new cfg.item.ArenaBoxInfo(ele);
                            return x;
                         }
            public static System.Collections.Generic.List<cfg.item.Item> xml_unmarshal_list_cfg_item_Item(XmlElement ele) {
                            var x = new System.Collections.Generic.List<cfg.item.Item>();
                            foreach(var c in ele.GetChildren()) {
                            x.Add(cfg.Extensions.xml_unmarshal_cfg_item_Item(c));
                            }return x;
                         }

        }
    }
}
