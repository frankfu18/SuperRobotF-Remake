using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrankGame
{
    [Serializable]
    public class VersionIndexInfo
    {
        public string new_client_version;//最新客户端版本号  必须是三段格式 1.0.1
        public string serverList_url;//服务器列表地址
        public string announcement_url;//公告地址
        public string font_url;//多语言字体地址
        public List<ClientVersionInfo> client_version_list;//客户端版本信息

        public bool GetCurrentClientVersion(out ClientVersionInfo _clientVersionInfo)
        {
            _clientVersionInfo = client_version_list.Find((c) => 
            {
                return c.version.Equals(ClientVersionInfo.GetClientVersion());
            });

            return _clientVersionInfo != null;
        }

        public bool GetNewestClientVersion(out ClientVersionInfo _clientVersionInfo)
        {
            _clientVersionInfo = client_version_list.Find((c) =>
            {
                return c.version.Equals(new_client_version);
            });

            return _clientVersionInfo != null;
        }
    }
}
