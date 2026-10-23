using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace db.parser
{
    public partial class BattleDataParser
    {
        public void Clear()
        {
            this.ArenaLevel = 0;

            Save();
        }
    }
}