using System.Collections.Generic;

using db.cfg;
using db.core;

namespace db.parser {
   public partial class EquipDataParser : BaseBeanParser<EquipData>{
      public static EquipDataParser Ins => DataBaseManager.Instance.GetRootParser<EquipDataParser, EquipData>();

      private MonitorDictionary<int, int> m_equipSlotDic;
              
      public IDictionary<int, int> EquipSlotDic {
          get {
              if (m_equipSlotDic != null) return m_equipSlotDic;
              if (Datasource.equipSlotDic == null) {
                  Datasource.equipSlotDic = new Dictionary<int, int>();
                  Save();
              }
              m_equipSlotDic = new MonitorDictionary<int, int>(Datasource.equipSlotDic) { OnChanged = Save };
              return m_equipSlotDic;
          }
          set {
              var ds = DataBaseUtil.GetMonitorDicSource(value);
              Datasource.equipSlotDic = ds ?? new Dictionary<int, int>();
              m_equipSlotDic = new MonitorDictionary<int, int>(Datasource.equipSlotDic) { OnChanged = Save };
              Save();
          }
      }
      private DictionaryParser<int, db.cfg.EquipInfo> m_equipInfoDic;
      public DictionaryParser<int, db.cfg.EquipInfo> EquipInfoDic => m_equipInfoDic ??= GetParser<DictionaryParser<int, db.cfg.EquipInfo>>("equipInfoDic");

   }

}
