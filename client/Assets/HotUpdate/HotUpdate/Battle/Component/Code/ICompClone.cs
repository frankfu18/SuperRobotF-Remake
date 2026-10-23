using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrankGame.Battle
{
    public interface ICompClone<T> where T : ComponentBase
    {
        public void Clone(T _t);
    }
}
