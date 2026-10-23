namespace FrankGame.Battle
{
    public interface IBuffExecute
    {
        void Execute(BuffTriggerContext context);
        void Reset();
        void Stack(int addCount);
        void Reapply();
        void Destroy();
    }
}
