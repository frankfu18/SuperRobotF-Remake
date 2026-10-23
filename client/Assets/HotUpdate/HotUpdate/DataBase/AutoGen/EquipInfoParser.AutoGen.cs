using System.Collections.Generic;

using db.cfg;
using db.core;

namespace db.parser {
   public partial class EquipInfoParser : BaseBeanParser<EquipInfo>{
      public static EquipInfoParser Ins => DataBaseManager.Instance.GetRootParser<EquipInfoParser, EquipInfo>();

      public int EquipId {
         get => Datasource.equipId;
         set {
            Datasource.equipId = value;
            Save();
         }
      }

      public int Level {
         get => Datasource.level;
         set {
            Datasource.level = value;
            Save();
         }
      }


   }

}
