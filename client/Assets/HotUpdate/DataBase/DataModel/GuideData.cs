using db.core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace db.cfg
{
    [System.Serializable, DatabaseRoot]
    public class GuideData
    {
        public Dictionary<int, int> guides;//物品信息，key: 引导id value 1代表完成
    }
}
