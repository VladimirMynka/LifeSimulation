using System;
using System.Collections.Generic;
using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.World;
using LifeSimulation.myCs.WorldObjects.Plants;

namespace LifeSimulation.myCs.WorldObjects.Animals
{
    public class Animal : WorldObject
    {
        public Animal(
            Cell keeper,
            int color = Colors.Animal1Const,
            MealType mealType = MealType.AllTypes,
            int maxHealth = Defaults.AnimalHealth,
            int maxSatiety = Defaults.AnimalSatiety,
            bool isMale = true
        ) : base(keeper, color)
        {
            components.Add(new MovingComponent(this));
            components.Add(new HealthComponent(this, maxHealth));
            components.Add(new EaterComponent(this, mealType, maxSatiety));
            components.Add(new EatableComponent(this, MealType.FreshMeat, Effect.None));
            if (isMale)
                components.Add(new MaleMatingComponent(this));
            else 
                components.Add(new FemaleMatingComponent(this));
            Start();
        }

        /*private void GoToTarget()
        {
            var target = _targets.Peek();
            var directionVector = Direction.GetDirectionVector(new[]{
                target[0] - Cell.Coords[0],
                target[1] - Cell.Coords[1]
            });
            var thereIsBarrier = Step(directionVector);
            if (Cell.Coords[0] == _targets.Peek()[0] &&
                Cell.Coords[1] == _targets.Peek()[1]) _targets.Pop();
            if (thereIsBarrier) FindBypass(directionVector);
        }

        private void FindBypass(int[] directionVector)
        {
            int[] normalVector = Direction.GetNormalDirection(directionVector);
            for (int i = 1; i < _visibleArea; i++)
            {
                int x = Cell.Coords[0] + i * normalVector[0] + directionVector[0];
                int y = Cell.Coords[1] + i * normalVector[1] + directionVector[1];
                if (CheckAndPushTarget(x, y)) return;
                
                x = Cell.Coords[0] - i * normalVector[0] + directionVector[0];
                y = Cell.Coords[1] - i * normalVector[1] + directionVector[1];
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
                    if (CheckAndAddPlant(Cell.Coords[0] - i, Cell.Coords[1] - j) ||
                        CheckAndAddPlant(Cell.Coords[0] + i, Cell.Coords[1] - j) ||
                        CheckAndAddPlant(Cell.Coords[0] - i, Cell.Coords[1] + j) ||
                        CheckAndAddPlant(Cell.Coords[0] + i, Cell.Coords[1] + j)
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
        }*/
    }
}