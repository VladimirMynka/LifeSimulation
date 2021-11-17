namespace LifeSimulation.myCs.WorldObjects.Animals.Animals
{
    public class PetComponent: WorldObjectComponent, IHaveInformation
    {
        public PetComponent(WorldObject owner) : base(owner)
        {
        }

        public string GetInformation()
        {
            throw new System.NotImplementedException();
        }
    }
}