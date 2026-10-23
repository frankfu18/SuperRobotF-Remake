using System.Collections;
using System.Collections.Generic;

namespace FrankGame.Battle
{
    public class ComponentPool : RefKeyObjectPool<ComponentBase, Entity>
    {
        protected override ComponentBase OnCreateObject(int _key, Entity _entity)
        {
            return ComponentFactory.Create(_entity, (ECompType)_key);
        }

        protected override void OnInitObject(ComponentBase _com, Entity _entity)
        {
            _com.Init(_entity);
            _com.Create();
        }
    }
}
