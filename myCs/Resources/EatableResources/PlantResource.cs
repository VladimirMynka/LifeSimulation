using LifeSimulation.myCs.WorldObjects.CommonComponents.Eatable;

namespace LifeSimulation.myCs.Resources.EatableResources
{
    public class PlantResource : EatableResource
    {
        public PlantResource(int count) : base(count, MealType.Plant)
        { }

        public PlantResource() : base(0, MealType.Plant)
        { }
    }
}