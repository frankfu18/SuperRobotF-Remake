using db.cfg;
using db.core;

namespace db.parser
{
    public partial class RoleDataParser
    {
        public void SetRoleInfo(int _roleId, int _level)
        {
            if(this.RoleInfoDic.TryGet<RoleInfoParser>(_roleId, out var info))
            {
                info.Level = _level;
            }
            else
            {
                var roleInfo = new RoleInfo();
                roleInfo.roleId = _roleId;
                roleInfo.level = _level;

                this.RoleInfoDic.Add(_roleId, roleInfo);
            }
        }

        public bool IsHasRole(int _roleId)
        {
            return this.RoleInfoDic.ContainsKey(_roleId);
        }

        public int GetRoleLevel(int _roleId)
        {
            if (this.RoleInfoDic.TryGetValue(_roleId, out var roleInfo))
            {
                return roleInfo.level;
            }

            return 0;
        }
    }

}