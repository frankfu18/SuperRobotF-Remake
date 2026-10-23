using System.Collections.Generic;
using db.core;
using editor.cfg.common;

namespace db.cfg
{
    [System.Serializable, DatabaseRoot]
    public class CurrencyData 
    {
        //所有货币，key ECurrencyType, value : 数量
        public Dictionary<ECurrencyType, long> currencyDic;
        public long strengthLastRecoverTimestamp;//体力上次恢复的时间戳
    }
}