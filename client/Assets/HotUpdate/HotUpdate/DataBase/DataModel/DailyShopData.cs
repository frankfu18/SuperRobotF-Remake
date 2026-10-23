using db.core;
using System.Collections.Generic;

namespace db.cfg
{
    /// <summary>
    /// 每日商店
    /// </summary>
    [System.Serializable, DatabaseRoot]
    public class DailyShopData
    {

        /// <summary>
        /// 今日刷新次数
        /// </summary>
        public int todayClickRefreshCount;

        /// <summary>
        /// 上次点击刷新的时间
        /// </summary>
        public long lastClickRefreshTime;

        /// <summary>
        /// 上次随机生成商品列表的时间
        /// </summary>
        public long goodsListRandomTime;

        /// <summary>
        /// 商品列表id
        /// </summary>
        public List<int> goodsIdList;

        /// <summary>
        /// 商品折扣列表
        /// </summary>
        public List<int> discountList;

        /// <summary>
        /// 已购买列表
        /// </summary>
        public List<int> boughtIdList;

    }

}