using LifeSimulation.myCs.WorldObjects.Animals.Mating;

namespace LifeSimulation.myCs.WorldObjects.Animals.Humans
{
    public class WomanComponent : FemaleMatingComponent, IHumanMating
    {
        public WomanComponent(WorldObject owner) : base(owner)
        {
        }

        public int GetPriority()
        {
            throw new System.NotImplementedException();
        }

        public WorldObject GetTarget()
        {
            throw new System.NotImplementedException();
        }
    }
}