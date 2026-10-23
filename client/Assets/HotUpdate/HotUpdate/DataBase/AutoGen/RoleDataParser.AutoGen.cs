using System.Collections.Generic;

using db.cfg;
using db.core;

namespace db.parser {
   public partial class RoleDataParser : BaseBeanParser<RoleData>{
      public static RoleDataParser Ins => DataBaseManager.Instance.GetRootParser<RoleDataParser, RoleData>();

      public int CurRoleId {
         get => Datasource.curRoleId;
         set {
            Datasource.curRoleId = value;
            Save();
         }
      }

      private DictionaryParser<int, db.cfg.RoleInfo> m_roleInfoDic;
      public DictionaryParser<int, db.cfg.RoleInfo> RoleInfoDic => m_roleInfoDic ??= GetParser<DictionaryParser<int, db.cfg.RoleInfo>>("roleInfoDic");

   }

}
