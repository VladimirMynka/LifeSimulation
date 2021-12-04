using LifeSimulation.myCs.WorldObjects.CommonComponents.Eatable;

namespace LifeSimulation.myCs.Resources.EatableResources
{
    public class MeatResource : EatableResource
    {
        public MeatResource(int count = 0) : base(count, MealType.FreshMeat)
        { }
    }
}