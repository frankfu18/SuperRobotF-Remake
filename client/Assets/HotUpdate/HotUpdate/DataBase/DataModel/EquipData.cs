using System.Collections;
using System.Collections.Generic;
using db.core;

namespace db.cfg
{
    [System.Serializable, DatabaseRoot]
    public class EquipData
    {
        public Dictionary<int, int> equipSlotDic;// key 索引 value 卡id  -1未解锁 0 空
        public Dictionary<int, EquipInfo> equipInfoDic;
    }

    [System.Serializable, DataBase]
    public class EquipInfo
    {
        public int equipId;
        public int level;
    }
}
