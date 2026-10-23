using UnityEngine;

namespace FrankGame
{
    [CreateAssetMenu(menuName = "FrankGame/ScriptableObject/ServerConfig", fileName = "ServerConfig")]
    public class ServerConfig : SingleScriptableObject<ServerConfig>
    {

        [Header("URL前缀（空不开启）")]
        public string HostPrefix;

        [Header("心跳检测周期（秒）")]
        public int Heartbeat = 0;

        [Header("MD5盐值")]
        public string MD5Salt;

    }
}