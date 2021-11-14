﻿using System.Drawing;
using LifeSimulation.myCs.Settings;
using LifeSimulation.myCs.World;
using LifeSimulation.myCs.WorldObjects.Animals.Animals;
using LifeSimulation.myCs.WorldObjects.Animals.Moving;
using LifeSimulation.myCs.WorldObjects.Eatable;

namespace LifeSimulation.myCs.WorldObjects.Animals.Humans
{
    public class Human : WorldObject
    {
        public Human(
            Cell keeper,
            Image image,
            Image afterDiedImage,
            int layer = 0,
            bool isMale = true,
            int maxHealth = Defaults.AnimalHealth,
            int maxSatiety = Defaults.AnimalSatiety,
            int maxReserve = Defaults.AnimalSatiety
            
        ) : base(keeper)
        {
            components.Add(new DrawableComponent(this, image, layer));
            components.Add(new InventoryComponent(this, maxReserve));
            components.Add(new HumanEaterComponent(this, MealType.AllTypes, maxSatiety));
            components.Add(new HealthComponent(this, maxHealth));
            components.Add(new HumanAgeComponent(this, isMale, afterDiedImage, layer, 20));
            components.Add(new EatableComponent(this, CreatureType.Human, MealType.FreshMeat, Effect.None));
            components.Add(new MovingComponent(this));
            components.Add(new HumanInformationComponent(this));
            Start();
        }
    }
}