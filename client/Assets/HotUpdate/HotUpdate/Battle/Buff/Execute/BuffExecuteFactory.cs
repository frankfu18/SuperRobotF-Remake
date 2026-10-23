using editor.cfg.skill;

namespace FrankGame.Battle
{
    public static class BuffExecuteFactory
    {
        public static IBuffExecute Create(BuffEffect effect)
        {
            BuffEffectContent content = effect.BuffConfig.effectContent;
            switch (content.GetTypeId())
            {
                case AddBuff.TYPE_ID: return Create<BuffExecuteAddBuff, AddBuff>(content, effect);
                case ImmuneCurrentDamage.TYPE_ID: return Create<BuffExecuteImmuneCurrentDamage, ImmuneCurrentDamage>(content, effect);
                default: throw new System.Exception("未找到 BuffEffectContent：" + content.GetType().Name);
            }
        }

        public static void Remove(BuffEffect effect, IBuffExecute execute)
        {
            switch (effect.BuffConfig.effectContent.GetTypeId())
            {
                case AddBuff.TYPE_ID: Recycle((BuffExecuteAddBuff)execute); break;
                case ImmuneCurrentDamage.TYPE_ID: Recycle((BuffExecuteImmuneCurrentDamage)execute); break;
                default: throw new System.Exception("未找到 BuffEffectContent：" + effect.BuffConfig.effectContent.GetType().Name);
            }
        }

        private static TExecute Create<TExecute, TContent>(BuffEffectContent content, BuffEffect effect)
            where TExecute : BuffExecuteBase<TContent>, new()
            where TContent : BuffEffectContent
        {
            TExecute execute = RefObjectPool<TExecute>.Instance.Get();
            execute.SetData((TContent)content, effect);
            return execute;
        }

        private static void Recycle<T>(T execute) where T : class, IClear, new()
        {
            RefObjectPool<T>.Instance.Recycle(execute);
        }
    }
}
