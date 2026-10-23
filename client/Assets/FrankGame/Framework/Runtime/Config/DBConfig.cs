using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrankGame
{
    [CreateAssetMenu(menuName = "FrankGame/ScriptableObject/DBConfig", fileName = "DBConfig")]
    public class DBConfig : SingleScriptableObject<DBConfig>
    {
        [Header("是否加密数据库")]
        public bool IsEncrypt;
    }
}