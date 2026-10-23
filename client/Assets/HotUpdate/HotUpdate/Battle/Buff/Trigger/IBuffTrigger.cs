namespace FrankGame.Battle
{
    public interface IBuffTrigger
    {
        bool CheckTrigger(EBuffTriggerEvent eventType, BuffTriggerContext context);
        bool Update(float deltaTime, BuffTriggerContext context);
    }
}
