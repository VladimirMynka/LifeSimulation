using LifeSimulation.myCs.WorldObjects.CommonComponents.Eatable;

namespace LifeSimulation.myCs.Resources.EatableResources
{
    public class UntypedMealResource : EatableResource
    {
        public UntypedMealResource(int count = 0) : base(count, MealType.AllTypes)
        { }

    }
}