using System.Collections.Generic;
using db.core;

namespace db.cfg
{
    [System.Serializable, DatabaseRoot]
    public class ItemData
    {
        public Dictionary<int, ItemInfo> itemInfoDic; //物品信息，key:item表id
    }
    [System.Serializable, DataBase]
    public class ItemInfo
    {
        public int itemId;
        public long itemCount;
    }
}