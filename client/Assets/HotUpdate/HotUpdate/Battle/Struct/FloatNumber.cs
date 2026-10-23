namespace FrankGame.Battle
{
    public class FloatNumber
    {
        public static FloatNumber Zero = new FloatNumber() { baseValue = 0, mutiValue = 0,addValue = 0};

        public float baseValue;
        public float mutiValue;
        public float addValue;
        
        public FloatNumber()
        {

        }

        public FloatNumber(float _baseValue, float _mutiValue=0,float _addValue=0)
        {
            this.baseValue = _baseValue;
            this.mutiValue = _mutiValue;
            this.addValue = _addValue;
        }

        public float GetResult()
        {
            return (this.baseValue + this.addValue) * (1 + mutiValue);
        }

        
    } 
}