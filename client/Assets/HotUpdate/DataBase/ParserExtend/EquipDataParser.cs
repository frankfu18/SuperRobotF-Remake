using db.cfg;
using db.core;

namespace db.parser
{
    public partial class EquipDataParser
    {
        public void SetEquipSlot(int _index, int _equipId)
        {
            if(this.EquipSlotDic.ContainsKey(_index))
            {
                this.EquipSlotDic[_index] = _equipId;
            }
            else
            {
                this.EquipSlotDic.Add(_index, _equipId);
            }
        }

        public void SetEquipInfo(int _equipId, int _level)
        {
            if(this.EquipInfoDic.TryGet<EquipInfoParser>(_equipId, out var info))
            {
                info.Level = _level;
            }
            else
            {
                var equipInfo = new EquipInfo();
                equipInfo.equipId = _equipId;
                equipInfo.level = _level;

                this.EquipInfoDic.Add(_equipId, equipInfo);
            }
        }

        public bool IsHasEquip(int _equipId)
        {
            return this.EquipInfoDic.ContainsKey(_equipId);
        }

        public int GetEquipLevel(int _equipId)
        {
            if (this.EquipInfoDic.TryGetValue(_equipId, out var equipInfo))
            {
                return equipInfo.level;
            }

            return 0;
        }

        public bool IsSlotUnlcok(int _index)
        {
            if(this.EquipSlotDic.TryGetValue(_index, out var equipId))
            {
                return equipId >= 0;
            }

            return false;
        }
    }

}