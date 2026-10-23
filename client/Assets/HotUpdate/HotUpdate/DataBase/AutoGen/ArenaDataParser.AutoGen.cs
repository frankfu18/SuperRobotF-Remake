using System.Collections.Generic;

using db.cfg;
using db.core;

namespace db.parser {
   public partial class ArenaDataParser : BaseBeanParser<ArenaData>{
      public static ArenaDataParser Ins => DataBaseManager.Instance.GetRootParser<ArenaDataParser, ArenaData>();

      public int TotalCount {
         get => Datasource.totalCount;
         set {
            Datasource.totalCount = value;
            Save();
         }
      }

      public int WinCount {
         get => Datasource.winCount;
         set {
            Datasource.winCount = value;
            Save();
         }
      }

      public int LoseCount {
         get => Datasource.loseCount;
         set {
            Datasource.loseCount = value;
            Save();
         }
      }

      public int WinningStreak {
         get => Datasource.winningStreak;
         set {
            Datasource.winningStreak = value;
            Save();
         }
      }

      public int LosingStreak {
         get => Datasource.losingStreak;
         set {
            Datasource.losingStreak = value;
            Save();
         }
      }

      public int AIDifficultyTier {
         get => Datasource.aiDifficultyTier;
         set {
            Datasource.aiDifficultyTier = value;
            Save();
         }
      }


   }

}
