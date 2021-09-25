using System.IO;
using LifeSimulation.myCs.WorldObjects.Plants;

namespace LifeSimulation.myCs.WorldObjects
{
    public class Animal : WorldObject
    {
        private int _health;
        private int _maxHealth;
        private int _satiety;
        private int _maxSatiety;
        private int[] mainTarget;
        private int[] target;
        
        
        public Animal(Cell keeper, 
            int newColor = 0,
            int maxHealth = Defaults.AnimalHealth,
            int maxSatiety = Defaults.AnimalSatiety) : base(keeper, newColor)
        {
            
        }

        public override void Update()
        {
            if (!IsHungry())
            {
                Plant();
                AddHealth(1);
            }
            else AddHealth(-1);
            
            if (CantLive()) Die();
            if (NeedsFood())
            {
                if (target == null)
                {
                    Walk();
                    SearchMeal();
                }
                else GoToTarget();
            }
            
            
        }

        private bool IsHungry()
        {
            return (_satiety <= _maxSatiety / 3);
        }

        private bool CantLive()
        {
            return (_health <= 0);
        }

        private void AddHealth(int delta)
        {
            _health += delta;
            if (_health > _maxHealth) _health = _maxHealth;
        }

        private void AddSatiety(int delta)
        {
            _satiety += delta;
            if (_satiety > _maxSatiety) _satiety = _maxSatiety;
        }

        private void Walk()
        {
            var directionVector = Direction.GetRandomDirectionVector();
            Step(directionVector);
        }

        private void GoToTarget()
        {
            var directionVector = Direction.GetDirectionVector(target);
            Step(target);
        }

        private void SearchMeal()
        {
            for (int i = cell.Coords[0] - 50; i < cell.Coords[0] + 50; i++)
            {
                if (i < 0) continue;
                if (i > world.Lenght) break;
                for (int j = cell.Coords[1] - 50; j < cell.Coords[1] + 50; j++)
                {
                    if (j < 0) continue;
                    if (j > world.Width) break;
                    var newCell = world.GetCell(i, j);
                    var plant = newCell.CurrentObjects[0] as Plant;
                    if (plant == null) continue;
                    if (!plant.CheckItsAdult()) continue;
                    mainTarget = new[]{i, j};
                    return;
                }
            }
        }

        private void Step(int[] directionVector)
        {
            var x = cell.Coords[0] + directionVector[0];
            var y = cell.Coords[1] + directionVector[1];
            var newCell = world.GetCell(x, y);
            if (newCell == null) return;
            if (newCell.CheckLocked()) return;
            else
            {
                GoAway();
                GoTo(newCell);
            }
        }

        private void Eat(Plant meal)
        {
            if (meal == null) return;
            if (!meal.CheckItsAdult()) return;
            AddSatiety(meal.NutritionalValue);
            meal.Die();
        }

        private void Plant()
        {
            if (cell.CurrentObjects[0] != null) return;
            var chance = _satiety / 2;
            var random = World.Random.Next(0, _maxSatiety);
            if (random < chance)
                new Plant(cell);
        }

        private void Die()
        {
            GoAway();
        }

        private void GoAway()
        {
            if (cell.CurrentObjects[0] == null) cell.ThrowOffColor();
            else cell.SetColor(cell.CurrentObjects[0].color);
            cell.CurrentObjects[1] = null;
            cell.Unlock();
            cell = null;
        }

        private void GoTo(Cell newCell)
        {
            newCell.Lock();
            newCell.CurrentObjects[1] = this;
            newCell.SetColor(color);
            cell = newCell;
        }
    }
}