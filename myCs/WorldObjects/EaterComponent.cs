using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.World;

namespace LifeSimulation.myCs.WorldObjects
{
    public class EaterComponent : WorldObjectComponent
    {
        public int Satiety;
        public readonly int MaxSatiety;
        public MealType MealType;
        private HealthComponent _health;

        public EaterComponent(WorldObject owner, MealType mealType, int satiety) : base(owner)
        {
            MealType = mealType;
            Satiety = satiety;
            MaxSatiety = satiety;
        }

        public override void Start()
        {
            _health = GetComponent<HealthComponent>();
        }

        public override void Update()
        {
            AddSatiety(-Defaults.AnimalSatietyDestruction);
            ChangeHealth();
            if (IsHungry()) EatSmth();
        }

        public void AddSatiety(int delta)
        {
            Satiety += delta;
            if (Satiety > MaxSatiety) Satiety = MaxSatiety;
            if (Satiety < 0) Satiety = 0;
        }

        private void ChangeHealth()
        {
            if (_health == null) return;
            if (IsHungry())
                _health.AddHealth(-Defaults.AnimalHealthDestruction);
            else
                _health.AddHealth(Defaults.AnimalHealthRegeneration);
        }
        
        public bool IsHungry()
        {
            return (Satiety <= MaxSatiety / 3);
        }

        private void EatSmth()
        {
            Eat(GetMeal());
        }

        private void Eat(EatableComponent meal)
        {
            if (CheckIEatIt(meal))
                meal.BeEatenBy(this);
        }

        private bool CheckIEatIt(EatableComponent meal)
        {
            return (meal != null && (MealType == MealType.AllTypes || meal.MealType == MealType));
        }

        private EatableComponent GetMeal()
        {
            var cell = worldObject.Cell;
            foreach (var inCellObject in cell.CurrentObjects)
            {
                var meal = inCellObject.GetComponent<EatableComponent>();
                if (!CheckIEatIt(meal))
                    return meal;
            }
            return null;
        }
    }
}