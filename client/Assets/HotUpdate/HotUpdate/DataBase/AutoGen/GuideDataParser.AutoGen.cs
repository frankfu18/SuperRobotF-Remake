using System.Collections.Generic;

using db.cfg;
using db.core;

namespace db.parser {
   public partial class GuideDataParser : BaseBeanParser<GuideData>{
      public static GuideDataParser Ins => DataBaseManager.Instance.GetRootParser<GuideDataParser, GuideData>();

      private MonitorDictionary<int, int> m_guides;
              
      public IDictionary<int, int> Guides {
          get {
              if (m_guides != null) return m_guides;
              if (Datasource.guides == null) {
                  Datasource.guides = new Dictionary<int, int>();
                  Save();
              }
              m_guides = new MonitorDictionary<int, int>(Datasource.guides) { OnChanged = Save };
              return m_guides;
          }
          set {
              var ds = DataBaseUtil.GetMonitorDicSource(value);
              Datasource.guides = ds ?? new Dictionary<int, int>();
              m_guides = new MonitorDictionary<int, int>(Datasource.guides) { OnChanged = Save };
              Save();
          }
      }

   }

}
