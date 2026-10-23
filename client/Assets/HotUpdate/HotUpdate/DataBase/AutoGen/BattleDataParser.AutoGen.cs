using System.Collections.Generic;

using db.cfg;
using db.core;

namespace db.parser {
   public partial class BattleDataParser : BaseBeanParser<BattleData>{
      public static BattleDataParser Ins => DataBaseManager.Instance.GetRootParser<BattleDataParser, BattleData>();

      public int ArenaLevel {
         get => Datasource.arenaLevel;
         set {
            Datasource.arenaLevel = value;
            Save();
         }
      }


   }

}
