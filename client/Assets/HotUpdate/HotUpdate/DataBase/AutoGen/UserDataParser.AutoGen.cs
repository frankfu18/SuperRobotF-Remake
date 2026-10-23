using System.Collections.Generic;

using db.cfg;
using db.core;

namespace db.parser {
   public partial class UserDataParser : BaseBeanParser<UserData>{
      public static UserDataParser Ins => DataBaseManager.Instance.GetRootParser<UserDataParser, UserData>();

      public string Uid {
         get => Datasource.uid;
         set {
            Datasource.uid = value;
            Save();
         }
      }

      public long LastOnlineTimestamp {
         get => Datasource.lastOnlineTimestamp;
         set {
            Datasource.lastOnlineTimestamp = value;
            Save();
         }
      }

      public string UserName {
         get => Datasource.userName;
         set {
            Datasource.userName = value;
            Save();
         }
      }

      public int HeadId {
         get => Datasource.headId;
         set {
            Datasource.headId = value;
            Save();
         }
      }

      public int Score {
         get => Datasource.score;
         set {
            Datasource.score = value;
            Save();
         }
      }


   }

}
