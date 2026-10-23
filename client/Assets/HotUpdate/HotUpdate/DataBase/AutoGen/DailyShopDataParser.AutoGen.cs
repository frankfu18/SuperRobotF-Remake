using System.Collections.Generic;

using db.cfg;
using db.core;

namespace db.parser {
   public partial class DailyShopDataParser : BaseBeanParser<DailyShopData>{
      public static DailyShopDataParser Ins => DataBaseManager.Instance.GetRootParser<DailyShopDataParser, DailyShopData>();

      public int TodayClickRefreshCount {
         get => Datasource.todayClickRefreshCount;
         set {
            Datasource.todayClickRefreshCount = value;
            Save();
         }
      }

      public long LastClickRefreshTime {
         get => Datasource.lastClickRefreshTime;
         set {
            Datasource.lastClickRefreshTime = value;
            Save();
         }
      }

      public long GoodsListRandomTime {
         get => Datasource.goodsListRandomTime;
         set {
            Datasource.goodsListRandomTime = value;
            Save();
         }
      }

      private IList<int> m_goodsIdList;
              
      public IList<int> GoodsIdList {
          get {
              if (m_goodsIdList != null) return m_goodsIdList;
              if (Datasource.goodsIdList == null) {
                  Datasource.goodsIdList = new List<int>();
                  Save();
              }
              m_goodsIdList = new MonitorList<int>(Datasource.goodsIdList) { OnChanged = Save };
              return m_goodsIdList;
          }
          set {
              var ds = DataBaseUtil.GetMonitorListSource(value);
              Datasource.goodsIdList = ds ?? new List<int>();
              m_goodsIdList = new MonitorList<int>(Datasource.goodsIdList) { OnChanged = Save };
              Save();
          }
      }
      private IList<int> m_discountList;
              
      public IList<int> DiscountList {
          get {
              if (m_discountList != null) return m_discountList;
              if (Datasource.discountList == null) {
                  Datasource.discountList = new List<int>();
                  Save();
              }
              m_discountList = new MonitorList<int>(Datasource.discountList) { OnChanged = Save };
              return m_discountList;
          }
          set {
              var ds = DataBaseUtil.GetMonitorListSource(value);
              Datasource.discountList = ds ?? new List<int>();
              m_discountList = new MonitorList<int>(Datasource.discountList) { OnChanged = Save };
              Save();
          }
      }
      private IList<int> m_boughtIdList;
              
      public IList<int> BoughtIdList {
          get {
              if (m_boughtIdList != null) return m_boughtIdList;
              if (Datasource.boughtIdList == null) {
                  Datasource.boughtIdList = new List<int>();
                  Save();
              }
              m_boughtIdList = new MonitorList<int>(Datasource.boughtIdList) { OnChanged = Save };
              return m_boughtIdList;
          }
          set {
              var ds = DataBaseUtil.GetMonitorListSource(value);
              Datasource.boughtIdList = ds ?? new List<int>();
              m_boughtIdList = new MonitorList<int>(Datasource.boughtIdList) { OnChanged = Save };
              Save();
          }
      }

   }

}
