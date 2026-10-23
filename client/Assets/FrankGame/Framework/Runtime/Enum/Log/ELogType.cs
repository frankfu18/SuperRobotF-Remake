using System;

namespace FrankGame
{
    [Flags]
    public enum ELogType
    {
        Default = 2,//默认
        Asset = 4,//资源管理
        Net = 8,
        UI = 16,
        Battle = 32,
        Guide = 64,
    }
}