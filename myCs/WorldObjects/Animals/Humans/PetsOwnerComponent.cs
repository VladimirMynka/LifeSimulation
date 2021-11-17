namespace LifeSimulation.myCs.WorldObjects.Animals.Humans
{
    public class PetsOwnerComponent : WorldObjectComponent, IHaveInformation
    {
        public PetsOwnerComponent(WorldObject owner) : base(owner)
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

        public string GetInformation()
        {
            throw new System.NotImplementedException();
        }
    }
}