using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrankGame.Battle
{
    public interface IComponent
    {
        public ECompType ComponentType { get; }
    }
}
