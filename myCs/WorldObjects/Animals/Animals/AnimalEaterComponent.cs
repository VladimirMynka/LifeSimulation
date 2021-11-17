using LifeSimulation.myCs.WorldObjects.Animals.Moving;
using LifeSimulation.myCs.WorldObjects.Eatable;

namespace LifeSimulation.myCs.WorldObjects.Animals.Animals
{
    public class AnimalEaterComponent : EaterComponent
    {
        private MovingComponent _moving;

        public AnimalEaterComponent(WorldObject owner, MealType mealType, int satiety) : base(owner, mealType, satiety)
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
            if (!CheckWereDestroyed(mealTarget))
                _moving.SetTarget(mealTarget.WorldObject);
        }
    }
}