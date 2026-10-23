using System.Collections.Generic;

using db.cfg;
using db.core;

namespace db.parser {
   public partial class RoleInfoParser : BaseBeanParser<RoleInfo>{
      public static RoleInfoParser Ins => DataBaseManager.Instance.GetRootParser<RoleInfoParser, RoleInfo>();

      public int RoleId {
         get => Datasource.roleId;
         set {
            Datasource.roleId = value;
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
