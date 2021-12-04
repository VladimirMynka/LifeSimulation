using LifeSimulation.myCs.WorldObjects.CommonComponents.Eatable;

namespace LifeSimulation.myCs.Resources.EatableResources
{
    public class RotMeatResource : EatableResource
    {
        public RotMeatResource(int count = 0) : base(count, MealType.Plant)
        { }

    }
}