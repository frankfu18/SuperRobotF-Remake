using db.core;

namespace db.cfg
{
    [System.Serializable, DatabaseRoot]
    public class ArenaData
    {
        public int aiDifficultyTier;//竞技场 AI 动态难度层级
        public int totalCount;//总场次
        public int winCount;//胜利场次
        public int loseCount;//失败场次
        public int winningStreak;//连胜场次
        public int losingStreak;//连败场次
    }

}
