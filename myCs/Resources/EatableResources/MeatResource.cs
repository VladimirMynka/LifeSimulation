using LifeSimulation.myCs.WorldObjects.CommonComponents.Eatable;

namespace LifeSimulation.myCs.Resources.EatableResources
{
    public class MeatResource : EatableResource
    {
        public MeatResource(int count) : base(count, MealType.FreshMeat)
        { }

        public MeatResource() : base(0, MealType.FreshMeat)
        { }

        public override Resource Clone()
        {
            return new MeatResource(GetCount());
        }
    }
}