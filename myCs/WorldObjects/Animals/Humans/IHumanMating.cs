namespace LifeSimulation.myCs.WorldObjects.Animals.Humans
{
    public interface IHumanMating
    {
        int GetPriority();
        WorldObject GetTarget();
        bool IsHungry();
        bool IsVeryHungry();
    }
}