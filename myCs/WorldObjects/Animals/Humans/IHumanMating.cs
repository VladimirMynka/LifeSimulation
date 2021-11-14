namespace LifeSimulation.myCs.WorldObjects.Animals.Humans
{
    public interface IHumanMating
    {
        public int GetPriority();
        public WorldObject GetTarget();
    }
}