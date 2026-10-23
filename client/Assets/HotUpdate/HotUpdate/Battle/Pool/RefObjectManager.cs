using System.Collections.Generic;

namespace FrankGame.Battle
{
    public class RefObjectManager : Singlet<RefObjectManager>
    {
        public List<IRefObjectPool> RefObjectPools => new List<IRefObjectPool>(this.refObjectPools);

        private List<IRefObjectPool> refObjectPools = new List<IRefObjectPool>();

        public void AddPool(IRefObjectPool _pool)
        {
            this.refObjectPools.Add(_pool);
        }

        public void RemovePool(IRefObjectPool _pool)
        {
            this.refObjectPools.Remove(_pool);
        }

        public void Clear()
        {
            List<IRefObjectPool> temp = new List<IRefObjectPool>(this.refObjectPools);

            foreach (var pool in temp)
            {
                pool.Clear();
            }
        }
    }
}