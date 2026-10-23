using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FrankGame;
using TMPro;

public class FontLocalizationProcedure : GameLaunchProcedure
{
    private Queue<TMPFontInfo> fontInfoQueue = new Queue<TMPFontInfo>();

    protected override void OnEnter(params object[] _params)
    {
        SetProgress(0, LocalizationManager.Instance.GetPackageLanguageText("asset_loading"));

        AssetSystem.Instance.LoadAsset<ScriptableObject>(EResType.Font, GameConfig.Font.FontLocalizationConfigName, ResConfig.Asset_Suffix, LoadFontConfigSuccess);
    }

    private void LoadFontConfigSuccess(ScriptableObject _scriptableObject)
    {
        TMPLocalizationConfig fontLocalCfg = (TMPLocalizationConfig)_scriptableObject;

        foreach (var fontInfo in fontLocalCfg.fontInfoList)
        {
            fontInfoQueue.Enqueue(fontInfo);
        }

        CheckLoadFont();
    }

    private void CheckLoadFont()
    {
        if (fontInfoQueue.Count == 0)
        {
            CompleteCurrentProcedure();
            return;
        }

        LoadFont(fontInfoQueue.Dequeue());
    }

    private void LoadFont(TMPFontInfo _fontInfo)
    {
        Log.Debug("加载字体模板：" + _fontInfo.baseFontAssetPath);

        AssetSystem.Instance.LoadAsset<TMP_FontAsset>(EResType.Font, _fontInfo.baseFontAssetPath, ResConfig.Asset_Suffix, (fontAsset) =>
        {
            if (GameConfig.Font.ClearAllFallbackFont)
            {
                fontAsset.fallbackFontAssetTable.Clear();
            }

            LoadFontLanguage(fontAsset, _fontInfo);
        });
    }

    private void LoadFontLanguage(TMP_FontAsset _fontAsset, TMPFontInfo _fontInfo)
    {
        int totalCount = _fontInfo.languageInfoList.Count;
        if (totalCount > 0)
        {
            SystemLanguage curLanguage = LocalizationManager.Instance.Lanuage;
            if (GameConfig.Font.LoadAllLanguage)
            {
                int curCount = 0;

                foreach (var languageInfo in _fontInfo.languageInfoList)
                {
                    Log.Debug("加载多语言字体：" + languageInfo.fontAssetPath);
                    AssetSystem.Instance.LoadAsset<TMP_FontAsset>(EResType.Font, languageInfo.fontAssetPath, ResConfig.Asset_Suffix, (fontAsset) =>
                    {
                        _fontAsset.fallbackFontAssetTable.Add(fontAsset);

                        curCount++;
                        if (curCount == totalCount)
                        {
                            CheckLoadFont();
                        }
                    });
                }
            }
            else
            {
                List<TMPLanguageInfo> tmpLanguageInfoList = new List<TMPLanguageInfo>();

                foreach (var languageInfo in _fontInfo.languageInfoList)
                {
                    SystemLanguage sysLanguage = languageInfo.language;
                    if ((sysLanguage == SystemLanguage.Chinese || sysLanguage == SystemLanguage.ChineseSimplified || sysLanguage == SystemLanguage.ChineseTraditional) &&
                        (curLanguage == SystemLanguage.Chinese || curLanguage == SystemLanguage.ChineseSimplified || curLanguage == SystemLanguage.ChineseTraditional))
                    {
                        tmpLanguageInfoList.Add(languageInfo);
                    }
                    if ((sysLanguage == SystemLanguage.Chinese || sysLanguage == SystemLanguage.ChineseSimplified 
                        || sysLanguage == SystemLanguage.ChineseTraditional || sysLanguage == SystemLanguage.Japanese) &&
                        (curLanguage == SystemLanguage.Japanese))
                    {
                        tmpLanguageInfoList.Add(languageInfo);
                    }
                    else if (sysLanguage == curLanguage)
                    {
                        tmpLanguageInfoList.Add(languageInfo);
                        break;
                    }
                    else if (sysLanguage == GameConfig.Localization.DefaultLanguage)
                    {
                        tmpLanguageInfoList.Add(languageInfo);
                    }
                }

                if (tmpLanguageInfoList.Count == 0)
                {
                    //Log.Error("多语言字体未找到:" + curLanguage);
                    CompleteCurrentProcedure();
                }
                else
                {
                    int assetCount = tmpLanguageInfoList.Count;
                    int curCount = 0;
                    foreach (var tmpLanguage in tmpLanguageInfoList)
                    {
                        Log.Debug("加载多语言字体：" + tmpLanguage.fontAssetPath);
                        AssetSystem.Instance.LoadAsset<TMP_FontAsset>(EResType.Font, tmpLanguage.fontAssetPath, ResConfig.Asset_Suffix, (fontAsset) =>
                        {
                            _fontAsset.fallbackFontAssetTable.Add(fontAsset);
                            curCount++;
                            if (curCount == assetCount)
                            {
                                CheckLoadFont();
                            }
                        });
                    }
                    
                }
            }
        }
        else
        {
            CompleteCurrentProcedure();
        }
    }

    protected override void OnExit()
    {

    }
}

