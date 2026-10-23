using System.Collections.Generic;

using db.cfg;
using db.core;

namespace db.parser {
   public partial class ItemDataParser : BaseBeanParser<ItemData>{
      public static ItemDataParser Ins => DataBaseManager.Instance.GetRootParser<ItemDataParser, ItemData>();

      private DictionaryParser<int, db.cfg.ItemInfo> m_itemInfoDic;
      public DictionaryParser<int, db.cfg.ItemInfo> ItemInfoDic => m_itemInfoDic ??= GetParser<DictionaryParser<int, db.cfg.ItemInfo>>("itemInfoDic");

   }

}
