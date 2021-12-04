using LifeSimulation.myCs.WorldObjects.CommonComponents.Eatable;

namespace LifeSimulation.myCs.Resources.EatableResources
{
    public class PlantResource : EatableResource
    {
        public PlantResource(int count = 0) : base(count, MealType.Plant)
        { }
    }
}