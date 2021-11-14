using LifeSimulation.myCs.WorldObjects.Animals.Mating;

namespace LifeSimulation.myCs.WorldObjects.Animals.Humans
{
    public class ManComponent : MaleMatingComponent, IHumanMating
    {
        
        public ManComponent(WorldObject owner) : base(owner)
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

        protected override FemaleMatingComponent GetComponentFrom(WorldObject worldObject)
        {
            return worldObject is Human 
                ? worldObject.GetComponent<WomanComponent>()
                : null;
        }
    }
}