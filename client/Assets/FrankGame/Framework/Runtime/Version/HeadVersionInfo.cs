using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace FrankGame
{
    [Serializable]
    public class HeadVersionInfo
    {
        public bool debug;//是否开启调试模式
        public bool check_client;//检测客户端版本
        public string client_version;//客户端版本号  必须是三段格式 1.0.1
        public string serverList_url;//服务器列表地址
        public string package_url;//最新包地址
        public string files_url;//目录文件地址
        public string resource_url;//Bundle资源地址
        public string announcement_url;//公告地址

        public bool IsNeedForceUpdateClient(string _targetVersion)
        {
            if (!check_client)
            {
                return false;
            }

            string[] thisSplits = client_version.Split('.');
            string[] targetSplits = _targetVersion.Split('.');

            if (thisSplits.Length != targetSplits.Length)
            {
                return false;
            }

            for (int i = 0; i < thisSplits.Length; i++)
            {
                int thisNumber = int.Parse(thisSplits[i]);
                int targetNumber = int.Parse(targetSplits[i]);
                if (thisNumber > targetNumber)
                {
                    return true;
                }

                if (thisNumber == targetNumber)
                {
                    continue;
                }

                return false;
            }

            return false;
        }
    }
}