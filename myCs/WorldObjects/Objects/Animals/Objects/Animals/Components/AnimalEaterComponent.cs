using LifeSimulation.myCs.WorldObjects.CommonComponents.Eatable;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents;
using LifeSimulation.myCs.WorldObjects.Objects.Animals.CommonComponents.Moving;

namespace LifeSimulation.myCs.WorldObjects.Objects.Animals.Objects.Animals.Components
{
    public class AnimalEaterComponent : EaterComponent
    {
        private MovingComponent _moving;

        public AnimalEaterComponent(WorldObject owner, MealType mealType, int satiety, int destruction) 
            : base(owner, mealType, satiety, destruction)
        {
        }

        public override void Start()
        {
            base.Start();
            _moving = GetComponent<MovingComponent>();
        }

        public override void Update()
        {
            base.Update();

            if (IsHungry() && mealTarget == null) 
                SearchMeal();
            
            if (CheckWereDestroyed(mealTarget))
                mealTarget = null;
        }
    }
}