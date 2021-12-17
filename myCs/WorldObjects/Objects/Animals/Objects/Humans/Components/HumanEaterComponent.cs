using System.Collections.Generic;
using LifeSimulation.myCs.Resources;
using LifeSimulation.myCs.Resources.EatableResources;
using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.WorldObjects.CommonComponents.Eatable;
using LifeSimulation.myCs.WorldObjects.CommonComponents.Resources;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Animals.Components;

namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Humans.Components
{
    public class HumanEaterComponent : EaterComponent
    {
        private InventoryComponent<Resource> _inventory;
        public readonly List<MealType> CollectingTypes;
        public HumanEaterComponent(WorldObject owner, MealType mealType, int satiety, int destruction) 
            : base(owner, mealType, satiety, destruction)
        {
            CollectingTypes = new List<MealType>(){MealType.Plant, MealType.DeadMeat, MealType.FreshMeat};
        }

        public override void Start()
        {
            base.Start();
            _inventory = GetComponent<InventoryComponent<Resource>>();
        }

        public override void Update()
        {
            base.Update();
            
            if (_inventory.IsFilled()) 
                return;
            
            CollectFrom(GetMeal());
            if (CheckWereDestroyed(mealTarget))
                SearchMeal();
        }

        protected override void EatSomething()
        {
            AddSatiety(_inventory.Remove<EatableResource>(maxSatiety - satiety));
        }

        private void CollectFrom(EatableComponent meal)
        {
            if (!CheckIEatIt(meal)) 
                return;
            _inventory.Add(meal.GetResource());
        }

        protected override bool CheckIEatIt(EatableComponent meal)
        {
            return base.CheckIEatIt(meal) 
                   && !meal.IsPoisonous() 
                   && CollectingTypes.Contains(meal.MealType) 
                   && HasNotOwner(meal);
        }

        private static bool HasNotOwner(WorldObjectComponent meal)
        {
            if (CheckWereDestroyed(meal))
                return true;
            var petComponent = meal.GetComponent<PetComponent>();
            if (CheckWereDestroyed(petComponent))
                return true;
            return petComponent.GetOwner() == null;
        }

        public int PriorityWhenVeryHungry = Defaults.BehaviourVeryHungry;
        public int PriorityWhenHungry = Defaults.BehaviourHungry;
        public int PrioritySearching = Defaults.BehaviourSearching;
        
        public override int GetPriorityInBehaviour()
        {
            return CheckWereDestroyed(mealTarget) ? Defaults.BehaviourHaveNotPriority 
                : IsVeryHungry() ? PriorityWhenVeryHungry
                : IsHungry() ? PriorityWhenHungry
                : !_inventory.IsFilled() ? PrioritySearching
                : Defaults.BehaviourHaveNotPriority;
        }
    }
}