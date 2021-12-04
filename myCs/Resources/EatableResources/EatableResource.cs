using LifeSimulation.myCs.WorldObjects.CommonComponents.Eatable;

namespace LifeSimulation.myCs.Resources.EatableResources
{
    public abstract class EatableResource : Resource
    {
        private readonly MealType _mealType;

        protected EatableResource(int count = 0, MealType mealType = MealType.AllTypes) : base(count)
        {
            _mealType = mealType;
        }

        public MealType GetMealType()
        {
            return _mealType;
        }

        public static EatableResource CreateResource(MealType mealType, int count)
        {
            switch (mealType)
            {
                case MealType.FreshMeat:
                    return new MeatResource(count);
                case MealType.DeadMeat:
                    return new RotMeatResource(count);
                case MealType.Plant:
                    return new PlantResource(count);
                default:
                    return new UntypedMealResource(count);
            }
        }
    }
}