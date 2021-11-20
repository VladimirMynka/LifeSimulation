using LifeSimulation.myCs.WorldObjects.Eatable;

namespace LifeSimulation.myCs.WorldObjects.Animals.Humans
{
    public class HumanEaterComponent : EaterComponent
    {
        private InventoryComponent _inventory;
        public HumanEaterComponent(WorldObject owner, MealType mealType, int satiety, int destruction) 
            : base(owner, mealType, satiety, destruction)
        {
        }

        public override void Start()
        {
            base.Start();
            _inventory = GetComponent<InventoryComponent>();
        }

        protected override void EatSomething()
        {
            AddSatiety(_inventory.Remove(maxSatiety - satiety));
            if (_inventory.IsFilled())
                return;
            CollectFrom(GetMeal());
        }

        private void CollectFrom(EatableComponent meal)
        {
            if (!CheckIEatIt(meal)) 
                return;
            _inventory.Add(meal.NutritionalValue / 2, meal.MealType);
            meal.WorldObject.Destroy();
        }

        protected override bool CheckIEatIt(EatableComponent meal)
        {
            if (CheckWereDestroyed(meal))
                return false;
            if (meal.CreatureType == CreatureType.Human)
                return false;
            return !meal.IsPoisonous();
        }

        public int GetPriority()
        {
            return IsVeryHungry() ? 10 : IsHungry() ? 5 : 0;
        }

        public WorldObject GetTarget()
        {
            return mealTarget != null 
                ? mealTarget.WorldObject 
                : null;
        }
    }
}