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
            bool isMale = true,
            int maxHealth = Defaults.AnimalHealth,
            int maxSatiety = Defaults.AnimalSatiety
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
    }
}