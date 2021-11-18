﻿using System.Drawing;
using LifeSimulation.myCs.Drawer;
using LifeSimulation.myCs.World;
using LifeSimulation.myCs.WorldObjects.Eatable;

namespace LifeSimulation.myCs.WorldObjects.Plants.Plants
{
    public class Plant : WorldObject
    {
        private Plant(
            Cell keeper, 
            Image image,
            CreatureType creatureType,
            int layer = 0,
            Effect effect = Effect.None, 
            bool isAnnual = false,
            int[] transAges = null) 
            : base(keeper)
        {
            components.Add(new PlantAgeComponent(this, creatureType, effect, image, layer, transAges));
            components.Add(new PlantOnWeatherComponent(this, isAnnual));
            components.Add(new DependingOnWeatherComponent(this));
            components.Add(new PlantInformationComponent(this));
            Start();
        }
        
        public static Plant SpawnRandomPlant(Cell cell)
        {
            int randomTypeNumber = World.World.Random.Next((int) CreatureType.EatableGreenPlant,
                (int) CreatureType.UneatableBrownPlant + 1);
            return SpawnPlantByType(cell, (CreatureType) randomTypeNumber);
        }
        public static Plant SpawnPlantByType(Cell cell, CreatureType creatureType)
        {
            switch (creatureType)
            {
                case CreatureType.EatableGreenPlant:
                    return new Plant(cell, Pictures.Plant, creatureType, 2, Effect.None, true);
                case CreatureType.PoisonousPurplePlant:
                    return new Plant(cell, Pictures.PoisonousPlant, creatureType, 2, Effect.Heart, true);
                case CreatureType.UneatableBrownPlant:
                    return new Plant(cell, Pictures.UneatablePlant, creatureType, 2, Effect.Uneatable, false);
                default:
                    return null;
            }
        }
    }
}