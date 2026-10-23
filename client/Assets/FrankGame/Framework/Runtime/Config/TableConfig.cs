using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrankGame
{
    [CreateAssetMenu(menuName = "FrankGame/ScriptableObject/TableConfig", fileName = "TableConfig")]
    public class TableConfig : SingleScriptableObject<TableConfig>
    {
        [Header("本地资源路径")]
        public string LocalTablePath = "Assets/Res/config/";

        [Header("加载相对路径")]
        public string TableRoot = "config/";

        [Header("表格配置文件后缀")]
        public string TableSuffix = ".bytes";

        [Header("表格配置打包层级")]
        public int TableLayer = 0;
    }
}