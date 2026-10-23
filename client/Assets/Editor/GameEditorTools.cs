using System.Diagnostics;
using System.IO;
using FrankGame;
using UnityEditor;
using UnityEngine;

public static class GameEditorTools
{
    [MenuItem("FrankGame/缓存/清理系统缓存")]
    public static void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    [MenuItem("FrankGame/缓存/清空数据库")]
    public static void ClearDataBase()
    {
        DataSaveManager.ClearAll();
#if BYTE_GAME
        FrankGame.DataSaveManager.DeleteAllDatas();
#endif
    }


}
