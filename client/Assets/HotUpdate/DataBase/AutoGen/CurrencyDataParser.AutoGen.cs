using System.Collections.Generic;

using db.cfg;
using db.core;

namespace db.parser {
   public partial class CurrencyDataParser : BaseBeanParser<CurrencyData>{
      public static CurrencyDataParser Ins => DataBaseManager.Instance.GetRootParser<CurrencyDataParser, CurrencyData>();

      private MonitorDictionary<editor.cfg.common.ECurrencyType, long> m_currencyDic;
              
      public IDictionary<editor.cfg.common.ECurrencyType, long> CurrencyDic {
          get {
              if (m_currencyDic != null) return m_currencyDic;
              if (Datasource.currencyDic == null) {
                  Datasource.currencyDic = new Dictionary<editor.cfg.common.ECurrencyType, long>();
                  Save();
              }
              m_currencyDic = new MonitorDictionary<editor.cfg.common.ECurrencyType, long>(Datasource.currencyDic) { OnChanged = Save };
              return m_currencyDic;
          }
          set {
              var ds = DataBaseUtil.GetMonitorDicSource(value);
              Datasource.currencyDic = ds ?? new Dictionary<editor.cfg.common.ECurrencyType, long>();
              m_currencyDic = new MonitorDictionary<editor.cfg.common.ECurrencyType, long>(Datasource.currencyDic) { OnChanged = Save };
              Save();
          }
      }
      public long StrengthLastRecoverTimestamp {
         get => Datasource.strengthLastRecoverTimestamp;
         set {
            Datasource.strengthLastRecoverTimestamp = value;
            Save();
         }
      }


   }

}
