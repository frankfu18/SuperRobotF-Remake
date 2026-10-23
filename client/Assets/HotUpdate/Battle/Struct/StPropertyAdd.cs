using UnityEngine;

namespace FrankGame.Battle
{
    public struct StPropertyAdd
    {
        public float addValue;
        public float mutiValue;
        public float addMaxValue;
        public float mutiMaxValue;

        public void SetPropertyAddValue(float _add,float _muti)
        {
            addValue = addMaxValue == 0 ? _add : Mathf.Clamp(_add,0,addMaxValue);
            mutiValue = mutiMaxValue == 0 ? _muti : Mathf.Clamp(_muti,0,mutiMaxValue);
        }
    }
}