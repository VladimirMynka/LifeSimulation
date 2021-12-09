namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents.Behaviour
{
    public interface IHaveTarget
    {
        int GetPriorityInBehaviour();
        WorldObject GetTarget();
    }
}