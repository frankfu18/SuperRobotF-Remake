using System.Collections;
using System.Collections.Generic;
using db.core;

namespace db.cfg
{
    [System.Serializable, DatabaseRoot]
    public class RoleData
    {
        public int curRoleId;//当前上阵的角色
        public Dictionary<int, RoleInfo> roleInfoDic;
    }

    [System.Serializable, DataBase]
    public class RoleInfo
    {
        public int roleId;
        public int level;
    }
}
