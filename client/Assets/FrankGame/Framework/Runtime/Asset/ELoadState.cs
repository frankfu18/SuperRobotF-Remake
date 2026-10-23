using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrankGame
{
    public enum ELoadState
    {
        NotLoad,//没有加载
        Loading,//正在加载
        Downloading,//正在下载
        LoadDone,//加载完成
    }
}