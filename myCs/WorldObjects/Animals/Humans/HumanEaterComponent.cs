using LifeSimulation.myCs.WorldObjects.Eatable;

namespace LifeSimulation.myCs.WorldObjects.Animals.Humans
{
    public class HumanEaterComponent : EaterComponent
    {
        private InventoryComponent _inventory;
        public HumanEaterComponent(WorldObject owner, MealType mealType, int satiety) 
            : base(owner, mealType, satiety)
        {
        }

        public override void Start()
        {
            base.Start();
            _inventory = GetComponent<InventoryComponent>();
        }

        protected override void EatSomething()
        {
            AddSatiety(_inventory.Remove(MaxSatiety - Satiety));
            if (_inventory.IsFilled())
                return;
            CollectFrom(GetMeal());
        }

        private void CollectFrom(EatableComponent meal)
        {
            if (CheckIEatIt(meal))
                _inventory.Add(meal.NutritionalValue / 2);
        }

        protected override bool CheckIEatIt(EatableComponent meal)
        {
            if (meal == null || meal.WorldObject == null || meal.WorldObject.Cell == null)
                return false;
            if (meal.CreatureType == CreatureType.Human)
                return false;
            if (meal.IsPoisonous())
                return false;
            return true;
        }

        public int GetPriority()
        {
            return IsVeryHungry() ? 10 : IsHungry() ? 5 : 0;
        }

        public WorldObject GetTarget()
        {
            return mealTarget?.WorldObject;
        }
    }
}