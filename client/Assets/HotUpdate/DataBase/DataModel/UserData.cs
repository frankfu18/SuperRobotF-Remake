using db.core;
using System.Collections.Generic;

namespace db.cfg
{
    [System.Serializable, DatabaseRoot]
    public class UserData
    {
        public string uid;
        public long lastOnlineTimestamp;// 最后在线时间
        public string userName;
        public int headId;
        public int score;//竞技场分数
    }
}