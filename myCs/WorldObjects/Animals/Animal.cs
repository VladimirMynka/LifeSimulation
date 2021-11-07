using System;
using System.Collections.Generic;
using System.Drawing;
using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.World;
using LifeSimulation.myCs.WorldObjects.Animals.Mating;
using LifeSimulation.myCs.WorldObjects.Animals.Moving;
using LifeSimulation.myCs.WorldObjects.Eatable;
using LifeSimulation.myCs.WorldObjects.Plants;

namespace LifeSimulation.myCs.WorldObjects.Animals
{
    public class Animal : WorldObject
    {
        public Animal(
            Cell keeper,
            Image image,
            int layer = 0,
            MealType mealType = MealType.AllTypes,
            bool isMale = true,
            int maxHealth = Defaults.AnimalHealth,
            int maxSatiety = Defaults.AnimalSatiety
            
        ) : base(keeper)
        {
            components.Add(new DrawableComponent(this, image, layer));
            components.Add(new MovingComponent(this));
            components.Add(new HealthComponent(this, maxHealth));
            components.Add(new EaterComponent(this, mealType, maxSatiety));
            components.Add(new EatableComponent(this, MealType.FreshMeat, Effect.None));
            
            if (isMale)
                components.Add(new MaleMatingComponent(this));
            else 
                components.Add(new FemaleMatingComponent(this));
            
            if (mealType == MealType.Plant)
                components.Add(new PlanterComponent(this));
            
            Start();

        }
    }
}