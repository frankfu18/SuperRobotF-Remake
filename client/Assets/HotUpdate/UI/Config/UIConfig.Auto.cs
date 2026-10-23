//auto generic code don't edit.
using System.Collections.Generic;

public enum UIConfig
{
	GuidePanel,
	GMPanel,
	MainPanel,
	CommonPopupPanel,
	GetRewardPanel,
	BattlePanel,
	MatchingPanel,
	BattleResultPanel,
	SettingPanel,
	RankPanel,
	UserPanel,
	UserChangeHeadPanel,
	UserChangeNamePanel,
	GetBoxPanel,
	CardPanel,
	BattleGMPanel,
}

public class UIInfoTable
{
	private static Dictionary<UIConfig, UIInfo> UIInfoDic = new Dictionary<UIConfig, UIInfo>()
	{
		{ UIConfig.GuidePanel, new UIInfo() { layer = EUILayer.Guide, uiConfigId = UIConfig.GuidePanel, prefabName = "Guide/GuidePanel", instance = true, showPlayAnimation = false, loadResources = false}},
		{ UIConfig.GMPanel, new UIInfo() { layer = EUILayer.Default, uiConfigId = UIConfig.GMPanel, prefabName = "GM/GMPanel", instance = true, showPlayAnimation = false, loadResources = false}},
		{ UIConfig.MainPanel, new UIInfo() { layer = EUILayer.Main, uiConfigId = UIConfig.MainPanel, prefabName = "Main/MainPanel", instance = true, showPlayAnimation = false, loadResources = false}},
		{ UIConfig.CommonPopupPanel, new UIInfo() { layer = EUILayer.Default, uiConfigId = UIConfig.CommonPopupPanel, prefabName = "Popup/CommonPopupPanel", instance = true, showPlayAnimation = true, loadResources = false}},
		{ UIConfig.GetRewardPanel, new UIInfo() { layer = EUILayer.Default, uiConfigId = UIConfig.GetRewardPanel, prefabName = "GetReward/GetRewardPanel", instance = true, showPlayAnimation = false, loadResources = false}},
		{ UIConfig.BattlePanel, new UIInfo() { layer = EUILayer.Default, uiConfigId = UIConfig.BattlePanel, prefabName = "Battle/BattlePanel", instance = true, showPlayAnimation = false, loadResources = false}},
		{ UIConfig.MatchingPanel, new UIInfo() { layer = EUILayer.High, uiConfigId = UIConfig.MatchingPanel, prefabName = "Arena/MatchingPanel", instance = true, showPlayAnimation = false, loadResources = false}},
		{ UIConfig.BattleResultPanel, new UIInfo() { layer = EUILayer.Default, uiConfigId = UIConfig.BattleResultPanel, prefabName = "Battle/BattleResultPanel", instance = true, showPlayAnimation = false, loadResources = false}},
		{ UIConfig.SettingPanel, new UIInfo() { layer = EUILayer.Default, uiConfigId = UIConfig.SettingPanel, prefabName = "Setting/SettingPanel", instance = true, showPlayAnimation = true, loadResources = false}},
		{ UIConfig.RankPanel, new UIInfo() { layer = EUILayer.Default, uiConfigId = UIConfig.RankPanel, prefabName = "Rank/RankPanel", instance = true, showPlayAnimation = false, loadResources = false}},
		{ UIConfig.UserPanel, new UIInfo() { layer = EUILayer.Default, uiConfigId = UIConfig.UserPanel, prefabName = "User/UserPanel", instance = true, showPlayAnimation = true, loadResources = false}},
		{ UIConfig.UserChangeHeadPanel, new UIInfo() { layer = EUILayer.Default, uiConfigId = UIConfig.UserChangeHeadPanel, prefabName = "User/UserChangeHeadPanel", instance = true, showPlayAnimation = true, loadResources = false}},
		{ UIConfig.UserChangeNamePanel, new UIInfo() { layer = EUILayer.Default, uiConfigId = UIConfig.UserChangeNamePanel, prefabName = "User/UserChangeNamePanel", instance = true, showPlayAnimation = true, loadResources = false}},
		{ UIConfig.GetBoxPanel, new UIInfo() { layer = EUILayer.Default, uiConfigId = UIConfig.GetBoxPanel, prefabName = "GetReward/GetBoxPanel", instance = true, showPlayAnimation = false, loadResources = false}},
		{ UIConfig.CardPanel, new UIInfo() { layer = EUILayer.Default, uiConfigId = UIConfig.CardPanel, prefabName = "Card/CardPanel", instance = true, showPlayAnimation = false, loadResources = false}},
		{ UIConfig.BattleGMPanel, new UIInfo() { layer = EUILayer.Default, uiConfigId = UIConfig.BattleGMPanel, prefabName = "GM/BattleGMPanel", instance = true, showPlayAnimation = false, loadResources = false}},
	};
	public static UIInfo GetUIInfo(UIConfig _uiConfig)
	{
		if (UIInfoDic.TryGetValue(_uiConfig, out UIInfo uiInfo))
		{
			return uiInfo;
		}
		return null;
	}
	public static IUIInterface CreateUIPanel(UIConfig _uiConfig)
	{
		switch (_uiConfig)
		{
			case UIConfig.GuidePanel:
				return new GuidePanel();
			case UIConfig.GMPanel:
				return new GMPanel();
			case UIConfig.MainPanel:
				return new MainPanel();
			case UIConfig.CommonPopupPanel:
				return new CommonPopupPanel();
			case UIConfig.GetRewardPanel:
				return new GetRewardPanel();
			case UIConfig.BattlePanel:
				return new BattlePanel();
			case UIConfig.MatchingPanel:
				return new MatchingPanel();
			case UIConfig.BattleResultPanel:
				return new BattleResultPanel();
			case UIConfig.SettingPanel:
				return new SettingPanel();
			case UIConfig.RankPanel:
				return new RankPanel();
			case UIConfig.UserPanel:
				return new UserPanel();
			case UIConfig.UserChangeHeadPanel:
				return new UserChangeHeadPanel();
			case UIConfig.UserChangeNamePanel:
				return new UserChangeNamePanel();
			case UIConfig.GetBoxPanel:
				return new GetBoxPanel();
			case UIConfig.CardPanel:
				return new CardPanel();
			case UIConfig.BattleGMPanel:
				return new BattleGMPanel();
		}
		throw new System.Exception($"Class {_uiConfig} not exist!");
	}
}
