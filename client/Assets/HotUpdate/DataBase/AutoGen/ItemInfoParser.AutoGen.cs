using System.Collections.Generic;

using db.cfg;
using db.core;

namespace db.parser {
   public partial class ItemInfoParser : BaseBeanParser<ItemInfo>{
      public static ItemInfoParser Ins => DataBaseManager.Instance.GetRootParser<ItemInfoParser, ItemInfo>();

      public int ItemId {
         get => Datasource.itemId;
         set {
            Datasource.itemId = value;
            Save();
         }
      }

      public long ItemCount {
         get => Datasource.itemCount;
         set {
            Datasource.itemCount = value;
            Save();
         }
      }


   }

}
