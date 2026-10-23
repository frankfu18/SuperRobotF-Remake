namespace FrankGame.Battle
{
    public static class ComponentUtil
    {
        public static void PassData<T>(Entity _fromEntity, Entity _toEntity, ECompType _compType) where T: ComponentBase
        {
            if (_fromEntity.TryGetComponent(_compType, out T fromComponent))
            {
                if (fromComponent is ICompClone<T>)
                {
                    var toComponent = _toEntity.GetOrAddComponent<T>(_compType);
                    ((ICompClone<T>)toComponent).Clone(fromComponent);
                }
                else
                {
                    BattleLog.Error("该组件不能克隆:" + _compType);
                }
            }
        }
    }
}