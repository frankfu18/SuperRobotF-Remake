namespace db.core
{
    public interface IParser
    {

        /// <summary>
        /// 上下文
        /// </summary>
        public ParserContext Context { get;}

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="_context"></param>
        public void Init(ParserContext _context);
        
        /// <summary>
        /// 获取 子元素 parser
        /// </summary>
        /// <param name="_idx">子元素索引， Bean 是属性，Dic 是key，List 是 idx</param>
        /// <typeparam name="P"></typeparam>
        /// <returns>子元素解析类</returns>
        public P GetParser<P>(object _idx) where P : IParser;

        
        /// <summary>
        /// 创建数据源
        /// </summary>
        public void CreateDatasource();

        /// <summary>
        /// 刷新一下数据
        /// </summary>
        public void RefreshDatasource();

        /// <summary>
        /// 加载完成回调
        /// </summary>
        public void OnLoaded();
        
        /// <summary>
        /// 保存
        /// </summary>
        public void Save();

        /// <summary>
        /// 是否空/初始化
        /// </summary>
        /// <returns></returns>
        public bool IsNull();

        /// <summary>
        /// 根据 key 设置其数据
        /// </summary>
        /// <param name="_key"></param>
        /// <param name="_newData"></param>
        public void SetDataByKey(object _key, object _newData);
        
        
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="_key"></param>
        /// <typeparam name="D"></typeparam>
        /// <returns></returns>
        public D GetDataByKey<D>(object _key);
    }
}