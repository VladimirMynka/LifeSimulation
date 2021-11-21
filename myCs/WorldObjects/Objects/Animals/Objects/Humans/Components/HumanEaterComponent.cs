using LifeSimulation.myCs.WorldObjects.CommonComponents;
using LifeSimulation.myCs.WorldObjects.CommonComponents.Eatable;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents;

namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Humans.Components
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
    }
}