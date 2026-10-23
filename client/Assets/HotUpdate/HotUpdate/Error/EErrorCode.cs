
public class EErrorCode
{
    public const int OK = 0;

    public const int Exception_Error = -1;

    public const int Currency_Not_Enough = 1;//货币不足
    public const int Currency_Amount_Error = 2;//货币数量错误
    public const int Strength_Not_Enough = 3;//体力不足
    public const int Explore_Not_Enough = 4;//探索值

    public const int Item_Not_Enough = 101;//物品数量不足
    public const int Item_Amount_Error = 102;//物品数量错误
    public const int Item_Not_Exist = 103;//物品不存在
    public const int Item_Not_Use = 104;//物品不可以使用

    public const int Arena_Not_Box_Can_Receive = 2001;//没有可领取的宝箱

    public const int User_Name_Empty = 3001;//用户名不能为空
    public const int User_Name_Word_Limt = 3002;//超过字符限制

    public const int Role_HasUnlock = 4001;//角色已经解锁
    public const int Role_NotUnlock = 4002;//角色未解锁
    public const int Role_Level_Max = 4003;//角色等级达到最大等级
    public const int Role_HasGoInBattle = 4004;//角色已上阵

    public const int Equip_Slot_Max = 6001;//上阵位置已满
    public const int Equip_Level_Max = 6002;//装备等级达到最大等级
    public const int Equip_In_Slot = 6003;//装备已上阵
    public const int Equip_Not_In_Slot = 6004;//装备未上阵



}
