
namespace FrankGame
{
    public abstract class TimeSinglet<T> where T : TimeSinglet<T>, new()
    {
        /// <summary>
        /// 静态实例
        /// </summary>
        protected static T mInstance;

        /// <summary>
        /// 初始化标识
        /// </summary>
        static bool mIsInit;
        protected static bool IsInit => mIsInit;


        /// <summary>
        /// 静态属性
        /// </summary>
        public static T Instance
        {
            get
            {
                if (mInstance == null)
                {
                    mInstance = new T();
                    mInstance.OnSingletonInit();
                }

                return mInstance;
            }
        }

        /// <summary>
        /// 资源释放
        /// </summary>
        public virtual void Dispose()
        {
            mInstance = null;
        }

        /// <summary>
        /// 单例初始化方法
        /// </summary>
        public virtual void OnSingletonInit()
        {
        }

        public virtual void OnInit()
        {
            mIsInit = true;
        }
    }
}