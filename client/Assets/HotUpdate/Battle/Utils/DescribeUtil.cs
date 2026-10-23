using editor.cfg.skill;

public static class DescribeUtil
{
    private const string ProbabilityFlag = "[#probability]";

    public static string GetSkillDesc(int skillId)
    {
        SkillReleaseConfig config = ConfigSystem.CurrentData.GetSkillReleaseConfig(skillId);
        return config?.desc ?? string.Empty;
    }

    public static string GetBuffDesc(int buffId)
    {
        BuffConfig config = ConfigSystem.CurrentData.GetBuffConfig(buffId);
        if (config == null) return string.Empty;
        string text = config.desc;
        if (config.triggerType.ratio < 10000)
            text = text.Replace(ProbabilityFlag, GetRatio(config.triggerType.ratio));
        return text;
    }

    public static string GetRatio(long ratio)
    {
        return (ratio / 100f).ToString("0.##") + "%";
    }
    public static string GetRatio(float ratio)
    {
        return (ratio * 100f).ToString("0.##") + "%";
    }
}
