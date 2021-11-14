using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.World;
using LifeSimulation.myCs.WorldObjects.Animals.Moving;
using LifeSimulation.myCs.WorldObjects.Eatable;

namespace LifeSimulation.myCs.WorldObjects.Animals
{
    public abstract class EaterComponent : WorldObjectComponent
    {
        public int Satiety;
        public readonly int MaxSatiety;
        public MealType MealType;
        private HealthComponent _health;

        protected EatableComponent mealTarget;
        private CreatureType _creatureType;

        private int _visibility;
        private Cell _cell;

        protected EaterComponent(WorldObject owner, MealType mealType, int satiety) : base(owner)
        {
            MealType = mealType;
            Satiety = satiety;
            MaxSatiety = satiety;
        }

        public override void Start()
        {
            _health = GetComponent<HealthComponent>();
            _creatureType = GetComponent<EatableComponent>().CreatureType;
            _cell = WorldObject.Cell;
            _visibility = Defaults.AnimalVisibleArea;
        }

        public override void Update()
        {
            AddSatiety(-Defaults.AnimalSatietyDestruction);
            ChangeHealth();

            if (!IsHungry())
            {
                mealTarget = null;
                return;
            }

            EatSomething();
            
            if (IsHungry() && mealTarget == null) 
                SearchMeal();
            
            if (mealTarget != null &&
                (mealTarget.WorldObject == null || mealTarget.WorldObject.Cell == null))
                mealTarget = null;
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
            if (IsVeryHungry())
                _health.AddHealth(-Defaults.AnimalHealthDestruction);
            else
                _health.AddHealth(Defaults.AnimalHealthRegeneration);
        }
        
        public bool IsHungry()
        {
            return (Satiety <= 2 * MaxSatiety / 3);
        }

        public bool IsVeryHungry()
        {
            return (Satiety <= 0);
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
            var x = _cell.Coords[0];
            var y = _cell.Coords[1];
            for (var radius = 0; radius < _visibility; radius++)
            {
                for (var j = 0; j <= radius; j++)
                {
                    var i = radius - j;
                    var currentCell = world.GetCell(x + i, y + j);
                    if (TakeMealFrom(currentCell)) 
                        return;
                    if (j != 0)
                    {
                        currentCell = world.GetCell(x + i, y - j);
                        if (TakeMealFrom(currentCell))
                            return;
                    }
                    if (i == 0) continue;
                    currentCell = world.GetCell(x - i, y + j);
                    if (TakeMealFrom(currentCell)) 
                        return;
                    if (j == 0) continue;
                    currentCell = world.GetCell(x - i, y - j);
                    if (TakeMealFrom(currentCell))
                        return;
                }
            }
        }

        private bool TakeMealFrom(Cell checkingCell)
        {
            if (checkingCell == null) return false;
            foreach (var wo in checkingCell.CurrentObjects)
            {
                var meal = wo.GetComponent<EatableComponent>();
                if (meal == null) continue;
                if (!CheckIEatIt(meal)) continue;
                SetMeal(meal);
                
                return true;
            }
            return false;
        }

        protected virtual void SetMeal(EatableComponent meal)
        {
            mealTarget = meal;
        }

        protected virtual bool CheckIEatIt(EatableComponent meal)
        {
            if (meal == null || meal.WorldObject == null || meal.WorldObject.Cell == null)
            {
                meal = null;
                return false;
            }

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
            info += "Satiety: " + Satiety + '/' + MaxSatiety + '\n';
            info += "Wants eat: ";
            if (mealTarget == null || 
                mealTarget.WorldObject == null || 
                mealTarget.WorldObject.Cell == null)
                info += "none";
            else
                info += mealTarget.CreatureType + " on " +
                        mealTarget.WorldObject.Cell.Coords[0] + ',' +
                        mealTarget.WorldObject.Cell.Coords[1];
            return info;
        }
    }
}