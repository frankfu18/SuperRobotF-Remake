using System.Collections;
using System.Collections.Generic;
using db.core;

namespace db.cfg
{
    [System.Serializable, DatabaseRoot]
    public class BattleData
    {
        public int arenaLevel;//保存的竞技场等级，0代表没有记录
    }
}
