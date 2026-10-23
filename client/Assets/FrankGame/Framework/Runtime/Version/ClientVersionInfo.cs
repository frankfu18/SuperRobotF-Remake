using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrankGame
{
    [Serializable]
    public class ClientVersionInfo
    {
        public bool debug;//是否开启调试模式
        public string version;//客户端版本号  必须是三段格式 1.0.1
        public string res_url;//Bundle资源地址
        public string files_url;//目录文件地址

        public static string GetClientVersion()
        {
            return Application.version;
        }
    }
}
