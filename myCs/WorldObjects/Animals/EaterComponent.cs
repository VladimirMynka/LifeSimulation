using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.World;
using LifeSimulation.myCs.World.Weather;
using LifeSimulation.myCs.WorldObjects.Eatable;

namespace LifeSimulation.myCs.WorldObjects.Animals
{
    public abstract class EaterComponent : WorldObjectComponent, IHaveInformation, IDependingOnWeather
    {
        protected int satiety;
        protected readonly int maxSatiety;
        private int _destruction;
        private readonly int _normalDestruction;
        public MealType MealType;
        private HealthComponent _health;

        protected EatableComponent mealTarget;
        private CreatureType _creatureType;

        private VisibilityComponent _visibilityComponent;
        private Cell _cell;

        protected EaterComponent(
            WorldObject owner, 
            MealType mealType, 
            int satiety,
            int destruction) 
            : base(owner)
        {
            MealType = mealType;
            this.satiety = satiety;
            maxSatiety = satiety;
            _destruction = destruction;
            _normalDestruction = destruction;
        }

        public override void Start()
        {
            _health = GetComponent<HealthComponent>();
            _visibilityComponent = GetComponent<VisibilityComponent>();
            _creatureType = GetComponent<EatableComponent>().CreatureType;
            _cell = WorldObject.Cell;
        }

        public override void Update()
        {
            AddSatiety(-_destruction);
            ChangeHealth();

            if (!IsHungry())
            {
                mealTarget = null;
                return;
            }

            EatSomething();
            
            if (IsHungry() && mealTarget == null) 
                SearchMeal();
            
            if (CheckWereDestroyed(mealTarget))
                mealTarget = null;
        }

        public void AddSatiety(int delta)
        {
            satiety += delta;
            if (satiety > maxSatiety) 
                satiety = maxSatiety;
            if (satiety < 0) 
                satiety = 0;
        }

        private void ChangeHealth()
        {
            if (_health == null) return;
            if (IsVeryHungry())
                _health.AddHealth(-Defaults.AnimalHealthDestruction);
        }
        
        public bool IsHungry()
        {
            return (satiety <= 2 * maxSatiety / 3);
        }

        public bool IsVeryHungry()
        {
            return (satiety <= 0);
        }

        protected virtual void EatSomething()
        {
            Eat(GetMeal());
        }

        private void Eat(EatableComponent meal)
        {
            if (CheckIEatIt(meal))
                meal.BeEatenBy(this);
        }

        protected void SearchMeal()
        {
            SetMeal(_visibilityComponent.Search<EatableComponent>(CheckIEatIt));
        }

        protected virtual void SetMeal(EatableComponent meal)
        {
            mealTarget = meal;
        }

        protected virtual bool CheckIEatIt(EatableComponent meal)
        {
            if (CheckWereDestroyed(meal))
                return false;

            if (MealType != MealType.AllTypes && meal.MealType != MealType) 
                return false;
            return _creatureType != meal.CreatureType;
        }

        protected virtual EatableComponent GetMeal()       
        {
            var cell = WorldObject.Cell;
            foreach (var inCellObject in cell.CurrentObjects)
            {
                var meal = inCellObject.GetComponent<EatableComponent>();
                if (CheckIEatIt(meal))
                    return meal;
            }
            return null;
        }

        public virtual string GetInformation()
        {
            var info = "Type: " + _creatureType + '\n';
            info += "Meal type: " + MealType + '\n';
            info += "Satiety: " + satiety + '/' + maxSatiety + '\n';
            info += "Wants eat: ";
            if (CheckWereDestroyed(mealTarget))
                info += "none";
            else
                info += mealTarget.CreatureType + " on " + InformationComponent.GetInfoAboutCoords(mealTarget);
            return info;
        }

        public void ConfigureByWeather(Weather weather)
        {
            var t = weather.GetTemperature();
            if (t <= -30)
                _destruction = 2 * _normalDestruction;
            else if (t >= 30)
                _destruction = _normalDestruction / 2;
            else
                _destruction = _normalDestruction;
        }


        public int GetSatiety()
        {
            return satiety;
        }

        public int GetMaxSatiety()
        {
            return maxSatiety;
        }
    }
}