using LifeSimulation.myCs.WorldObjects.CommonComponents.Eatable;

namespace LifeSimulation.myCs.Resources.EatableResources
{
    public class RotMeatResource : EatableResource
    {
        public RotMeatResource(int count) : base(count, MealType.Plant)
        { }

        public RotMeatResource() : base(0, MealType.Plant)
        { }
    }
}