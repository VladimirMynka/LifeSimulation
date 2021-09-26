using System;
using System.Collections;
using System.Collections.Generic;
using LifeSimulation.myCs.WorldObjects.Plants;

namespace LifeSimulation.myCs.WorldObjects.Animals
{
    public class Animal : WorldObject
    {
        private int _health;
        private int _maxHealth;
        private int _satiety;
        private int _maxSatiety;
        private int[] _mainTarget;
        private readonly int _visibleArea;
        private Stack<int[]> _targets;
        
        
        public Animal(Cell keeper, 
            int newColor = Colors.Animal1Const,
            int maxHealth = Defaults.AnimalHealth,
            int maxSatiety = Defaults.AnimalSatiety,
            int visibleArea = Defaults.AnimalVisibleArea) : base(keeper, newColor)
        {
            _health = maxHealth;
            _maxHealth = maxHealth;
            _satiety = maxSatiety;
            _maxSatiety = maxSatiety;
            _visibleArea = visibleArea;
            
            GoTo(keeper);
        }

        public override void Update()
        {
            AddSatiety(-3);
            if (!IsHungry())
            {
                Plant();
                AddHealth(3);
            }
            else AddHealth(-3);

            if (CantLive())
            {
                Die();
                return;
            }
            if (!NeedsFood())
            {
                Walk();
                return;
            }
            if (_targets == null || _targets.Count == 0)
            {
                Walk();
                SearchMeal();
            }
            else GoToTarget();
        }

        private bool IsHungry()
        {
            return (_satiety <= _maxSatiety / 3);
        }

        private bool NeedsFood()
        {
            return (_health < _maxHealth);
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
            if (_satiety < 0) _satiety = 0;
        }

        private void Walk()
        {
            var directionVector = Direction.GetRandomDirectionVector();
            Step(directionVector);
        }

        private void GoToTarget()
        {
            var directionVector = Direction.GetDirectionVector(_targets.Peek());
            var thereIsBarrier = Step(directionVector);
            if (cell.Coords == _targets.Peek()) _targets.Pop();
            if (thereIsBarrier) FindBypass(directionVector);
        }

        private void FindBypass(int[] directionVector)
        {
            int[] normalVector = Direction.GetNormalDirection(directionVector);
            for (int i = 1; i < _visibleArea; i++)
            {
                int x = cell.Coords[0] + i * normalVector[0] + directionVector[0];
                int y = cell.Coords[1] + i * normalVector[1] + directionVector[1];
                if (CheckAndPushTarget(x, y)) return;
                
                x = cell.Coords[0] - i * normalVector[0] + directionVector[0];
                y = cell.Coords[1] - i * normalVector[1] + directionVector[1];
                if (CheckAndPushTarget(x, y)) return;
            }
        }

        private bool CheckAndPushTarget(int x, int y)
        {
            var newCell = world.GetCell(x, y);
            if (newCell == null || newCell.CheckLocked()) return false;
            _targets.Push(new int[]{x, y});
            return true;

        }

        private void SearchMeal()
        {
            for (int i = cell.Coords[0] - _visibleArea; i < cell.Coords[0] + _visibleArea; i++)
            {
                if (i < 0) continue;
                if (i >= world.Length) break;
                for (int j = cell.Coords[1] - _visibleArea; j < cell.Coords[1] + _visibleArea; j++)
                {
                    if (j < 0) continue;
                    if (j >= world.Width) break;
                    var newCell = world.GetCell(i, j);
                    var plant = newCell.CurrentObjects[0] as Plant;
                    if (plant == null) continue;
                    if (!plant.CheckItsAdult()) continue;
                    
                    _mainTarget = new[]{i, j};
                    _targets = new Stack<int[]>();
                    _targets.Push(_mainTarget);
                    return;
                }
            }
        }

        private bool Step(int[] directionVector)
        {
            var x = cell.Coords[0] + directionVector[0];
            var y = cell.Coords[1] + directionVector[1];
            var newCell = world.GetCell(x, y);
            if (newCell == null) return false;
            if (newCell.CheckLocked()) return false;
            GoAway();
            GoTo(newCell);

            return true;
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
            var onThisCellPlant = cell.CurrentObjects[0] as Plant;
            
            if (onThisCellPlant == null || !onThisCellPlant.CheckItsAdult()) 
                cell.ThrowOffColor();
            else 
                cell.SetColor(cell.CurrentObjects[0].GetColor());
            
            cell.CurrentObjects[1] = null;
            cell.Unlock();
            cell = null;
        }

        private void GoTo(Cell newCell)
        {
            if (newCell == null) return;
            newCell.Lock();
            newCell.CurrentObjects[1] = this;
            newCell.SetColor(color);
            cell = newCell;
        }
    }
}