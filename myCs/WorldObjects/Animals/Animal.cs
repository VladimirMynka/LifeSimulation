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
        private AbstractPlant _mainTarget;
        private readonly int _visibleArea;
        private Stack<int[]> _targets;
        
        
        public Animal(
            Cell keeper, 
            int newColor = Colors.Animal1Const,
            int maxHealth = Defaults.AnimalHealth,
            int maxSatiety = Defaults.AnimalSatiety,
            int visibleArea = Defaults.AnimalVisibleArea
            ) : base(keeper, newColor)
        {
            _health = maxHealth;
            _maxHealth = maxHealth;
            _satiety = maxSatiety;
            _maxSatiety = maxSatiety;
            _visibleArea = visibleArea;
            
            GoToCell(keeper);
        }

        public override void Update()
        {
            base.Update();
            AddSatiety(Defaults.AnimalSatietyDestruction);
            if (!IsHungry())
            {
                Plant();
                AddHealth(Defaults.AnimalHealthRegeneration);
            }
            else AddHealth(Defaults.AnimalHealthDestruction);

            if (CantLive())
            {
                Die();
                return;
            }
            if (!NeedsFood())
            {
                Walk();
                _targets = null;
                return;
            }
            Eat(cell.CurrentObjects[0] as AbstractPlant);
            if (_targets == null || _targets.Count == 0 || _mainTarget == null)
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
            var target = _targets.Peek();
            var directionVector = Direction.GetDirectionVector(new[]{
                target[0] - cell.Coords[0],
                target[1] - cell.Coords[1]
            });
            var thereIsBarrier = Step(directionVector);
            if (cell.Coords[0] == _targets.Peek()[0] &&
                cell.Coords[1] == _targets.Peek()[1]) _targets.Pop();
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
            for (int i = 0; i < _visibleArea; i++)
            {
                for (int j = 0; j < _visibleArea; j++)
                {
                    if (CheckAndAddPlant(cell.Coords[0] - i, cell.Coords[1] - j) ||
                        CheckAndAddPlant(cell.Coords[0] + i, cell.Coords[1] - j) ||
                        CheckAndAddPlant(cell.Coords[0] - i, cell.Coords[1] + j) ||
                        CheckAndAddPlant(cell.Coords[0] + i, cell.Coords[1] + j)
                    ) return;
                }
            }
        }

        private bool CheckAndAddPlant(int x, int y)
        {
            if (x < 0 || y < 0 || x >= world.Width || y >= world.Length) return false;
            var newCell = world.GetCell(x, y);
            var plant = newCell.CurrentObjects[0] as AbstractPlant;
            if (plant == null || !plant.IsEatable()) return false;
            UpdateMainTarget(plant, x, y);
            return true;
        }

        private void UpdateMainTarget(AbstractPlant plant, int i, int j)
        {
            _mainTarget = plant;
            _targets = new Stack<int[]>();
            _targets.Push(new[]{i, j});
        }

        private bool Step(int[] directionVector)
        {
            var x = cell.Coords[0] + directionVector[0];
            var y = cell.Coords[1] + directionVector[1];
            var newCell = world.GetCell(x, y);
            if (newCell == null) return false;
            if (newCell.CheckLocked()) return false;
            GoAway();
            GoToCell(newCell);

            return true;
        }

        private void Eat(AbstractPlant meal)
        {
            if (meal == null) return;
            if (!meal.IsEatable()) return;
            switch (meal.Effect)
            {
                case Effect.None:
                    AddSatiety(meal.NutritionalValue);
                    break;
                case Effect.Heart:
                    AddHealth(-meal.NutritionalValue);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            meal.Die();
        }

        private void Plant()
        {
            if (cell.CurrentObjects[0] != null) return;
            var chance = _satiety * Defaults.AnimalPlantProbability / 100;
            var random = World.Random.Next(0, _maxSatiety);
            if (random < chance)
            {
                world.AddPlant(cell);
            }
        }

        private void Die()
        {
            GoAway();
        }

        private void GoAway()
        {
            var plantOnThisCell = cell.CurrentObjects[0] as Plant;
            
            if (plantOnThisCell == null || !plantOnThisCell.CheckItsAdult()) 
                cell.ThrowOffColor();
            else 
                cell.SetColor(cell.CurrentObjects[0].GetColor());
            
            cell.CurrentObjects[1] = null;
            cell.Unlock();
            cell = null;
        }

        private void GoToCell(Cell newCell)
        {
            if (newCell == null) return;
            newCell.Lock();
            newCell.CurrentObjects[1] = this;
            newCell.SetColor(color);
            cell = newCell;
        }
    }
}