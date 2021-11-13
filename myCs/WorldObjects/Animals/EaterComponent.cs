using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.World;
using LifeSimulation.myCs.WorldObjects.Animals.Moving;
using LifeSimulation.myCs.WorldObjects.Eatable;

namespace LifeSimulation.myCs.WorldObjects.Animals
{
    public class EaterComponent : WorldObjectComponent
    {
        public int Satiety;
        public readonly int MaxSatiety;
        public MealType MealType;
        private HealthComponent _health;

        private EatableComponent _mealTarget;
        private CreatureType _creatureType;
        private MovingComponent _moving;

        private int _visibility;
        private Cell _cell;

        public EaterComponent(WorldObject owner, MealType mealType, int satiety) : base(owner)
        {
            MealType = mealType;
            Satiety = satiety;
            MaxSatiety = satiety;
        }

        public override void Start()
        {
            _health = GetComponent<HealthComponent>();
            _moving = GetComponent<MovingComponent>();
            _creatureType = GetComponent<EatableComponent>().CreatureType;
            _cell = WorldObject.Cell;
            _visibility = Defaults.AnimalVisibleArea;
        }

        public override void Update()
        {
            AddSatiety(-Defaults.AnimalSatietyDestruction);
            ChangeHealth();
            if (!IsHungry()) return;
            EatSmth();
            if (_mealTarget == null) SearchMeal();
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

        private void SearchMeal()
        {
            var x = _cell.Coords[0];
            var y = _cell.Coords[1];
            for (var i = 0; i < _visibility; i++)
            {
                for (var j = 0; j < _visibility; j++)
                {
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

        private void SetMeal(EatableComponent meal)
        {
            _mealTarget = meal;
            bool isPlant = (meal.MealType == MealType.Plant);
            _moving.SetTarget(meal.WorldObject, isPlant);
        }

        private bool CheckIEatIt(EatableComponent meal)
        {
            if (meal == null) 
                return false; 
            if (MealType != MealType.AllTypes && meal.MealType != MealType) 
                return false;
            return _creatureType != meal.CreatureType;
        }

        private EatableComponent GetMeal()       
        {
            var cell = WorldObject.Cell;
            foreach (var inCellObject in cell.CurrentObjects)
            {
                var meal = inCellObject.GetComponent<EatableComponent>();
                if (!CheckIEatIt(meal))
                    return meal;
            }
            return null;
        }

        public string GetInformation()
        {
            var info = "Type: " + _creatureType + '\n';
            info += "Meal type: " + MealType + '\n';
            info += "Satiety: " + Satiety + '/' + MaxSatiety + '\n';
            info += "Wants eat: ";
            if (_mealTarget == null || 
                _mealTarget.WorldObject == null || 
                _mealTarget.WorldObject.Cell == null)
                info += "none";
            else
                info += _mealTarget.CreatureType + " on " +
                        _mealTarget.WorldObject.Cell.Coords[0] + ',' +
                        _mealTarget.WorldObject.Cell.Coords[1];
            return info;
        }
    }
}